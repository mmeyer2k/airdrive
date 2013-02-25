<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.checkLoadCache = New System.Windows.Forms.CheckBox()
        Me.checkUpdates = New System.Windows.Forms.CheckBox()
        Me.checkDisplayConsole = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.checkEncryptLog = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblLogLocation = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.checkLogging = New System.Windows.Forms.CheckBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.chkAutoLogin = New System.Windows.Forms.CheckBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 274)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "&OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "&Cancel"
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(2, 7)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(431, 258)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LinkLabel2)
        Me.TabPage1.Controls.Add(Me.chkAutoLogin)
        Me.TabPage1.Controls.Add(Me.checkLoadCache)
        Me.TabPage1.Controls.Add(Me.checkUpdates)
        Me.TabPage1.Controls.Add(Me.checkDisplayConsole)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(423, 229)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Interface"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'checkLoadCache
        '
        Me.checkLoadCache.AutoSize = True
        Me.checkLoadCache.Location = New System.Drawing.Point(19, 62)
        Me.checkLoadCache.Name = "checkLoadCache"
        Me.checkLoadCache.Size = New System.Drawing.Size(211, 17)
        Me.checkLoadCache.TabIndex = 3
        Me.checkLoadCache.Text = "&Always load cached file data on startup"
        Me.checkLoadCache.UseVisualStyleBackColor = True
        '
        'checkUpdates
        '
        Me.checkUpdates.AutoSize = True
        Me.checkUpdates.Location = New System.Drawing.Point(19, 39)
        Me.checkUpdates.Name = "checkUpdates"
        Me.checkUpdates.Size = New System.Drawing.Size(217, 17)
        Me.checkUpdates.TabIndex = 2
        Me.checkUpdates.Text = "&Check for updates on application startup"
        Me.checkUpdates.UseVisualStyleBackColor = True
        '
        'checkDisplayConsole
        '
        Me.checkDisplayConsole.AutoSize = True
        Me.checkDisplayConsole.Location = New System.Drawing.Point(19, 16)
        Me.checkDisplayConsole.Name = "checkDisplayConsole"
        Me.checkDisplayConsole.Size = New System.Drawing.Size(150, 17)
        Me.checkDisplayConsole.TabIndex = 1
        Me.checkDisplayConsole.Text = "&Display console on startup"
        Me.checkDisplayConsole.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.checkEncryptLog)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.checkLogging)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(423, 229)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Logging"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'checkEncryptLog
        '
        Me.checkEncryptLog.AutoSize = True
        Me.checkEncryptLog.Enabled = False
        Me.checkEncryptLog.Location = New System.Drawing.Point(153, 74)
        Me.checkEncryptLog.Name = "checkEncryptLog"
        Me.checkEncryptLog.Size = New System.Drawing.Size(95, 17)
        Me.checkEncryptLog.TabIndex = 5
        Me.checkEncryptLog.Text = "Encrypt log file"
        Me.checkEncryptLog.UseVisualStyleBackColor = True
        Me.checkEncryptLog.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblLogLocation)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 139)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 55)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Location of log file:"
        '
        'lblLogLocation
        '
        Me.lblLogLocation.Location = New System.Drawing.Point(8, 16)
        Me.lblLogLocation.Name = "lblLogLocation"
        Me.lblLogLocation.Size = New System.Drawing.Size(394, 36)
        Me.lblLogLocation.TabIndex = 3
        Me.lblLogLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(219, 111)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 22)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "&Clear Log"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(153, 111)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 22)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "&View Log"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'checkLogging
        '
        Me.checkLogging.AutoSize = True
        Me.checkLogging.Location = New System.Drawing.Point(153, 55)
        Me.checkLogging.Name = "checkLogging"
        Me.checkLogging.Size = New System.Drawing.Size(96, 17)
        Me.checkLogging.TabIndex = 0
        Me.checkLogging.Text = "Enable logging"
        Me.checkLogging.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(12, 293)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'chkAutoLogin
        '
        Me.chkAutoLogin.AutoSize = True
        Me.chkAutoLogin.Location = New System.Drawing.Point(19, 85)
        Me.chkAutoLogin.Name = "chkAutoLogin"
        Me.chkAutoLogin.Size = New System.Drawing.Size(223, 17)
        Me.chkAutoLogin.TabIndex = 4
        Me.chkAutoLogin.Text = "Log in to this account profile automatically"
        Me.chkAutoLogin.UseVisualStyleBackColor = True
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(248, 86)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(103, 13)
        Me.LinkLabel2.TabIndex = 5
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Security Implications"
        '
        'frmOptions
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents checkLogging As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLogLocation As System.Windows.Forms.Label
    Friend WithEvents checkDisplayConsole As System.Windows.Forms.CheckBox
    Friend WithEvents checkUpdates As System.Windows.Forms.CheckBox
    Friend WithEvents checkLoadCache As System.Windows.Forms.CheckBox
    Friend WithEvents checkEncryptLog As System.Windows.Forms.CheckBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents chkAutoLogin As System.Windows.Forms.CheckBox
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel

End Class
