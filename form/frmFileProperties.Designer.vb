<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileProperties
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.lvArcList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.lblFileSize = New System.Windows.Forms.Label()
        Me.lblEncrypted = New System.Windows.Forms.Label()
        Me.lblMD5 = New System.Windows.Forms.Label()
        Me.lblArcNum = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSegments = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(226, 284)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "&OK"
        '
        'lvArcList
        '
        Me.lvArcList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvArcList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvArcList.Location = New System.Drawing.Point(8, 185)
        Me.lvArcList.MultiSelect = False
        Me.lvArcList.Name = "lvArcList"
        Me.lvArcList.Size = New System.Drawing.Size(287, 93)
        Me.lvArcList.TabIndex = 1
        Me.lvArcList.UseCompatibleStateImageBehavior = False
        Me.lvArcList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Archived Versions"
        Me.ColumnHeader1.Width = 280
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "File Size:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Archives:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Encrypted:"
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(8, 284)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(89, 22)
        Me.btnDownload.TabIndex = 6
        Me.btnDownload.Text = "Download &To..."
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 26)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "MD5 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checksum:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFileName
        '
        Me.lblFileName.AutoEllipsis = True
        Me.lblFileName.Location = New System.Drawing.Point(71, 9)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(222, 14)
        Me.lblFileName.TabIndex = 8
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblFileSize
        '
        Me.lblFileSize.Location = New System.Drawing.Point(71, 31)
        Me.lblFileSize.Name = "lblFileSize"
        Me.lblFileSize.Size = New System.Drawing.Size(222, 13)
        Me.lblFileSize.TabIndex = 9
        '
        'lblEncrypted
        '
        Me.lblEncrypted.Location = New System.Drawing.Point(73, 55)
        Me.lblEncrypted.Name = "lblEncrypted"
        Me.lblEncrypted.Size = New System.Drawing.Size(50, 13)
        Me.lblEncrypted.TabIndex = 10
        '
        'lblMD5
        '
        Me.lblMD5.Location = New System.Drawing.Point(71, 138)
        Me.lblMD5.Name = "lblMD5"
        Me.lblMD5.Size = New System.Drawing.Size(222, 13)
        Me.lblMD5.TabIndex = 11
        Me.lblMD5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblArcNum
        '
        Me.lblArcNum.Location = New System.Drawing.Point(74, 83)
        Me.lblArcNum.Name = "lblArcNum"
        Me.lblArcNum.Size = New System.Drawing.Size(93, 13)
        Me.lblArcNum.TabIndex = 12
        '
        'SaveFileDialog1
        '
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Segments:"
        '
        'lblSegments
        '
        Me.lblSegments.Location = New System.Drawing.Point(73, 166)
        Me.lblSegments.Name = "lblSegments"
        Me.lblSegments.Size = New System.Drawing.Size(85, 14)
        Me.lblSegments.TabIndex = 14
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(103, 284)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(83, 22)
        Me.btnDelete.TabIndex = 15
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(9, 309)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 16
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'frmFileProperties
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 331)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.lblSegments)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblArcNum)
        Me.Controls.Add(Me.lblMD5)
        Me.Controls.Add(Me.lblEncrypted)
        Me.Controls.Add(Me.lblFileSize)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnDownload)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lvArcList)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileProperties"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "File Properties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents lvArcList As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents lblFileSize As System.Windows.Forms.Label
    Friend WithEvents lblEncrypted As System.Windows.Forms.Label
    Friend WithEvents lblMD5 As System.Windows.Forms.Label
    Friend WithEvents lblArcNum As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblSegments As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel

End Class
