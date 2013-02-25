Imports AirDrive.vars
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActs
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
        Me.pTitleBar = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.tabQuota = New System.Windows.Forms.TabPage()
        Me.txtPercent = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.tabFinish = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAddAgain = New System.Windows.Forms.Button()
        Me.lblSave = New System.Windows.Forms.Label()
        Me.lblConfig = New System.Windows.Forms.Label()
        Me.lblSpace = New System.Windows.Forms.Label()
        Me.lblVerify = New System.Windows.Forms.Label()
        Me.tabOther = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtOtherPassword = New System.Windows.Forms.TextBox()
        Me.txtOtherUsername = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.checkOtherSSL = New System.Windows.Forms.CheckBox()
        Me.txtOtherPort = New System.Windows.Forms.TextBox()
        Me.txtOtherHostname = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblOtherMaxDisp = New System.Windows.Forms.Label()
        Me.trackOtherAttachmentSize = New System.Windows.Forms.TrackBar()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtOtherEmail = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbOtherEmailSame = New System.Windows.Forms.CheckBox()
        Me.tabGMail = New System.Windows.Forms.TabPage()
        Me.txtGMailPassword = New System.Windows.Forms.TextBox()
        Me.txtGMailUsername = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tabActType = New System.Windows.Forms.TabPage()
        Me.rbGmail = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbOther = New System.Windows.Forms.RadioButton()
        Me.tcWizard = New System.Windows.Forms.TabControl()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.pTitleBar.SuspendLayout()
        Me.tabQuota.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabFinish.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tabOther.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.trackOtherAttachmentSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGMail.SuspendLayout()
        Me.tabActType.SuspendLayout()
        Me.tcWizard.SuspendLayout()
        Me.SuspendLayout()
        '
        'pTitleBar
        '
        Me.pTitleBar.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.pTitleBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pTitleBar.Controls.Add(Me.Label2)
        Me.pTitleBar.Controls.Add(Me.Label1)
        Me.pTitleBar.Location = New System.Drawing.Point(-3, -3)
        Me.pTitleBar.Name = "pTitleBar"
        Me.pTitleBar.Size = New System.Drawing.Size(641, 88)
        Me.pTitleBar.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(134, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(236, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Enter your email account details below"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(133, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(315, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Welcome to the account linking wizard"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(508, 331)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(97, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(352, 331)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(97, 24)
        Me.btnNext.TabIndex = 0
        Me.btnNext.Text = "&Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(244, 331)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(97, 24)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "&Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'tabQuota
        '
        Me.tabQuota.Controls.Add(Me.txtPercent)
        Me.tabQuota.Controls.Add(Me.Label10)
        Me.tabQuota.Controls.Add(Me.TrackBar1)
        Me.tabQuota.Location = New System.Drawing.Point(4, 25)
        Me.tabQuota.Name = "tabQuota"
        Me.tabQuota.Padding = New System.Windows.Forms.Padding(3)
        Me.tabQuota.Size = New System.Drawing.Size(632, 233)
        Me.tabQuota.TabIndex = 5
        Me.tabQuota.Text = "Quota"
        Me.tabQuota.UseVisualStyleBackColor = True
        '
        'txtPercent
        '
        Me.txtPercent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercent.Location = New System.Drawing.Point(6, 158)
        Me.txtPercent.Name = "txtPercent"
        Me.txtPercent.Size = New System.Drawing.Size(620, 41)
        Me.txtPercent.TabIndex = 2
        Me.txtPercent.Text = "70%"
        Me.txtPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(179, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(269, 32)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Select the amount of free space you want " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to utilize for filestorage on this ema" & _
            "il account."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(149, 110)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(326, 45)
        Me.TrackBar1.TabIndex = 0
        Me.TrackBar1.Value = 80
        '
        'tabFinish
        '
        Me.tabFinish.Controls.Add(Me.Label7)
        Me.tabFinish.Controls.Add(Me.GroupBox1)
        Me.tabFinish.Location = New System.Drawing.Point(4, 25)
        Me.tabFinish.Name = "tabFinish"
        Me.tabFinish.Size = New System.Drawing.Size(632, 233)
        Me.tabFinish.TabIndex = 4
        Me.tabFinish.Text = "Finish"
        Me.tabFinish.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(139, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(282, 16)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Please wait while account settings are verified."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddAgain)
        Me.GroupBox1.Controls.Add(Me.lblSave)
        Me.GroupBox1.Controls.Add(Me.lblConfig)
        Me.GroupBox1.Controls.Add(Me.lblSpace)
        Me.GroupBox1.Controls.Add(Me.lblVerify)
        Me.GroupBox1.Location = New System.Drawing.Point(142, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(442, 151)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnAddAgain
        '
        Me.btnAddAgain.Location = New System.Drawing.Point(261, 109)
        Me.btnAddAgain.Name = "btnAddAgain"
        Me.btnAddAgain.Size = New System.Drawing.Size(161, 25)
        Me.btnAddAgain.TabIndex = 2
        Me.btnAddAgain.Text = "Add another account"
        Me.btnAddAgain.UseVisualStyleBackColor = True
        '
        'lblSave
        '
        Me.lblSave.AutoSize = True
        Me.lblSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSave.Location = New System.Drawing.Point(44, 109)
        Me.lblSave.Name = "lblSave"
        Me.lblSave.Size = New System.Drawing.Size(139, 16)
        Me.lblSave.TabIndex = 3
        Me.lblSave.Text = "Save account settings"
        '
        'lblConfig
        '
        Me.lblConfig.AutoSize = True
        Me.lblConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfig.Location = New System.Drawing.Point(43, 54)
        Me.lblConfig.Name = "lblConfig"
        Me.lblConfig.Size = New System.Drawing.Size(145, 16)
        Me.lblConfig.TabIndex = 2
        Me.lblConfig.Text = "Configure storage area"
        '
        'lblSpace
        '
        Me.lblSpace.AutoSize = True
        Me.lblSpace.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpace.Location = New System.Drawing.Point(43, 81)
        Me.lblSpace.Name = "lblSpace"
        Me.lblSpace.Size = New System.Drawing.Size(113, 16)
        Me.lblSpace.TabIndex = 1
        Me.lblSpace.Text = "Check free space"
        '
        'lblVerify
        '
        Me.lblVerify.AutoSize = True
        Me.lblVerify.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVerify.Location = New System.Drawing.Point(43, 25)
        Me.lblVerify.Name = "lblVerify"
        Me.lblVerify.Size = New System.Drawing.Size(165, 16)
        Me.lblVerify.TabIndex = 0
        Me.lblVerify.Text = "Verify connection to server"
        '
        'tabOther
        '
        Me.tabOther.Controls.Add(Me.GroupBox3)
        Me.tabOther.Controls.Add(Me.GroupBox2)
        Me.tabOther.Controls.Add(Me.GroupBox4)
        Me.tabOther.Location = New System.Drawing.Point(4, 25)
        Me.tabOther.Name = "tabOther"
        Me.tabOther.Size = New System.Drawing.Size(632, 233)
        Me.tabOther.TabIndex = 2
        Me.tabOther.Text = "Other"
        Me.tabOther.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtOtherPassword)
        Me.GroupBox3.Controls.Add(Me.txtOtherUsername)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(22, 97)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(391, 121)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Authentication"
        '
        'txtOtherPassword
        '
        Me.txtOtherPassword.Location = New System.Drawing.Point(204, 34)
        Me.txtOtherPassword.MaxLength = 64
        Me.txtOtherPassword.Name = "txtOtherPassword"
        Me.txtOtherPassword.Size = New System.Drawing.Size(161, 20)
        Me.txtOtherPassword.TabIndex = 7
        '
        'txtOtherUsername
        '
        Me.txtOtherUsername.Location = New System.Drawing.Point(14, 34)
        Me.txtOtherUsername.MaxLength = 64
        Me.txtOtherUsername.Name = "txtOtherUsername"
        Me.txtOtherUsername.Size = New System.Drawing.Size(160, 20)
        Me.txtOtherUsername.TabIndex = 6
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(201, 18)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Password:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Username"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.checkOtherSSL)
        Me.GroupBox2.Controls.Add(Me.txtOtherPort)
        Me.GroupBox2.Controls.Add(Me.txtOtherHostname)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 11)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(395, 75)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Connection"
        '
        'checkOtherSSL
        '
        Me.checkOtherSSL.AutoSize = True
        Me.checkOtherSSL.Location = New System.Drawing.Point(292, 34)
        Me.checkOtherSSL.Name = "checkOtherSSL"
        Me.checkOtherSSL.Size = New System.Drawing.Size(88, 17)
        Me.checkOtherSSL.TabIndex = 18
        Me.checkOtherSSL.Text = "SSL Enabled"
        Me.checkOtherSSL.UseVisualStyleBackColor = True
        '
        'txtOtherPort
        '
        Me.txtOtherPort.Location = New System.Drawing.Point(208, 32)
        Me.txtOtherPort.MaxLength = 6
        Me.txtOtherPort.Name = "txtOtherPort"
        Me.txtOtherPort.Size = New System.Drawing.Size(71, 20)
        Me.txtOtherPort.TabIndex = 17
        '
        'txtOtherHostname
        '
        Me.txtOtherHostname.Location = New System.Drawing.Point(18, 32)
        Me.txtOtherHostname.MaxLength = 64
        Me.txtOtherHostname.Name = "txtOtherHostname"
        Me.txtOtherHostname.Size = New System.Drawing.Size(160, 20)
        Me.txtOtherHostname.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Hostname"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(205, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Port:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblOtherMaxDisp)
        Me.GroupBox4.Controls.Add(Me.trackOtherAttachmentSize)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtOtherEmail)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.cbOtherEmailSame)
        Me.GroupBox4.Location = New System.Drawing.Point(422, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(194, 205)
        Me.GroupBox4.TabIndex = 21
        Me.GroupBox4.TabStop = False
        '
        'lblOtherMaxDisp
        '
        Me.lblOtherMaxDisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherMaxDisp.Location = New System.Drawing.Point(7, 169)
        Me.lblOtherMaxDisp.Name = "lblOtherMaxDisp"
        Me.lblOtherMaxDisp.Size = New System.Drawing.Size(181, 16)
        Me.lblOtherMaxDisp.TabIndex = 19
        Me.lblOtherMaxDisp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'trackOtherAttachmentSize
        '
        Me.trackOtherAttachmentSize.Location = New System.Drawing.Point(10, 121)
        Me.trackOtherAttachmentSize.Maximum = 100
        Me.trackOtherAttachmentSize.Name = "trackOtherAttachmentSize"
        Me.trackOtherAttachmentSize.Size = New System.Drawing.Size(178, 45)
        Me.trackOtherAttachmentSize.TabIndex = 18
        Me.trackOtherAttachmentSize.TickFrequency = 2
        Me.trackOtherAttachmentSize.Value = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(131, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Maximum attachment size:"
        '
        'txtOtherEmail
        '
        Me.txtOtherEmail.Location = New System.Drawing.Point(9, 33)
        Me.txtOtherEmail.MaxLength = 128
        Me.txtOtherEmail.Name = "txtOtherEmail"
        Me.txtOtherEmail.Size = New System.Drawing.Size(173, 20)
        Me.txtOtherEmail.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Email Address:"
        '
        'cbOtherEmailSame
        '
        Me.cbOtherEmailSame.AutoSize = True
        Me.cbOtherEmailSame.Location = New System.Drawing.Point(9, 59)
        Me.cbOtherEmailSame.Name = "cbOtherEmailSame"
        Me.cbOtherEmailSame.Size = New System.Drawing.Size(180, 17)
        Me.cbOtherEmailSame.TabIndex = 16
        Me.cbOtherEmailSame.Text = "Address is username@hostname"
        Me.cbOtherEmailSame.UseVisualStyleBackColor = True
        '
        'tabGMail
        '
        Me.tabGMail.Controls.Add(Me.txtGMailPassword)
        Me.tabGMail.Controls.Add(Me.txtGMailUsername)
        Me.tabGMail.Controls.Add(Me.Label5)
        Me.tabGMail.Controls.Add(Me.Label4)
        Me.tabGMail.Location = New System.Drawing.Point(4, 25)
        Me.tabGMail.Name = "tabGMail"
        Me.tabGMail.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGMail.Size = New System.Drawing.Size(632, 233)
        Me.tabGMail.TabIndex = 1
        Me.tabGMail.Text = "GMail"
        Me.tabGMail.UseVisualStyleBackColor = True
        '
        'txtGMailPassword
        '
        Me.txtGMailPassword.Location = New System.Drawing.Point(131, 129)
        Me.txtGMailPassword.Name = "txtGMailPassword"
        Me.txtGMailPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtGMailPassword.Size = New System.Drawing.Size(227, 20)
        Me.txtGMailPassword.TabIndex = 3
        '
        'txtGMailUsername
        '
        Me.txtGMailUsername.Location = New System.Drawing.Point(132, 71)
        Me.txtGMailUsername.Name = "txtGMailUsername"
        Me.txtGMailUsername.Size = New System.Drawing.Size(227, 20)
        Me.txtGMailUsername.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(128, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(128, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Address@gmail.com"
        '
        'tabActType
        '
        Me.tabActType.Controls.Add(Me.rbGmail)
        Me.tabActType.Controls.Add(Me.Label3)
        Me.tabActType.Controls.Add(Me.rbOther)
        Me.tabActType.Location = New System.Drawing.Point(4, 25)
        Me.tabActType.Name = "tabActType"
        Me.tabActType.Padding = New System.Windows.Forms.Padding(3)
        Me.tabActType.Size = New System.Drawing.Size(632, 233)
        Me.tabActType.TabIndex = 0
        Me.tabActType.Text = "tabActType"
        Me.tabActType.UseVisualStyleBackColor = True
        '
        'rbGmail
        '
        Me.rbGmail.AutoSize = True
        Me.rbGmail.Checked = True
        Me.rbGmail.Location = New System.Drawing.Point(168, 81)
        Me.rbGmail.Name = "rbGmail"
        Me.rbGmail.Size = New System.Drawing.Size(288, 17)
        Me.rbGmail.TabIndex = 5
        Me.rbGmail.TabStop = True
        Me.rbGmail.Text = "GMail (includes GoogleMail and  GoogleApps accounts)"
        Me.rbGmail.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(131, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select account type:"
        '
        'rbOther
        '
        Me.rbOther.AutoSize = True
        Me.rbOther.Location = New System.Drawing.Point(168, 104)
        Me.rbOther.Name = "rbOther"
        Me.rbOther.Size = New System.Drawing.Size(51, 17)
        Me.rbOther.TabIndex = 6
        Me.rbOther.Text = "Other"
        Me.rbOther.UseVisualStyleBackColor = True
        '
        'tcWizard
        '
        Me.tcWizard.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcWizard.Controls.Add(Me.tabActType)
        Me.tcWizard.Controls.Add(Me.tabGMail)
        Me.tcWizard.Controls.Add(Me.tabOther)
        Me.tcWizard.Controls.Add(Me.tabFinish)
        Me.tcWizard.Controls.Add(Me.tabQuota)
        Me.tcWizard.Location = New System.Drawing.Point(-3, 55)
        Me.tcWizard.Margin = New System.Windows.Forms.Padding(0)
        Me.tcWizard.Name = "tcWizard"
        Me.tcWizard.SelectedIndex = 0
        Me.tcWizard.Size = New System.Drawing.Size(640, 262)
        Me.tcWizard.TabIndex = 2
        Me.tcWizard.TabStop = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(12, 351)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 4
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'frmActs
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(629, 373)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.tcWizard)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pTitleBar)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmActs"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Account Wizard"
        Me.pTitleBar.ResumeLayout(False)
        Me.pTitleBar.PerformLayout()
        Me.tabQuota.ResumeLayout(False)
        Me.tabQuota.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabFinish.ResumeLayout(False)
        Me.tabFinish.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tabOther.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.trackOtherAttachmentSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGMail.ResumeLayout(False)
        Me.tabGMail.PerformLayout()
        Me.tabActType.ResumeLayout(False)
        Me.tabActType.PerformLayout()
        Me.tcWizard.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pTitleBar As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tabQuota As System.Windows.Forms.TabPage
    Friend WithEvents txtPercent As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents tabFinish As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddAgain As System.Windows.Forms.Button
    Friend WithEvents lblSave As System.Windows.Forms.Label
    Friend WithEvents lblConfig As System.Windows.Forms.Label
    Friend WithEvents lblSpace As System.Windows.Forms.Label
    Friend WithEvents lblVerify As System.Windows.Forms.Label
    Friend WithEvents tabOther As System.Windows.Forms.TabPage
    Friend WithEvents txtOtherEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtOtherHostname As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtOtherPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtOtherUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tabGMail As System.Windows.Forms.TabPage
    Friend WithEvents txtGMailPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtGMailUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tabActType As System.Windows.Forms.TabPage
    Friend WithEvents rbGmail As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rbOther As System.Windows.Forms.RadioButton
    Friend WithEvents tcWizard As System.Windows.Forms.TabControl
    Friend WithEvents cbOtherEmailSame As System.Windows.Forms.CheckBox
    Friend WithEvents txtOtherPort As System.Windows.Forms.TextBox
    Friend WithEvents checkOtherSSL As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents trackOtherAttachmentSize As System.Windows.Forms.TrackBar
    Friend WithEvents lblOtherMaxDisp As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
