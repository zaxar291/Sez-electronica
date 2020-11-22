namespace Sede_Checker
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslProxy = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslProxyValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslProxy,
            this.tsslProxyValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 966);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1556, 38);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslProxy
            // 
            this.tsslProxy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsslProxy.Image = global::Sede_Checker.Properties.Resources.Server_icon_icons_com_53749;
            this.tsslProxy.Name = "tsslProxy";
            this.tsslProxy.Size = new System.Drawing.Size(32, 33);
            this.tsslProxy.Text = "PROXY:";
            // 
            // tsslProxyValue
            // 
            this.tsslProxyValue.Name = "tsslProxyValue";
            this.tsslProxyValue.Size = new System.Drawing.Size(28, 33);
            this.tsslProxyValue.Text = "0";
            this.tsslProxyValue.ToolTipText = "Proxy addresses count";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.windowsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1556, 40);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "msMainMenu";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataDirectoryToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.tsmiClose});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(81, 36);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // openDataDirectoryToolStripMenuItem
            // 
            this.openDataDirectoryToolStripMenuItem.Image = global::Sede_Checker.Properties.Resources.Directory_icon_icons_com_53726;
            this.openDataDirectoryToolStripMenuItem.Name = "openDataDirectoryToolStripMenuItem";
            this.openDataDirectoryToolStripMenuItem.Size = new System.Drawing.Size(333, 38);
            this.openDataDirectoryToolStripMenuItem.Text = "Open Data Directory";
            this.openDataDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDataDirectoryToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Sede_Checker.Properties.Resources.API_icon_icons_com_53706;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(333, 38);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(330, 6);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Image = global::Sede_Checker.Properties.Resources.Ban_icon_icons_com_53710;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(333, 38);
            this.tsmiClose.Text = "Close";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.leftToRightToolStripMenuItem,
            this.topToBottomToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.groupToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(124, 36);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // leftToRightToolStripMenuItem
            // 
            this.leftToRightToolStripMenuItem.Name = "leftToRightToolStripMenuItem";
            this.leftToRightToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.leftToRightToolStripMenuItem.Text = "Tile Vertical";
            this.leftToRightToolStripMenuItem.Click += new System.EventHandler(this.leftToRightToolStripMenuItem_Click);
            // 
            // topToBottomToolStripMenuItem
            // 
            this.topToBottomToolStripMenuItem.Name = "topToBottomToolStripMenuItem";
            this.topToBottomToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.topToBottomToolStripMenuItem.Text = "Top to bottom";
            this.topToBottomToolStripMenuItem.Click += new System.EventHandler(this.topToBottomToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.closeAllToolStripMenuItem.Text = "Close all";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(268, 38);
            this.groupToolStripMenuItem.Text = "Arange Icons";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::Sede_Checker.Properties.Resources.изображение_viber_2019_06_15_11_43_09;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1556, 1004);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SEDE ELECTRONICA AUTOMATIC CITA CATCHER | SEA PROJECT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripStatusLabel tsslProxy;
        private System.Windows.Forms.ToolStripStatusLabel tsslProxyValue;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}