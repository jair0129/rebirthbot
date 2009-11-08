<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BotInterface
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container
        Me.cmbTextEntry = New System.Windows.Forms.ComboBox
        Me.ilIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.rtbChat = New System.Windows.Forms.RichTextBox
        Me.tabLists = New System.Windows.Forms.TabControl
        Me.tabChannel = New System.Windows.Forms.TabPage
        Me.lvChannel = New System.Windows.Forms.ListView
        Me.gameIcon = New System.Windows.Forms.ColumnHeader
        Me.userName = New System.Windows.Forms.ColumnHeader
        Me.userPing = New System.Windows.Forms.ColumnHeader
        Me.userFlags = New System.Windows.Forms.ColumnHeader
        Me.imgIndexVal = New System.Windows.Forms.ColumnHeader
        Me.ChannelContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.IgnoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UnignoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.KickToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.ProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtChannel = New System.Windows.Forms.TextBox
        Me.tabFriends = New System.Windows.Forms.TabPage
        Me.lvFriends = New System.Windows.Forms.ListView
        Me.colFriend = New System.Windows.Forms.ColumnHeader
        Me.colFriendMutual = New System.Windows.Forms.ColumnHeader
        Me.tabBotnet = New System.Windows.Forms.TabPage
        Me.lvBotnetUsers = New System.Windows.Forms.ListView
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.txtBotnet = New System.Windows.Forms.TextBox
        Me.tabClan = New System.Windows.Forms.TabPage
        Me.lvClan = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.txtClan = New System.Windows.Forms.TextBox
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.tabLists.SuspendLayout()
        Me.tabChannel.SuspendLayout()
        Me.ChannelContext.SuspendLayout()
        Me.tabFriends.SuspendLayout()
        Me.tabBotnet.SuspendLayout()
        Me.tabClan.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbTextEntry
        '
        Me.cmbTextEntry.BackColor = System.Drawing.Color.Teal
        Me.cmbTextEntry.FormattingEnabled = True
        Me.cmbTextEntry.Location = New System.Drawing.Point(3, 296)
        Me.cmbTextEntry.Name = "cmbTextEntry"
        Me.cmbTextEntry.Size = New System.Drawing.Size(491, 21)
        Me.cmbTextEntry.TabIndex = 0
        '
        'ilIcons
        '
        Me.ilIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ilIcons.ImageSize = New System.Drawing.Size(28, 14)
        Me.ilIcons.TransparentColor = System.Drawing.Color.Transparent
        '
        'rtbChat
        '
        Me.rtbChat.BackColor = System.Drawing.Color.Black
        Me.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbChat.ForeColor = System.Drawing.Color.White
        Me.rtbChat.Location = New System.Drawing.Point(3, 4)
        Me.rtbChat.Name = "rtbChat"
        Me.rtbChat.ReadOnly = True
        Me.rtbChat.Size = New System.Drawing.Size(491, 292)
        Me.rtbChat.TabIndex = 0
        Me.rtbChat.TabStop = False
        Me.rtbChat.Text = ""
        '
        'tabLists
        '
        Me.tabLists.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tabLists.Controls.Add(Me.tabChannel)
        Me.tabLists.Controls.Add(Me.tabFriends)
        Me.tabLists.Controls.Add(Me.tabBotnet)
        Me.tabLists.Controls.Add(Me.tabClan)
        Me.tabLists.Location = New System.Drawing.Point(494, 4)
        Me.tabLists.Name = "tabLists"
        Me.tabLists.SelectedIndex = 0
        Me.tabLists.Size = New System.Drawing.Size(187, 339)
        Me.tabLists.TabIndex = 4
        Me.tabLists.TabStop = False
        '
        'tabChannel
        '
        Me.tabChannel.Controls.Add(Me.lvChannel)
        Me.tabChannel.Controls.Add(Me.txtChannel)
        Me.tabChannel.ForeColor = System.Drawing.Color.Black
        Me.tabChannel.Location = New System.Drawing.Point(4, 25)
        Me.tabChannel.Name = "tabChannel"
        Me.tabChannel.Padding = New System.Windows.Forms.Padding(3)
        Me.tabChannel.Size = New System.Drawing.Size(179, 310)
        Me.tabChannel.TabIndex = 0
        Me.tabChannel.Text = "Channel"
        Me.tabChannel.UseVisualStyleBackColor = True
        '
        'lvChannel
        '
        Me.lvChannel.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lvChannel.BackColor = System.Drawing.Color.Black
        Me.lvChannel.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.gameIcon, Me.userName, Me.userPing, Me.userFlags, Me.imgIndexVal})
        Me.lvChannel.ContextMenuStrip = Me.ChannelContext
        Me.lvChannel.ForeColor = System.Drawing.Color.White
        Me.lvChannel.FullRowSelect = True
        Me.lvChannel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvChannel.Location = New System.Drawing.Point(1, 20)
        Me.lvChannel.Name = "lvChannel"
        Me.lvChannel.Size = New System.Drawing.Size(176, 291)
        Me.lvChannel.SmallImageList = Me.ilIcons
        Me.lvChannel.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvChannel.TabIndex = 8
        Me.lvChannel.TabStop = False
        Me.lvChannel.UseCompatibleStateImageBehavior = False
        Me.lvChannel.View = System.Windows.Forms.View.Details
        '
        'gameIcon
        '
        Me.gameIcon.Width = 34
        '
        'userName
        '
        Me.userName.Width = 111
        '
        'userPing
        '
        Me.userPing.Width = 0
        '
        'userFlags
        '
        Me.userFlags.Width = 0
        '
        'imgIndexVal
        '
        Me.imgIndexVal.Width = 0
        '
        'ChannelContext
        '
        Me.ChannelContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IgnoreToolStripMenuItem, Me.UnignoreToolStripMenuItem, Me.ToolStripMenuItem2, Me.KickToolStripMenuItem, Me.BanToolStripMenuItem, Me.ToolStripMenuItem1, Me.ProfileToolStripMenuItem})
        Me.ChannelContext.Name = "ContextMenuStrip1"
        Me.ChannelContext.Size = New System.Drawing.Size(118, 126)
        '
        'IgnoreToolStripMenuItem
        '
        Me.IgnoreToolStripMenuItem.Name = "IgnoreToolStripMenuItem"
        Me.IgnoreToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.IgnoreToolStripMenuItem.Text = "Ignore"
        '
        'UnignoreToolStripMenuItem
        '
        Me.UnignoreToolStripMenuItem.Name = "UnignoreToolStripMenuItem"
        Me.UnignoreToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.UnignoreToolStripMenuItem.Text = "Unignore"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(114, 6)
        '
        'KickToolStripMenuItem
        '
        Me.KickToolStripMenuItem.Name = "KickToolStripMenuItem"
        Me.KickToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.KickToolStripMenuItem.Text = "Kick"
        '
        'BanToolStripMenuItem
        '
        Me.BanToolStripMenuItem.Name = "BanToolStripMenuItem"
        Me.BanToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.BanToolStripMenuItem.Text = "Ban"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(114, 6)
        '
        'ProfileToolStripMenuItem
        '
        Me.ProfileToolStripMenuItem.Name = "ProfileToolStripMenuItem"
        Me.ProfileToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.ProfileToolStripMenuItem.Text = "Profile"
        '
        'txtChannel
        '
        Me.txtChannel.BackColor = System.Drawing.Color.Black
        Me.txtChannel.ForeColor = System.Drawing.Color.White
        Me.txtChannel.Location = New System.Drawing.Point(1, 0)
        Me.txtChannel.Name = "txtChannel"
        Me.txtChannel.ReadOnly = True
        Me.txtChannel.Size = New System.Drawing.Size(177, 21)
        Me.txtChannel.TabIndex = 7
        Me.txtChannel.TabStop = False
        Me.txtChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tabFriends
        '
        Me.tabFriends.Controls.Add(Me.lvFriends)
        Me.tabFriends.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabFriends.ForeColor = System.Drawing.Color.Black
        Me.tabFriends.Location = New System.Drawing.Point(4, 25)
        Me.tabFriends.Name = "tabFriends"
        Me.tabFriends.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFriends.Size = New System.Drawing.Size(179, 310)
        Me.tabFriends.TabIndex = 1
        Me.tabFriends.Text = "Friends"
        Me.tabFriends.UseVisualStyleBackColor = True
        '
        'lvFriends
        '
        Me.lvFriends.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lvFriends.BackColor = System.Drawing.Color.Black
        Me.lvFriends.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFriend, Me.colFriendMutual})
        Me.lvFriends.ForeColor = System.Drawing.Color.White
        Me.lvFriends.FullRowSelect = True
        Me.lvFriends.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvFriends.Location = New System.Drawing.Point(1, 2)
        Me.lvFriends.Name = "lvFriends"
        Me.lvFriends.Size = New System.Drawing.Size(175, 306)
        Me.lvFriends.TabIndex = 10
        Me.lvFriends.TabStop = False
        Me.lvFriends.UseCompatibleStateImageBehavior = False
        Me.lvFriends.View = System.Windows.Forms.View.Details
        '
        'colFriend
        '
        Me.colFriend.Width = 150
        '
        'colFriendMutual
        '
        Me.colFriendMutual.Width = 0
        '
        'tabBotnet
        '
        Me.tabBotnet.Controls.Add(Me.lvBotnetUsers)
        Me.tabBotnet.Controls.Add(Me.txtBotnet)
        Me.tabBotnet.Location = New System.Drawing.Point(4, 25)
        Me.tabBotnet.Name = "tabBotnet"
        Me.tabBotnet.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBotnet.Size = New System.Drawing.Size(179, 310)
        Me.tabBotnet.TabIndex = 2
        Me.tabBotnet.Text = "Botnet"
        Me.tabBotnet.UseVisualStyleBackColor = True
        '
        'lvBotnetUsers
        '
        Me.lvBotnetUsers.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lvBotnetUsers.BackColor = System.Drawing.Color.Black
        Me.lvBotnetUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11})
        Me.lvBotnetUsers.ForeColor = System.Drawing.Color.White
        Me.lvBotnetUsers.FullRowSelect = True
        Me.lvBotnetUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvBotnetUsers.Location = New System.Drawing.Point(1, 20)
        Me.lvBotnetUsers.Name = "lvBotnetUsers"
        Me.lvBotnetUsers.Size = New System.Drawing.Size(177, 291)
        Me.lvBotnetUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvBotnetUsers.TabIndex = 10
        Me.lvBotnetUsers.TabStop = False
        Me.lvBotnetUsers.UseCompatibleStateImageBehavior = False
        Me.lvBotnetUsers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Width = 150
        '
        'txtBotnet
        '
        Me.txtBotnet.BackColor = System.Drawing.Color.Black
        Me.txtBotnet.ForeColor = System.Drawing.Color.White
        Me.txtBotnet.Location = New System.Drawing.Point(0, 0)
        Me.txtBotnet.Name = "txtBotnet"
        Me.txtBotnet.ReadOnly = True
        Me.txtBotnet.Size = New System.Drawing.Size(179, 21)
        Me.txtBotnet.TabIndex = 9
        Me.txtBotnet.TabStop = False
        Me.txtBotnet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tabClan
        '
        Me.tabClan.Controls.Add(Me.lvClan)
        Me.tabClan.Controls.Add(Me.txtClan)
        Me.tabClan.Location = New System.Drawing.Point(4, 25)
        Me.tabClan.Name = "tabClan"
        Me.tabClan.Padding = New System.Windows.Forms.Padding(3)
        Me.tabClan.Size = New System.Drawing.Size(179, 310)
        Me.tabClan.TabIndex = 3
        Me.tabClan.Text = "Clan"
        Me.tabClan.UseVisualStyleBackColor = True
        '
        'lvClan
        '
        Me.lvClan.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lvClan.BackColor = System.Drawing.Color.Black
        Me.lvClan.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7})
        Me.lvClan.ForeColor = System.Drawing.Color.White
        Me.lvClan.FullRowSelect = True
        Me.lvClan.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvClan.Location = New System.Drawing.Point(1, 20)
        Me.lvClan.Name = "lvClan"
        Me.lvClan.Size = New System.Drawing.Size(177, 291)
        Me.lvClan.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvClan.TabIndex = 12
        Me.lvClan.TabStop = False
        Me.lvClan.UseCompatibleStateImageBehavior = False
        Me.lvClan.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Width = 150
        '
        'txtClan
        '
        Me.txtClan.BackColor = System.Drawing.Color.Black
        Me.txtClan.ForeColor = System.Drawing.Color.White
        Me.txtClan.Location = New System.Drawing.Point(0, 0)
        Me.txtClan.Name = "txtClan"
        Me.txtClan.ReadOnly = True
        Me.txtClan.Size = New System.Drawing.Size(179, 21)
        Me.txtClan.TabIndex = 11
        Me.txtClan.TabStop = False
        Me.txtClan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 34
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Width = 111
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Width = 0
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Width = 0
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Width = 0
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Width = 150
        '
        'BotInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.tabLists)
        Me.Controls.Add(Me.cmbTextEntry)
        Me.Controls.Add(Me.rtbChat)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "BotInterface"
        Me.Size = New System.Drawing.Size(685, 464)
        Me.tabLists.ResumeLayout(False)
        Me.tabChannel.ResumeLayout(False)
        Me.tabChannel.PerformLayout()
        Me.ChannelContext.ResumeLayout(False)
        Me.tabFriends.ResumeLayout(False)
        Me.tabBotnet.ResumeLayout(False)
        Me.tabBotnet.PerformLayout()
        Me.tabClan.ResumeLayout(False)
        Me.tabClan.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbTextEntry As System.Windows.Forms.ComboBox
    Friend WithEvents rtbChat As System.Windows.Forms.RichTextBox
    Friend WithEvents ilIcons As System.Windows.Forms.ImageList
    Friend WithEvents tabLists As System.Windows.Forms.TabControl
    Friend WithEvents tabChannel As System.Windows.Forms.TabPage
    Friend WithEvents lvChannel As System.Windows.Forms.ListView
    Friend WithEvents gameIcon As System.Windows.Forms.ColumnHeader
    Friend WithEvents userName As System.Windows.Forms.ColumnHeader
    Friend WithEvents userPing As System.Windows.Forms.ColumnHeader
    Friend WithEvents userFlags As System.Windows.Forms.ColumnHeader
    Friend WithEvents imgIndexVal As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtChannel As System.Windows.Forms.TextBox
    Friend WithEvents tabFriends As System.Windows.Forms.TabPage
    Friend WithEvents tabBotnet As System.Windows.Forms.TabPage
    Friend WithEvents lvFriends As System.Windows.Forms.ListView
    Friend WithEvents colFriend As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvBotnetUsers As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtBotnet As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFriendMutual As System.Windows.Forms.ColumnHeader
    Friend WithEvents ChannelContext As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents IgnoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnignoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents KickToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabClan As System.Windows.Forms.TabPage
    Friend WithEvents lvClan As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtClan As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader

End Class
