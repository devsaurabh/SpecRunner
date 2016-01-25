namespace NSpecRunner.GUI
{
    partial class SpecRunner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Assemblies");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecRunner));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTestCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SpecTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeSpecs = new System.Windows.Forms.TreeView();
            this.TreeSpecContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UnloadAssemblyContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunSpecsContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pbTestProgress = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunSelected = new System.Windows.Forms.Button();
            this.lblSelectedNode = new System.Windows.Forms.Label();
            this.lblTestDetail = new System.Windows.Forms.Label();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabErrors = new System.Windows.Forms.TabPage();
            this.lstErrors = new System.Windows.Forms.ListView();
            this.header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabTextView = new System.Windows.Forms.TabPage();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AddAssemblyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAssemblyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SpecTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.TreeSpecContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabErrors.SuspendLayout();
            this.tabTextView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFileName,
            this.toolStripStatusLabel1,
            this.lblTestCount,
            this.lblTotalErrors});
            this.statusBar.Location = new System.Drawing.Point(0, 470);
            this.statusBar.Margin = new System.Windows.Forms.Padding(10);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(843, 22);
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblFileName
            // 
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(724, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblTestCount
            // 
            this.lblTestCount.AutoSize = false;
            this.lblTestCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTestCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblTestCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblTestCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblTestCount.Name = "lblTestCount";
            this.lblTestCount.Size = new System.Drawing.Size(100, 22);
            // 
            // lblTotalErrors
            // 
            this.lblTotalErrors.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTotalErrors.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblTotalErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblTotalErrors.Name = "lblTotalErrors";
            this.lblTotalErrors.Size = new System.Drawing.Size(4, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SpecTabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(843, 446);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 5;
            // 
            // SpecTabs
            // 
            this.SpecTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.SpecTabs.Controls.Add(this.tabPage1);
            this.SpecTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecTabs.Location = new System.Drawing.Point(0, 0);
            this.SpecTabs.Multiline = true;
            this.SpecTabs.Name = "SpecTabs";
            this.SpecTabs.SelectedIndex = 0;
            this.SpecTabs.Size = new System.Drawing.Size(279, 446);
            this.SpecTabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.treeSpecs);
            this.tabPage1.Location = new System.Drawing.Point(23, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(252, 438);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tests";
            // 
            // treeSpecs
            // 
            this.treeSpecs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeSpecs.ContextMenuStrip = this.TreeSpecContextMenu;
            this.treeSpecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSpecs.HideSelection = false;
            this.treeSpecs.ImageKey = "grey";
            this.treeSpecs.ImageList = this.imageList1;
            this.treeSpecs.Location = new System.Drawing.Point(3, 3);
            this.treeSpecs.Name = "treeSpecs";
            treeNode1.ImageKey = "grey";
            treeNode1.Name = "Assemblies";
            treeNode1.SelectedImageKey = "grey";
            treeNode1.Text = "Assemblies";
            this.treeSpecs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeSpecs.SelectedImageKey = "grey";
            this.treeSpecs.Size = new System.Drawing.Size(242, 428);
            this.treeSpecs.StateImageList = this.imageList1;
            this.treeSpecs.TabIndex = 0;
            this.treeSpecs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSpecs_AfterSelect);
            this.treeSpecs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeSpecs_MouseClick);
            // 
            // TreeSpecContextMenu
            // 
            this.TreeSpecContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UnloadAssemblyContextMenuItem,
            this.RunSpecsContextMenu});
            this.TreeSpecContextMenu.Name = "TreeSpecContextMenu";
            this.TreeSpecContextMenu.ShowImageMargin = false;
            this.TreeSpecContextMenu.Size = new System.Drawing.Size(142, 48);
            // 
            // UnloadAssemblyContextMenuItem
            // 
            this.UnloadAssemblyContextMenuItem.Name = "UnloadAssemblyContextMenuItem";
            this.UnloadAssemblyContextMenuItem.Size = new System.Drawing.Size(141, 22);
            this.UnloadAssemblyContextMenuItem.Text = "Unload Assembly";
            this.UnloadAssemblyContextMenuItem.Click += new System.EventHandler(this.CloseAssemblyMenuItem_Click);
            // 
            // RunSpecsContextMenu
            // 
            this.RunSpecsContextMenu.Name = "RunSpecsContextMenu";
            this.RunSpecsContextMenu.Size = new System.Drawing.Size(141, 22);
            this.RunSpecsContextMenu.Text = "Run Specs";
            this.RunSpecsContextMenu.Click += new System.EventHandler(this.btnRunSelected_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "red");
            this.imageList1.Images.SetKeyName(1, "green");
            this.imageList1.Images.SetKeyName(2, "grey");
            this.imageList1.Images.SetKeyName(3, "disabled.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.splitContainer2.Size = new System.Drawing.Size(560, 446);
            this.splitContainer2.SplitterDistance = 214;
            this.splitContainer2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtStackTrace, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(560, 214);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.pbTestProgress, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTestDetail, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(548, 125);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // pbTestProgress
            // 
            this.pbTestProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTestProgress.Location = new System.Drawing.Point(5, 46);
            this.pbTestProgress.Margin = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.pbTestProgress.MarqueeAnimationSpeed = 0;
            this.pbTestProgress.Name = "pbTestProgress";
            this.pbTestProgress.Size = new System.Drawing.Size(533, 31);
            this.pbTestProgress.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRunSelected);
            this.flowLayoutPanel1.Controls.Add(this.lblSelectedNode);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(542, 35);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnRunSelected
            // 
            this.btnRunSelected.Location = new System.Drawing.Point(3, 3);
            this.btnRunSelected.Name = "btnRunSelected";
            this.btnRunSelected.Size = new System.Drawing.Size(75, 32);
            this.btnRunSelected.TabIndex = 0;
            this.btnRunSelected.Text = "&Run";
            this.btnRunSelected.UseVisualStyleBackColor = true;
            this.btnRunSelected.Click += new System.EventHandler(this.btnRunSelected_Click);
            // 
            // lblSelectedNode
            // 
            this.lblSelectedNode.AutoSize = true;
            this.lblSelectedNode.Location = new System.Drawing.Point(84, 10);
            this.lblSelectedNode.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblSelectedNode.Name = "lblSelectedNode";
            this.lblSelectedNode.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedNode.TabIndex = 2;
            // 
            // lblTestDetail
            // 
            this.lblTestDetail.AutoSize = true;
            this.lblTestDetail.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestDetail.Location = new System.Drawing.Point(5, 87);
            this.lblTestDetail.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.lblTestDetail.Name = "lblTestDetail";
            this.lblTestDetail.Size = new System.Drawing.Size(0, 16);
            this.lblTestDetail.TabIndex = 2;
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.BackColor = System.Drawing.SystemColors.Window;
            this.txtStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStackTrace.Location = new System.Drawing.Point(3, 153);
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.Size = new System.Drawing.Size(554, 58);
            this.txtStackTrace.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabErrors);
            this.tabControl1.Controls.Add(this.tabTextView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 228);
            this.tabControl1.TabIndex = 0;
            // 
            // tabErrors
            // 
            this.tabErrors.Controls.Add(this.lstErrors);
            this.tabErrors.Location = new System.Drawing.Point(4, 22);
            this.tabErrors.Name = "tabErrors";
            this.tabErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrors.Size = new System.Drawing.Size(547, 202);
            this.tabErrors.TabIndex = 0;
            this.tabErrors.Text = "Errors";
            this.tabErrors.UseVisualStyleBackColor = true;
            // 
            // lstErrors
            // 
            this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header,
            this.detail});
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErrors.FullRowSelect = true;
            this.lstErrors.GridLines = true;
            this.lstErrors.Location = new System.Drawing.Point(3, 3);
            this.lstErrors.MultiSelect = false;
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.ShowItemToolTips = true;
            this.lstErrors.Size = new System.Drawing.Size(541, 196);
            this.lstErrors.TabIndex = 0;
            this.lstErrors.UseCompatibleStateImageBehavior = false;
            this.lstErrors.View = System.Windows.Forms.View.Details;
            this.lstErrors.Click += new System.EventHandler(this.lstErrors_Click);
            // 
            // header
            // 
            this.header.Text = "Example";
            this.header.Width = 100;
            // 
            // detail
            // 
            this.detail.Text = "Error Message";
            this.detail.Width = 600;
            // 
            // tabTextView
            // 
            this.tabTextView.Controls.Add(this.txtResult);
            this.tabTextView.Location = new System.Drawing.Point(4, 22);
            this.tabTextView.Name = "tabTextView";
            this.tabTextView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTextView.Size = new System.Drawing.Size(547, 202);
            this.tabTextView.TabIndex = 1;
            this.tabTextView.Text = "Text View";
            this.tabTextView.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.ForeColor = System.Drawing.Color.Black;
            this.txtResult.Location = new System.Drawing.Point(3, 3);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(541, 196);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 446);
            this.panel1.TabIndex = 8;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.HelpMenuStrip});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(843, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProjectMenuItem,
            this.ReloadMenuItem,
            this.CloseProjectMenuItem,
            this.toolStripSeparator2,
            this.AddAssemblyMenuItem,
            this.CloseAssemblyMenuItem,
            this.toolStripSeparator3,
            this.SaveProjectMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator4,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // OpenProjectMenuItem
            // 
            this.OpenProjectMenuItem.Name = "OpenProjectMenuItem";
            this.OpenProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenProjectMenuItem.Size = new System.Drawing.Size(220, 22);
            this.OpenProjectMenuItem.Text = "&Open Project";
            this.OpenProjectMenuItem.Click += new System.EventHandler(this.OpenProjectMenuItem_Click);
            // 
            // ReloadMenuItem
            // 
            this.ReloadMenuItem.Name = "ReloadMenuItem";
            this.ReloadMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ReloadMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ReloadMenuItem.Text = "&Reload";
            this.ReloadMenuItem.Click += new System.EventHandler(this.ReloadMenuItem_Click);
            // 
            // CloseProjectMenuItem
            // 
            this.CloseProjectMenuItem.Name = "CloseProjectMenuItem";
            this.CloseProjectMenuItem.Size = new System.Drawing.Size(220, 22);
            this.CloseProjectMenuItem.Text = "Close Project";
            this.CloseProjectMenuItem.Click += new System.EventHandler(this.CloseProjectMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // AddAssemblyMenuItem
            // 
            this.AddAssemblyMenuItem.Name = "AddAssemblyMenuItem";
            this.AddAssemblyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.AddAssemblyMenuItem.Size = new System.Drawing.Size(220, 22);
            this.AddAssemblyMenuItem.Text = "&Add Spec Assembly";
            this.AddAssemblyMenuItem.Click += new System.EventHandler(this.AddAssemblyMenuItem_Click);
            // 
            // CloseAssemblyMenuItem
            // 
            this.CloseAssemblyMenuItem.Name = "CloseAssemblyMenuItem";
            this.CloseAssemblyMenuItem.Size = new System.Drawing.Size(220, 22);
            this.CloseAssemblyMenuItem.Text = "&Close Spec Assembly";
            this.CloseAssemblyMenuItem.Click += new System.EventHandler(this.CloseAssemblyMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // SaveProjectMenuItem
            // 
            this.SaveProjectMenuItem.Name = "SaveProjectMenuItem";
            this.SaveProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveProjectMenuItem.Size = new System.Drawing.Size(220, 22);
            this.SaveProjectMenuItem.Text = "&Save Project";
            this.SaveProjectMenuItem.Click += new System.EventHandler(this.SaveProjectMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.SaveAsMenuItem.Size = new System.Drawing.Size(220, 22);
            this.SaveAsMenuItem.Text = "Save Project As";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(217, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ExitMenuItem.Text = "&Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // HelpMenuStrip
            // 
            this.HelpMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuStrip});
            this.HelpMenuStrip.Name = "HelpMenuStrip";
            this.HelpMenuStrip.Size = new System.Drawing.Size(44, 20);
            this.HelpMenuStrip.Text = "&Help";
            // 
            // AboutMenuStrip
            // 
            this.AboutMenuStrip.Name = "AboutMenuStrip";
            this.AboutMenuStrip.Size = new System.Drawing.Size(107, 22);
            this.AboutMenuStrip.Text = "&About";
            this.AboutMenuStrip.Click += new System.EventHandler(this.AboutMenuStrip_Click);
            // 
            // SpecRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 492);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "SpecRunner";
            this.Text = "Spec Runner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpecRunner_FormClosing);
            this.Load += new System.EventHandler(this.SpecRunner_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.SpecTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.TreeSpecContextMenu.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabErrors.ResumeLayout(false);
            this.tabTextView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeSpecs;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.ToolStripStatusLabel lblFileName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel lblTestCount;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalErrors;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabErrors;
        private System.Windows.Forms.TabPage tabTextView;
        private System.Windows.Forms.ListView lstErrors;
        private System.Windows.Forms.ColumnHeader header;
        private System.Windows.Forms.ColumnHeader detail;
        private System.Windows.Forms.TabControl SpecTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtStackTrace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ProgressBar pbTestProgress;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRunSelected;
        private System.Windows.Forms.Label lblSelectedNode;
        private System.Windows.Forms.Label lblTestDetail;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddAssemblyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAssemblyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem SaveProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ContextMenuStrip TreeSpecContextMenu;
        private System.Windows.Forms.ToolStripMenuItem UnloadAssemblyContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunSpecsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenProjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseProjectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ReloadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuStrip;
    }
}

