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
'    Filename: frmDupDiag.vb


Imports AirDrive.vars

Public Class frmDupDiag

    Private Sub cbalways_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAlways.CheckedChanged
        If cbAlways.Checked Then
            cbDifferent.Enabled = True
            rbSession.Enabled = True
            rbQueue.Enabled = True
        Else
            cbDifferent.Enabled = False
            rbSession.Enabled = False
            rbQueue.Enabled = False
        End If
    End Sub

    Private Sub frmDupDiag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'bug fix keeps blank dup pages from appearing
        If lblRemoteFile.Text = Nothing Then
            Me.Hide()
            Me.Close()
        End If

        'set option checks and radio buttons
        cbAlways.Checked = True
        cbAlways.Checked = False

        lblRemoteMD5.AutoSize = True
        lblLocalMD5.AutoSize = True

        If lblRemoteMD5.Text = lblLocalMD5.Text Then
            lblLocalMD5.BackColor = Color.Green
            lblRemoteMD5.BackColor = Color.Green
        Else
            lblLocalMD5.BackColor = Color.Red
            lblRemoteMD5.BackColor = Color.Red
        End If

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.rbReplace.Checked Then
            ePreviousAction = enums.RemoteFileExistsResponse.Overwrite
        ElseIf Me.rbSkip.Checked Then
            ePreviousAction = enums.RemoteFileExistsResponse.Skip
        ElseIf Me.rbRename.Checked Then
            ePreviousAction = enums.RemoteFileExistsResponse.Rename
        ElseIf Me.rbArchive.Checked Then
            ePreviousAction = enums.RemoteFileExistsResponse.Archive
        End If

        'add variables to the global
        bDupResAlways = cbAlways.Checked
        bDupResOnlyDiff = cbDifferent.Checked
        bDupResQueue = If(rbQueue.Checked, True, False)

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Duplicate_notification")
    End Sub
End Class