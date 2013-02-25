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
'    Filename: frmLogViewer.vb


Imports AirDrive.frmOptions
Imports AirDrive.func_rcrypt
Imports System.IO

Public Class frmLogViewer

    Public Sub New(ByVal c As enums.CryptType)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If File.Exists(sLogFileName) Then
            Dim arr As New ArrayList
            If c = enums.CryptType.Decrypt Then
                arr = LogCrypt(enums.CryptType.Decrypt)
                For Each line As String In arr
                    txtView.Text += line & vbNewLine
                Next
            Else
                Try
                    Dim sr As New StreamReader(sLogFileName)
                    Dim t As String = sr.ReadToEnd
                    txtView.Text = t
                Catch ex As Exception
                    MsgBox("An error occurred")
                    Me.Close()
                End Try
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub checkWrap_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkWrap.CheckedChanged
        If checkWrap.Checked Then
            txtView.WordWrap = True
        Else
            txtView.WordWrap = False
        End If
    End Sub

    Private Sub btnSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAs.Click
        SaveFileDialog1.Title = "Choose a location to save this file..."
        SaveFileDialog1.FileName = strProfileName & ".txt"
        SaveFileDialog1.OverwritePrompt = True
        SaveFileDialog1.ValidateNames = True
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop

        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim localpath As String = SaveFileDialog1.FileName
        'frmMain.sub_QueueDownload(objCurrent.FilePath, False, objCurrent.ArchiveNumber, localpath)

        Dim fw As New StreamWriter(localpath)
        Dim chars() As Char = System.Text.Encoding.ASCII.GetChars(System.Text.Encoding.ASCII.GetBytes(txtView.Text))
        fw.Write(chars, 0, chars.Length)
        fw.Close()

    End Sub

    Private Sub frmLogViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class