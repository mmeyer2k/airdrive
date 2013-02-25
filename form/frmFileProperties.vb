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
'    Filename: frmFileProperties.vb


Imports System.Windows.Forms

Public Class frmFileProperties

    Private c As New Collection
    Private objCurrent As objSegment

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        Me.Dispose()
    End Sub

    Public Sub New(ByVal path As String)
        InitializeComponent()
        Dim target As New objSegment
        For Each item As objSegment In colRemoteFileData
            If item.FilePath.ToLower = path.ToLower Then
                PopulateInfo(item)
                Exit Sub
            End If
        Next
    End Sub

    Protected Sub PopulateInfo(ByRef objSeg As objSegment)
        For Each item As objSegment In colRemoteFileData
            If item.FilePath = objSeg.FilePath AndAlso objSeg.Segment = 1 Then

                Dim lvi As New ListViewItem

                lvi.Text = String.Format("{0}.  {1}", item.ArchiveNumber, item.DateValue)

                lvi.Tag = item.ArchiveNumber

                If item.IsEncrypted Then
                    lvi.ForeColor = Color.Green
                End If

                lvArcList.Items.Add(lvi)

                If Not c.Contains(objSeg.ArchiveNumber.ToString) Then
                    c.Add(item, objSeg.ArchiveNumber.ToString)
                Else
                    c.Add(item)
                End If

            End If
        Next

        lblFileName.Text = objSeg.FileName
        lblArcNum.Text = c.Count


        lvArcList.Enabled = True
        lvArcList.Items(0).Selected = True
        lvArcList.Select()
    End Sub

    Private Sub lvArcList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvArcList.SelectedIndexChanged
        If lvArcList.SelectedItems.Count = 0 Then Exit Sub
        objCurrent = c.Item(lvArcList.SelectedItems(0).Tag)
        If objCurrent.IsEncrypted = True Then
            lblEncrypted.Text = "Yes"
        Else
            lblEncrypted.Text = "No"
        End If
        lblSegments.Text = objCurrent.TotalSegments
        lblMD5.Text = objCurrent.MD5Total
        lblFileSize.Text = ReturnNiceSize(0, , objCurrent.SizeTotal)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        SaveFileDialog1.Title = "Choose a location to save this file..."
        SaveFileDialog1.FileName = lblFileName.Text
        SaveFileDialog1.OverwritePrompt = True
        SaveFileDialog1.ValidateNames = True
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop

        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim localpath As String = SaveFileDialog1.FileName
        frmMain.sub_QueueDownload(objCurrent.FilePath, False, objCurrent.ArchiveNumber, localpath)
    End Sub

    Private Sub frmFileProperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ColumnHeader1.Width = 280
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If lvArcList.SelectedItems.Count > 1 OrElse lvArcList.SelectedItems.Count <= 0 Then
            Exit Sub
        End If

        Dim obj As ListViewItem = lvArcList.SelectedItems(0)

        Dim sDate As String = obj.Text
        sDate = sDate.Substring(sDate.LastIndexOf(chrPeriod) + 1)
        sDate = sDate.Trim

        For Each objSeg As objSegment In colRemoteFileData
            If sDate = objSeg.DateValue Then
                colPendingDelete.Add(objSeg)
                b_PendingDelete = True
                If Not frmMain.bgwQueueHandler.IsBusy Then frmMain.bgwQueueHandler.RunWorkerAsync()
                lvArcList.Items.Remove(obj)
                If lvArcList.Items.Count <= 0 Then
                    Me.Close()
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Remote_file_properties")
    End Sub
End Class
