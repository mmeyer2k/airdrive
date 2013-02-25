<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDupDiag
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
        Me.rbReplace = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbRename = New System.Windows.Forms.RadioButton()
        Me.rbSkip = New System.Windows.Forms.RadioButton()
        Me.rbArchive = New System.Windows.Forms.RadioButton()
        Me.cbAlways = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblRemoteSize = New System.Windows.Forms.Label()
        Me.lblRemoteMD5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblRemoteFile = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblLocalMD5 = New System.Windows.Forms.Label()
        Me.lblLocalSize = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblLocalFile = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbDifferent = New System.Windows.Forms.CheckBox()
        Me.rbQueue = New System.Windows.Forms.RadioButton()
        Me.rbSession = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbReplace
        '
        Me.rbReplace.AutoSize = True
        Me.rbReplace.Checked = True
        Me.rbReplace.Location = New System.Drawing.Point(26, 19)
        Me.rbReplace.Name = "rbReplace"
        Me.rbReplace.Size = New System.Drawing.Size(65, 17)
        Me.rbReplace.TabIndex = 0
        Me.rbReplace.TabStop = True
        Me.rbReplace.Text = "Replace"
        Me.rbReplace.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbRename)
        Me.GroupBox1.Controls.Add(Me.rbSkip)
        Me.GroupBox1.Controls.Add(Me.rbArchive)
        Me.GroupBox1.Controls.Add(Me.rbReplace)
        Me.GroupBox1.Location = New System.Drawing.Point(328, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(185, 112)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Action"
        '
        'rbRename
        '
        Me.rbRename.AutoSize = True
        Me.rbRename.Location = New System.Drawing.Point(26, 86)
        Me.rbRename.Name = "rbRename"
        Me.rbRename.Size = New System.Drawing.Size(65, 17)
        Me.rbRename.TabIndex = 6
        Me.rbRename.TabStop = True
        Me.rbRename.Text = "Rename"
        Me.rbRename.UseVisualStyleBackColor = True
        '
        'rbSkip
        '
        Me.rbSkip.AutoSize = True
        Me.rbSkip.Location = New System.Drawing.Point(26, 63)
        Me.rbSkip.Name = "rbSkip"
        Me.rbSkip.Size = New System.Drawing.Size(46, 17)
        Me.rbSkip.TabIndex = 5
        Me.rbSkip.Text = "Skip"
        Me.rbSkip.UseVisualStyleBackColor = True
        '
        'rbArchive
        '
        Me.rbArchive.AutoSize = True
        Me.rbArchive.Location = New System.Drawing.Point(26, 40)
        Me.rbArchive.Name = "rbArchive"
        Me.rbArchive.Size = New System.Drawing.Size(61, 17)
        Me.rbArchive.TabIndex = 1
        Me.rbArchive.Text = "Archive"
        Me.rbArchive.UseVisualStyleBackColor = True
        '
        'cbAlways
        '
        Me.cbAlways.AutoSize = True
        Me.cbAlways.Location = New System.Drawing.Point(20, 19)
        Me.cbAlways.Name = "cbAlways"
        Me.cbAlways.Size = New System.Drawing.Size(148, 17)
        Me.cbAlways.TabIndex = 2
        Me.cbAlways.Text = "Always perform this action"
        Me.cbAlways.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(274, 253)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(163, 252)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(87, 28)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblRemoteSize)
        Me.GroupBox2.Controls.Add(Me.lblRemoteMD5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblRemoteFile)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(310, 114)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Existing Remote File"
        '
        'lblRemoteSize
        '
        Me.lblRemoteSize.Location = New System.Drawing.Point(62, 47)
        Me.lblRemoteSize.Name = "lblRemoteSize"
        Me.lblRemoteSize.Size = New System.Drawing.Size(227, 12)
        Me.lblRemoteSize.TabIndex = 8
        '
        'lblRemoteMD5
        '
        Me.lblRemoteMD5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRemoteMD5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemoteMD5.Location = New System.Drawing.Point(62, 73)
        Me.lblRemoteMD5.Name = "lblRemoteMD5"
        Me.lblRemoteMD5.Size = New System.Drawing.Size(242, 11)
        Me.lblRemoteMD5.TabIndex = 7
        Me.lblRemoteMD5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "MD5:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Size:"
        '
        'lblRemoteFile
        '
        Me.lblRemoteFile.AutoEllipsis = True
        Me.lblRemoteFile.Location = New System.Drawing.Point(64, 23)
        Me.lblRemoteFile.Name = "lblRemoteFile"
        Me.lblRemoteFile.Size = New System.Drawing.Size(225, 13)
        Me.lblRemoteFile.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Filename:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblLocalMD5)
        Me.GroupBox3.Controls.Add(Me.lblLocalSize)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.lblLocalFile)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 132)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(310, 114)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Pending Local File"
        '
        'lblLocalMD5
        '
        Me.lblLocalMD5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocalMD5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalMD5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocalMD5.Location = New System.Drawing.Point(62, 71)
        Me.lblLocalMD5.Name = "lblLocalMD5"
        Me.lblLocalMD5.Size = New System.Drawing.Size(242, 11)
        Me.lblLocalMD5.TabIndex = 6
        Me.lblLocalMD5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLocalSize
        '
        Me.lblLocalSize.Location = New System.Drawing.Point(62, 48)
        Me.lblLocalSize.Name = "lblLocalSize"
        Me.lblLocalSize.Size = New System.Drawing.Size(227, 12)
        Me.lblLocalSize.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "MD5:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Size:"
        '
        'lblLocalFile
        '
        Me.lblLocalFile.AutoEllipsis = True
        Me.lblLocalFile.Location = New System.Drawing.Point(64, 25)
        Me.lblLocalFile.Name = "lblLocalFile"
        Me.lblLocalFile.Size = New System.Drawing.Size(225, 13)
        Me.lblLocalFile.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Filename:"
        '
        'cbDifferent
        '
        Me.cbDifferent.AutoSize = True
        Me.cbDifferent.Enabled = False
        Me.cbDifferent.Location = New System.Drawing.Point(43, 88)
        Me.cbDifferent.Name = "cbDifferent"
        Me.cbDifferent.Size = New System.Drawing.Size(122, 17)
        Me.cbDifferent.TabIndex = 6
        Me.cbDifferent.Text = "Only if file is different"
        Me.cbDifferent.UseVisualStyleBackColor = True
        Me.cbDifferent.Visible = False
        '
        'rbQueue
        '
        Me.rbQueue.AutoSize = True
        Me.rbQueue.Checked = True
        Me.rbQueue.Location = New System.Drawing.Point(43, 42)
        Me.rbQueue.Name = "rbQueue"
        Me.rbQueue.Size = New System.Drawing.Size(98, 17)
        Me.rbQueue.TabIndex = 7
        Me.rbQueue.TabStop = True
        Me.rbQueue.Text = "Only this queue"
        Me.rbQueue.UseVisualStyleBackColor = True
        '
        'rbSession
        '
        Me.rbSession.AutoSize = True
        Me.rbSession.Location = New System.Drawing.Point(43, 65)
        Me.rbSession.Name = "rbSession"
        Me.rbSession.Size = New System.Drawing.Size(103, 17)
        Me.rbSession.TabIndex = 8
        Me.rbSession.Text = "Only this session"
        Me.rbSession.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbSession)
        Me.GroupBox4.Controls.Add(Me.cbAlways)
        Me.GroupBox4.Controls.Add(Me.rbQueue)
        Me.GroupBox4.Controls.Add(Me.cbDifferent)
        Me.GroupBox4.Location = New System.Drawing.Point(328, 132)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(185, 114)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Repeat action"
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(13, 268)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 8
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'frmDupDiag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(525, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "frmDupDiag"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "File already exists"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbReplace As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbArchive As System.Windows.Forms.RadioButton
    Friend WithEvents cbAlways As System.Windows.Forms.CheckBox
    Friend WithEvents rbSkip As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbDifferent As System.Windows.Forms.CheckBox
    Friend WithEvents rbSession As System.Windows.Forms.RadioButton
    Friend WithEvents rbQueue As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRemoteFile As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLocalFile As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblLocalSize As System.Windows.Forms.Label
    Friend WithEvents lblRemoteSize As System.Windows.Forms.Label
    Friend WithEvents lblRemoteMD5 As System.Windows.Forms.Label
    Friend WithEvents lblLocalMD5 As System.Windows.Forms.Label
    Friend WithEvents rbRename As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
