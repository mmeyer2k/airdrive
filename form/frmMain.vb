'    AirDrive Project: Array of IMAP repositories
'    Copyright (C) 2010 Michael Meyer
'
'    This program is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
'    Filename: frmMain.vb


#Region "Imports"
Imports System.IO
Imports System.Diagnostics
Imports System.Threading
Imports Microsoft.Win32
Imports System.Xml
Imports System.Text
Imports System.Net.Sockets
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Text.RegularExpressions
Imports AirDrive.enums
Imports AirDrive.func_transfer_upload
Imports AirDrive.vars
Imports AirDrive.functions
Imports AirDrive.func_transfer_download
Imports AirDrive.methods
Imports AirDrive.func_dllimport
Imports System.Reflection
Imports System.Net
Imports System.Security.AccessControl

#End Region

Public Class frmMain

    'Heading information
#Region "Globals, Event References, Delegate Declarations"

    '------------------------------------------------------
    'Delegate functions and their dependant variables
    Private Delegate Sub RemoteRefreshDelegate()
    Private RemoteRefreshNewDelegate As New RemoteRefreshDelegate(AddressOf RemoteRefresh)

    'Private Delegate Sub LocalTreenodeAdd()
    'Private LocalTreenodeAddNewDelegate As New LocalTreenodeAdd(AddressOf LocalTreenodeAddToParent)
    'Protected strLocalTargetNodeFullPath As String = String.Empty
    '------------------------------------------------------



    'needed for local drag/drop
    'Private m_DragSource As ListBox = Nothing

    Public tvnLastRemoteNode As TreeNode

    Public Event RefreshRemoteViews As eventhandler
    Public Event TriggerEvent As EventHandler
    Private WithEvents events_Props As New props



    Private WithEvents fsw As FileSystemWatcher
    Private _LastEvent As Date

    Protected bClickedUpdate As Boolean = False    'this keeps track of wether an update was manually selected

    Protected bCancelProcess As Boolean = False
#End Region


    'Operations conducted on background threads
#Region "Background Workers"

