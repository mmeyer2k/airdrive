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
'    Filename: frmActs.vb


#Region "Imports"
Imports Microsoft.Win32
Imports AirDrive.frmMain
Imports AirDrive.vars
Imports AirDrive.functions
Imports AirDrive.methods
Imports System.Threading
Imports AirDrive.enums
#End Region

Public Class frmActs

#Region "Global Variables"
    Private bCancelClose As Boolean = False
    Private ActType As enumActType
    Private strHostname As String = String.Empty
    Private strUsername As String = String.Empty
    Private strPassword As String = String.Empty
    Private strEmail As String = String.Empty
    Private intPort As Integer = 993
    Private bSSL As Boolean = True
    Private intQuota As Integer = 0
    Private bDefault As Boolean = False
    Private intAttachmentMaximum As Long = 0
    Private xIMAP As New objIMAP
    Private bIsGmail As Boolean = False
#End Region

#Region "Subroutines"

    Private Sub Verify()
        'prevent threading errors
        CheckForIllegalCrossThreadCalls = False

        'check if the email address already exists in the collection
        If colRemoteFileData.Contains(strEmail) Then
            MsgBox("This profile already contains an account with this name")

            DisposeAllComponents()

            Call frmActs_Load(Me, New EventArgs)
        End If

        'disable the buttons so the user cant interupt the process
        btnNext.Enabled = False
        btnBack.Enabled = False
        btnCancel.Enabled = False

        'create hashtable to store data in
        Dim xObj As New objAccount()

        'Bold first item in process
        lblVerify.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)

        'attempt log in
        Dim boolSuccess As Boolean
        boolSuccess = xIMAP.Login(strHostname, intPort, bSSL, strUsername, strPassword)

        If boolSuccess Then
            'on successful login:

            xObj.IsFunctioning = True

            'update interface to inform user, unbold top line and bold the second
            lblVerify.Text += ": complete."
            lblConfig.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            lblVerify.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)


            'add working credentials to hashtable
            xObj.IsFunctioning = True
            xObj.Hostname = strHostname
            xObj.Password = strPassword
            xObj.Username = strUsername
            xObj.EmailAddress = strEmail
            xObj.Quota = intQuota
            xObj.MaxAttachmentSize = intAttachmentMaximum
            xObj.SSL = bSSL
            xObj.Port = intPort
            xObj.GMail = bIsGmail

            'create a temporary collection
            Dim tCollection As New Collection

            'open the AirDrive folder
            Dim bFolderOpen As Boolean = OpenIMAPFolder(xIMAP)

            If bFolderOpen = True Then
                'after opening folder:

                xObj.Items = xIMAP.int_TotalMessages

                'check how many file fragments are in the folder.
                If xIMAP.int_TotalMessages > 0 Then
                    'if there are more than 0 files:

                    Dim i As Integer = 0

                    xIMAP.num_CommandValue += 1

                    Dim sCommand As String = xIMAP.imap_cmd_Identifier & String.Format("fetch 1:{0} (envelope UID)", xIMAP.int_TotalMessages) & strEOL
                    Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(sCommand.ToCharArray())

                    'if there is an SSL connection the write to SSL stream, else, write to normal stream
                    If xIMAP.b_SSL Then
                        xIMAP.stream_SSL.Write(data, 0, data.Length)
                    Else
                        xIMAP.stream_NetStrm.Write(data, 0, data.Length)
                    End If

                    'create an imap response variable
                    Dim eResponse As enumIMAPResponse = enumIMAPResponse.imapSuccess

                    'begin reading server response
                    Do
                        'read line to string variable
                        Dim sLine As String = If(xIMAP.b_SSL, xIMAP.obj_Reader.ReadLine(), xIMAP.stream_NetworkReader.ReadLine())

                        'if the line is a response line, then exit the loop
                        If IsResponeLine(sLine, eResponse) Then

                            'check if there was a failure response from the IMAP server.
                            If eResponse = enumIMAPResponse.imapFailure OrElse eResponse = enumIMAPResponse.IMAP_IGNORE_RESPONSE Then
                                'if there is an error response:

                                'log the error
                                LogOutPut(enumLogType.ERROR, String.Format("Failure response from server during enumeration procedure on {0}: {1}; exiting...", xIMAP.str_Email, sLine))
                            End If

                            'exit the loop, enumeration is over.
                            Exit Do
                        End If

                        'create a new hashtable and subject string to store segment data in
                        Dim xObjSeg As New objSegment
                        Dim sNewSubject As String = String.Empty

                        'pass the raw envelope and virgin hashtable to the envelope parser
                        ParseEnvelope(xIMAP, sLine, xObjSeg)

                        'calculate percentage to display to user and update label, refresh gui
                        If Not i = 0 Then lblConfig.Text = "Configure storage area:  gathering data (" & Convert.ToString(Math.Floor(i / xIMAP.int_TotalMessages * 100)) & "%)"
                        Application.DoEvents()

                        'add file segment hashtable to the master collection
                        AddToCollection(colRemoteFileData, xObjSeg)

                        'if it is the last message, exit loop
                        If i = xIMAP.int_TotalMessages - 1 Then Exit Do

                        'increment counter
                        i += 1
                    Loop

                    'create the XML data store.
                    StoreFileDataToXML()

                Else
                    'no messages to store:

                    'save file data just in case. also as a place holder file.
                    StoreFileDataToXML()
                End If
            ElseIf bFolderOpen = False Then
                'if connection could be established but folder could not be opened:

                'inform user with message box
                MessageBox.Show("Could not create or open storage folder on server. AirDrive may not be compatible with this server.", "Quota error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'logout of IMAP instance
                xIMAP.LogOut()

                'reload the form for the user to try again with.
                Call frmActs_Load(Me, New EventArgs)

                'exit subroutine
                Exit Sub
            Else
                'any unhandled error:

                'inform user with messagebox
                MessageBox.Show(String.Format("An unhandled exception occurred when attempting to retreive quota information from the server {0}", xIMAP.str_Host), "Quota error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'log out of the instance of IMAP
                xIMAP.LogOut()

                'reload the form for the user to try again with.
                Call frmActs_Load(Me, New EventArgs)

                'exit the subroutine
                Exit Sub
            End If

            'set the CONFIG line to complete
            lblConfig.Text = "Configure storage area: complete"
            lblConfig.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

            'bold the "checking free space" line
            lblSpace.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)

            'call quota retreive subroutine
            'pass by reference the hashtable and fill it with quota data
            GetIMAPQuota(xIMAP, xObj)


            'check if the GetQuota sub returned any data
            If xObj.BytesTotal <> 0 Then
                'if quota command returns valid information:

                'update free space line to show completed
                lblSpace.Text += ": complete."

                'unbold free space line
                lblSpace.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

                'bold the saving line
                lblSave.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            Else


                MessageBox.Show("Could not fetch size information settings for this account.", "Quota error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'log out of the instance of IMAP
                xIMAP.LogOut()

                'reload the form for the user to try again with.
                Call frmActs_Load(Me, New EventArgs)

                'exit the subroutine
                Exit Sub
            End If

            'add to frmMain account list collection
            'colAccountList.Add(hash, strEmail)
            colAccountList.Add(xObj, xObj.EmailAddress) '<--experimental

            'save encrypted XML file with account data inside of it
            StoreAcntDataToXML()

            'sleep for a second so that the execution pauses for at least a second on this item
            System.Threading.Thread.Sleep(1000)

            'unbold the final line label
            lblSave.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

            'update the label for the final line to show completed
            lblSave.Text += ": complete."

            'enable the add another account button
            btnAddAgain.Enabled = True

            'bring the add again button to the front
            btnAddAgain.BringToFront()

            'disable back button because there is no where to go back to
            btnBack.Enabled = False

            'enable the NEXT button and change the text to 'finish'
            btnNext.Enabled = True
            btnNext.Text = "Finish"
        Else
            'if the original connection attempt returns false:

            xObj.IsFunctioning = False

            'update the text on the first line to show failed
            lblVerify.Text += ": failed."
            Application.DoEvents()

            'show message box to user
            Dim m As String = String.Empty
            Dim t As String = String.Empty
            m = "Could not log in to server, "
            If xIMAP.str_LastError.Trim = String.Empty Then
                m = m & vbNewLine & "please check your internet connection."
                t = "Connection error"
            Else
                m = m & "please verify your connection information."
                m = m & vbNewLine & vbNewLine & "Server  Response: " & xIMAP.str_LastError
                t = "Authentication error"
            End If
            MessageBox.Show(m, t, MessageBoxButtons.OK, MessageBoxIcon.Error)

            'cancel form escape routine
            bCancelClose = True

            'close the IMAP instance
            xIMAP.LogOut()

            'reload the form for the user to try again with.
            Call frmActs_Load(Me, New EventArgs)

            'exit subroutine
            Exit Sub

        End If
    End Sub

    Private Sub DisposeAllComponents()
        For Each Control In Me.Controls
            For i As Integer = 0 To Controls.Count
                Try
                    Controls(i).Dispose()
                Catch ex As Exception
                    'do nothing
                End Try
            Next
            Try
                Controls.Clear()
                InitializeComponent()
            Catch ex As Exception
                'do nothing
            End Try
        Next
    End Sub

#End Region

#Region "Events"
    Private Sub cbOtherEmailSame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOtherEmailSame.CheckedChanged
        If cbOtherEmailSame.CheckState = CheckState.Unchecked Then
            txtOtherEmail.Clear()
            txtOtherEmail.Enabled = True
        Else
            If txtOtherUsername.Text = Nothing OrElse txtOtherHostname.Text = Nothing Then Exit Sub
            txtOtherEmail.Text = txtOtherUsername.Text & "@" & txtOtherHostname.Text
            txtOtherEmail.Enabled = False
        End If
    End Sub
    Private Sub btnAddAgain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAgain.Click

        'check if there are the maximum number of accounts in this collection
        If colAccountList.Count >= nActLim Then
            'if the EU as at the max number of accounts then:

            'inform user that they can not add a new account
            MessageBox.Show("For testing purposes, you are restricted to " & nActLim & " accounts at this time.")

            'set boolean to skip closing the form
            bCancelClose = True

            'exit this subroutine
            Exit Sub

        Else
            'if eu can still add accounts:

            'dispose of all of the controls on the form
            Me.DisposeAllComponents()

            'reload form
            Call frmActs_Load(sender, e)

        End If

    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If tcWizard.SelectedTab Is tabQuota Then
            btnBack.Enabled = False
            tcWizard.SelectedTab = tabActType
        ElseIf tcWizard.SelectedTab Is tabGMail OrElse tcWizard.SelectedTab Is tabOther Then
            tcWizard.SelectedTab = tabQuota
        ElseIf tcWizard.SelectedTab Is tabFinish Then

        End If

    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If tcWizard.SelectedTab Is tabActType Then
            '===Account Type page:

            If rbGmail.Checked = True Then

                'if GMAIL is the chosen account type:

                'move wizard to quota page 
                tcWizard.SelectedTab = tabQuota

                'imap server must be imap.gmail.com:993
                strHostname = "imap.gmail.com"
                intPort = 993

                'set the account type enumerator to GMAIL
                ActType = enumActType.GMAIL

                intAttachmentMaximum = 1024 * 1000 * 23 '23 megabyte maximum for gmail accounts

            ElseIf rbOther.Checked = True Then
                'if OTHER is chosen account type

                tcWizard.SelectedTab = tabQuota
                ActType = enumActType.Other

            Else
                'handle any unlikely input errors for safety
                'close application because getting here should be impossible
                MsgBox("Invalid selection.")
                Application.Exit()
            End If

            'enable back button
            btnBack.Enabled = True

        ElseIf tcWizard.SelectedTab Is tabQuota Then
            '===Quota page:

            'send the user to the next tab appropriately by checking the account type enumerator value
            If ActType = enumActType.GMAIL Then
                tcWizard.SelectedTab = tabGMail
            ElseIf ActType = enumActType.Other Then
                tcWizard.SelectedTab = tabOther
            End If

        ElseIf tcWizard.SelectedTab Is tabGMail Then
            '===GMAIL page:

            bIsGmail = True

            If txtGMailPassword.Text = String.Empty OrElse txtGMailPassword.Text = String.Empty Then
                MsgBox("Invalid input.")
                Exit Sub
            End If

            If colAccountList.Contains(txtGMailUsername.Text) Then
                MsgBox("Account has already been added to this profile.")
                Exit Sub
            End If

            'populate connection variables with textbox data
            strPassword = txtGMailPassword.Text
            strUsername = txtGMailUsername.Text
            strEmail = txtGMailUsername.Text

            intQuota = TrackBar1.Value

            'change tab selection to Verify page
            tcWizard.SelectedTab = tabFinish

            'disable the cancel button
            btnCancel.Enabled = False

            'start the account verification thread
            Dim thd As New Threading.Thread(AddressOf Verify)
            thd.Start()



        ElseIf tcWizard.SelectedTab Is tabOther Then
            '===Other page

            'not complete!
            If txtOtherUsername.Text Is Nothing Then
                MsgBox("Please enter your username")
                txtOtherUsername.Focus()
                Exit Sub
            ElseIf txtOtherPassword.Text Is Nothing Then
                MsgBox("Please enter your IMAP username")
                txtOtherPassword.Focus()
                Exit Sub
            ElseIf txtOtherPort.Text Is Nothing Then
                MsgBox("Please enter your IMAP server's port number")
                txtOtherPort.Focus()
                Exit Sub
            ElseIf txtOtherEmail.Text Is Nothing Then
                MsgBox("Please enter your email address")
                txtOtherEmail.Focus()
                Exit Sub
            End If

            strHostname = txtOtherHostname.Text
            strUsername = txtOtherUsername.Text
            strPassword = txtOtherPassword.Text
            intPort = txtOtherPort.Text
            bSSL = checkOtherSSL.Checked
            strEmail = txtOtherEmail.Text
            intAttachmentMaximum = trackOtherAttachmentSize.Value * 1024 * 1000

            'disable the cancel button
            btnCancel.Enabled = False

            'change tab selection to Verify page
            tcWizard.SelectedTab = tabFinish

            'start the account verification thread
            Dim thd As New Threading.Thread(AddressOf Verify)
            thd.Start()

        ElseIf tcWizard.SelectedTab Is tabFinish Then
            '===Verify page

            'log out of the IMAP instance
            xIMAP.LogOut()

            'hide the login and accounts forms
            frmLogin.Visible = False
            Me.Visible = False

            'set boolean refresh switch to off
            'this will prevent the triggerload subroutine from firing off the refresh BGW
            bRefreshRemoteData = False

            'fire triggerload
            Call frmMain.TriggerLoad()

            'exit instances of all of the precursor forms
            'dispose all of their data
            Try
                Me.Visible = False
                frmLogin.Visible = False
            Catch ex As Exception
                'do nothing on error
            End Try


        Else
            'if, somehow, an invalid tab index is selected, the program should exit.
            MessageBox.Show("Invalid operation. AirDrive must now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LogOutput(enumLogType.ERROR, "Program execution reached an impossible location. Closing.")
            Application.Exit()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If colAccountList.Count = 0 Then
            MessageBox.Show("No email accounts added for this profile." & vbNewLine & "To link email accounts to your profile, go to Add Accounts in the File menu", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If

        If colRemoteFileData.Count = 0 Then
            frmMain.sub_FormLock(enums.frmMainStates.RemoteFilesEmpty)
        Else
            frmMain.sub_FormLock(enums.frmMainStates.Unlocked)
        End If

        bCancelClose = False

        Me.Close()
    End Sub
    Private Sub frmActs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'check if the private boolean cancel switch is true
        If bCancelClose Then
            'abort cancel if selected
            e.Cancel = True
        End If

        'dispose of all of the form's components
        'probably obsolete with me.dispose()
        DisposeAllComponents()
    End Sub
    Private Sub frmActs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'dispose of all of the variables incase this is just a reuse of this instance of the form (add another account button)
        'this is probably redundant do to the me.dispose() call at refresh
        strHostname = String.Empty
        strPassword = String.Empty
        strEmail = String.Empty
        strUsername = String.Empty
        intPort = 0

        'clean all text input boxes
        txtGMailPassword.Text = String.Empty
        txtGMailUsername.Text = String.Empty

        'reset the quota trackback
        TrackBar1.Value = 70
        txtPercent.Text = "70%"

        'enable the cancel button
        btnCancel.Enabled = True

        'hide the login form
        frmLogin.Visible = False

        'enable the next button and ensure that text is correct
        btnNext.Text = "Next"
        btnNext.Enabled = True

        trackOtherAttachmentSize.Value = 25

        'reenable cancel button just incase
        btnCancel.Enabled = True

        'disable the add again button until the final tab of the wizard
        btnAddAgain.Enabled = False

        'check if the program is in design mode
        'this way i can navigate the tabs, but the users cant see them
        If DesignMode = True Then
            pTitleBar.SendToBack()
            tcWizard.BringToFront()
        Else
            pTitleBar.BringToFront()
            tcWizard.SendToBack()
        End If

        'reset all of the labels on the finish page
        lblVerify.Text = "Verify connection to server"
        lblSave.Text = "Save account settings"
        lblSpace.Text = "Check free space"
        lblConfig.Text = "Configure IMAP storage area"

        lblVerify.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        lblSave.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        lblSpace.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        lblConfig.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)


        ActType = Nothing

        'open the first tab of the wizard
        tcWizard.SelectedTab = tabActType

        'disable the backbutton because we are switching to the first tab.
        btnBack.Enabled = False
    End Sub
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        txtPercent.Text = TrackBar1.Value & "%"
        intQuota = TrackBar1.Value
    End Sub
    Private Sub trackOtherAttachmentSize_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackOtherAttachmentSize.ValueChanged
        lblOtherMaxDisp.Text = trackOtherAttachmentSize.Value & " MB"
    End Sub
    Private Sub txtOtherPort_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOtherPort.KeyDown
        Dim c As Char = Convert.ToChar(e.KeyValue)
        If Not Char.IsDigit(c) Then
            e.SuppressKeyPress = True
        End If
    End Sub
#End Region

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Account_creation_form")
    End Sub
End Class

