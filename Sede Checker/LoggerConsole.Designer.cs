namespace Sede_Checker
{
    partial class LoggerConsole
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerConsole));
            this.ConsoleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuCopyToClickBoardButton = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConsoleContextMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConsoleContextMenu
            // 
            this.ConsoleContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ConsoleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuCopyToClickBoardButton});
            this.ConsoleContextMenu.Name = "ConsoleContextMenu";
            this.ConsoleContextMenu.Size = new System.Drawing.Size(312, 40);
            // 
            // ContextMenuCopyToClickBoardButton
            // 
            this.ContextMenuCopyToClickBoardButton.Name = "ContextMenuCopyToClickBoardButton";
            this.ContextMenuCopyToClickBoardButton.Size = new System.Drawing.Size(311, 36);
            this.ContextMenuCopyToClickBoardButton.Text = "Copy all to clipboard";
            this.ContextMenuCopyToClickBoardButton.Click += new System.EventHandler(this.ContextMenuCopyToClickBoardButton_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.richTextBox.ContextMenuStrip = this.ConsoleContextMenu;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(1216, 865);
            this.richTextBox.TabIndex = 3;
            this.richTextBox.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 827);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1216, 38);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(238, 33);
            this.tsslStatus.Text = "toolStripStatusLabel1";
            // 
            // LoggerConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1216, 865);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.richTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "LoggerConsole";
            this.Opacity = 0.5D;
            this.Text = "SYSTEM LOGS | SEA PROJECT";
            this.Load += new System.EventHandler(this.SedeMainForm_Load);
            this.ConsoleContextMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ConsoleContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuCopyToClickBoardButton;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
    }
}