#Region "CheckUpdates"

    Private Sub bgwCheckUpdates_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwCheckUpdates.DoWork
        If oSettings.CheckForUpdates OrElse bClickedUpdate Then
            Try
                Dim req As WebRequest = WebRequest.Create("http://airdrive.sourceforge.net/release/ver.txt")
                Dim resp As WebResponse = req.GetResponse()

                Dim s As Stream = resp.GetResponseStream()
                Dim sr As StreamReader = New StreamReader(s, Encoding.ASCII)
                Dim doc As String = sr.ReadToEnd()

                'check if there are any open brackets.
                'this would probably indicate an error opening the file....
                If doc.Contains("<") Then
                    doc = doc.Substring(0, doc.IndexOf("<"))
                    For Each c As Char In doc
                        If Char.IsLetter(c) Then Exit Sub
                        If Not Char.IsDigit(c) AndAlso Not c = chrPeriod Then Exit Sub
                    Next
                End If

                Dim Clst As String() = cv.Split(".")
                Dim Nlst As String() = doc.Split(".")

                If Convert.ToInt16(Nlst(2)) > Convert.ToInt16(Clst(2)) Then
                    Dim r As MsgBoxResult
                    r = MessageBox.Show("A new version of AirDrive is available. Do you want to download it now?" & vbNewLine & vbNewLine & "Latest version: " & doc & vbNewLine & "Your Version: " & cv, "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If r = MsgBoxResult.Yes Then
                        System.Diagnostics.Process.Start("http://sourceforge.net/projects/airdrive/files/setup.exe/download")
                    ElseIf r = MsgBoxResult.No Then
                        'do nothing
                    Else
                        'execution should never be able to get here. close program if it does
                        Application.Exit()
                    End If
                Else
                    If bClickedUpdate Then
                        bClickedUpdate = False
                        MessageBox.Show("You have the latest version of " & strAppName & chrPeriod, _
                                        String.Empty, _
                                        MessageBoxButtons.OK, _
                                        MessageBoxIcon.Information)

                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

#End Region

#Region "Idler"

    Private Sub bgwIdle_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwIdle.DoWork
        'this worker sends IDLE messages to the IMAP server to keep the connection alive.
        'this code is quite buggy and could possibly unneeded

        'set the interval between keep alive commands
        'set interval to 5 minutes
        Dim iWait As Integer = 600000

        Do
            'pause for interval
            System.Threading.Thread.Sleep(iWait)

            'check if the smIMAP instance is logged in to a server
            If smIMAP.b_LoggedIn Then

                'make sure that the queue handler is not uploading/downloading any files
                'also ensure that the loadcloud data BGW is also idle
                'this would cause a massive response parsing problem
                If Not bgwLoadCloudData.IsBusy AndAlso Not bgwQueueHandler.IsBusy Then

                    'create register for the IDLE response
                    Dim er As New enumIMAPResponse

                    'send idle command to server
                    er = smIMAP.IMAPInterface("idle", New ArrayList, True, True, True)

                    If er = enumIMAPResponse.imapBeginAppend Then

                        Dim es As enumIMAPResponse = smIMAP.IMAPInterface("DONE", New ArrayList, True, True, True)
                        If es = enumIMAPResponse.imapSuccess Then
                            'update log if IDLE was successful
                            LogOutput(enumLogType.INFO, String.Format("IDLE command to {0} was successful", smIMAP.str_Host))
                        Else
                            'do something
                        End If
                    Else    'handle unexpected response from IMAP server
                        LogOutput(enumLogType.ERROR, String.Format("IDLE command to {0} was NOT successful [{1}] [last_error={2}]", smIMAP.str_Host, smIMAP.str_Email, smIMAP.str_LastError))
                    End If
                Else
                    LogOutput(enumLogType.INFO, String.Format("Aborted IDLE command to {0} due to pending actions", smIMAP.str_Email))
                End If
            End If

            'check if there is a cancellation pending for this worker thread
            If bgwIdle.CancellationPending Then
                'tell the log
                LogOutput(enumLogType.INFO, "Exiting the 'IDLER' thread. Normal termination.")

                'cancel thread loop
                e.Cancel = True
            End If
        Loop

    End Sub

#End Region

#Region "Load Cloud Data"
    Private Sub bgwLoadCloudData_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadCloudData.DoWork
        'skip this process if this is a new profile
        'prevent threading errors
        CheckForIllegalCrossThreadCalls = False

        lvRemoteFiles.Items.Clear()
        tvRemoteDir.Nodes.Clear()

        'lock remote side of frmMain
        sub_FormLock(frmMainStates.Locked)

        'clear Remote Data collection
        colRemoteFileData.Clear()

        'For Each hash In colAccountList
        For Each objAcnt As objAccount In colAccountList
            'create variables for the gathered data
            Dim hostname, username, password, email As String
            Dim ssl As Boolean = True
            Dim port As Integer = 993

            'pull down values into variables
            hostname = objAcnt.Hostname
            username = objAcnt.Username
            password = objAcnt.Password
            ssl = objAcnt.SSL
            port = objAcnt.Port
            email = objAcnt.EmailAddress

            'refresh GUI
            Application.DoEvents()

            'Connect to server, load data and collect output, add to current hashtable
            Dim boolSuccess As Boolean

            props.StatusMessage = "Connecting to: " & hostname

            boolSuccess = smIMAP.Login(hostname, Convert.ToInt16(port), Convert.ToBoolean(ssl), username, password)
            If boolSuccess Then
                objAcnt.IsFunctioning = True
                props.StatusMessage = "Gathering data for: " & email
                Call OpenIMAPFolder(smIMAP)
                Call GetIMAPQuota(smIMAP, objAcnt)
                Call func_FetchRemoteFileData(smIMAP)
            Else
                MessageBox.Show("Could not connect to account: " & email)
                objAcnt.IsFunctioning = False
            End If

            objAcnt.Items = smIMAP.int_TotalMessages
        Next

        props.StatusMessage = String.Empty
    End Sub

    Private Sub bgwLoadCloudData_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadCloudData.RunWorkerCompleted
        bRefreshRemoteData = False
        Call TriggerLoad()
    End Sub

#End Region

#Region "QueueHandler"

    Private Sub bgwQueueHandler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwQueueHandler.DoWork
        'disable thread safe checking
        'this is the only thread operating on these items, so it should be secure
        CheckForIllegalCrossThreadCalls = False

        'create an escape switch
        Dim bEscape As Boolean = False

        If Not bgwRate.IsBusy Then bgwRate.RunWorkerAsync() 'start the speed counting thread

        'start iterating through items in Queue list
        Do Until bEscape = True

            'check if the queue handler has a cancellation pending
            'if there is a cancellation pending, then exit the worker.
            If bgwQueueHandler.CancellationPending Then
                lvQueue.Items.Clear() 'clear the queue list
                e.Cancel = True 'exit the worker thread
            End If

            'check if there are any pending deletions
            If b_PendingDelete = True Then
                If Not scMainH.Panel2Collapsed AndAlso lvQueue.Items.Count > 1 Then
                    lvQueue.Enabled = False
                End If
                'if there are deletions pending, call deletion subroutine
                sub_IMAPUIDCollectionDelete()

                b_PendingDelete = False
                props.StatusMessage = String.Empty
                props.ProgressBarValue = 0
            End If

            'find first pending item
            Dim sItem As ListViewItem
            Dim i As Integer = 0

            'locate first PENDING file in queue list
            Do
                'if this is the last item in the queue then
                If i = lvQueue.Items.Count Then Exit Sub

                'expand queue panel and refresh GUI
                scMainH.Panel2Collapsed = False
                lvQueue.Enabled = True
                Application.DoEvents()

                sItem = lvQueue.Items(i)

                If sItem.SubItems(4).Text = "Pending" Then
                    Exit Do
                End If
                i += 1
            Loop

            If sItem.SubItems(1).Text = "<--" Then 'download
                If IMAPDownload(sItem) Then
                    sItem.Remove()
                    UpdateQueueCount()
                Else
                    sItem.SubItems(4).Text = "Failed"
                End If

            ElseIf sItem.SubItems(1).Text = "-->" Then 'upload
                If IMAPUpload(sItem) Then

                    Dim strP As String = String.Empty

                    If strRemoteNodeSelected = chrFrontslash Then
                        strP = chrFrontslash
                    Else
                        strP = strRemoteNodeSelected & chrFrontslash
                    End If

                    If sItem.SubItems(2).Text.StartsWith(strP) Then
                        Dim str As String = sItem.SubItems(2).Text.Substring(strP.Length) 'remove root from name
                        If Not str.Contains(chrFrontslash) Then 'make sure user is not in a lower level folder
                            sub_AddRemoteFiles(strP)
                        End If
                    End If

                    sItem.Remove()
                    UpdateQueueCount()
                Else
                    sItem.SubItems(4).Text = "Failed"
                    smIMAP.b_LoggedIn = False
                End If
            End If
            nByteRegister = 0

            'escape when there is nothing left
            If lvQueue.Items.Count = 0 Then bEscape = True
        Loop


        If bDupResQueue Then
            bDupResQueue = False
            bDupResAlways = False
            bDupResOnlyDiff = False
            ePreviousAction = Nothing
        End If

        scMainH.Panel2Collapsed = True

        'this takes up a lot of processor time, so update in BG thread
        Try
            Me.Invoke(RemoteRefreshNewDelegate)
        Catch ex As Exception
            'do nothing
        End Try

        Application.DoEvents()
        StoreAcntDataToXML()
        StoreFileDataToXML()

        If bgwRate.IsBusy Then bgwRate.CancelAsync()

        LogOutput(enumLogType.INFO, "Exiting queue handling thread normally.")

    End Sub

    Private Sub bgwQueueHandler_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwQueueHandler.RunWorkerCompleted
        If bDupResQueue Then
            bDupResQueue = False
            bDupResAlways = False
        End If

        If e.Cancelled = True Then
            If bgwRate.IsBusy Then bgwRate.CancelAsync()

            LogOutput(enumLogType.INFO, "Exited queue handling thread due to [pendingcancellation] flag.")

            StoreAcntDataToXML()
            StoreFileDataToXML()

            scMainH.Panel2Collapsed = True
            Application.DoEvents()
        End If

    End Sub

#End Region

#Region "Rate"

    Private Sub bgwRate_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles bgwRate.Disposed
        lblRate.Text = String.Empty
        nByteRegister = 0
    End Sub

    Private Sub bgwRate_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwRate.DoWork
        Do
            System.Threading.Thread.Sleep(250)
            If nByteRegister = 0 Then
                lblRate.Text = String.Empty
                Continue Do
            End If

            If bgwRate.CancellationPending Then
                e.Cancel = True
                Return
            End If

            Dim sRate As String = ReturnNiceSize(0, , nByteRegister * 4)
            sRate += "/s"
            lblRate.Text = sRate
            nByteRegister = 0
            Application.DoEvents()
        Loop
    End Sub

    Private Sub bgwRate_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwRate.RunWorkerCompleted
        lblRate.Text = String.Empty
        nByteRegister = 0
    End Sub

#End Region


#End Region


    'Tool strip buttons above each frame
#Region "Buttons"

#Region "Local Directiories"

    Private Sub btnLocalRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalRefresh.Click
        lvLocalFiles.Items.Clear()
        tvLocalDir.Nodes.Clear()
        sub_LoadFolderTree()
    End Sub

    Private Sub btnLocalDirDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalDirDelete.Click
        Dim result As MsgBoxResult = MessageBox.Show("Are you sure you want to delete " & tvLocalDir.SelectedNode.Text & _
                                                     " and all of its child files and folders?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = MsgBoxResult.Yes Then
            Try
                System.IO.Directory.Delete(tvLocalDir.SelectedNode.Tag, True)
                If Not System.IO.Directory.Exists(tvLocalDir.SelectedNode.Tag) Then
                    tvLocalDir.SelectedNode.Remove()
                End If
            Catch ex As Exception
                LogOutput(enumLogType.ERROR, String.Format("Error occurred in the [btn_LocalDirDelete_Click] event. System message: {0}", ex.Message))

                'show dialog box to user
                MessageBox.Show(String.Format("Could not delete folder due to system error: {0}{1}{2}", vbNewLine, vbNewLine, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub btnLocalDirNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalDirNew.Click
        Dim dirname As String

        'gather desired directory name
        dirname = InputBox("Folder name:", "Create Folder", String.Empty)

        'escape if directory name is nothing
        If dirname = String.Empty Then
            Exit Sub
        End If

        'create directory path string
        Dim newfolder As String = txtLocalDir.Text & "\" & dirname

        'escape if directory alraedy exists
        If System.IO.Directory.Exists(newfolder) = True Then
            MessageBox.Show("Directory already exists", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'create directory
        System.IO.Directory.CreateDirectory(newfolder)

        'create treenode
        Dim tvn As New TreeNode
        With tvn
            .Tag = newfolder
            .Text = dirname
            .SelectedImageIndex = 3
            .ImageIndex = 3
        End With

        'add treenode
        tvLocalDir.SelectedNode.Nodes.Add(tvn)
        tvLocalDir.SelectedNode = tvn
    End Sub

    Private Sub btnLocalDirRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalDirRename.Click

        Dim root As String = String.Empty
        Dim tv As TreeNode = tvLocalDir.SelectedNode    'mirror treenode object
        Dim strOldPath As String = tv.Tag    'obtain old tag information
        Dim strOldName As String = tv.Text    'obtain old filename info
        Dim strNewPath As String = String.Empty
        Dim strNewName As String = String.Empty

        If strOldPath = "My Computer" OrElse _
            strOldPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments Then    'check if target is a special folder
            MessageBox.Show("Can not rename this item.", _
                            "Information", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Information)    'inform user of error
            Exit Sub
        End If

        'begin logical block to determine if the directory is a drive
        Try
            If strOldPath.LastIndexOf(chrBackslash) = strOldPath.Length - 1 Then
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try


        strNewName = InputBox("Rename " & strOldName & " to:", _
                              "Rename directory", _
                              strOldName)    'promp user for new name

        If strNewName = String.Empty Then Exit Sub 'exit if new name is the same as the old name
        root = strOldPath.Substring(0, strOldPath.LastIndexOf(chrBackslash))    'gather parent directory of target directory
        strNewPath = SlashScrubber(chrBackslash, root & chrBackslash & strNewName)

        If System.IO.Directory.Exists(strNewPath) Then
            MessageBox.Show("Directory already exists", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        System.IO.Directory.Move(strOldPath, strNewPath)    'use API to move

        With tv
            .Text = strNewName
            .Tag = strNewPath
        End With

    End Sub

    Private Sub btnLocalDirUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalDirUpload.Click
        Dim path As String = SlashScrubber("\", tvLocalDir.SelectedNode.Tag)
        sub_QueueUpload(path, True, False, False)
    End Sub

    Private Sub btnLocalDirUploadEncrypted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalDirUploadEncrypted.Click
        Dim path As String = SlashScrubber("\", tvLocalDir.SelectedNode.Tag)
        sub_QueueUpload(path, True, False, True)
    End Sub

#End Region

#Region "Local Files"

    Private Sub btnLocalFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalFileDelete.Click
        'add recycle capability with my.computer.filesystem.deletefile

        If lvLocalFiles.SelectedItems.Count = 0 Then
            MsgBox("No files selected.")
            Exit Sub
        End If

        Dim result As MsgBoxResult = MessageBox.Show("Are you sure you want to permanently delete " & _
                                                     lvLocalFiles.SelectedItems.Count.ToString & _
                                                     " selected file(s)?", "Testing", MessageBoxButtons.YesNo, _
                                                     MessageBoxIcon.Question)
        If result = MsgBoxResult.Yes Then
            For Each lvi As ListViewItem In lvLocalFiles.SelectedItems

                Try
                    System.IO.File.Delete(Trim(lvi.Tag))
                Catch ex As Exception
                    MessageBox.Show(ex.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                lvi.Remove()
            Next
        End If
    End Sub

    Private Sub btnLocalFileUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalFileUpload.Click
        'enumerate selected items in listview.
        For Each selected As ListViewItem In lvLocalFiles.SelectedItems
            sub_QueueUpload(selected.Tag, False, False, False)
        Next
    End Sub

    Private Sub btnLocalFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalFileRename.Click
        If lvLocalFiles.SelectedItems.Count > 1 Then 'exit the routine if anything other than 1 file is selected
            MsgBox("Please select only one file to rename")
            Exit Sub
        ElseIf lvLocalFiles.Items.Count = 0 Then
            MsgBox("Select a file to rename")
            Exit Sub
        End If

        Dim lvi As ListViewItem = lvLocalFiles.SelectedItems(0)   'create an object for the selected listview item
        Dim strOriginalName As String = lvi.Text    'gather original file name for comparison later
        Dim strOriginalExt As String = ReturnExtention(strOriginalName)    'gather original extention
        Dim strNewName As String = InputBox("Enter new file name:", String.Empty, lvi.Text)    'prompt user for new file name

        If strNewName = String.Empty Then Exit Sub 'exit if user supplied blank name
        If strOriginalName = strNewName Then Exit Sub 'exit if new name is the same as the old name

        Try
            Dim fi As New FileInfo(lvi.Tag)    'create FILEINFO object for the selected item
            Dim t As String = fi.DirectoryName
            t = SlashScrubber(chrBackslash, t & chrBackslash & strNewName)

            fi.MoveTo(t)    'perform the file move

            lvi.Text = strNewName    'change listview item to reflect new name (added 0.1.19)
            lvi.Tag = t    'update tag so that subsequent calls to rename file do not fail
            Dim strNewExt As String = ReturnExtention(strNewName)    'exit the routine if anything other than 1 file is selected

            If Not strOriginalExt = strNewExt Then    'detect if extention has changed, therefore icon needs to change
                If ilLocalFiles.Images.ContainsKey(strNewExt) Then
                    lvi.ImageKey = strNewExt    'change to new icon 
                Else    'handle adding new icon to the image list
                    Dim cIcon As New objIcon    'create new icon object
                    ilRemoteFiles.Images.Add(strNewExt, cIcon.GetDefaultIcon(chrPeriod & strNewExt, objIcon.IconSize.SmallIcon))
                    lvi.ImageKey = strNewExt    'apply new image index
                End If
            End If
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, "Exception during file rename." & ex.Message)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLocalFileUploadEncrypted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalFileUploadEncrypted.Click
        For Each selected As ListViewItem In lvLocalFiles.SelectedItems
            sub_QueueUpload(selected.Tag, False, False, True)
        Next
    End Sub

#End Region

#Region "Remote Directories"

    Private Sub btnRemoteDirDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteDirDelete.Click
        'check if not has a value, if not exit sub
        If tvRemoteDir.SelectedNode Is Nothing Then
            Exit Sub
        End If

        Dim tNode As TreeNode = tvRemoteDir.SelectedNode    'grab node value and add to variables
        Dim Target As String = SlashScrubber(chrFrontslash, tNode.FullPath)

        'confirm delete
        Dim d As DialogResult
        If txtRemoteDir.Text = chrFrontslash Then
            d = MessageBox.Show("You have choosen to delete the root directory. All files on every linked email account will be deleted." & vbNewLine & _
                                "Do you want to continue?", "Delete root?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)
        Else
            d = MessageBox.Show("Are you sure you want to delete this item?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, False)

        End If

        If d = Windows.Forms.DialogResult.No Then Exit Sub


        'move through filedata collection
        For Each objSeg As objSegment In colRemoteFileData
            Dim rf As String = objSeg.FilePath
            Try
                If rf.StartsWith(Target) Then
                    colPendingDelete.Add(objSeg)
                End If
            Catch ex As Exception
                LogOutput(enumLogType.ERROR, String.Format("An error occurred in the [btnRemoteDirDelete_Click] event. System message: {0}", ex.Message))
            End Try
        Next

        b_PendingDelete = True

        If Not bgwQueueHandler.IsBusy Then bgwQueueHandler.RunWorkerAsync()

        props.StatusMessage = String.Empty

        If txtRemoteDir.Text = "/" Then
            lvRemoteFiles.Items.Clear()
            sub_FormLock(frmMainStates.RemoteFilesEmpty)
        Else
            tvRemoteDir.SelectedNode = tNode.Parent
        End If

        'remove treenode
        tNode.Remove()
    End Sub

    Private Sub btnRemoteDirDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteDirDownload.Click
        sub_QueueDownload(txtRemoteDir.Text, True)
    End Sub

    Private Sub btnRemoteDirNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteDirNew.Click
        Dim newdir As String = InputBox("New folder name:", "Create folder")
        If newdir = String.Empty Then Exit Sub

        Dim tvn As New TreeNode
        tvn.Text = newdir

        If txtRemoteDir.Text = "/" Then
            tvRemoteDir.SelectedNode = tvRemoteDir.Nodes(0)
        End If

        tvRemoteDir.SelectedNode.Nodes.Add(tvn)

        Dim tvnPath As String = Replace(tvn.FullPath, "//", "/")
        tvn.Tag = tvnPath
        tvn.SelectedImageIndex = 3
        tvn.ImageIndex = 3

        tvRemoteDir.SelectedNode = tvn

    End Sub

    Private Sub btnRemoteRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteRefresh.Click
        If colAccountList.Count = 0 Then
            Dim msgres As MsgBoxResult
            msgres = MessageBox.Show("You have not added any IMAP accounts to your profile. Would you like to do so now?", "Add account", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If msgres = MsgBoxResult.Yes Then
                frmActs.ShowDialog(Me)
            End If
            Exit Sub
        End If
        Dim r As MsgBoxResult = MessageBox.Show("Refreshing your file data from the server can take a long time. " & vbNewLine & "Are you sure you want to proceed?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If r = MsgBoxResult.Yes Then
            bgwLoadCloudData.RunWorkerAsync()
        End If
    End Sub

    Private Sub btnRemoteDirRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteDirRename.Click
        MsgBox("This function is not currently supported")
    End Sub

#End Region

#Region "Remote Files"

    Private Sub btnRemoteFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteFileDelete.Click

        Dim d As DialogResult
        If lvRemoteFiles.SelectedItems.Count > 1 Then
            Dim nCount As Integer = lvRemoteFiles.SelectedItems.Count
            d = MessageBox.Show(String.Format("Are you sure you want to delete these {0} items?", nCount), "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, False)
        ElseIf lvRemoteFiles.SelectedItems.Count = 1 Then
            Dim sDisplay As String = lvRemoteFiles.SelectedItems(0).Text
            d = MessageBox.Show(String.Format("Are you sure you want to delete this item:  {0}?", sDisplay), "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, False)
        Else
            Exit Sub
        End If

        If d = Windows.Forms.DialogResult.No Then Exit Sub

        'lvRemoteFiles.BeginUpdate()

        For Each lvi As ListViewItem In lvRemoteFiles.SelectedItems
            Dim Target As String = SlashScrubber(chrFrontslash, txtRemoteDir.Text & chrFrontslash & lvi.Text)
            For Each objSeg As objSegment In colRemoteFileData
                Dim rf As String = objSeg.FilePath
                Try
                    If rf = Target Then
                        colPendingDelete.Add(objSeg)
                        lvi.Remove()
                        Continue For
                    End If
                Catch ex As Exception
                    LogOutput(enumLogType.ERROR, String.Format("Unhandled exception occurred in [btn_RemoteFileDelete_Click] event. System message: {0}", ex.Message))
                End Try
                'Loop
            Next
        Next

        'lvRemoteFiles.EndUpdate()

        b_PendingDelete = True
        If Not bgwQueueHandler.IsBusy Then bgwQueueHandler.RunWorkerAsync()
        props.StatusMessage = String.Empty
    End Sub

    Private Sub btnRemoteFileDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteFileDownload.Click
        For Each lvi As ListViewItem In lvRemoteFiles.SelectedItems
            sub_QueueDownload(txtRemoteDir.Text & "/" & lvi.Text, False)
        Next
    End Sub

    Private Sub btnRemoteFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoteFileRename.Click
        MsgBox("This function is not currently supported.")
    End Sub

#End Region


    Private Sub DownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadToolStripMenuItem.Click
        Call btnRemoteFileDownload_Click(Me, New EventArgs)
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem1.Click
        Call btnRemoteFileDelete_Click(Me, New EventArgs)
    End Sub

    Private Sub DownloadToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadToolStripMenuItem1.Click
        Call btnRemoteDirDownload_Click(Me, New EventArgs)
    End Sub

    Private Sub NewFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFolderToolStripMenuItem.Click
        Call btnRemoteDirNew_Click(Me, New EventArgs)
    End Sub

    Private Sub DeleteToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem2.Click
        Call btnRemoteDirDelete_Click(Me, New EventArgs)
    End Sub

    Private Sub RefreshDisplayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDisplayToolStripMenuItem.Click
        Call btnRemoteRefresh_Click(Me, New EventArgs)
    End Sub

    Private Sub SendFeedbackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendFeedbackToolStripMenuItem.Click
        System.Diagnostics.Process.Start("http://airdrive.sourceforge.net/contact.php")
    End Sub

    Private Sub ClearConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearConsoleToolStripMenuItem.Click
        txtConsole.Clear()
    End Sub

    Private Sub UploadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadToolStripMenuItem.Click
        For Each selected As ListViewItem In lvLocalFiles.SelectedItems
            sub_QueueUpload(selected.Tag, False, False, False)
        Next
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        btnLocalFileDelete_Click(Me, New EventArgs)
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        frmOptions.ShowDialog()
    End Sub

    Private Sub UploadEncryptedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadEncryptedToolStripMenuItem.Click
        For Each selected As ListViewItem In lvLocalFiles.SelectedItems
            sub_QueueUpload(selected.Tag, False, False, True)
        Next
    End Sub

    Private Sub UploadEncryptedToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadEncryptedToolStripMenuItem1.Click
        Dim path As String = SlashScrubber(chrBackslash, tvLocalDir.SelectedNode.Tag)
        sub_QueueUpload(path, True, False, True)
    End Sub

    Private Sub RemoveSelectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveSelectedToolStripMenuItem.Click
        For Each lvi In lvQueue.SelectedItems
            lvQueue.Items.Remove(lvi)
        Next
    End Sub

    Private Sub RenameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        Call btnLocalFileRename_Click(Me, New EventArgs)
    End Sub

    Private Sub RenameToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem2.Click
        MsgBox("This function is not currently supported")
    End Sub

    Private Sub RenameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem1.Click
        MsgBox("This function is not currently supported")
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem1.Click
        OpenWiki()
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        Dim s As New frmFileProperties(lvRemoteFiles.SelectedItems(0).Tag)
        s.ShowDialog()
    End Sub

    Private Sub DonateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DonateToolStripMenuItem.Click

    End Sub

    Private Sub AirDriveHeadquartersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AirDriveHeadquartersToolStripMenuItem.Click
        System.Diagnostics.Process.Start("http://airdrive.sourceforge.net")
    End Sub


#End Region


    'Right-click context button menu events
#Region "Context menu buttons and events"

    Private Sub cmenu_LocalDirNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenu_LocalDirNew.Click
        Call btnLocalDirNew_Click(sender, e)
    End Sub

    Private Sub cmenu_LocalDirRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenu_LocalDirRename.Click
        Call btnLocalDirRename_Click(sender, e)
    End Sub

    Private Sub cmenu_LocalFiles_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmenu_LocalFiles.Opening
        If lvLocalFiles.SelectedItems.Count = 0 Then e.Cancel = True
    End Sub

    Private Sub cmenu_QueueClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenu_QueueClear.Click
        'block painting of queue box
        lvQueue.BeginUpdate()
        Try
            'try to shutdown imap commection
            If smIMAP IsNot Nothing Then
                LogOutput(enumLogType.INFO, String.Format("Aborting IMAP connection to account: {0}", smIMAP.str_Email))
                smIMAP.LogOut()
            End If
            If bgwQueueHandler.IsBusy Then
                bgwQueueHandler.CancelAsync()
            End If
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, ex.Message)
        End Try

        bgwQueueHandler.CancelAsync()
        scMainH.Panel2Collapsed = True

        lvQueue.Items.Clear()
        lvQueue.View = View.Details
        lvQueue.EndUpdate()
    End Sub

    Private Sub cmenu_QueueSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenu_QueueSave.Click
        Dim sf As New SaveFileDialog
        sf.FileName = strProfileName & ".txt"
        sf.Title = "Save log file..."
        sf.AutoUpgradeEnabled = True
        sf.SupportMultiDottedExtensions = True
        sf.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        sf.OverwritePrompt = True
        sf.RestoreDirectory = True
        sf.ShowDialog(Me)
        Dim fstream As Stream = sf.OpenFile
        Dim buffer() As Byte = System.Text.Encoding.ASCII.GetBytes(txtConsole.Text)
        fstream.Write(buffer, 0, buffer.Length)
        fstream.Flush()
        fstream.Close()
    End Sub

    Private Sub cmsRemoteFiles_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsRemoteFiles.Opening
        If lvRemoteFiles.SelectedItems.Count = 0 Then e.Cancel = True
        If lvRemoteFiles.SelectedItems.Count = 1 Then
            PropertiesToolStripMenuItem.Visible = True
        Else
            PropertiesToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub cmsQueueBox_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsQueueBox.Opening
        If lvQueue.SelectedItems.Count > 0 Then
            RemoveSelectedToolStripMenuItem.Visible = True
        Else
            RemoveSelectedToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub cmsLocalDirUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsLocalDirUpload.Click
        Dim path As String = SlashScrubber(chrBackslash, tvLocalDir.SelectedNode.Tag)
        sub_QueueUpload(path, True, False, False)
    End Sub

    Private Sub cmsLocalDirDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsLocalDirDelete.Click
        Call btnLocalDirDelete_Click(Me, New EventArgs)
    End Sub

#End Region


    'Monitor file system changes to update UI

#Region "Filesystem Watchers"

    Private Sub fsw_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsw.Created
        'CheckForIllegalCrossThreadCalls = False

        'If DateAndTime.Now.Subtract(_LastEvent).TotalMilliseconds < 500 Then
        '    Exit Sub
        'End If
        '_LastEvent = DateAndTime.Now

        Dim root, fname As String
        ChopPath(e.FullPath, root, fname)

        If File.Exists(e.FullPath) Then 'handle as a file

            If Not strLocalDir.EndsWith(chrBackslash) Then
                strLocalDir += chrBackslash
            End If
            If root.ToLower = strLocalDir.ToLower Then
                AddLocalFiles(e.FullPath)
            End If
        Else 'handle as a directory
            'strLocalTargetNodeFullPath = e.FullPath
            'Me.Invoke(LocalTreenodeAddNewDelegate)
        End If

    End Sub

    Private Sub fsw_Deleted(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsw.Deleted
        Dim root, fname As String
        ChopPath(e.FullPath, root, fname)
        If Not strLocalDir.EndsWith(chrBackslash) Then
            strLocalDir += chrBackslash
        End If
        If root.ToLower = strLocalDir.ToLower Then
            Try
                For Each item As ListViewItem In lvLocalFiles.Items
                    If item.Text = fname Then
                        item.Remove()
                        Exit Sub
                    End If
                Next
            Catch ex As Exception
                LogOutput(enumLogType.WARN, ex.Message)
            End Try

        End If

    End Sub

    Private Sub fsw_Renamed(ByVal sender As Object, ByVal e As System.IO.RenamedEventArgs) Handles fsw.Renamed
        If File.Exists(e.FullPath) Then 'handle as a file
            Dim root, fname, oldext As String
            ChopPath(e.OldFullPath, root, fname)

            oldext = ReturnExtention(e.OldFullPath)

            If Not strLocalDir.EndsWith(chrBackslash) Then
                strLocalDir += chrBackslash
            End If

            If root.ToLower = strLocalDir.ToLower Then
                For Each item As ListViewItem In lvLocalFiles.Items
                    If item.Text = fname Then
                        Dim newroot, newname, newext As String
                        newext = ReturnExtention(e.FullPath)
                        ChopPath(e.FullPath, newroot, newname)

                        If newext <> oldext Then 'handle changing icon if necessary
                            AddIconsToLocalFileIL(newext)
                            item.ImageKey = newext
                        End If

                        item.Text = newname
                        Exit Sub
                    End If
                Next
            End If
        Else 'handle as a directory
        End If
    End Sub

#End Region


    'Events associated with the main form
#Region "Form Events"

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            props.StatusMessage = "Cleaning up..."
            Application.DoEvents()

            smIMAP.DisconnectStream()

            'save clean shutdown indicator
            My.Settings.HardShutdown = False
            'SaveTextToFile("1", SlashScrubber(chrBackslash, Application.StartupPath & chrBackslash & "shutdown.bin"))

            Application.Exit()
        Catch ex As Exception
            'LogOutput(LogTypeEnum.INFO, String.Format("Shutdown attempt resulted in error: {0}", ex.Message))
            Me.Close()
            Application.Exit()
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call GrabIcons()
        scMainH.Panel2Collapsed = True
        props.frmMainConsoleView = ConsoleViewSetting.None
        Me.Text += String.Format(" {0}.{1}.{2}", nMinor, nBuild, nRevis)
    End Sub

    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If bFirstPaint = False Then Exit Sub 'run the following stuff after first paint
        bFirstPaint = False

        'check if system has .NET 3.5
        'technically, a machine without it would not even be able to get to this point
        If Not func_HasNet35() Then
            Dim result As MsgBoxResult
            result = MessageBox.Show("AirDrive requires .NET Framework 3.5 to run. Would you like to download this from Microsoft's website?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If result = MsgBoxResult.No Then
                Application.Exit()
            ElseIf result = MsgBoxResult.Yes Then
                Me.Hide()
                frmLogin.Hide()

                'launch browser to .NET download page
                System.Diagnostics.Process.Start(sNetFrameWorkURL)
                Application.Exit()
            End If
        End If

        'launch the check first run routine
        'this is important for the bgwCheckUpdates thread
        sub_CheckFirstRun()

        'populate the global profile arraylist
        arrProfiles = func_LoadProfiles()

        'count the number of loaded profiles
        'force user to add profiles if none exist in the array
        If arrProfiles.Count = 0 Then
            'show profile creation window if no profiles exist
            Dim t As New frmProfile
            t.ShowDialog(Me)
            Dim a As New frmActs
            a.ShowDialog(Me)
        Else
            'if profiles do exist then show login window
            Dim x As New frmLogin
            x.ShowDialog(Me)
            'If bCloseApp Then Exit Sub
        End If

        'configure the file system watchers
        fsw = New FileSystemWatcher("c:\")
        fsw.EnableRaisingEvents = True
        fsw.IncludeSubdirectories = True

        'load dirs/files in to the local file boxes
        sub_LoadFolderTree()

        'double buffer boxes to prevent some flickering
        DoubleBufferListview(lvQueue)
        sub_DoubleBufferTreeview(tvRemoteDir)

        'start the idler worker
        If Not bgwIdle.IsBusy Then bgwIdle.RunWorkerAsync()

        'start updatechecker worker
        If Not bgwCheckUpdates.IsBusy Then bgwCheckUpdates.RunWorkerAsync()

    End Sub

#End Region


    'Functions that must be run from UI thread
#Region "Functions"

    Public Function func_FetchRemoteFileData(ByRef instIMAP As objIMAP) As Boolean
        If Not instIMAP.str_MailBoxName = strAppName Then
            Return False
        End If

        If instIMAP.int_TotalMessages = 0 Then
            colRemoteFileData.Clear()
            StoreFileDataToXML()
            Return True
        End If

        Me.tspbStatus.Maximum = instIMAP.int_TotalMessages

        'create command to write to server
        smIMAP.num_CommandValue += 1
        Dim sCommand As String = smIMAP.imap_cmd_Identifier & String.Format("fetch 1:{0} (envelope UID){1}", smIMAP.int_TotalMessages, strEOL)
        Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(sCommand.ToCharArray())

        'write command to stream
        If smIMAP.b_SSL Then
            smIMAP.stream_SSL.Write(data, 0, data.Length)
        Else
            smIMAP.stream_NetStrm.Write(data, 0, data.Length)
        End If

        Dim eResponse As enumIMAPResponse = enumIMAPResponse.imapSuccess

        Dim i As Integer = 0
        Do
            Dim sLine As String = String.Empty

            Dim iRetries As Integer = 4
            Dim bSkip As Boolean = False
            Do Until bSkip = True
                Try
                    If iRetries <= 0 Then
                        bSkip = True
                    End If
                    sLine = If(smIMAP.b_SSL, smIMAP.obj_Reader.ReadLine(), smIMAP.stream_NetworkReader.ReadLine())
                    bSkip = True
                Catch ex As Exception
                    sLine = String.Empty
                    iRetries -= 1
                End Try
            Loop

            Dim sNewSubject As String = String.Empty

            Me.tspbStatus.Value = i
            props.StatusMessage = String.Format("Fetching information for segment {0} of {1} on {2}.", i, smIMAP.int_TotalMessages, smIMAP.str_Email)

            If IsResponeLine(sLine, eResponse) Then
                If eResponse = enumIMAPResponse.imapFailure OrElse eResponse = enumIMAPResponse.IMAP_IGNORE_RESPONSE Then
                    LogOutput(enumLogType.ERROR, String.Format("Failure response from server during enumeration procedure on {0}: {1};", instIMAP.str_Email, sLine))
                    Return False
                End If
                Exit Do
            Else
                Dim xObj As New objSegment
                ParseEnvelope(smIMAP, sLine, xObj)
                AddToCollection(colRemoteFileData, xObj)
            End If
            i = i + 1
        Loop

        Call StoreFileDataToXML()

        Me.tspbStatus.Value = 0
        Return True
    End Function

#End Region


    'This handles all listview events. 
#Region "Listview Events"

    Private Sub lvRemoteFiles_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvRemoteFiles.ColumnClick
        If e.Column = 0 Then

        End If
    End Sub

    Private Sub lvRemoteFiles_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvRemoteFiles.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filepaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each fileloc As String In filepaths
                sub_QueueUpload(fileloc, False, False, False)
            Next
        End If
    End Sub

    Private Sub lvRemoteFiles_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvRemoteFiles.DragEnter
        'If e.Data.GetDataPresent(DataFormats.FileDrop) Then
        '    e.Effect = DragDropEffects.Copy
        'Else
        '    e.Effect = DragDropEffects.None
        'End If

        e.Effect = DragDropEffects.Copy
    End Sub

#End Region


    'Handles events for the main menu strip buttons
#Region "Menu Button Events"

    Private Sub menu_About_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_About.Click
        frmAbout.ShowDialog(Me)
    End Sub

    Private Sub menu_AddAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_AddAccount.Click
        If colAccountList.Count >= nActLim Then
            MsgBox("For testing purposes, you are limited to " & nActLim.ToString & " accounts per profile.")
            Exit Sub
        End If
        frmActs.ShowDialog(Me)
    End Sub

    Private Sub menu_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_Exit.Click
        Me.Close()
    End Sub

    Private Sub menu_ManageAccounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_ManageAccounts.Click
        frmManage.ShowDialog(Me)
    End Sub

    Private Sub menu_None_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_None.Click
        props.frmMainConsoleView = ConsoleViewSetting.None
    End Sub

    Private Sub menu_Normal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_Normal.Click
        props.frmMainConsoleView = ConsoleViewSetting.Normal
    End Sub

    Private Sub menu_Profiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_Profiles.Click
        My.Settings.AutoLoginProfile = String.Empty
        My.Settings.AutoLoginString = String.Empty
        frmOptions.chkAutoLogin.Checked = False
        Call sub_LogOutProfile()
        Dim x As New frmLogin
        x.ShowDialog(Me)
    End Sub

    Private Sub menu_Verbose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_Verbose.Click
        props.frmMainConsoleView = ConsoleViewSetting.Verbose
    End Sub

    Private Sub menu_CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        bClickedUpdate = True
        If Not bgwCheckUpdates.IsBusy Then bgwCheckUpdates.RunWorkerAsync()
    End Sub

#End Region


    'Subroutines that must be associated with the frmMain class
#Region "Subroutines"

    Private Sub AddFolder(ByRef SmallImageList As ImageList, Optional ByVal LargeImageList As ImageList = Nothing, Optional ByVal indx As Integer = 3)
        'This routine just adds the (closed) folder icon. I use it before I add any other icons
        'so I can be sure that the icon's index in the imagelist is 0

        If Not bEnableDragDrop Then Exit Sub

        Dim hIcon As IntPtr
        Try
            hIcon = ExtractIcon(IntPtr.Zero, "Shell32.dll", indx)
            SmallImageList.Images.Add(Icon.FromHandle(hIcon))
        Catch ex As Exception
            'if icon is not valid then add a generic one
            Dim hIcon2 As IntPtr = ExtractIcon(IntPtr.Zero, "Shell32.dll", 1)
            SmallImageList.Images.Add(Icon.FromHandle(hIcon2))
        End Try

        If Not LargeImageList Is Nothing Then LargeImageList.Images.Add(Icon.FromHandle(hIcon))
        DestroyIcon(hIcon)
    End Sub

    Private Sub AddNodes(ByVal name As String, ByVal ParentNode As TreeNode)
        CheckForIllegalCrossThreadCalls = False
        Application.DoEvents()

        'escape if there is malformed input
        If name.Length < 2 Then Exit Sub

        Dim SubDir As String = String.Empty
        Dim NewName As String = String.Empty
        Dim SubNode As New TreeNode

        'set the image icon index for the node.
        SubNode.ImageIndex = 3
        SubNode.SelectedImageIndex = 3

        'returns top directory name with no leading '/'
        If name.IndexOf(chrFrontslash, 1) = -1 Then
            'dir has no sub dirs
            SubDir = name.Substring(1)
        Else
            'dir has sub dirs
            SubDir = name.Substring(0, name.IndexOf(chrFrontslash, 1))
        End If

        NewName = name.Substring(SubDir.Length)
        SubNode.Text = Replace(SubDir, chrFrontslash, String.Empty)
        SubNode.Tag = name

        Dim tvi As TreeNode
        For Each tvi In ParentNode.Nodes
            If tvi.Text = SubNode.Text Then
                If Trim(NewName) <> String.Empty Then
                    AddNodes(NewName, tvi)
                    Exit Sub
                End If
            End If
        Next

        If Trim(NewName) <> String.Empty Then
            ParentNode.Nodes.Add(SubNode)
            AddNodes(NewName, SubNode)
        End If
    End Sub

    Private Sub AddLocalFiles(ByVal path As String)
        On Error Resume Next 'bug fix for windows 7 64 bit computers
        Dim lvi As New ListViewItem
        Dim FixPath = SlashScrubber(chrBackslash, path)
        Dim g As New System.IO.FileInfo(FixPath)
        Dim ext As String = Replace(g.Extension, chrPeriod, String.Empty)

        If ext = "exe" Then
            If Not ilLocalFiles.Images.ContainsKey(g.Name) Then
                Dim cIcon As New objIcon
                ilLocalFiles.Images.Add(g.Name, cIcon.GetDefaultIcon(g.FullName, objIcon.IconSize.SmallIcon))
            End If

            lvi.ImageKey = g.Name

        Else
            If Not ilLocalFiles.Images.ContainsKey(ext) Then
                Dim cIcon As New objIcon
                ilLocalFiles.Images.Add(ext, cIcon.GetDefaultIcon(chrPeriod & ext, objIcon.IconSize.SmallIcon))
            End If

            lvi.ImageKey = ext
        End If

        lvi.SubItems(0).Text = g.Name
        lvi.SubItems.Add(ReturnNiceSize(2, g.FullName))
        lvi.Tag = g.FullName

        'add file to listview control
        lvLocalFiles.Items.Add(lvi)
    End Sub

    Private Sub AddIconsToLocalFileIL(ByVal ext As String)
        If Not ilLocalFiles.Images.ContainsKey(ext) Then
            Dim cIcon As New objIcon
            ilLocalFiles.Images.Add(ext, cIcon.GetDefaultIcon(chrPeriod & ext, objIcon.IconSize.SmallIcon))
        End If
    End Sub

    Private Sub sub_AddRemoteFiles(ByVal path As String)
        'reset views
        Me.lvRemoteFiles.BeginUpdate()
        Me.lvRemoteFiles.Items.Clear()

        'loop through the file collection
        For Each objSeg As objSegment In colRemoteFileData
            'assign segment value
            'if the segment is not the first, then continue the loop
            'first segment contains the most important data
            Dim seg As Integer = objSeg.Segment
            If Not seg = 1 Then Continue For

            'assign path variable from hashtable
            'skip to next iteration if the path is longer than the target
            Dim strFilePath As String = objSeg.FilePath 'hash("subject")
            If path.Length > strFilePath.Length Then Continue For

            Try
                'check if there is a directory match
                If strFilePath.StartsWith(path) Then
                    Dim fName As String = strFilePath.Substring(path.Length)

                    'go to next iteration if there is no filename
                    If Trim(Replace(fName, "/", String.Empty)) = String.Empty Then Continue For

                    'check if file is at root of chosen directory and not a file of a child directory
                    If fName.IndexOf("/", 1) = -1 Then
                        'create storage object
                        Dim lvi As New ListViewItem
                        Dim ext As String = String.Empty

                        If fName.Contains(".") Then
                            ext = fName.Substring(fName.LastIndexOf("."))
                            ext = ext.Replace(".", String.Empty)
                        End If

                        If Not ilRemoteFiles.Images.ContainsKey(ext) Then
                            Dim cIcon As New objIcon
                            ilRemoteFiles.Images.Add(ext, cIcon.GetDefaultIcon("." & ext, objIcon.IconSize.SmallIcon))
                            lvi.ImageKey = ext
                        End If

                        lvi.ImageKey = ext

                        'add filename to listview item
                        lvi.Text = Replace(fName, "/", String.Empty)

                        'create tag for file identification
                        lvi.Tag = strFilePath

                        'format file size to list view item.
                        Dim fSize As String = ReturnNiceSize(2, , objSeg.SizeTotal)

                        'if fsize is empty due to a 0 size file, manually make the label string
                        'this is a bug fix
                        If fSize = String.Empty Then
                            fSize = "0 Bytes"
                        End If

                        lvi.SubItems.Add(fSize)

                        If objSeg.IsEncrypted Then
                            lvi.ForeColor = Color.Green
                        End If

                        Dim bSkip As Boolean = False
                        For Each item As ListViewItem In lvRemoteFiles.Items
                            If item.Text = objSeg.FileName Then
                                item.ForeColor = Color.Blue
                                bSkip = True
                            End If
                        Next

                        If bSkip Then Continue For

                        'add listview item to control
                        Me.lvRemoteFiles.Items.Add(lvi)
                    End If
                End If
            Catch ex As Exception
                LogOutput(enumLogType.ERROR, String.Format("Unhandled exception occurred in the [AddRemoteFiles] subroutine. System message: {0}", ex.Message))
            End Try
            Application.DoEvents()
        Next

        'lvRemoteFiles.Sorting = SortOrder.Ascending
        lvRemoteFiles.Sort()

        Me.lvRemoteFiles.EndUpdate()
    End Sub

    Private Sub sub_AppendLine(ByVal sMessage As String) Handles events_Props.AppendConsole
        Try
            txtConsole.AppendText(vbNewLine & sMessage)
            txtConsole.SelectionStart = txtConsole.TextLength
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub sub_CheckFirstRun()

        'load the name of the ver file as an string
        Dim fn As String = String.Empty
        Try
            fn = My.Settings.Version
        Catch ex As Exception
            fn = String.Empty
        End Try

        'generate the hash key if none exists
        If My.Settings.HashKey = String.Empty Then GenerateHashKey()

        'check if the version file exists
        'If Not System.IO.File.Exists(fn) Then
        If fn = String.Empty Then
            '=========
            'first run

            'save the current version to the file
            'SaveTextToFile(cv, fn)
            My.Settings.Version = cv
        Else

            '=============
            'not first run

            'compare current version to version from file [ver.txt]
            'Dim pv As String = GetFileContents(fn)

            If Not cv = fn Then
                'if they do not match, then an update must have occurred:

                'inform user of successful update
                MessageBox.Show(String.Format("Successfully updated to version {0}", cv), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'then overwrite the old file so the EU is not prompted on next startup
                'SaveTextToFile(cv, fn)
                My.Settings.Version = cv
            End If

            'Detect shutdown status and act accordingly.
            Dim b As Boolean = My.Settings.HardShutdown
            If b = False Then
                'if clean shutdown is found, set global varable
                bCleanShutdown = True
            Else
                bCleanShutdown = False
                LogOutput(enumLogType.WARN, "Unclean shutdown detected.")
            End If

            My.Settings.HardShutdown = True

        End If
    End Sub

    Private Sub ChopPath(ByVal raw As String, ByRef root As String, ByRef fname As String)
        root = raw.Substring(0, raw.LastIndexOf(chrBackslash) + 1)
        fname = raw.Substring(raw.LastIndexOf(chrBackslash) + 1)
    End Sub

    Private Sub ConsoleViewChanged(ByVal NewValue As Integer) Handles events_Props.ConsoleViewChanged
        'uncheck all menu items
        menu_None.CheckState = CheckState.Unchecked
        menu_Verbose.CheckState = CheckState.Unchecked
        menu_Normal.CheckState = CheckState.Unchecked


        'txtConsole.SelectionStart = txtConsole.Text.Length

        If NewValue = enums.ConsoleViewSetting.None Then
            scConsoleWrapper.Panel2Collapsed = True
            b_EnableIMAPOutputing = False
            menu_None.CheckState = CheckState.Checked
        ElseIf NewValue = enums.ConsoleViewSetting.Normal Then
            b_EnableIMAPOutputing = False
            scConsoleWrapper.Panel2Collapsed = False
            menu_Normal.CheckState = CheckState.Checked
        ElseIf NewValue = enums.ConsoleViewSetting.Verbose Then
            b_EnableIMAPOutputing = True
            scConsoleWrapper.Panel2Collapsed = False
            menu_Verbose.CheckState = CheckState.Checked
        End If
    End Sub

    Private Sub DoubleBufferListview(ByRef lv As ListView)
        lv.[GetType]().GetProperty("DoubleBuffered", BindingFlags.NonPublic Or BindingFlags.Instance).SetValue(lv, True, Nothing)
    End Sub

    Private Sub sub_DoubleBufferTreeview(ByRef tv As TreeView)
        tv.[GetType]().GetProperty("DoubleBuffered", BindingFlags.NonPublic Or BindingFlags.Instance).SetValue(tv, True, Nothing)
    End Sub

    Public Sub sub_FormLock(ByVal param As enums.frmMainStates)
        CheckForIllegalCrossThreadCalls = False

        If param = frmMainStates.Unlocked Then
            btnLocalDirUpload.Enabled = True
            btnLocalDirUploadEncrypted.Enabled = True
            btnLocalFileUpload.Enabled = True
            btnLocalFileUploadEncrypted.Enabled = True
            lvQueue.Enabled = True
            tvRemoteDir.Enabled = True
            lvRemoteFiles.Enabled = True
            tvRemoteDir.BackColor = lvRemoteFiles.BackColor
            btnRemoteDirDownload.Enabled = True
            btnRemoteDirDelete.Enabled = True
            btnRemoteDirNew.Enabled = True
            btnRemoteRefresh.Enabled = True
            btnRemoteDirRename.Enabled = True
            btnRemoteFileDelete.Enabled = True
            btnRemoteFileDownload.Enabled = True

        ElseIf param = frmMainStates.RemoteFilesEmpty Then
            Call sub_FormLock(frmMainStates.Locked)

            txtRemoteDir.Text = "No remote files to display."

            btnLocalDirUpload.Enabled = True
            btnLocalFileUpload.Enabled = True
            btnLocalDirUploadEncrypted.Enabled = True
            btnLocalFileUploadEncrypted.Enabled = True
            btnRemoteRefresh.Enabled = True

        ElseIf param = frmMainStates.Locked Then
            btnLocalDirUploadEncrypted.Enabled = False
            btnLocalFileUploadEncrypted.Enabled = False
            btnLocalDirUpload.Enabled = False
            btnLocalFileUpload.Enabled = False
            tvRemoteDir.Enabled = False
            lvRemoteFiles.Enabled = False
            tvRemoteDir.BackColor = txtRemoteDir.BackColor
            btnRemoteDirDownload.Enabled = False
            btnRemoteDirDelete.Enabled = False
            btnRemoteDirNew.Enabled = False
            btnRemoteRefresh.Enabled = False
            btnRemoteFileDelete.Enabled = False
            btnRemoteFileDownload.Enabled = False
            btnRemoteDirRename.Enabled = False
        End If

        enumMainState = param
    End Sub

    Private Sub GrabIcons()
        'load an index of shell32 icons into imagelist
        Dim i As Integer = 0
        Do Until i = 50
            AddFolder(ilDir, , i)
            i = i + 1
        Loop
    End Sub

    Private Sub sub_IMAPUIDCollectionDelete()
        Dim arrGmailUIDList As New ArrayList

        'check if there are pending deletes in the collection
        If colPendingDelete.Count <= 0 Then 'fixed at 0.1.27, constrains count to prevent overflow
            b_PendingDelete = False
            Exit Sub
        End If

        'set the max value on the progress bar
        props.ProgressBarMax = colPendingDelete.Count

        'create a tracking integer
        Dim i As Integer = 0

        'iterate through the messages in the delete pending collection
        For Each xObj As objSegment In colPendingDelete
            'extract email address and fetch account info
            Dim email As String = xObj.EmailAddress
            Dim objAssociatedAccount As objAccount = ReturnAccountInfo(email)

            'check the connection is live
            If CheckConnection(smIMAP, email) Then
                Dim rf As String = xObj.FilePath
                Dim suid As String = xObj.UID

                'cull the file name
                rf = rf.Substring(rf.LastIndexOf(chrFrontslash) + 1)

                'update status label and progress bar
                props.StatusMessage = String.Format("Deleting stored file: {0} ({1}/{2})", rf, i, colPendingDelete.Count)
                props.ProgressBarValue = i

                'check the type of email account
                'gmail is the odd one out here, so do it first
                If email.Contains("@gmail.com") OrElse email.Contains("@googlemail.com") OrElse objAssociatedAccount.GMail Then
                    'since gmail uses labels rather than folders, using the \Deleted flag just removes the label, still leaving the message in All Mail
                    'to actually delete a message we have to *add* the Bin/Trash label, then expunge or wait 30 days for gmail to collect garbage
                    'to add a label, we copy the message to the "folder"

                    'array which will collect the return UID of the copied message
                    Dim sReturnUID As String = String.Empty

                    'copy message to bin and get the server reply
                    Dim sCommand As String = String.Format("uid copy {0} {1}[Google Mail]/Bin{1}", suid, chrQuote)
                    Dim eResponse As enumIMAPResponse = smIMAP.IMAPInterface(sCommand, New ArrayList, True, True, True, sReturnUID)

                    'check for success with Bin
                    If eResponse = enumIMAPResponse.imapSuccess Then
                        'remove from column view
                        arrGmailUIDList.Add(sReturnUID)
                        Dim iIndex As Integer = 1
                        For Each sobj As objSegment In colRemoteFileData
                            If sobj.UID = suid AndAlso sobj.EmailAddress = email Then
                                colRemoteFileData.Remove(iIndex)
                            End If
                            iIndex += 1
                        Next

                        'log success
                        LogOutput(enumLogType.INFO, String.Format("Successfully deleted:  [{0}] from [{1}];", rf, smIMAP.str_Email))
                    Else
                        'even if Bin failed, Trash might work
                        sCommand = String.Format("uid copy {0} {1}[Gmail]/Trash{1}", suid, chrQuote)
                        eResponse = smIMAP.IMAPInterface(sCommand, New ArrayList, True, True, True, sReturnUID)
                        If eResponse = enumIMAPResponse.imapSuccess Then
                            arrGmailUIDList.Add(sReturnUID)
                            Dim iIndex As Integer = 1
                            For Each sobj As objSegment In colRemoteFileData
                                If sobj.UID = suid AndAlso sobj.EmailAddress = email Then
                                    colRemoteFileData.Remove(iIndex)
                                End If
                                iIndex += 1
                            Next

                            LogOutput(enumLogType.INFO, String.Format("Successfully deleted:  [{0}] from [{1}];", rf, smIMAP.str_Email))
                        Else
                            'if both Bin AND Trash fail, log the failure
                            LogOutput(enumLogType.INFO, String.Format("Error deleting: [{0}] from [{1}];", rf, smIMAP.str_Email))
                        End If
                    End If
                Else
                    'other providers
                    'for everyone else, we just have to set the \Deleted flag
                End If
            Else
                LogOutput(enumLogType.ERROR, String.Format("Aborted delete attempt. Could not connect to server for account {0}", email))
                Exit Sub
            End If

            'check if any items have been added to the queue list
            'expand queue but show it as diabled
            If lvQueue.Items.Count > 0 AndAlso scMainH.Panel2Collapsed Then
                scMainH.Panel2Collapsed = False
                lvQueue.Enabled = False
            End If


            'increment tracking counter and move to next file
            i += 1
        Next

        'take final step to delete remnants of AirDrive files on gmail hosts
        If arrGmailUIDList IsNot Nothing Then
            If smIMAP.SelectMailBox("[Google Mail]/Bin") OrElse smIMAP.SelectMailBox("[Gmail]/Trash") Then
                Dim nDots As String = "."
                For Each t In arrGmailUIDList
                    If nDots = "....." Then nDots = "."
                    props.StatusMessage = "Purging" & nDots
                    Dim sCommand As String = String.Format("uid store {0} +FLAGS (\Deleted \Seen)", t)
                    Dim e As enumIMAPResponse = smIMAP.IMAPInterface(sCommand, New ArrayList(), True, True, True)
                    If e = enumIMAPResponse.imapSuccess Then

                    Else

                    End If
                    nDots += "."
                Next
                smIMAP.Expunge()
            End If
        End If

        If colRemoteFileData.Count = 0 Then
            Call sub_FormLock(frmMainStates.RemoteFilesEmpty)
        End If

        'clear the pending delete collection
        colPendingDelete.Clear()
    End Sub

    Private Sub sub_ListViewDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvRemoteFiles.DragEnter, lvLocalFiles.DragEnter
        'If bEnableDragDrop Then
        '    e.Effect = DragDropEffects.Move
        'End If
    End Sub

    Private Sub sub_ListViewItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvRemoteFiles.ItemDrag, lvLocalFiles.ItemDrag
        'If bEnableDragDrop Then
        '    DoDragDrop(e.Item, DragDropEffects.Move)
        'End If
    End Sub

    Private Sub sub_LoadDir(ByVal DirPath As String, ByVal Node As Windows.Forms.TreeNode)
        On Error Resume Next
        Dim sExclude() As String = {"RECYCLER", _
                                    "$Recycle.Bin", _
                                    "System Volume Information", _
                                    "$RECYCLE.BIN", _
                                    "Config.Msi"}

        Dim Dir As String

        If Node.Nodes.Count = 0 Then
            For Each Dir In My.Computer.FileSystem.GetDirectories(DirPath)
                If Dir Is Nothing Then Continue For 'catch null and continue
                Dim subnode As New TreeNode
                Dim index As Integer = Dir.LastIndexOf(chrBackslash)

                subnode.ImageIndex = 3
                subnode.SelectedImageIndex = 3
                subnode.Tag = Dir

                subnode.Text = Dir.Substring(index + 1, Dir.Length - index - 1)

                If sExclude.Contains(subnode.Text) Then
                    Continue For
                End If

                Node.Nodes.Add(subnode)
            Next
        End If
    End Sub

    Private Sub sub_LoadFolderTree()
        'clear the listviews before writing the filesystem contents into them.
        lvLocalFiles.Items.Clear()
        tvLocalDir.Nodes.Clear()

        'generate tree items
        Dim basenode, nodeMyComputer, nodeMyDocuments As New System.Windows.Forms.TreeNode

        'create Desktop supernode
        basenode.Text = "Desktop"
        basenode.Tag = My.Computer.FileSystem.SpecialDirectories.Desktop
        basenode.ImageIndex = 34
        basenode.SelectedImageIndex = 34

        'create My Documents node
        nodeMyDocuments.Text = "My Documents"
        nodeMyDocuments.Tag = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        nodeMyDocuments.ImageIndex = 3
        nodeMyDocuments.SelectedImageIndex = 3

        'create My Computer node
        nodeMyComputer.Text = "My Computer"
        nodeMyComputer.Tag = "My Computer"
        nodeMyComputer.ImageIndex = 15
        nodeMyComputer.SelectedImageIndex = 15

        'add items to the my computer node
        'loop through each drive
        For Each item In My.Computer.FileSystem.Drives
            If item.IsReady = True Then
                Dim strName As String = Trim(item.Name)
                If strName IsNot Nothing Then
                    Dim tvi As New TreeNode

                    If item.IsReady Then
                        If item.VolumeLabel = String.Empty Then
                            tvi.Text = item.DriveType.ToString & " (" & Replace(item.Name, "\", String.Empty) & ")"
                        Else
                            tvi.Text = item.VolumeLabel & " (" & Replace(item.Name, "\", String.Empty) & ")"
                        End If
                    End If


                    If item.DriveType = DriveType.CDRom Then tvi.ImageIndex = 11
                    If item.DriveType = DriveType.Fixed Then tvi.ImageIndex = 8
                    If item.DriveType = DriveType.Unknown Then tvi.ImageIndex = 7
                    If item.DriveType = DriveType.Network Then tvi.ImageIndex = 9
                    If item.DriveType = DriveType.Removable Then tvi.ImageIndex = 8
                    If item.DriveType = DriveType.Ram Then tvi.ImageIndex = 12

                    tvi.SelectedImageIndex = tvi.ImageIndex
                    tvi.Tag = strName

                    'sets selected image index to normal index
                    'prevents icon change when highlighting node
                    tvi.SelectedImageIndex = tvi.ImageIndex

                    sub_LoadDir(strName, tvi)
                    nodeMyComputer.Nodes.Add(tvi)
                End If
            End If
        Next

        'Add subnodes to Super Node
        basenode.Nodes.Add(nodeMyDocuments)
        basenode.Nodes.Add(nodeMyComputer)

        'Add desktop dirs to supernode
        For Each Folder In System.IO.Directory.GetDirectories(My.Computer.FileSystem.SpecialDirectories.Desktop)
            Dim tvn As New TreeNode
            tvn.Text = Folder.Substring(Folder.LastIndexOf(chrBackslash) + 1)
            tvn.Tag = Folder
            tvn.ImageIndex = 3
            tvn.SelectedImageIndex = 3
            sub_LoadDir(Folder, tvn)

            basenode.Nodes.Add(tvn)
        Next

        'this for/next fixes an icon display issue in mycomputer items.
        For Each dnode As TreeNode In nodeMyComputer.Nodes
            For Each bnode As TreeNode In dnode.Nodes
                bnode.SelectedImageIndex = 3
                bnode.ImageIndex = 3
            Next
        Next

        'add base node to treeview
        tvLocalDir.Nodes.Add(basenode)

        'expand base node
        tvLocalDir.SelectedNode = basenode
        basenode.Expand()
    End Sub

    Public Sub sub_LogOutProfile()
        Try
            'lock the form from user input on the remote side
            sub_FormLock(frmMainStates.Locked)

            'clear the data collections
            colRemoteFileData.Clear()
            colAccountList.Clear()
            htProfileHeader.Clear()
            colPendingDelete.Clear()
            arrProfiles.Clear()

            'clear form controlls
            txtRemoteDir.Text = String.Empty
            lvRemoteFiles.Items.Clear()
            tvRemoteDir.Nodes.Clear()
            lvQueue.Clear()

            'reset pending delete switch
            b_PendingDelete = False

            'clear the console
            txtConsole.Text = String.Empty

            'stop all threads that are open
            bgwQueueHandler.CancelAsync()
            bgwLoadCloudData.CancelAsync()

            'log out of the imap session
            If smIMAP.b_LoggedIn Then
                smIMAP.LogOut()
            End If

            strProfilePassword = Nothing

            'LogOutput(LogTypeEnum.INFO, String.Format("Logged out of profile: {0}", strProfileName))

            strProfileName = Nothing

        Catch ex As Exception
            LogOutput(enumLogType.ERROR, String.Format("Could not log out of profile: {0}", strProfileName))
        End Try
    End Sub

    Public Sub PopulateRemoteViews(ByVal bReset As Boolean)
        If colAccountList.Count = 0 Then
            tvRemoteDir.Nodes.Clear()
            lvRemoteFiles.Items.Clear()
            sub_FormLock(frmMainStates.RemoteFilesEmpty)
            Exit Sub
        End If

        If colRemoteFileData.Count > 0 Then
            If enumMainState = frmMainStates.Locked OrElse enumMainState = frmMainStates.RemoteFilesEmpty Then
                sub_FormLock(frmMainStates.Unlocked)
            End If
        Else
            sub_FormLock(frmMainStates.RemoteFilesEmpty)
        End If

        Dim BaseNodeRemote As New System.Windows.Forms.TreeNode

        BaseNodeRemote.Nodes.Clear()
        Me.tvRemoteDir.Nodes.Clear()

        'add properties to basenode
        With BaseNodeRemote
            .Text = "/"
            .ImageIndex = 3
            .SelectedImageIndex = 3
            .Tag = "/"
        End With

        For Each xObj As objSegment In colRemoteFileData
            Dim dir As String = xObj.FilePath
            If Trim(dir) <> String.Empty Then
                Try
                    Dim strPath As String = xObj.FilePath
                    strPath = strPath.Substring(0, strPath.LastIndexOf("/"))
                    AddNodes(strPath, BaseNodeRemote)
                Catch ex As Exception
                    Console.WriteLine("Error: problem populating remote listview")
                End Try
            End If
        Next

        tvRemoteDir.Nodes.Add(BaseNodeRemote)
        tvRemoteDir.Sort()

        BaseNodeRemote.Expand()
        sub_AddRemoteFiles("/")
    End Sub

    Private Sub sub_ProgressBarMax(ByVal nMax As Long) Handles events_Props.ProgressBarMaxChanged
        tspbStatus.Maximum = nMax
    End Sub

    Private Sub sub_ProgressBarValue(ByVal nVal As Long) Handles events_Props.ProgressBarValueChanged
        If nVal > tspbStatus.Maximum Then
            tspbStatus.Value = tspbStatus.Maximum
        Else
            tspbStatus.Value = nVal
        End If
        Application.DoEvents()
    End Sub

    Public Sub sub_QueueDownload(ByRef path As String, ByRef isDir As Boolean, Optional ByVal ArcNum As Integer = 1, Optional ByRef locpath As String = "")
        EnableCancelProcessButton()
        scMainH.Panel2Collapsed = False 'expand the queue panel

        path = SlashScrubber(chrFrontslash, path)

        For Each xObj As objSegment In colRemoteFileData
            If bCancelProcess Then
                ProcessCancelled()
                Exit Sub
            End If

            'gather data from file data collection
            Dim File As String = xObj.FilePath
            Dim Size As Integer = xObj.SizeTotal
            Dim seg As Integer = xObj.Segment

            'only list first segment
            If Not seg = 1 Then Continue For

            'iterate if filename is shorter than target length
            If File.Length < path.Length Then Continue For

            'match path
            If File.StartsWith(path) Then
                'Handle adding individual file then escape
                If Not isDir Then
                    If Not File = path Then
                        Continue For
                    End If
                End If


                'create destination path string
                Dim strDest As String = String.Empty
                If locpath = String.Empty Then

                    If Not txtRemoteDir.Text.EndsWith(chrFrontslash) Then
                        strDest = File.Substring(txtRemoteDir.Text.Length + 1)
                    Else
                        strDest = File.Substring(txtRemoteDir.Text.Length)
                    End If

                    If isDir Then
                        strDest = txtLocalDir.Text & chrFrontslash & tvnLastRemoteNode.Text & chrFrontslash & strDest
                    Else
                        strDest = txtLocalDir.Text & chrFrontslash & strDest
                    End If

                    strDest = SlashScrubber(chrBackslash, strDest)

                Else
                    strDest = locpath
                End If


                'add properties to listview item
                Dim lvi As New ListViewItem(strDest)
                With lvi
                    .SubItems.Add("<--")
                    .SubItems.Add(File)
                    .SubItems.Add(ReturnNiceSize(2, , Size))
                    .SubItems.Add("Pending")
                    .Tag = File
                End With

                If ArcNum <> 1 Then
                    lvi.Tag = "<arc>" & ArcNum.ToString & "</arc>"
                End If

                'update status
                props.StatusMessage = "Processing file: " & File
                Application.DoEvents()

                lvQueue.Items.Add(lvi)

                UpdateQueueCount()

                If Not isDir Then
                    Exit For
                End If
            End If
        Next

        ProcessCancelled()

        'clear status text box
        props.StatusMessage = String.Empty

        If Not bgwQueueHandler.IsBusy Then bgwQueueHandler.RunWorkerAsync()
    End Sub

    Private Sub sub_QueueUpload(ByVal path As String, ByVal isDir As Boolean, ByVal isEmpty As Boolean, ByVal encrypted As Boolean)
        EnableCancelProcessButton()

        Dim arrList As New ArrayList

        If Not txtRemoteDir.Text.StartsWith(chrFrontslash) Then txtRemoteDir.Text = chrFrontslash

        'expand the queue panel
        scMainH.Panel2Collapsed = False

        'check if path is a dirctory that needs enumerating or just a file
        If isDir = True Then
            If isEmpty = True Then
                'empty directories cant be enumerated
                arrList.Add(path)
            Else
                'create string of enumerated paths separated by ";"
                Dim t As String = func_EnumerateDirectory(path)

                'add paths to arraylist
                For Each item In t.Split(";")
                    arrList.Add(item)
                Next
            End If
        Else
            'add single path to list
            arrList.Add(path)
        End If

        'enumerate paths in array
        For Each FilePath As String In arrList
            If bCancelProcess Then
                ProcessCancelled()
                Exit Sub
            End If

            'escape if blank path, next iteration of loop
            If Trim(FilePath) = Nothing Then Continue For

            'set status label to indicate processing
            props.StatusMessage = "Processing File:   " & FilePath
            Application.DoEvents()

            Dim g As String = String.Empty

            'if current filepath is an empty directory, switch isDir to true
            If FilePath.LastIndexOf(chrBackslash) = FilePath.Length - 1 Then
                isDir = True
            End If

            'build remote directory data string
            If isDir = True Then
                'if isEmpty is true, then folder is a new folder with no local counterpart
                If isEmpty = True Then
                    g = path
                Else
                    'remove the leading data from local path to the root of the upload path
                    g = FilePath.Substring(path.Length)

                    'combine with remote directory info to make remote path string
                    g = chrFrontslash & txtRemoteDir.Text & chrFrontslash & tvLocalDir.SelectedNode.Text & chrFrontslash & g

                    'replace backslashes with frontslashes and clean up
                    g = SlashScrubber(chrFrontslash, g)
                End If
            Else
                'get file name
                g = path.Substring(path.LastIndexOf(chrBackslash) + 1)

                'combine with remote directory info to make remote path string
                g = chrFrontslash & txtRemoteDir.Text & chrFrontslash & g

                g = SlashScrubber(chrFrontslash, g)
            End If

            Dim t As Integer = FilePath.LastIndexOf(chrBackslash)

            If FilePath.LastIndexOf(chrBackslash) <> FilePath.Length - 1 Then
                'create listview item
                Dim lvi As New ListViewItem(Trim(FilePath))

                If encrypted Then
                    lvi.Tag += "encrypted;"
                    'g = g & "  (encrypted)"
                End If

                'add properties to listview item
                With lvi
                    .SubItems.Add("-->")
                    .SubItems.Add(g)
                    .SubItems.Add(ReturnNiceSize(1, lvi.SubItems(0).Text))
                    .SubItems.Add("Pending")
                End With

                'add listview item to queue listview
                lvQueue.Items.Add(lvi)

                UpdateQueueCount()
            End If
        Next

        'empty status label
        props.StatusMessage = String.Empty

        ProcessCancelled()

        If Not bgwQueueHandler.IsBusy Then bgwQueueHandler.RunWorkerAsync()
    End Sub

    Public Sub RemoteRefresh() Handles Me.RefreshRemoteViews
        PopulateRemoteViews(False)
    End Sub

    Private Sub StatusMessageChanged(ByVal sMessage As String) Handles events_Props.StatusMessageChanged
        Me.tsslStatus.Text = sMessage
    End Sub

    Public Sub TriggerLoad()
        RaiseEvent TriggerEvent(Me, New EventArgs)

        If bSettingsLoaded Then
            If oSettings.ShowConsole Then
                scConsoleWrapper.Panel2Collapsed = False
                menu_Normal.Checked = True
                menu_None.Checked = False
            Else
                scConsoleWrapper.Panel2Collapsed = True
                menu_Normal.Checked = False
                menu_None.Checked = True
            End If
        Else
            scConsoleWrapper.Panel2Collapsed = True
        End If

        'if argument to refresh remote file data is passed, then call file data refresh backgroundworkder thread
        If bRefreshRemoteData = True Then
            colRemoteFileData.Clear()

            'start worker
            If Not bgwLoadCloudData.IsBusy Then bgwLoadCloudData.RunWorkerAsync()

            'close sub, BGW handles populating remote views.
            Exit Sub
        End If

        'if file data is already loaded, check to make sure there are any files to add
        If colRemoteFileData.Count <> 0 Then
            'set form to remote unlocked configuration
            sub_FormLock(frmMainStates.Unlocked)

            Call PopulateRemoteViews(True)    'populate the remote view boxed

            txtRemoteDir.Text = chrFrontslash    'set title text to root directory

            Application.DoEvents()
        Else    'if files list is empty, then load the empty frmMain configuration
            sub_FormLock(frmMainStates.RemoteFilesEmpty)
        End If

        If colAccountList.Count = 0 Then
            Dim fa As New frmActs
            fa.ShowDialog(Me)
        End If

    End Sub

    Private Sub UpdateQueueCount()
        lblQueueCount.Text = "Items:  " & lvQueue.Items.Count 'update item count

        Dim tbytes As Long = 0

        For Each lvi As ListViewItem In lvQueue.Items 'calculate size of queue
            Dim strArray As String() = lvi.SubItems(3).Text.Split(chrSpace) 'pull values from size string and move to string array
            Dim iNumeric As Decimal = Convert.ToDecimal(strArray(0)) 'pull the numeric part of the string array and convert to integer
            Dim i As Integer = 0

            If strArray(1) = "GB" Then
                i = iNumeric * 1024 * 1024 * 1024
            ElseIf strArray(1) = "MB" Then
                i = iNumeric * 1024 * 1024
            ElseIf strArray(1) = "KB" Then
                i = iNumeric * 1024
            Else
                i = iNumeric
            End If
            tbytes += i
        Next

        lblQueueSize.Text = "Size:  " & ReturnNiceSize(1, , tbytes) & " (approx.)"

    End Sub

#End Region


    'This handles all treeview events. 
#Region "Treeview Events"

    Private Sub tvLocalDir_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvLocalDir.AfterCollapse
        'revert folder icons back to closed
        If e.Node.ImageIndex = 4 Then
            e.Node.SelectedImageIndex = 3
            e.Node.ImageIndex = 3
        End If
    End Sub

    Private Sub tvLocalDir_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvLocalDir.AfterExpand
        Dim n As System.Windows.Forms.TreeNode
        For Each n In e.Node.Nodes

            'if node is a folder, change to open folder icon
            If e.Node.ImageIndex = 3 Then
                e.Node.ImageIndex = 4
                e.Node.SelectedImageIndex = 4
            End If

            sub_LoadDir(n.Tag, n)
        Next
    End Sub

    Private Sub tvLocalDir_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvLocalDir.AfterSelect
        On Error Resume Next
        'escape on invalid special directories
        If e.Node.Tag = "My Computer" Then Exit Sub

        txtLocalDir.Text = e.Node.Tag

        'set global variable that can be accessed from other threads
        strLocalDir = e.Node.Tag

        'create comparison string to check if directory has been changed. 
        'this fixes problem of files from more than one directory populating simultaneously.
        Dim strComp As String = txtLocalDir.Text

        lvLocalFiles.BeginUpdate()

        'reset view boxes
        lvLocalFiles.Items.Clear()

        For Each item In System.IO.Directory.GetFiles(txtLocalDir.Text)
            If item Is Nothing Then Continue For

            'escape sub if directory changes while file list is being populated
            If strComp <> txtLocalDir.Text Then Exit Sub

            'files to skip
            If item.ToLower.EndsWith(".lnk") Then Continue For
            If item.ToLower.EndsWith("thumbs.db") Then Continue For
            If item.ToLower.EndsWith("desktop.ini") Then Continue For

            AddLocalFiles(item) 'this causes errors on windows 7 64 bit machines

            'Application.DoEvents()
        Next

        lvLocalFiles.EndUpdate()

    End Sub

    Private Sub tvLocalDir_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvLocalDir.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            tvLocalDir.SelectedNode = e.Node
        End If
    End Sub

    Private Sub tvRemoteDir_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvRemoteDir.AfterCollapse
        tvnLastRemoteNode = e.Node
        tvRemoteDir.SelectedNode = tvnLastRemoteNode
        e.Node.SelectedImageIndex = 3
        e.Node.ImageIndex = 3
    End Sub

    Private Sub tvRemoteDir_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvRemoteDir.AfterExpand
        tvnLastRemoteNode = e.Node
        tvRemoteDir.SelectedNode = tvnLastRemoteNode
        e.Node.SelectedImageIndex = 4
        e.Node.ImageIndex = 4
    End Sub

    Private Sub tvRemoteDir_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvRemoteDir.AfterSelect
        Dim s As String = SlashScrubber("/", e.Node.FullPath)
        txtRemoteDir.Text = s
        strRemoteNodeSelected = s
        Try
            sub_AddRemoteFiles(s)
        Catch ex As Exception
            LogOutput(enumLogType.ERROR, "Error with Remote Node Selection: " & ex.Message)
        End Try


        tvRemoteDir.SelectedNode = e.Node
        tvnLastRemoteNode = e.Node
    End Sub

#End Region

    Private Sub EnableCancelProcessButton()
        If Not tsslCancelProcess.Visible Then tsslCancelProcess.Visible = True
    End Sub
    Private Sub CancelProcess(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsslCancelProcess.Click
        bCancelProcess = True
    End Sub
    Private Sub ProcessCancelled(Optional ByVal ClearStatus = True)
        bCancelProcess = False
        tsslCancelProcess.Visible = False
        If ClearStatus Then tsslStatus.Text = String.Empty
    End Sub
    Private Sub GenerateHashKey()
        My.Settings.HashKey = RandomString(50, False)
    End Sub
    Private Function RandomString(ByVal size As Integer, ByVal lowerCase As Boolean) As String
        Dim builder As New StringBuilder()
        Dim random As New Random()
        Dim ch As Char
        Dim i As Integer
        For i = 0 To size - 1
            ch = Convert.ToChar(Convert.ToInt32((26 * random.NextDouble() + 65)))
            builder.Append(ch)
        Next
        If lowerCase Then
            Return builder.ToString().ToLower()
        End If
        Return builder.ToString()
    End Function
End Class