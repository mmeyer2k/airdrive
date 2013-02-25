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
'    Filename: frmLocDup.vb


Public Class frmLocDup

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'action codes: (windows.forms.dialogresult))
        'YES - Rename
        'OK - Overwrite
        'ABORT - Skip


        If rbSkip.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.Abort
        ElseIf rbOverWrite.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf rbRename.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        End If
    End Sub

    Private Sub frmLocDup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Local duplicate")
    End Sub
End Class