Imports AirDrive.vars

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslCancelProcess = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblRate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.scConsoleWrapper = New System.Windows.Forms.SplitContainer()
        Me.scMainH = New System.Windows.Forms.SplitContainer()
        Me.scFilesMainV = New System.Windows.Forms.SplitContainer()
        Me.scLocalFilesH = New System.Windows.Forms.SplitContainer()
        Me.txtLocalDir = New System.Windows.Forms.TextBox()
        Me.tsLocalDir = New System.Windows.Forms.ToolStrip()
        Me.btnLocalDirUpload = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalDirUploadEncrypted = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalDirNew = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalDirDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalDirRename = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tvLocalDir = New System.Windows.Forms.TreeView()
        Me.cmenu_LocalDir = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsLocalDirUpload = New System.Windows.Forms.ToolStripMenuItem()
        Me.UploadEncryptedToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmenu_LocalDirNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenu_LocalDirRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsLocalDirDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilDir = New System.Windows.Forms.ImageList(Me.components)
        Me.tsLocalFiles = New System.Windows.Forms.ToolStrip()
        Me.btnLocalFileUpload = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalFileUploadEncrypted = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalFileRename = New System.Windows.Forms.ToolStripButton()
        Me.btnLocalFileDelete = New System.Windows.Forms.ToolStripButton()
        Me.lvLocalFiles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmenu_LocalFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UploadEncryptedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilLocalFiles = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.tvRemoteDir = New System.Windows.Forms.TreeView()
        Me.cmenu_RemoteDir = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DownloadToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshDisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsRemoteDir = New System.Windows.Forms.ToolStrip()
        Me.btnRemoteDirDownload = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteDirNew = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteDirDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteDirRename = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteRefresh = New System.Windows.Forms.ToolStripButton()
        Me.txtRemoteDir = New System.Windows.Forms.TextBox()
        Me.tsRemoteFiles = New System.Windows.Forms.ToolStrip()
        Me.btnRemoteFileDownload = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteFileRename = New System.Windows.Forms.ToolStripButton()
        Me.btnRemoteFileDelete = New System.Windows.Forms.ToolStripButton()
        Me.lvRemoteFiles = New System.Windows.Forms.ListView()
        Me.lvRemoteFilesCHFilename = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvRemoteFilesCHSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmsRemoteFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilRemoteFiles = New System.Windows.Forms.ImageList(Me.components)
        Me.lblQueueSize = New System.Windows.Forms.Label()
        Me.lblQueueCount = New System.Windows.Forms.Label()
        Me.lvQueue = New System.Windows.Forms.ListView()
        Me.lvQueueCHLocal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvQueueCHTransferDirection = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvQueueCHRemote = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvQueueCHFileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvQueueCHStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmsQueueBox = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveSelectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmenu_QueueClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtConsole = New System.Windows.Forms.TextBox()
        Me.cmenu_Console = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmenu_QueueSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menu_AddAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.menu_ManageAccounts = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menu_Profiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.menu_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExplorerViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.menu_None = New System.Windows.Forms.ToolStripMenuItem()
        Me.menu_Normal = New System.Windows.Forms.ToolStripMenuItem()
        Me.menu_Verbose = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AirDriveHeadquartersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendFeedbackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DonateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.menu_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.bgwLoadCloudData = New System.ComponentModel.BackgroundWorker()
        Me.bgwQueueHandler = New System.ComponentModel.BackgroundWorker()
        Me.bgwIdle = New System.ComponentModel.BackgroundWorker()
        Me.bgwCheckUpdates = New System.ComponentModel.BackgroundWorker()
        Me.bgwRate = New System.ComponentModel.BackgroundWorker()
        Me.ToolStripContainer1.BottomToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.scConsoleWrapper.Panel1.SuspendLayout()
        Me.scConsoleWrapper.Panel2.SuspendLayout()
        Me.scConsoleWrapper.SuspendLayout()
        Me.scMainH.Panel1.SuspendLayout()
        Me.scMainH.Panel2.SuspendLayout()
        Me.scMainH.SuspendLayout()
        Me.scFilesMainV.Panel1.SuspendLayout()
        Me.scFilesMainV.Panel2.SuspendLayout()
        Me.scFilesMainV.SuspendLayout()
        Me.scLocalFilesH.Panel1.SuspendLayout()
        Me.scLocalFilesH.Panel2.SuspendLayout()
        Me.scLocalFilesH.SuspendLayout()
        Me.tsLocalDir.SuspendLayout()
        Me.cmenu_LocalDir.SuspendLayout()
        Me.tsLocalFiles.SuspendLayout()
        Me.cmenu_LocalFiles.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.cmenu_RemoteDir.SuspendLayout()
        Me.tsRemoteDir.SuspendLayout()
        Me.tsRemoteFiles.SuspendLayout()
        Me.cmsRemoteFiles.SuspendLayout()
        Me.cmsQueueBox.SuspendLayout()
        Me.cmenu_Console.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.BottomToolStripPanel
        '
        Me.ToolStripContainer1.BottomToolStripPanel.Controls.Add(Me.StatusStrip1)
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.scConsoleWrapper)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(1049, 548)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(1049, 594)
        Me.ToolStripContainer1.TabIndex = 0
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.MenuStrip1)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspbStatus, Me.tsslCancelProcess, Me.tsslStatus, Me.lblRate})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1049, 22)
        Me.StatusStrip1.TabIndex = 0
        '
        'tspbStatus
        '
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(100, 16)
        Me.tspbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsslCancelProcess
        '
        Me.tsslCancelProcess.Image = Global.AirDrive.My.Resources.Resources._error
        Me.tsslCancelProcess.IsLink = True
        Me.tsslCancelProcess.Name = "tsslCancelProcess"
        Me.tsslCancelProcess.Size = New System.Drawing.Size(16, 17)
        Me.tsslCancelProcess.Visible = False
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(885, 17)
        Me.tsslStatus.Spring = True
        Me.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRate
        '
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(0, 17)
        '
        'scConsoleWrapper
        '
        Me.scConsoleWrapper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scConsoleWrapper.Location = New System.Drawing.Point(0, 0)
        Me.scConsoleWrapper.Name = "scConsoleWrapper"
        Me.scConsoleWrapper.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scConsoleWrapper.Panel1
        '
        Me.scConsoleWrapper.Panel1.Controls.Add(Me.scMainH)
        '
        'scConsoleWrapper.Panel2
        '
        Me.scConsoleWrapper.Panel2.Controls.Add(Me.txtConsole)
        Me.scConsoleWrapper.Size = New System.Drawing.Size(1049, 548)
        Me.scConsoleWrapper.SplitterDistance = 461
        Me.scConsoleWrapper.SplitterWidth = 6
        Me.scConsoleWrapper.TabIndex = 14
        '
        'scMainH
        '
        Me.scMainH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.scMainH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMainH.Location = New System.Drawing.Point(0, 0)
        Me.scMainH.Name = "scMainH"
        Me.scMainH.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMainH.Panel1
        '
        Me.scMainH.Panel1.Controls.Add(Me.scFilesMainV)
        '
        'scMainH.Panel2
        '
        Me.scMainH.Panel2.Controls.Add(Me.lblQueueSize)
        Me.scMainH.Panel2.Controls.Add(Me.lblQueueCount)
        Me.scMainH.Panel2.Controls.Add(Me.lvQueue)
        Me.scMainH.Size = New System.Drawing.Size(1049, 461)
        Me.scMainH.SplitterDistance = 357
        Me.scMainH.SplitterWidth = 8
        Me.scMainH.TabIndex = 13
        '
        'scFilesMainV
        '
        Me.scFilesMainV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scFilesMainV.Location = New System.Drawing.Point(0, 0)
        Me.scFilesMainV.Name = "scFilesMainV"
        '
        'scFilesMainV.Panel1
        '
        Me.scFilesMainV.Panel1.Controls.Add(Me.scLocalFilesH)
        '
        'scFilesMainV.Panel2
        '
        Me.scFilesMainV.Panel2.Controls.Add(Me.SplitContainer2)
        Me.scFilesMainV.Size = New System.Drawing.Size(1047, 355)
        Me.scFilesMainV.SplitterDistance = 524
        Me.scFilesMainV.SplitterWidth = 8
        Me.scFilesMainV.TabIndex = 12
        '
        'scLocalFilesH
        '
        Me.scLocalFilesH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scLocalFilesH.Location = New System.Drawing.Point(0, 0)
        Me.scLocalFilesH.Name = "scLocalFilesH"
        Me.scLocalFilesH.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scLocalFilesH.Panel1
        '
        Me.scLocalFilesH.Panel1.Controls.Add(Me.txtLocalDir)
        Me.scLocalFilesH.Panel1.Controls.Add(Me.tsLocalDir)
        Me.scLocalFilesH.Panel1.Controls.Add(Me.tvLocalDir)
        '
        'scLocalFilesH.Panel2
        '
        Me.scLocalFilesH.Panel2.Controls.Add(Me.tsLocalFiles)
        Me.scLocalFilesH.Panel2.Controls.Add(Me.lvLocalFiles)
        Me.scLocalFilesH.Size = New System.Drawing.Size(524, 355)
        Me.scLocalFilesH.SplitterDistance = 133
        Me.scLocalFilesH.SplitterWidth = 8
        Me.scLocalFilesH.TabIndex = 8
        '
        'txtLocalDir
        '
        Me.txtLocalDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocalDir.Location = New System.Drawing.Point(3, 25)
        Me.txtLocalDir.Margin = New System.Windows.Forms.Padding(0)
        Me.txtLocalDir.Name = "txtLocalDir"
        Me.txtLocalDir.ReadOnly = True
        Me.txtLocalDir.Size = New System.Drawing.Size(521, 20)
        Me.txtLocalDir.TabIndex = 10
        '
        'tsLocalDir
        '
        Me.tsLocalDir.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLocalDir.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLocalDirUpload, Me.btnLocalDirUploadEncrypted, Me.btnLocalDirNew, Me.btnLocalDirDelete, Me.btnLocalDirRename, Me.btnLocalRefresh})
        Me.tsLocalDir.Location = New System.Drawing.Point(0, 0)
        Me.tsLocalDir.Name = "tsLocalDir"
        Me.tsLocalDir.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsLocalDir.Size = New System.Drawing.Size(524, 25)
        Me.tsLocalDir.TabIndex = 9
        Me.tsLocalDir.Text = "ToolStrip1"
        '
        'btnLocalDirUpload
        '
        Me.btnLocalDirUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalDirUpload.Image = Global.AirDrive.My.Resources.Resources.upload
        Me.btnLocalDirUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalDirUpload.Name = "btnLocalDirUpload"
        Me.btnLocalDirUpload.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalDirUpload.ToolTipText = "Upload entire selected directory"
        '
        'btnLocalDirUploadEncrypted
        '
        Me.btnLocalDirUploadEncrypted.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalDirUploadEncrypted.Image = CType(resources.GetObject("btnLocalDirUploadEncrypted.Image"), System.Drawing.Image)
        Me.btnLocalDirUploadEncrypted.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalDirUploadEncrypted.Name = "btnLocalDirUploadEncrypted"
        Me.btnLocalDirUploadEncrypted.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalDirUploadEncrypted.Text = "ToolStripButton1"
        Me.btnLocalDirUploadEncrypted.ToolTipText = "Encrypt and upload directory"
        '
        'btnLocalDirNew
        '
        Me.btnLocalDirNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalDirNew.Image = Global.AirDrive.My.Resources.Resources.folderplus
        Me.btnLocalDirNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalDirNew.Name = "btnLocalDirNew"
        Me.btnLocalDirNew.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalDirNew.Text = "ToolStripButton9"
        Me.btnLocalDirNew.ToolTipText = "Create directory"
        '
        'btnLocalDirDelete
        '
        Me.btnLocalDirDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalDirDelete.Image = Global.AirDrive.My.Resources.Resources.folder_x
        Me.btnLocalDirDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalDirDelete.Name = "btnLocalDirDelete"
        Me.btnLocalDirDelete.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalDirDelete.Text = "ToolStripButton3"
        Me.btnLocalDirDelete.ToolTipText = "Delete directory"
        '
        'btnLocalDirRename
        '
        Me.btnLocalDirRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalDirRename.Image = Global.AirDrive.My.Resources.Resources.folderrename
        Me.btnLocalDirRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalDirRename.Name = "btnLocalDirRename"
        Me.btnLocalDirRename.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalDirRename.Text = "Rename selected directory"
        '
        'btnLocalRefresh
        '
        Me.btnLocalRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalRefresh.Image = Global.AirDrive.My.Resources.Resources.refresh
        Me.btnLocalRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalRefresh.Name = "btnLocalRefresh"
        Me.btnLocalRefresh.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalRefresh.Text = "ToolStripButton1"
        Me.btnLocalRefresh.ToolTipText = "Refresh local file displays (F5)"
        '
        'tvLocalDir
        '
        Me.tvLocalDir.AllowDrop = True
        Me.tvLocalDir.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvLocalDir.ContextMenuStrip = Me.cmenu_LocalDir
        Me.tvLocalDir.HideSelection = False
        Me.tvLocalDir.ImageIndex = 0
        Me.tvLocalDir.ImageList = Me.ilDir
        Me.tvLocalDir.ItemHeight = 16
        Me.tvLocalDir.Location = New System.Drawing.Point(3, 45)
        Me.tvLocalDir.Margin = New System.Windows.Forms.Padding(0)
        Me.tvLocalDir.Name = "tvLocalDir"
        Me.tvLocalDir.SelectedImageIndex = 0
        Me.tvLocalDir.Size = New System.Drawing.Size(521, 85)
        Me.tvLocalDir.TabIndex = 8
        '
        'cmenu_LocalDir
        '
        Me.cmenu_LocalDir.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsLocalDirUpload, Me.UploadEncryptedToolStripMenuItem1, Me.ToolStripSeparator3, Me.cmenu_LocalDirNew, Me.cmenu_LocalDirRename, Me.cmsLocalDirDelete})
        Me.cmenu_LocalDir.Name = "cmsLocalDir"
        Me.cmenu_LocalDir.Size = New System.Drawing.Size(171, 120)
        '
        'cmsLocalDirUpload
        '
        Me.cmsLocalDirUpload.Image = CType(resources.GetObject("cmsLocalDirUpload.Image"), System.Drawing.Image)
        Me.cmsLocalDirUpload.Name = "cmsLocalDirUpload"
        Me.cmsLocalDirUpload.Size = New System.Drawing.Size(170, 22)
        Me.cmsLocalDirUpload.Text = "Upload"
        '
        'UploadEncryptedToolStripMenuItem1
        '
        Me.UploadEncryptedToolStripMenuItem1.Image = Global.AirDrive.My.Resources.Resources.upload_encrypted
        Me.UploadEncryptedToolStripMenuItem1.Name = "UploadEncryptedToolStripMenuItem1"
        Me.UploadEncryptedToolStripMenuItem1.Size = New System.Drawing.Size(170, 22)
        Me.UploadEncryptedToolStripMenuItem1.Text = "Upload Encrypted"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(167, 6)
        '
        'cmenu_LocalDirNew
        '
        Me.cmenu_LocalDirNew.Image = CType(resources.GetObject("cmenu_LocalDirNew.Image"), System.Drawing.Image)
        Me.cmenu_LocalDirNew.Name = "cmenu_LocalDirNew"
        Me.cmenu_LocalDirNew.Size = New System.Drawing.Size(170, 22)
        Me.cmenu_LocalDirNew.Text = "New folder"
        '
        'cmenu_LocalDirRename
        '
        Me.cmenu_LocalDirRename.Image = Global.AirDrive.My.Resources.Resources.folderrename
        Me.cmenu_LocalDirRename.Name = "cmenu_LocalDirRename"
        Me.cmenu_LocalDirRename.Size = New System.Drawing.Size(170, 22)
        Me.cmenu_LocalDirRename.Text = "Rename"
        '
        'cmsLocalDirDelete
        '
        Me.cmsLocalDirDelete.Image = CType(resources.GetObject("cmsLocalDirDelete.Image"), System.Drawing.Image)
        Me.cmsLocalDirDelete.Name = "cmsLocalDirDelete"
        Me.cmsLocalDirDelete.Size = New System.Drawing.Size(170, 22)
        Me.cmsLocalDirDelete.Text = "Delete folder"
        '
        'ilDir
        '
        Me.ilDir.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ilDir.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilDir.TransparentColor = System.Drawing.Color.Transparent
        '
        'tsLocalFiles
        '
        Me.tsLocalFiles.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLocalFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLocalFileUpload, Me.btnLocalFileUploadEncrypted, Me.btnLocalFileRename, Me.btnLocalFileDelete})
        Me.tsLocalFiles.Location = New System.Drawing.Point(0, 0)
        Me.tsLocalFiles.Name = "tsLocalFiles"
        Me.tsLocalFiles.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsLocalFiles.Size = New System.Drawing.Size(524, 25)
        Me.tsLocalFiles.TabIndex = 10
        Me.tsLocalFiles.Text = "ToolStrip1"
        '
        'btnLocalFileUpload
        '
        Me.btnLocalFileUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalFileUpload.Image = Global.AirDrive.My.Resources.Resources.upload
        Me.btnLocalFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalFileUpload.Name = "btnLocalFileUpload"
        Me.btnLocalFileUpload.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalFileUpload.Text = "ToolStripButton7"
        Me.btnLocalFileUpload.ToolTipText = "Upload selected files"
        '
        'btnLocalFileUploadEncrypted
        '
        Me.btnLocalFileUploadEncrypted.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalFileUploadEncrypted.Image = CType(resources.GetObject("btnLocalFileUploadEncrypted.Image"), System.Drawing.Image)
        Me.btnLocalFileUploadEncrypted.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalFileUploadEncrypted.Name = "btnLocalFileUploadEncrypted"
        Me.btnLocalFileUploadEncrypted.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalFileUploadEncrypted.Text = "ToolStripButton1"
        Me.btnLocalFileUploadEncrypted.ToolTipText = "Encrypt and upload selected files"
        '
        'btnLocalFileRename
        '
        Me.btnLocalFileRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalFileRename.Image = Global.AirDrive.My.Resources.Resources.filerename
        Me.btnLocalFileRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalFileRename.Name = "btnLocalFileRename"
        Me.btnLocalFileRename.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalFileRename.Text = "Rename selected file"
        '
        'btnLocalFileDelete
        '
        Me.btnLocalFileDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocalFileDelete.Image = CType(resources.GetObject("btnLocalFileDelete.Image"), System.Drawing.Image)
        Me.btnLocalFileDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocalFileDelete.Name = "btnLocalFileDelete"
        Me.btnLocalFileDelete.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalFileDelete.Text = "ToolStripButton10"
        Me.btnLocalFileDelete.ToolTipText = "Delete selected files"
        '
        'lvLocalFiles
        '
        Me.lvLocalFiles.AllowDrop = True
        Me.lvLocalFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvLocalFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvLocalFiles.ContextMenuStrip = Me.cmenu_LocalFiles
        Me.lvLocalFiles.HideSelection = False
        Me.lvLocalFiles.Location = New System.Drawing.Point(3, 28)
        Me.lvLocalFiles.Name = "lvLocalFiles"
        Me.lvLocalFiles.Size = New System.Drawing.Size(522, 183)
        Me.lvLocalFiles.SmallImageList = Me.ilLocalFiles
        Me.lvLocalFiles.TabIndex = 1
        Me.lvLocalFiles.UseCompatibleStateImageBehavior = False
        Me.lvLocalFiles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Filename"
        Me.ColumnHeader1.Width = 365
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Size"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader2.Width = 68
        '
        'cmenu_LocalFiles
        '
        Me.cmenu_LocalFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UploadToolStripMenuItem, Me.UploadEncryptedToolStripMenuItem, Me.ToolStripSeparator4, Me.RenameToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.cmenu_LocalFiles.Name = "cmsLocalFiles"
        Me.cmenu_LocalFiles.Size = New System.Drawing.Size(171, 98)
        '
        'UploadToolStripMenuItem
        '
        Me.UploadToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.upload
        Me.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem"
        Me.UploadToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.UploadToolStripMenuItem.Text = "Upload"
        '
        'UploadEncryptedToolStripMenuItem
        '
        Me.UploadEncryptedToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.upload_encrypted
        Me.UploadEncryptedToolStripMenuItem.Name = "UploadEncryptedToolStripMenuItem"
        Me.UploadEncryptedToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.UploadEncryptedToolStripMenuItem.Text = "Upload Encrypted"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(167, 6)
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.filerename
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.file_x
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ilLocalFiles
        '
        Me.ilLocalFiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ilLocalFiles.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilLocalFiles.TransparentColor = System.Drawing.Color.Transparent
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.tvRemoteDir)
        Me.SplitContainer2.Panel1.Controls.Add(Me.tsRemoteDir)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemoteDir)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.tsRemoteFiles)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lvRemoteFiles)
        Me.SplitContainer2.Size = New System.Drawing.Size(515, 355)
        Me.SplitContainer2.SplitterDistance = 133
        Me.SplitContainer2.SplitterWidth = 8
        Me.SplitContainer2.TabIndex = 10
        '
        'tvRemoteDir
        '
        Me.tvRemoteDir.AllowDrop = True
        Me.tvRemoteDir.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvRemoteDir.ContextMenuStrip = Me.cmenu_RemoteDir
        Me.tvRemoteDir.Enabled = False
        Me.tvRemoteDir.HideSelection = False
        Me.tvRemoteDir.ImageIndex = 0
        Me.tvRemoteDir.ImageList = Me.ilDir
        Me.tvRemoteDir.ItemHeight = 16
        Me.tvRemoteDir.Location = New System.Drawing.Point(3, 45)
        Me.tvRemoteDir.Margin = New System.Windows.Forms.Padding(0)
        Me.tvRemoteDir.Name = "tvRemoteDir"
        Me.tvRemoteDir.PathSeparator = "/"
        Me.tvRemoteDir.SelectedImageIndex = 0
        Me.tvRemoteDir.Size = New System.Drawing.Size(509, 85)
        Me.tvRemoteDir.TabIndex = 11
        '
        'cmenu_RemoteDir
        '
        Me.cmenu_RemoteDir.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadToolStripMenuItem1, Me.ToolStripSeparator7, Me.NewFolderToolStripMenuItem, Me.RenameToolStripMenuItem2, Me.DeleteToolStripMenuItem2, Me.RefreshDisplayToolStripMenuItem})
        Me.cmenu_RemoteDir.Name = "cmsRemoteDir"
        Me.cmenu_RemoteDir.Size = New System.Drawing.Size(161, 120)
        '
        'DownloadToolStripMenuItem1
        '
        Me.DownloadToolStripMenuItem1.Image = Global.AirDrive.My.Resources.Resources.download
        Me.DownloadToolStripMenuItem1.Name = "DownloadToolStripMenuItem1"
        Me.DownloadToolStripMenuItem1.Size = New System.Drawing.Size(160, 22)
        Me.DownloadToolStripMenuItem1.Text = "Download"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(157, 6)
        '
        'NewFolderToolStripMenuItem
        '
        Me.NewFolderToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.folderplus
        Me.NewFolderToolStripMenuItem.Name = "NewFolderToolStripMenuItem"
        Me.NewFolderToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.NewFolderToolStripMenuItem.Text = "New Folder"
        '
        'RenameToolStripMenuItem2
        '
        Me.RenameToolStripMenuItem2.Image = Global.AirDrive.My.Resources.Resources.folderrename
        Me.RenameToolStripMenuItem2.Name = "RenameToolStripMenuItem2"
        Me.RenameToolStripMenuItem2.Size = New System.Drawing.Size(160, 22)
        Me.RenameToolStripMenuItem2.Text = "Rename"
        '
        'DeleteToolStripMenuItem2
        '
        Me.DeleteToolStripMenuItem2.Image = Global.AirDrive.My.Resources.Resources.folder_x
        Me.DeleteToolStripMenuItem2.Name = "DeleteToolStripMenuItem2"
        Me.DeleteToolStripMenuItem2.Size = New System.Drawing.Size(160, 22)
        Me.DeleteToolStripMenuItem2.Text = "Delete"
        '
        'RefreshDisplayToolStripMenuItem
        '
        Me.RefreshDisplayToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.refresh
        Me.RefreshDisplayToolStripMenuItem.Name = "RefreshDisplayToolStripMenuItem"
        Me.RefreshDisplayToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.RefreshDisplayToolStripMenuItem.Text = "Refresh Display"
        '
        'tsRemoteDir
        '
        Me.tsRemoteDir.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsRemoteDir.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRemoteDirDownload, Me.btnRemoteDirNew, Me.btnRemoteDirDelete, Me.btnRemoteDirRename, Me.btnRemoteRefresh})
        Me.tsRemoteDir.Location = New System.Drawing.Point(0, 0)
        Me.tsRemoteDir.Name = "tsRemoteDir"
        Me.tsRemoteDir.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsRemoteDir.Size = New System.Drawing.Size(515, 25)
        Me.tsRemoteDir.TabIndex = 10
        Me.tsRemoteDir.Text = "ToolStrip1"
        '
        'btnRemoteDirDownload
        '
        Me.btnRemoteDirDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteDirDownload.Image = Global.AirDrive.My.Resources.Resources.download
        Me.btnRemoteDirDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteDirDownload.Name = "btnRemoteDirDownload"
        Me.btnRemoteDirDownload.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteDirDownload.Text = "ToolStripButton5"
        Me.btnRemoteDirDownload.ToolTipText = "Download entire directory"
        '
        'btnRemoteDirNew
        '
        Me.btnRemoteDirNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteDirNew.Image = Global.AirDrive.My.Resources.Resources.folderplus
        Me.btnRemoteDirNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteDirNew.Name = "btnRemoteDirNew"
        Me.btnRemoteDirNew.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteDirNew.Text = "ToolStripButton8"
        Me.btnRemoteDirNew.ToolTipText = "Create remote directory"
        '
        'btnRemoteDirDelete
        '
        Me.btnRemoteDirDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteDirDelete.Image = Global.AirDrive.My.Resources.Resources.folder_x
        Me.btnRemoteDirDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteDirDelete.Name = "btnRemoteDirDelete"
        Me.btnRemoteDirDelete.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteDirDelete.Text = "ToolStripButton2"
        Me.btnRemoteDirDelete.ToolTipText = "Delete directory within IMAP array"
        '
        'btnRemoteDirRename
        '
        Me.btnRemoteDirRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteDirRename.Image = Global.AirDrive.My.Resources.Resources.folderrename
        Me.btnRemoteDirRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteDirRename.Name = "btnRemoteDirRename"
        Me.btnRemoteDirRename.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteDirRename.Text = "ToolStripButton1"
        Me.btnRemoteDirRename.ToolTipText = "Rename directory"
        '
        'btnRemoteRefresh
        '
        Me.btnRemoteRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteRefresh.Image = Global.AirDrive.My.Resources.Resources.refresh
        Me.btnRemoteRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteRefresh.Name = "btnRemoteRefresh"
        Me.btnRemoteRefresh.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteRefresh.Text = "ToolStripButton1"
        Me.btnRemoteRefresh.ToolTipText = "Refresh remote file display"
        '
        'txtRemoteDir
        '
        Me.txtRemoteDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemoteDir.BackColor = System.Drawing.SystemColors.Control
        Me.txtRemoteDir.Location = New System.Drawing.Point(3, 25)
        Me.txtRemoteDir.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRemoteDir.Name = "txtRemoteDir"
        Me.txtRemoteDir.ReadOnly = True
        Me.txtRemoteDir.Size = New System.Drawing.Size(509, 20)
        Me.txtRemoteDir.TabIndex = 8
        '
        'tsRemoteFiles
        '
        Me.tsRemoteFiles.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsRemoteFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRemoteFileDownload, Me.btnRemoteFileRename, Me.btnRemoteFileDelete})
        Me.tsRemoteFiles.Location = New System.Drawing.Point(0, 0)
        Me.tsRemoteFiles.Name = "tsRemoteFiles"
        Me.tsRemoteFiles.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsRemoteFiles.Size = New System.Drawing.Size(515, 25)
        Me.tsRemoteFiles.TabIndex = 4
        Me.tsRemoteFiles.Text = "ToolStrip2"
        '
        'btnRemoteFileDownload
        '
        Me.btnRemoteFileDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteFileDownload.Image = Global.AirDrive.My.Resources.Resources.download
        Me.btnRemoteFileDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteFileDownload.Name = "btnRemoteFileDownload"
        Me.btnRemoteFileDownload.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteFileDownload.Text = "ToolStripButton6"
        Me.btnRemoteFileDownload.ToolTipText = "Download selected files"
        '
        'btnRemoteFileRename
        '
        Me.btnRemoteFileRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteFileRename.Image = Global.AirDrive.My.Resources.Resources.filerename
        Me.btnRemoteFileRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteFileRename.Name = "btnRemoteFileRename"
        Me.btnRemoteFileRename.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteFileRename.Text = "ToolStripButton1"
        Me.btnRemoteFileRename.ToolTipText = "Rename file"
        '
        'btnRemoteFileDelete
        '
        Me.btnRemoteFileDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemoteFileDelete.Image = CType(resources.GetObject("btnRemoteFileDelete.Image"), System.Drawing.Image)
        Me.btnRemoteFileDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemoteFileDelete.Name = "btnRemoteFileDelete"
        Me.btnRemoteFileDelete.Size = New System.Drawing.Size(23, 22)
        Me.btnRemoteFileDelete.Text = "ToolStripButton11"
        Me.btnRemoteFileDelete.ToolTipText = "Delete selected files"
        '
        'lvRemoteFiles
        '
        Me.lvRemoteFiles.AllowDrop = True
        Me.lvRemoteFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvRemoteFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvRemoteFilesCHFilename, Me.lvRemoteFilesCHSize})
        Me.lvRemoteFiles.ContextMenuStrip = Me.cmsRemoteFiles
        Me.lvRemoteFiles.HideSelection = False
        Me.lvRemoteFiles.Location = New System.Drawing.Point(3, 28)
        Me.lvRemoteFiles.Name = "lvRemoteFiles"
        Me.lvRemoteFiles.Size = New System.Drawing.Size(509, 183)
        Me.lvRemoteFiles.SmallImageList = Me.ilRemoteFiles
        Me.lvRemoteFiles.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvRemoteFiles.TabIndex = 3
        Me.lvRemoteFiles.UseCompatibleStateImageBehavior = False
        Me.lvRemoteFiles.View = System.Windows.Forms.View.Details
        '
        'lvRemoteFilesCHFilename
        '
        Me.lvRemoteFilesCHFilename.Text = "Filename"
        Me.lvRemoteFilesCHFilename.Width = 334
        '
        'lvRemoteFilesCHSize
        '
        Me.lvRemoteFilesCHSize.Text = "Size"
        Me.lvRemoteFilesCHSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.lvRemoteFilesCHSize.Width = 77
        '
        'cmsRemoteFiles
        '
        Me.cmsRemoteFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadToolStripMenuItem, Me.ToolStripSeparator6, Me.RenameToolStripMenuItem1, Me.DeleteToolStripMenuItem1, Me.PropertiesToolStripMenuItem})
        Me.cmsRemoteFiles.Name = "cmsRemoteFiles"
        Me.cmsRemoteFiles.Size = New System.Drawing.Size(135, 98)
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.download
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.DownloadToolStripMenuItem.Text = "Download"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(131, 6)
        '
        'RenameToolStripMenuItem1
        '
        Me.RenameToolStripMenuItem1.Image = Global.AirDrive.My.Resources.Resources.filerename
        Me.RenameToolStripMenuItem1.Name = "RenameToolStripMenuItem1"
        Me.RenameToolStripMenuItem1.Size = New System.Drawing.Size(134, 22)
        Me.RenameToolStripMenuItem1.Text = "Rename"
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Image = Global.AirDrive.My.Resources.Resources.file_x
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(134, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete"
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.file_props
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.PropertiesToolStripMenuItem.Text = "Properties"
        Me.PropertiesToolStripMenuItem.Visible = False
        '
        'ilRemoteFiles
        '
        Me.ilRemoteFiles.ImageStream = CType(resources.GetObject("ilRemoteFiles.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilRemoteFiles.TransparentColor = System.Drawing.Color.Transparent
        Me.ilRemoteFiles.Images.SetKeyName(0, "lock-icon.png")
        '
        'lblQueueSize
        '
        Me.lblQueueSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblQueueSize.Location = New System.Drawing.Point(770, 78)
        Me.lblQueueSize.Name = "lblQueueSize"
        Me.lblQueueSize.Size = New System.Drawing.Size(266, 13)
        Me.lblQueueSize.TabIndex = 13
        Me.lblQueueSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQueueCount
        '
        Me.lblQueueCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblQueueCount.Location = New System.Drawing.Point(11, 78)
        Me.lblQueueCount.Name = "lblQueueCount"
        Me.lblQueueCount.Size = New System.Drawing.Size(176, 13)
        Me.lblQueueCount.TabIndex = 12
        Me.lblQueueCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvQueue
        '
        Me.lvQueue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvQueue.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvQueueCHLocal, Me.lvQueueCHTransferDirection, Me.lvQueueCHRemote, Me.lvQueueCHFileSize, Me.lvQueueCHStatus})
        Me.lvQueue.ContextMenuStrip = Me.cmsQueueBox
        Me.lvQueue.FullRowSelect = True
        Me.lvQueue.GridLines = True
        Me.lvQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvQueue.Location = New System.Drawing.Point(0, 0)
        Me.lvQueue.Name = "lvQueue"
        Me.lvQueue.Size = New System.Drawing.Size(1047, 75)
        Me.lvQueue.TabIndex = 11
        Me.lvQueue.UseCompatibleStateImageBehavior = False
        Me.lvQueue.View = System.Windows.Forms.View.Details
        '
        'lvQueueCHLocal
        '
        Me.lvQueueCHLocal.Text = "Local File"
        Me.lvQueueCHLocal.Width = 371
        '
        'lvQueueCHTransferDirection
        '
        Me.lvQueueCHTransferDirection.Text = "Direction"
        Me.lvQueueCHTransferDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lvQueueCHTransferDirection.Width = 56
        '
        'lvQueueCHRemote
        '
        Me.lvQueueCHRemote.Text = "Remote File"
        Me.lvQueueCHRemote.Width = 303
        '
        'lvQueueCHFileSize
        '
        Me.lvQueueCHFileSize.Text = "File Size"
        Me.lvQueueCHFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lvQueueCHStatus
        '
        Me.lvQueueCHStatus.Text = "Status"
        Me.lvQueueCHStatus.Width = 105
        '
        'cmsQueueBox
        '
        Me.cmsQueueBox.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveSelectedToolStripMenuItem, Me.cmenu_QueueClear})
        Me.cmsQueueBox.Name = "cmsQueueBox"
        Me.cmsQueueBox.Size = New System.Drawing.Size(178, 48)
        '
        'RemoveSelectedToolStripMenuItem
        '
        Me.RemoveSelectedToolStripMenuItem.Name = "RemoveSelectedToolStripMenuItem"
        Me.RemoveSelectedToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RemoveSelectedToolStripMenuItem.Text = "Remove selected"
        '
        'cmenu_QueueClear
        '
        Me.cmenu_QueueClear.Image = CType(resources.GetObject("cmenu_QueueClear.Image"), System.Drawing.Image)
        Me.cmenu_QueueClear.Name = "cmenu_QueueClear"
        Me.cmenu_QueueClear.Size = New System.Drawing.Size(177, 22)
        Me.cmenu_QueueClear.Text = "Cancel and clear all"
        '
        'txtConsole
        '
        Me.txtConsole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConsole.ContextMenuStrip = Me.cmenu_Console
        Me.txtConsole.Location = New System.Drawing.Point(4, 3)
        Me.txtConsole.Multiline = True
        Me.txtConsole.Name = "txtConsole"
        Me.txtConsole.ReadOnly = True
        Me.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConsole.Size = New System.Drawing.Size(1042, 75)
        Me.txtConsole.TabIndex = 0
        '
        'cmenu_Console
        '
        Me.cmenu_Console.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmenu_QueueSave, Me.ClearConsoleToolStripMenuItem})
        Me.cmenu_Console.Name = "cmsConsole"
        Me.cmenu_Console.ShowImageMargin = False
        Me.cmenu_Console.Size = New System.Drawing.Size(125, 48)
        '
        'cmenu_QueueSave
        '
        Me.cmenu_QueueSave.Name = "cmenu_QueueSave"
        Me.cmenu_QueueSave.Size = New System.Drawing.Size(124, 22)
        Me.cmenu_QueueSave.Text = "Save to File"
        '
        'ClearConsoleToolStripMenuItem
        '
        Me.ClearConsoleToolStripMenuItem.Name = "ClearConsoleToolStripMenuItem"
        Me.ClearConsoleToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ClearConsoleToolStripMenuItem.Text = "Clear console"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1049, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.menu_AddAccount, Me.menu_ManageAccounts, Me.ExportProfileToolStripMenuItem, Me.ToolStripMenuItem1, Me.menu_Profiles, Me.menu_Exit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(167, 6)
        '
        'menu_AddAccount
        '
        Me.menu_AddAccount.Image = CType(resources.GetObject("menu_AddAccount.Image"), System.Drawing.Image)
        Me.menu_AddAccount.Name = "menu_AddAccount"
        Me.menu_AddAccount.Size = New System.Drawing.Size(170, 22)
        Me.menu_AddAccount.Text = "&Add account"
        '
        'menu_ManageAccounts
        '
        Me.menu_ManageAccounts.Image = Global.AirDrive.My.Resources.Resources.bars
        Me.menu_ManageAccounts.Name = "menu_ManageAccounts"
        Me.menu_ManageAccounts.Size = New System.Drawing.Size(170, 22)
        Me.menu_ManageAccounts.Text = "&Manage Accounts"
        '
        'ExportProfileToolStripMenuItem
        '
        Me.ExportProfileToolStripMenuItem.Name = "ExportProfileToolStripMenuItem"
        Me.ExportProfileToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ExportProfileToolStripMenuItem.Text = "Export Profile"
        Me.ExportProfileToolStripMenuItem.Visible = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(167, 6)
        '
        'menu_Profiles
        '
        Me.menu_Profiles.Image = Global.AirDrive.My.Resources.Resources.lock
        Me.menu_Profiles.Name = "menu_Profiles"
        Me.menu_Profiles.Size = New System.Drawing.Size(170, 22)
        Me.menu_Profiles.Text = "&Logout"
        '
        'menu_Exit
        '
        Me.menu_Exit.Name = "menu_Exit"
        Me.menu_Exit.Size = New System.Drawing.Size(170, 22)
        Me.menu_Exit.Text = "E&xit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExplorerViewToolStripMenuItem, Me.SplitViewToolStripMenuItem, Me.ToolStripSeparator2, Me.ShowConsoleToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.ViewToolStripMenuItem.Text = "&View"
        '
        'ExplorerViewToolStripMenuItem
        '
        Me.ExplorerViewToolStripMenuItem.Enabled = False
        Me.ExplorerViewToolStripMenuItem.Name = "ExplorerViewToolStripMenuItem"
        Me.ExplorerViewToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ExplorerViewToolStripMenuItem.Text = "Explorer view"
        '
        'SplitViewToolStripMenuItem
        '
        Me.SplitViewToolStripMenuItem.Checked = True
        Me.SplitViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SplitViewToolStripMenuItem.Name = "SplitViewToolStripMenuItem"
        Me.SplitViewToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SplitViewToolStripMenuItem.Text = "&Split view"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(157, 6)
        '
        'ShowConsoleToolStripMenuItem
        '
        Me.ShowConsoleToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menu_None, Me.menu_Normal, Me.menu_Verbose})
        Me.ShowConsoleToolStripMenuItem.Name = "ShowConsoleToolStripMenuItem"
        Me.ShowConsoleToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.ShowConsoleToolStripMenuItem.Text = "&Display Console"
        '
        'menu_None
        '
        Me.menu_None.Checked = True
        Me.menu_None.CheckOnClick = True
        Me.menu_None.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menu_None.Name = "menu_None"
        Me.menu_None.Size = New System.Drawing.Size(156, 22)
        Me.menu_None.Text = "&None"
        '
        'menu_Normal
        '
        Me.menu_Normal.Name = "menu_Normal"
        Me.menu_Normal.Size = New System.Drawing.Size(156, 22)
        Me.menu_Normal.Text = "Nor&mal"
        '
        'menu_Verbose
        '
        Me.menu_Verbose.Name = "menu_Verbose"
        Me.menu_Verbose.Size = New System.Drawing.Size(156, 22)
        Me.menu_Verbose.Text = "&Verbose (slow)"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.CheckForUpdatesToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.gear
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'CheckForUpdatesToolStripMenuItem
        '
        Me.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem"
        Me.CheckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.CheckForUpdatesToolStripMenuItem.Text = "Check for &updates"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AirDriveHeadquartersToolStripMenuItem, Me.SendFeedbackToolStripMenuItem, Me.DonateToolStripMenuItem, Me.HelpToolStripMenuItem1, Me.ToolStripSeparator5, Me.menu_About})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AirDriveHeadquartersToolStripMenuItem
        '
        Me.AirDriveHeadquartersToolStripMenuItem.Name = "AirDriveHeadquartersToolStripMenuItem"
        Me.AirDriveHeadquartersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.AirDriveHeadquartersToolStripMenuItem.Text = "AirDrive Homepage"
        '
        'SendFeedbackToolStripMenuItem
        '
        Me.SendFeedbackToolStripMenuItem.Image = Global.AirDrive.My.Resources.Resources.envelope_send
        Me.SendFeedbackToolStripMenuItem.Name = "SendFeedbackToolStripMenuItem"
        Me.SendFeedbackToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SendFeedbackToolStripMenuItem.Text = "&Send feedback"
        '
        'DonateToolStripMenuItem
        '
        Me.DonateToolStripMenuItem.Enabled = False
        Me.DonateToolStripMenuItem.Name = "DonateToolStripMenuItem"
        Me.DonateToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DonateToolStripMenuItem.Text = "Donate"
        Me.DonateToolStripMenuItem.Visible = False
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.AirDrive.My.Resources.Resources.questionmark
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
        Me.HelpToolStripMenuItem1.Text = "&Help"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(174, 6)
        '
        'menu_About
        '
        Me.menu_About.Name = "menu_About"
        Me.menu_About.Size = New System.Drawing.Size(177, 22)
        Me.menu_About.Text = "&About"
        '
        'bgwLoadCloudData
        '
        Me.bgwLoadCloudData.WorkerSupportsCancellation = True
        '
        'bgwQueueHandler
        '
        Me.bgwQueueHandler.WorkerReportsProgress = True
        Me.bgwQueueHandler.WorkerSupportsCancellation = True
        '
        'bgwIdle
        '
        Me.bgwIdle.WorkerSupportsCancellation = True
        '
        'bgwCheckUpdates
        '
        Me.bgwCheckUpdates.WorkerSupportsCancellation = True
        '
        'bgwRate
        '
        Me.bgwRate.WorkerSupportsCancellation = True
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1049, 594)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(500, 400)
        Me.Name = "frmMain"
        Me.Text = "AirDrive"
        Me.ToolStripContainer1.BottomToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.BottomToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.scConsoleWrapper.Panel1.ResumeLayout(False)
        Me.scConsoleWrapper.Panel2.ResumeLayout(False)
        Me.scConsoleWrapper.Panel2.PerformLayout()
        Me.scConsoleWrapper.ResumeLayout(False)
        Me.scMainH.Panel1.ResumeLayout(False)
        Me.scMainH.Panel2.ResumeLayout(False)
        Me.scMainH.ResumeLayout(False)
        Me.scFilesMainV.Panel1.ResumeLayout(False)
        Me.scFilesMainV.Panel2.ResumeLayout(False)
        Me.scFilesMainV.ResumeLayout(False)
        Me.scLocalFilesH.Panel1.ResumeLayout(False)
        Me.scLocalFilesH.Panel1.PerformLayout()
        Me.scLocalFilesH.Panel2.ResumeLayout(False)
        Me.scLocalFilesH.Panel2.PerformLayout()
        Me.scLocalFilesH.ResumeLayout(False)
        Me.tsLocalDir.ResumeLayout(False)
        Me.tsLocalDir.PerformLayout()
        Me.cmenu_LocalDir.ResumeLayout(False)
        Me.tsLocalFiles.ResumeLayout(False)
        Me.tsLocalFiles.PerformLayout()
        Me.cmenu_LocalFiles.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.cmenu_RemoteDir.ResumeLayout(False)
        Me.tsRemoteDir.ResumeLayout(False)
        Me.tsRemoteDir.PerformLayout()
        Me.tsRemoteFiles.ResumeLayout(False)
        Me.tsRemoteFiles.PerformLayout()
        Me.cmsRemoteFiles.ResumeLayout(False)
        Me.cmsQueueBox.ResumeLayout(False)
        Me.cmenu_Console.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents lvLocalFiles As System.Windows.Forms.ListView
    Friend WithEvents lvRemoteFiles As System.Windows.Forms.ListView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents txtRemoteDir As System.Windows.Forms.TextBox
    Friend WithEvents bgwLoadCloudData As System.ComponentModel.BackgroundWorker
    Friend WithEvents scFilesMainV As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents scMainH As System.Windows.Forms.SplitContainer
    Friend WithEvents scLocalFilesH As System.Windows.Forms.SplitContainer
    Friend WithEvents lvQueue As System.Windows.Forms.ListView
    Friend WithEvents lvQueueCHLocal As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvQueueCHTransferDirection As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvQueueCHRemote As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvQueueCHFileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvQueueCHStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmsQueueBox As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmenu_QueueClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tvLocalDir As System.Windows.Forms.TreeView
    Friend WithEvents tsLocalFiles As System.Windows.Forms.ToolStrip
    Friend WithEvents tsLocalDir As System.Windows.Forms.ToolStrip
    Friend WithEvents tsRemoteFiles As System.Windows.Forms.ToolStrip
    Friend WithEvents tsRemoteDir As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRemoteRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoteDirDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalDirDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalDirUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoteDirDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoteFileDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalDirNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalFileUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoteDirNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalFileDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoteFileDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tspbStatus As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsslStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgwQueueHandler As System.ComponentModel.BackgroundWorker
    Friend WithEvents tvRemoteDir As System.Windows.Forms.TreeView
    Friend WithEvents lvRemoteFilesCHFilename As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvRemoteFilesCHSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents ilLocalFiles As System.Windows.Forms.ImageList
    Friend WithEvents ilDir As System.Windows.Forms.ImageList
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtLocalDir As System.Windows.Forms.TextBox
    Friend WithEvents ilRemoteFiles As System.Windows.Forms.ImageList
    Friend WithEvents cmenu_LocalDir As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsLocalDirUpload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenu_LocalDirNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsLocalDirDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenu_LocalDirRename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_AddAccount As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_ManageAccounts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menu_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendFeedbackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DonateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExplorerViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bgwIdle As System.ComponentModel.BackgroundWorker
    Friend WithEvents menu_Profiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents scConsoleWrapper As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowConsoleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_None As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_Normal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menu_Verbose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtConsole As System.Windows.Forms.TextBox
    Friend WithEvents bgwCheckUpdates As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblRate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents bgwRate As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnLocalRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmenu_LocalFiles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenu_RemoteDir As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DownloadToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshDisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsRemoteFiles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmenu_Console As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmenu_QueueSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearConsoleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UploadEncryptedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UploadEncryptedToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnLocalDirUploadEncrypted As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalFileUploadEncrypted As System.Windows.Forms.ToolStripButton
    Friend WithEvents RemoveSelectedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnLocalFileRename As System.Windows.Forms.ToolStripButton
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRemoteFileRename As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRemoteDirRename As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocalDirRename As System.Windows.Forms.ToolStripButton
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckForUpdatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblQueueCount As System.Windows.Forms.Label
    Friend WithEvents lblQueueSize As System.Windows.Forms.Label
    Friend WithEvents AirDriveHeadquartersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsslCancelProcess As System.Windows.Forms.ToolStripStatusLabel
End Class
