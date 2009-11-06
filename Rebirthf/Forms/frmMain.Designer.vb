<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tabBots = New System.Windows.Forms.TabControl
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnuLoadBot = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuUnload = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuConnection = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReconnect = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDisconnect = New System.Windows.Forms.ToolStripMenuItem
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabBots
        '
        Me.tabBots.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.tabBots.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabBots.Location = New System.Drawing.Point(0, 24)
        Me.tabBots.Multiline = True
        Me.tabBots.Name = "tabBots"
        Me.tabBots.SelectedIndex = 0
        Me.tabBots.Size = New System.Drawing.Size(795, 434)
        Me.tabBots.TabIndex = 0
        Me.tabBots.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLoadBot, Me.mnuUnload, Me.mnuConnection, Me.TestToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(795, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuLoadBot
        '
        Me.mnuLoadBot.Name = "mnuLoadBot"
        Me.mnuLoadBot.Size = New System.Drawing.Size(61, 20)
        Me.mnuLoadBot.Text = "Load Bot"
        '
        'mnuUnload
        '
        Me.mnuUnload.Name = "mnuUnload"
        Me.mnuUnload.Size = New System.Drawing.Size(71, 20)
        Me.mnuUnload.Text = "Unload Bot"
        '
        'mnuConnection
        '
        Me.mnuConnection.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReconnect, Me.mnuDisconnect})
        Me.mnuConnection.Name = "mnuConnection"
        Me.mnuConnection.Size = New System.Drawing.Size(73, 20)
        Me.mnuConnection.Text = "Connection"
        '
        'mnuReconnect
        '
        Me.mnuReconnect.Name = "mnuReconnect"
        Me.mnuReconnect.Size = New System.Drawing.Size(126, 22)
        Me.mnuReconnect.Text = "Reconnect"
        '
        'mnuDisconnect
        '
        Me.mnuDisconnect.Name = "mnuDisconnect"
        Me.mnuDisconnect.Size = New System.Drawing.Size(126, 22)
        Me.mnuDisconnect.Text = "Disconnect"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Size = New System.Drawing.Size(38, 20)
        Me.TestToolStripMenuItem.Text = "test"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 458)
        Me.Controls.Add(Me.tabBots)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rebirth"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabBots As System.Windows.Forms.TabControl
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuLoadBot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUnload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuConnection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReconnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDisconnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
