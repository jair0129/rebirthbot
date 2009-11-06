<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadProfile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoadProfile))
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.cmbProfile = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Location = New System.Drawing.Point(2, 23)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(82, 27)
        Me.btnLoad.TabIndex = 0
        Me.btnLoad.Text = "Load Profile"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(84, 23)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(82, 27)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "New Profile"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(166, 23)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(82, 27)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cmbProfile
        '
        Me.cmbProfile.FormattingEnabled = True
        Me.cmbProfile.Location = New System.Drawing.Point(1, 0)
        Me.cmbProfile.Name = "cmbProfile"
        Me.cmbProfile.Size = New System.Drawing.Size(250, 21)
        Me.cmbProfile.TabIndex = 3
        '
        'LoadProfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(252, 50)
        Me.Controls.Add(Me.cmbProfile)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnLoad)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoadProfile"
        Me.Text = "Load Profile"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cmbProfile As System.Windows.Forms.ComboBox
End Class
