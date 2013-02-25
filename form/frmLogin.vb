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
'    Filename: frmLogin.vb


Imports AirDrive.vars
Imports AirDrive.functions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports AirDrive.func_gzip
Imports AirDrive.func_rcrypt
Imports AirDrive.func_io_file
Imports AirDrive.func_convert


Public Class frmLogin
    Private Shared strPW As String = String.Empty
    Public Shared pName As String = String.Empty
    Public Shared bLoginCancelClose As Boolean = False
    Public bClose As Boolean = False

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not bClose Then
            Call frmMain.TriggerLoad()
        End If
    End Sub

    Private Sub frmLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bLoginCancelClose = True Then
            e.Cancel = True
            bLoginCancelClose = False
        End If
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnLogin.Enabled = True
        btnCancel.Enabled = True

        arrProfiles = func_LoadProfiles()
        If Not arrProfiles.Count = 0 Then
            For Each line In arrProfiles
                Dim outline As String = line
                outline = outline.Substring(0, outline.IndexOf(chrPeriod))
                cbSelAccount.Items.Add(outline)
            Next

            Dim keyValue As String
            keyValue = My.Settings.LastProfile


            If Not keyValue = Nothing Then
                If cbSelAccount.Items.Contains(keyValue) Then
                    cbSelAccount.SelectedText = keyValue
                    cbSelAccount.Text = keyValue
                Else
                    cbSelAccount.SelectedIndex = 0
                End If
            Else
                cbSelAccount.SelectedIndex = 0
            End If
        Else
            'this should probably just load the profile creation page
            MsgBox("A fatal error has occurred, " & strAppName & " will now close.")
            Application.Exit()
        End If

        If Not My.Settings.AutoLoginString = String.Empty AndAlso Not My.Settings.AutoLoginProfile = String.Empty Then
            Dim decodedpw As String = ecrypt(My.Settings.AutoLoginString, My.Settings.HashKey, True)
            cbSelAccount.SelectedText = My.Settings.AutoLoginProfile
            cbSelAccount.Text = My.Settings.AutoLoginProfile
            txtPassword.Text = decodedpw
            btnLogin_Click(Me, New EventArgs)
        Else

            txtPassword.Focus()
        End If
    End Sub

    Private Sub btnCancel_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        bClose = True
        Application.Exit()

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If txtPassword.Text.Length < 4 Then
            MsgBox("Invalid password length. Please try again.")
            txtPassword.Focus()
            Exit Sub
        End If

        If cbSelAccount.SelectedItem = Nothing Then
            MsgBox("No profile selected")
            cbSelAccount.Focus()
            Exit Sub
        End If

        pName = cbSelAccount.SelectedItem

        strProfilePassword = txtPassword.Text
        strProfileName = pName

        If bgwVerify.IsBusy = False Then bgwVerify.RunWorkerAsync()

    End Sub

    Private Sub bgwVerify_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwVerify.DoWork
        CheckForIllegalCrossThreadCalls = False
        btnLogin.Enabled = False
        btnCancel.Enabled = False

        CheckForIllegalCrossThreadCalls = False
        Application.DoEvents()


        LoadSettingsXML()
        If LoadProfileXML() Then
            If colAccountList.Count <> 0 Then
                LoadFilesXML()
            End If
            'My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\" & strAppName, "LastProfile", pName)
            My.Settings.LastProfile = pName
            Me.Close()
        End If
    End Sub

