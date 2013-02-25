<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogViewer))
        Me.txtView = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSaveAs = New System.Windows.Forms.Button
        Me.checkWrap = New System.Windows.Forms.CheckBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'txtView
        '
        Me.txtView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtView.BackColor = System.Drawing.Color.Black
        Me.txtView.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtView.ForeColor = System.Drawing.Color.LimeGreen
        Me.txtView.Location = New System.Drawing.Point(6, 3)
        Me.txtView.Multiline = True
        Me.txtView.Name = "txtView"
        Me.txtView.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtView.Size = New System.Drawing.Size(732, 520)
        Me.txtView.TabIndex = 0
        Me.txtView.WordWrap = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(641, 533)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 25)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSaveAs
        '
        Me.btnSaveAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAs.Location = New System.Drawing.Point(525, 533)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(96, 25)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "&Save As"
        Me.btnSaveAs.UseVisualStyleBackColor = True
        '
        'checkWrap
        '
        Me.checkWrap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.checkWrap.AutoSize = True
        Me.checkWrap.Location = New System.Drawing.Point(12, 533)
        Me.checkWrap.MinimumSize = New System.Drawing.Size(600, 0)
        Me.checkWrap.Name = "checkWrap"
        Me.checkWrap.Size = New System.Drawing.Size(600, 17)
        Me.checkWrap.TabIndex = 3
        Me.checkWrap.Text = "&Wrap Text"
        Me.checkWrap.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        '
        'frmLogViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 564)
        Me.Controls.Add(Me.btnSaveAs)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtView)
        Me.Controls.Add(Me.checkWrap)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "frmLogViewer"
        Me.Text = "Secure Log Viewer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtView As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSaveAs As System.Windows.Forms.Button
    Friend WithEvents checkWrap As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
End Class
