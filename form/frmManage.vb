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
'    Filename: frmManage.vb


Imports AirDrive.vars

Public Class frmManage
    Private bManageCancelClose As Boolean = False

    Private Sub frmManage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bManageCancelClose Then
            e.Cancel = True
            bManageCancelClose = False
        End If
    End Sub

    Private Sub frmManage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvAcnts.Items.Clear()
        lvAcnts.View = View.List

        For Each xObj As objAccount In colAccountList
            Dim lvi As New ListViewItem

            'apply the icon to the account object
            If xObj.IsFunctioning Then
                lvi.ImageIndex = 0
            Else
                lvi.ImageIndex = 1
            End If

            'create the name
            lvi.Text = xObj.EmailAddress

            'add list view item to list
            lvAcnts.Items.Add(lvi)
        Next
        GenerateHiveStats()
    End Sub

    Private Sub GenerateHiveStats()
        lblHiveFiles.Text = colRemoteFileData.Count

        'collect data
        Dim intcapacity As Long = 0
        Dim intused As Long = 0

        For Each xObj As objAccount In colAccountList
            intcapacity += xObj.BytesTotal
            intused += xObj.BytesUsed
        Next

        'display data
        lblTotalAccounts.Text = colAccountList.Count
        lblHiveCapacity.Text = ReturnNiceSize(2, , intcapacity)
        progHive.Maximum = intcapacity / 1024 / 1024
        progHive.Value = intused / 1024 / 1024
    End Sub

    Private Sub btnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Me.Close()
    End Sub


    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If frmMain.bgwQueueHandler.IsBusy Then
            MessageBox.Show("Can not remove accounts while transfers are active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If lvAcnts.Items.Count = 0 OrElse lvAcnts.SelectedItems.Count = 0 OrElse lvAcnts.SelectedItems.Count > 1 Then Exit Sub

        Dim ad As objAccount = colAccountList.Item(lvAcnts.SelectedItems(0).Text)
        Dim r As MsgBoxResult

        If ad.Items > 0 Then
            r = MessageBox.Show("There are file segments located on this IMAP account. Removing this account will result in segments of your data being unaccessable by " & strAppName & ". Are you sure you want to remove this account?" & vbNewLine & "(files will remain on the server)", lvAcnts.SelectedItems(0).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            bManageCancelClose = False
        Else
            r = MessageBox.Show(String.Format("Are you sure you want to delete {0}?", lvAcnts.SelectedItems(0).Text), lvAcnts.SelectedItems(0).Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        End If

        If r = MsgBoxResult.Yes Then
            lblAccountCapacity.Text = String.Empty
            lblAccountFiles.Text = String.Empty
            lblAcntQuota.Text = String.Empty
            lblQuotaLimit.Text = String.Empty
            progAccount.Value = 0
            colAccountList.Remove(ad.EmailAddress)
            StoreAcntDataToXML()

            For Each item As objSegment In colRemoteFileData
                If item.EmailAddress = ad.EmailAddress Then
                    'colRemoteFileData.Remove(item.EmailAddress & ":" & item.UID)
                    AddToCollection(colRemoteFileData, item)
                End If
            Next

            Call frmMain.RemoteRefresh()
            Call frmManage_Load(Me, New EventArgs)
        End If

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If colAccountList.Count >= nActLim Then
            MsgBox("Can not add any more accounts")
            Exit Sub
        Else
            frmActs.ShowDialog(Me)
            Call frmManage_Load(Me, New EventArgs)
        End If
    End Sub

    Private Sub lvAcnts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvAcnts.SelectedIndexChanged
        If lvAcnts.SelectedItems.Count = 0 Then Exit Sub
        If lvAcnts.SelectedItems.Count > 1 Then Exit Sub
        If lvAcnts.SelectedItems(0).Text = Nothing Then Exit Sub
        Dim oAcnt As objAccount = colAccountList(lvAcnts.SelectedItems(0).Text)
        lblQuotaLimit.Text = oAcnt.Quota & "%"
        lblAccountCapacity.Text = ReturnNiceSize(2, , oAcnt.BytesTotal)
        lblAccountFiles.Text = oAcnt.Items
        progAccount.Maximum = oAcnt.BytesTotal / 1024 / 1024
        progAccount.Value = oAcnt.BytesUsed / 1024 / 1024
        lblAcntQuota.Text = Math.Floor(oAcnt.BytesUsed / oAcnt.BytesTotal * 100) & "%"
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenWiki("Manage_accounts")
    End Sub
End Class