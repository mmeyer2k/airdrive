<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocDup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLocDup))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbSkip = New System.Windows.Forms.RadioButton()
        Me.rbRename = New System.Windows.Forms.RadioButton()
        Me.rbOverWrite = New System.Windows.Forms.RadioButton()
        Me.lblFname = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblDiff = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbSkip)
        Me.GroupBox1.Controls.Add(Me.rbRename)
        Me.GroupBox1.Controls.Add(Me.rbOverWrite)
        Me.GroupBox1.Location = New System.Drawing.Point(343, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(229, 115)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Actions:"
        '
        'rbSkip
        '
        Me.rbSkip.AutoSize = True
        Me.rbSkip.Location = New System.Drawing.Point(14, 69)
        Me.rbSkip.Name = "rbSkip"
        Me.rbSkip.Size = New System.Drawing.Size(46, 17)
        Me.rbSkip.TabIndex = 2
        Me.rbSkip.Text = "Skip"
        Me.rbSkip.UseVisualStyleBackColor = True
        '
        'rbRename
        '
        Me.rbRename.AutoSize = True
        Me.rbRename.Location = New System.Drawing.Point(14, 46)
        Me.rbRename.Name = "rbRename"
        Me.rbRename.Size = New System.Drawing.Size(65, 17)
        Me.rbRename.TabIndex = 1
        Me.rbRename.Text = "Rename"
        Me.rbRename.UseVisualStyleBackColor = True
        '
        'rbOverWrite
        '
        Me.rbOverWrite.AutoSize = True
        Me.rbOverWrite.Checked = True
        Me.rbOverWrite.Location = New System.Drawing.Point(14, 23)
        Me.rbOverWrite.Name = "rbOverWrite"
        Me.rbOverWrite.Size = New System.Drawing.Size(70, 17)
        Me.rbOverWrite.TabIndex = 0
        Me.rbOverWrite.TabStop = True
        Me.rbOverWrite.Text = "Overwrite"
        Me.rbOverWrite.UseVisualStyleBackColor = True
        '
        'lblFname
        '
        Me.lblFname.Location = New System.Drawing.Point(6, 16)
        Me.lblFname.Name = "lblFname"
        Me.lblFname.Size = New System.Drawing.Size(302, 24)
        Me.lblFname.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblDiff)
        Me.GroupBox2.Controls.Add(Me.lblFname)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(314, 115)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "File"
        '
        'lblDiff
        '
        Me.lblDiff.Location = New System.Drawing.Point(6, 54)
        Me.lblDiff.Name = "lblDiff"
        Me.lblDiff.Size = New System.Drawing.Size(301, 58)
        Me.lblDiff.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(493, 133)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 26)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "&OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(9, 146)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel1.TabIndex = 4
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Help"
        '
        'frmLocDup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 169)
        Me.ControlBox = False
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLocDup"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "File exists"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbSkip As System.Windows.Forms.RadioButton
    Friend WithEvents rbRename As System.Windows.Forms.RadioButton
    Friend WithEvents rbOverWrite As System.Windows.Forms.RadioButton
    Friend WithEvents lblFname As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDiff As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
