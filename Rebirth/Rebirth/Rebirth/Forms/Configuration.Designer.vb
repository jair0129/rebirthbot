<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configuration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configuration))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabGlobal = New System.Windows.Forms.TabPage
        Me.tabProfile = New System.Windows.Forms.TabPage
        Me.tabColors = New System.Windows.Forms.TabPage
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabGlobal)
        Me.TabControl1.Controls.Add(Me.tabProfile)
        Me.TabControl1.Controls.Add(Me.tabColors)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(790, 379)
        Me.TabControl1.TabIndex = 0
        '
        'tabGlobal
        '
        Me.tabGlobal.Location = New System.Drawing.Point(4, 22)
        Me.tabGlobal.Name = "tabGlobal"
        Me.tabGlobal.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGlobal.Size = New System.Drawing.Size(782, 353)
        Me.tabGlobal.TabIndex = 0
        Me.tabGlobal.Text = "Global Settings"
        Me.tabGlobal.UseVisualStyleBackColor = True
        '
        'tabProfile
        '
        Me.tabProfile.Location = New System.Drawing.Point(4, 22)
        Me.tabProfile.Name = "tabProfile"
        Me.tabProfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProfile.Size = New System.Drawing.Size(782, 353)
        Me.tabProfile.TabIndex = 1
        Me.tabProfile.Text = "Profile Settings"
        Me.tabProfile.UseVisualStyleBackColor = True
        '
        'tabColors
        '
        Me.tabColors.Location = New System.Drawing.Point(4, 22)
        Me.tabColors.Name = "tabColors"
        Me.tabColors.Size = New System.Drawing.Size(782, 353)
        Me.tabColors.TabIndex = 2
        Me.tabColors.Text = "TabPage3"
        Me.tabColors.UseVisualStyleBackColor = True
        '
        'Configuration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 379)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Configuration"
        Me.Text = "Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabGlobal As System.Windows.Forms.TabPage
    Friend WithEvents tabProfile As System.Windows.Forms.TabPage
    Friend WithEvents tabColors As System.Windows.Forms.TabPage
End Class
