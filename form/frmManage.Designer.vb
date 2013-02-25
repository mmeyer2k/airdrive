<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManage
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManage))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.progAccount = New System.Windows.Forms.ProgressBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCapacity = New System.Windows.Forms.Label()
        Me.btnDone = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTotalAccounts = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblHiveCapacity = New System.Windows.Forms.Label()
        Me.lblHiveFiles = New System.Windows.Forms.Label()
        Me.progHive = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblQuotaLimit = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAcntQuota = New System.Windows.Forms.Label()
        Me.lblAccountCapacity = New System.Windows.Forms.Label()
        Me.lblAccountFiles = New System.Windows.Forms.Label()
        Me.lvAcnts = New System.Windows.Forms.ListView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "success.png")
        Me.ImageList1.Images.SetKeyName(1, "error.png")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Objects:"
        '
        'progAccount
        '
        Me.progAccount.Location = New System.Drawing.Point(92, 107)
        Me.progAccount.Name = "progAccount"
        Me.progAccount.Size = New System.Drawing.Size(101, 10)
        Me.progAccount.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.progAccount.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Capacity:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Used:"
        '
        'lblCapacity
        '
        Me.lblCapacity.AutoSize = True
        Me.lblCapacity.Location = New System.Drawing.Point(89, 49)
        Me.lblCapacity.Name = "lblCapacity"
        Me.lblCapacity.Size = New System.Drawing.Size(0, 13)
        Me.lblCapacity.TabIndex = 8
        '
        'btnDone
        '
        Me.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnDone.Location = New System.Drawing.Point(199, 225)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(57, 24)
        Me.btnDone.TabIndex = 10
        Me.btnDone.Text = "&Done"
        Me.btnDone.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotalAccounts)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.lblHiveCapacity)
        Me.GroupBox2.Controls.Add(Me.lblHiveFiles)
        Me.GroupBox2.Controls.Add(Me.progHive)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 22)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(533, 76)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Hive Totals:"
        '
        'lblTotalAccounts
        '
        Me.lblTotalAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAccounts.Location = New System.Drawing.Point(80, 20)
        Me.lblTotalAccounts.Name = "lblTotalAccounts"
        Me.lblTotalAccounts.Size = New System.Drawing.Size(148, 16)
        Me.lblTotalAccounts.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Accounts:"
        '
        'lblHiveCapacity
        '
        Me.lblHiveCapacity.AutoEllipsis = True
        Me.lblHiveCapacity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHiveCapacity.Location = New System.Drawing.Point(80, 47)
        Me.lblHiveCapacity.Name = "lblHiveCapacity"
        Me.lblHiveCapacity.Size = New System.Drawing.Size(148, 16)
        Me.lblHiveCapacity.TabIndex = 9
        '
        'lblHiveFiles
        '
        Me.lblHiveFiles.AutoEllipsis = True
        Me.lblHiveFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHiveFiles.Location = New System.Drawing.Point(313, 20)
        Me.lblHiveFiles.Name = "lblHiveFiles"
        Me.lblHiveFiles.Size = New System.Drawing.Size(104, 16)
        Me.lblHiveFiles.TabIndex = 8
        '
        'progHive
        '
        Me.progHive.Location = New System.Drawing.Point(316, 43)
        Me.progHive.Name = "progHive"
        Me.progHive.Size = New System.Drawing.Size(101, 20)
        Me.progHive.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.progHive.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(272, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Used:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Capacity:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(234, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Total Objects:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblQuotaLimit)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblAcntQuota)
        Me.GroupBox3.Controls.Add(Me.lblAccountCapacity)
        Me.GroupBox3.Controls.Add(Me.lblAccountFiles)
        Me.GroupBox3.Controls.Add(Me.lblCapacity)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.progAccount)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(285, 105)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(271, 144)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Account:"
        '
        'lblQuotaLimit
        '
        Me.lblQuotaLimit.Location = New System.Drawing.Point(89, 75)
        Me.lblQuotaLimit.Name = "lblQuotaLimit"
        Me.lblQuotaLimit.Size = New System.Drawing.Size(161, 13)
        Me.lblQuotaLimit.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Quota:"
        '
        'lblAcntQuota
        '
        Me.lblAcntQuota.Location = New System.Drawing.Point(199, 104)
        Me.lblAcntQuota.Name = "lblAcntQuota"
        Me.lblAcntQuota.Size = New System.Drawing.Size(47, 13)
        Me.lblAcntQuota.TabIndex = 11
        '
        'lblAccountCapacity
        '
        Me.lblAccountCapacity.AutoEllipsis = True
        Me.lblAccountCapacity.Location = New System.Drawing.Point(89, 49)
        Me.lblAccountCapacity.Name = "lblAccountCapacity"
        Me.lblAccountCapacity.Size = New System.Drawing.Size(133, 16)
        Me.lblAccountCapacity.TabIndex = 10
        '
        'lblAccountFiles
        '
        Me.lblAccountFiles.AutoEllipsis = True
        Me.lblAccountFiles.Location = New System.Drawing.Point(89, 26)
        Me.lblAccountFiles.Name = "lblAccountFiles"
        Me.lblAccountFiles.Size = New System.Drawing.Size(133, 16)
        Me.lblAccountFiles.TabIndex = 9
        '
        'lvAcnts
        '
        Me.lvAcnts.Location = New System.Drawing.Point(23, 121)
        Me.lvAcnts.Name = "lvAcnts"
        Me.lvAcnts.Size = New System.Drawing.Size(233, 98)
        Me.lvAcnts.SmallImageList = Me.ImageList1
        Me.lvAcnts.TabIndex = 13
        Me.lvAcnts.UseCompatibleStateImageBehavior = False
        Me.lvAcnts.View = System.Windows.Forms.View.List
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(20, 105)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Select account:"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(23, 225)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(48, 24)
        Me.btnAdd.TabIndex = 15
        Me.btnAdd.Text = "&Add"
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.Location = New System.Drawing.Point(77, 225)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(57, 24)
        Me.btnRemove.TabIndex = 16
        Me.btnRemove.Text = "&Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnSettings
        '
        Me.btnSettings.Enabled = False
        Me.btnSettings.Location = New System.Drawing.Point(140, 225)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(53, 24)
        Me.btnSettings.TabIndex = 17
        Me.btnSettings.Text = "&Settings"
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(527, 252)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 18
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'frmManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnDone
        Me.ClientSize = New System.Drawing.Size(587, 274)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lvAcnts)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnDone)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManage"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Accounts"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents progAccount As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCapacity As System.Windows.Forms.Label
    Friend WithEvents btnDone As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents progHive As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHiveCapacity As System.Windows.Forms.Label
    Friend WithEvents lblHiveFiles As System.Windows.Forms.Label
    Friend WithEvents lblAccountCapacity As System.Windows.Forms.Label
    Friend WithEvents lblAccountFiles As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalAccounts As System.Windows.Forms.Label
    Friend WithEvents lvAcnts As System.Windows.Forms.ListView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblAcntQuota As System.Windows.Forms.Label
    Friend WithEvents btnSettings As System.Windows.Forms.Button
    Friend WithEvents lblQuotaLimit As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
