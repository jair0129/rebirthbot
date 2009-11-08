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
        Me.chkAutoconnect = New System.Windows.Forms.CheckBox
        Me.chkAutoload = New System.Windows.Forms.CheckBox
        Me.cmbLangs = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbGame = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.tabGlobal.SuspendLayout()
        Me.tabProfile.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabGlobal)
        Me.TabControl1.Controls.Add(Me.tabProfile)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(225, 347)
        Me.TabControl1.TabIndex = 0
        '
        'tabGlobal
        '
        Me.tabGlobal.Controls.Add(Me.Button2)
        Me.tabGlobal.Controls.Add(Me.ComboBox2)
        Me.tabGlobal.Controls.Add(Me.Label9)
        Me.tabGlobal.Location = New System.Drawing.Point(4, 22)
        Me.tabGlobal.Name = "tabGlobal"
        Me.tabGlobal.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGlobal.Size = New System.Drawing.Size(217, 321)
        Me.tabGlobal.TabIndex = 0
        Me.tabGlobal.Text = "Global Settings"
        Me.tabGlobal.UseVisualStyleBackColor = True
        '
        'tabProfile
        '
        Me.tabProfile.Controls.Add(Me.Button1)
        Me.tabProfile.Controls.Add(Me.cmbGame)
        Me.tabProfile.Controls.Add(Me.Label8)
        Me.tabProfile.Controls.Add(Me.chkAutoconnect)
        Me.tabProfile.Controls.Add(Me.chkAutoload)
        Me.tabProfile.Controls.Add(Me.cmbLangs)
        Me.tabProfile.Controls.Add(Me.Label7)
        Me.tabProfile.Controls.Add(Me.ComboBox1)
        Me.tabProfile.Controls.Add(Me.TextBox6)
        Me.tabProfile.Controls.Add(Me.TextBox5)
        Me.tabProfile.Controls.Add(Me.TextBox4)
        Me.tabProfile.Controls.Add(Me.TextBox3)
        Me.tabProfile.Controls.Add(Me.TextBox2)
        Me.tabProfile.Controls.Add(Me.TextBox1)
        Me.tabProfile.Controls.Add(Me.Label6)
        Me.tabProfile.Controls.Add(Me.Label5)
        Me.tabProfile.Controls.Add(Me.Label4)
        Me.tabProfile.Controls.Add(Me.Label3)
        Me.tabProfile.Controls.Add(Me.Label2)
        Me.tabProfile.Controls.Add(Me.Label1)
        Me.tabProfile.Location = New System.Drawing.Point(4, 22)
        Me.tabProfile.Name = "tabProfile"
        Me.tabProfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProfile.Size = New System.Drawing.Size(217, 321)
        Me.tabProfile.TabIndex = 1
        Me.tabProfile.Text = "Profile Settings"
        Me.tabProfile.UseVisualStyleBackColor = True
        '
        'chkAutoconnect
        '
        Me.chkAutoconnect.AutoSize = True
        Me.chkAutoconnect.Location = New System.Drawing.Point(10, 266)
        Me.chkAutoconnect.Name = "chkAutoconnect"
        Me.chkAutoconnect.Size = New System.Drawing.Size(81, 17)
        Me.chkAutoconnect.TabIndex = 9
        Me.chkAutoconnect.Text = "CheckBox2"
        Me.chkAutoconnect.UseVisualStyleBackColor = True
        '
        'chkAutoload
        '
        Me.chkAutoload.AutoSize = True
        Me.chkAutoload.Location = New System.Drawing.Point(10, 245)
        Me.chkAutoload.Name = "chkAutoload"
        Me.chkAutoload.Size = New System.Drawing.Size(81, 17)
        Me.chkAutoload.TabIndex = 8
        Me.chkAutoload.Text = "CheckBox1"
        Me.chkAutoload.UseVisualStyleBackColor = True
        '
        'cmbLangs
        '
        Me.cmbLangs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLangs.FormattingEnabled = True
        Me.cmbLangs.Location = New System.Drawing.Point(84, 220)
        Me.cmbLangs.Name = "cmbLangs"
        Me.cmbLangs.Size = New System.Drawing.Size(121, 21)
        Me.cmbLangs.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 225)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Label7"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(12, 7)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 21)
        Me.ComboBox1.TabIndex = 14
        Me.ComboBox1.TabStop = False
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(84, 169)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(121, 20)
        Me.TextBox6.TabIndex = 5
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(84, 145)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(121, 20)
        Me.TextBox5.TabIndex = 4
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(84, 122)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(121, 20)
        Me.TextBox4.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(84, 98)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(121, 20)
        Me.TextBox3.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(84, 74)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(121, 20)
        Me.TextBox2.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(84, 51)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(121, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Label6"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Label5"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 198)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Label8"
        '
        'cmbGame
        '
        Me.cmbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGame.FormattingEnabled = True
        Me.cmbGame.Location = New System.Drawing.Point(84, 193)
        Me.cmbGame.Name = "cmbGame"
        Me.cmbGame.Size = New System.Drawing.Size(121, 21)
        Me.cmbGame.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 292)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(194, 22)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 185)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(194, 22)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(85, 113)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 118)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Label9"
        '
        'Configuration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(225, 347)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Configuration"
        Me.Text = "Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.tabGlobal.ResumeLayout(False)
        Me.tabGlobal.PerformLayout()
        Me.tabProfile.ResumeLayout(False)
        Me.tabProfile.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabGlobal As System.Windows.Forms.TabPage
    Friend WithEvents tabProfile As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbLangs As System.Windows.Forms.ComboBox
    Friend WithEvents chkAutoconnect As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoload As System.Windows.Forms.CheckBox
    Friend WithEvents cmbGame As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
