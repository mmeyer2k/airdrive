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
'    Filename: frmProfile.vb


Imports AirDrive.methods
Imports AirDrive.vars
Imports AirDrive.func_rcrypt


Public Class frmProfile


    Private bCancelClose As Boolean = False


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        If txtUsername.Text = Nothing Then
            txtUsername.Focus()
            MsgBox("Please enter a name for this profile")
            bCancelClose = True
            Exit Sub
        End If

        If txtUsername.Text.Length < 4 Then
            MsgBox("Username must be atleast 4 characters long.")
            txtUsername.Focus()
            bCancelClose = True
            Exit Sub
        End If

        If txtPassword.Text <> txtPasswordConf.Text Then
            MsgBox("Passwords do not match. Please try again.")
            txtPassword.Text = String.Empty
            txtPasswordConf.Text = String.Empty
            txtPassword.Focus()
            bCancelClose = True
            Exit Sub
        End If

        If txtPassword.Text.Length < 6 Then
            MsgBox("Password must be atleast 6 characters long. Any characters are valid.")
            txtPassword.Focus()
            bCancelClose = True
            Exit Sub
        End If

        If System.IO.File.Exists(txtUsername.Text & ".axml") OrElse System.IO.File.Exists(txtUsername.Text & ".fxml") Then
            MsgBox("A profile with this name already exists.")
            bCancelClose = True
            Exit Sub
        End If

        For Each ch As Char In txtUsername.Text
            If Not Char.IsLetterOrDigit(ch) Then
                MsgBox("Profile name can contain only letters and numbers.")
                txtUsername.Text = String.Empty
                bCancelClose = True
                Exit Sub
            End If
        Next

        'new empty collection to pass to HTColToXML subroutine
        Dim c As New Collection

        UpdateHash(htProfileHeader, "type", "profile")
        UpdateHash(htProfileHeader, "name", txtUsername.Text)
        UpdateHash(htProfileHeader, "crib", func_MD5HashFromString(txtPassword.Text))

        strProfileName = txtUsername.Text
        strProfilePassword = txtPassword.Text

        HTColToXML(c, Application.StartupPath & "\" & txtUsername.Text & ".axml")

        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If arrProfiles.Count = 0 Then
            Dim result As New MsgBoxResult
            result = MessageBox.Show("You must create a profile to use " & strAppName & ". " & vbNewLine & "Are you sure you want to close the application without creating a profile?", _
                                     "Exit", _
                                     MessageBoxButtons.YesNo, _
                                     MessageBoxIcon.Hand)

            If result = MsgBoxResult.Yes Then
                Application.Exit()
            ElseIf result = MsgBoxResult.No Then
                bCancelClose = True
                Exit Sub
            End If
        Else
            Me.Dispose()
            Me.Close()
            frmLogin.ShowDialog()
        End If
    End Sub

    Private Sub cbUnmask_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUnmask.CheckedChanged
        If cbUnmask.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
            txtPasswordConf.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
            txtPasswordConf.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub frmProfile_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bCancelClose = True Then
            e.Cancel = True
        End If
        bCancelClose = False
    End Sub

    Private Sub frmProfile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cbUnmask.CheckState = CheckState.Unchecked
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Exit Sub
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Create_profile")
    End Sub
End Class