#Region "Threaded File Parsing Subroutines"

    Private Sub LoadSettingsXML()
        Dim xmlStream As New MemoryStream
        Dim strSettingsStore As String = String.Empty
        Dim strOutputName As String = pName & ".sxml"

        If Not File.Exists(strOutputName) Then Exit Sub

        strSettingsStore = GetFileContents(SlashScrubber(chrBackslash, Application.StartupPath & "\" & strOutputName))

        If strSettingsStore.Trim = String.Empty Then
            LogOutput(enums.enumLogType.ERROR, "[LoadSettingsXML]: Attempt to load setting file resulted in zero length file. Aborting. ")
            MessageBox.Show("Could not load the settings for this profile.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If bEncryptStore = True Then
            Try
                strSettingsStore = rDecrypt(strSettingsStore, strProfilePassword)
            Catch ex As Exception
                LogOutput(enums.enumLogType.ERROR, "[LoadSettingsXML]: Error decoding XML file. " & ex.Message)
            End Try
        End If

        If bCompressXML Then
            strSettingsStore = gzDecompress(strSettingsStore)
        End If

        Dim tBytes As Byte() = Encoding.ASCII.GetBytes(strSettingsStore)

        xmlStream.Write(tBytes, 0, tBytes.Length)
        xmlStream.Position = 0

        Try
            Dim m_xmld As XmlDocument
            Dim m_nodelist As XmlNodeList
            Dim m_node As XmlNode

            m_xmld = New XmlDocument()
            m_xmld.Load(xmlStream)
            m_nodelist = m_xmld.SelectNodes("/Nodes/Node")

            For Each m_node In m_nodelist
                Dim xHash As New Hashtable
                For Each cNode As XmlNode In m_node.ChildNodes
                    UpdateHash(xHash, cNode.Name, cNode.InnerText)
                Next

                'convert hashtable to Custom object type
                oSettings = New objSettingsTable(xHash)
            Next
        Catch errorVariable As Exception
            btnLogin.Enabled = True
            btnCancel.Enabled = True
        End Try

        bSettingsLoaded = True

        LogOutput(enums.enumLogType.INFO, String.Format("Successfully loaded settings from file: {0}", pName & ".sxml"))
    End Sub

    Private Function LoadProfileXML() As Boolean
        Dim xmlStream As New MemoryStream
        Dim strProfileStore As String = String.Empty

        'clear the profile header to prevent runtime error
        htProfileHeader.Clear()

        strProfileStore = GetFileContents(SlashScrubber(chrBackslash, Application.StartupPath & "\" & pName & ".axml"))

        If strProfileStore.Trim = String.Empty Then
            LogOutput(enums.enumLogType.ERROR, "[LoadSettingsXML]: Attempt to load setting file resulted in zero length file. Aborting. ")
            MessageBox.Show("Could not load this profile.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If

        If bEncryptStore = True Then
            Try
                strProfileStore = rDecrypt(strProfileStore, strProfilePassword)
            Catch ex As Exception
                MessageBox.Show("Invalid password. Please try again." & vbNewLine & "System error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Text = String.Empty
                txtPassword.Focus()
                btnLogin.Enabled = True
                btnCancel.Enabled = True
                Return False
            End Try
        End If

        If bCompressXML Then
            strProfileStore = gzDecompress(strProfileStore)
        End If

        Dim tBytes As Byte() = Encoding.ASCII.GetBytes(strProfileStore)

        xmlStream.Write(tBytes, 0, tBytes.Length)
        xmlStream.Position = 0

        If Not strProfileStore.Contains("?xml") Then
            MsgBox("Password incorrect. Please try again or recreate profile.")
            txtPassword.Text = String.Empty
            txtPassword.Focus()
            btnLogin.Enabled = True
            btnCancel.Enabled = True
            Return False
        Else

            Application.DoEvents()
            strProfileName = cbSelAccount.SelectedItem
            sLogFileName = strProfileName & ".txt"

            'This try block loads the profile information
            Try
                Dim m_xmld As XmlDocument
                Dim m_nodelist As XmlNodeList
                Dim m_node As XmlNode

                m_xmld = New XmlDocument()
                m_xmld.Load(xmlStream)
                m_nodelist = m_xmld.SelectNodes("/Nodes/Node")

                For Each m_node In m_nodelist
                    Dim xHash As New Hashtable
                    For Each cNode As XmlNode In m_node.ChildNodes
                        UpdateHash(xHash, cNode.Name, cNode.InnerText)
                    Next

                    'convert hashtable to Custom object type
                    Dim objAct As New objAccount(xHash)
                    colAccountList.Add(objAct, objAct.EmailAddress)
                Next
            Catch errorVariable As Exception
                Dim result As MsgBoxResult = MessageBox.Show("An error has occurred: " & errorVariable.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnLogin.Enabled = True
                btnCancel.Enabled = True
                Return False
            End Try
        End If

        LogOutput(enums.enumLogType.INFO, String.Format("Successfully unlocked profile: {0}", pName))

        Return True
    End Function

    Private Sub LoadFilesXML()
        'This try block loads the remote file information (if any)

        Dim strPath As String = SlashScrubber(chrBackslash, Application.StartupPath & "\" & pName & ".fxml")

        If System.IO.File.Exists(strPath) Then
            Dim result As New MsgBoxResult
            If Not oSettings.LoadCache AndAlso My.Settings.AutoLoginProfile = strProfileName Then
                If bCleanShutdown Then
                    result = MessageBox.Show("Cached file information has been detected for this profile." & vbNewLine & "Would you like to load the cached data?", _
                             "Load cached data?", _
                             MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question)
                Else
                    result = MessageBox.Show("AirDrive was abnormally shutdown on its last run or was recently updated. Any cached files may be corrupted our outdated. " & vbNewLine & "It is recommended that you refresh your file data from the servers now. " & vbNewLine & vbNewLine & "Do you want to continue WITHOUT refreshing the data?", _
                             "Load cached data?", _
                             MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question)
                End If
            Else
                result = MsgBoxResult.Yes
            End If

            If result = MsgBoxResult.Yes Then
                Dim xmlStream As New MemoryStream
                Dim strFilesStore As String = String.Empty

                strFilesStore = GetFileContents(strPath)

                If strFilesStore.Trim = String.Empty Then

                End If
                
                If bEncryptStore = True Then
                    strFilesStore = rDecrypt(strFilesStore, strProfilePassword)
                End If

                If bCompressXML Then
                    strFilesStore = gzDecompress(strFilesStore)
                End If

                Dim tBytes() As Byte = Encoding.ASCII.GetBytes(strFilesStore)

                xmlStream.Write(tBytes, 0, tBytes.Length)
                xmlStream.Position = 0

                If Not strFilesStore.Contains("?xml") Then
                    MsgBox("Cached remote file data could not be decoded. File data will be refreshed from servers.")
                    bRefreshRemoteData = True
                    Me.Close()
                Else
                    Try

                        Dim m_xmld As XmlDocument
                        Dim m_nodelist As XmlNodeList
                        Dim m_node As XmlNode

                        m_xmld = New XmlDocument()
                        m_xmld.Load(xmlStream)
                        m_nodelist = m_xmld.SelectNodes("/Nodes/Node")

                        For Each m_node In m_nodelist
                            Dim xHash As New Hashtable
                            For Each cNode As XmlNode In m_node.ChildNodes
                                UpdateHash(xHash, cNode.Name, cNode.InnerText)
                            Next

                            Dim objSeg As New objSegment(xHash)
                            'colRemoteFileData.Add(objSeg, objSeg.EmailAddress & ":" & objSeg.UID)
                            AddToCollection(colRemoteFileData, objSeg)
                        Next
                    Catch ex As Exception
                        LogOutput(enums.enumLogType.ERROR, "Error occurred while parsing the [loadfilexml] subroutine. Aborting.")
                        Exit Sub
                    End Try

                End If
                bRefreshRemoteData = False
                LogOutput(enums.enumLogType.INFO, "Unlocked profile data information in file: " & pName & ".fxml")

            ElseIf result = MsgBoxResult.No Then
                colRemoteFileData.Clear()
                bRefreshRemoteData = True
                'ElseIf result = MsgBoxResult.Cancel Then
                '   btnCancel.Enabled = True
                '  btnLogin.Enabled = True
                ' frmMain.sub_LogOutProfile()
                'Exit Sub
            End If
        Else
            bRefreshRemoteData = True
        End If

        Me.Close()
    End Sub

#End Region

    Private Sub btnNewProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewProfile.Click
        If arrProfiles.Count >= nproflim Then
            MsgBox("While in beta, the maximum number of profiles you can generate is " & nproflim)
            Exit Sub
        End If

        Me.Close()
        Me.Dispose()

        Try
            Dim t As New frmProfile
            t.ShowDialog(frmMain)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnDeleteAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAccount.Click
        Dim result As New MsgBoxResult
        result = MessageBox.Show("Are you sure you want to delete " & cbSelAccount.SelectedItem & "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = MsgBoxResult.Yes Then
            Dim cbItem As String = cbSelAccount.SelectedItem

            sub_DeleteProfile(cbItem)
            cbSelAccount.Items.Remove(cbItem)

            If cbSelAccount.Items.Count = 0 Then
                Me.Visible = False
                frmProfile.ShowDialog(Me)
                Me.Close()
            Else
                cbSelAccount.SelectedIndex = 0
            End If
        End If
        arrProfiles = func_LoadProfiles()
        bLoginCancelClose = True
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Login_Form")
    End Sub
End Class