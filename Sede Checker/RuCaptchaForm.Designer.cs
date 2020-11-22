namespace Sede_Checker
{
    partial class RuCaptchaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuCaptchaForm));
            this.dgwRucaptcha = new System.Windows.Forms.DataGridView();
            this.ssRuCaptcha = new System.Windows.Forms.StatusStrip();
            this.tsslBalanceValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssmBalanceIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.captchaStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.captchaDateSendDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.captchaSolutionSecondsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sedeRuCaptchaDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgwRucaptcha)).BeginInit();
            this.ssRuCaptcha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sedeRuCaptchaDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwRucaptcha
            // 
            this.dgwRucaptcha.AllowUserToAddRows = false;
            this.dgwRucaptcha.AllowUserToDeleteRows = false;
            this.dgwRucaptcha.AllowUserToOrderColumns = true;
            this.dgwRucaptcha.AutoGenerateColumns = false;
            this.dgwRucaptcha.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwRucaptcha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRucaptcha.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.captchaStatusDataGridViewTextBoxColumn,
            this.captchaDateSendDataGridViewTextBoxColumn,
            this.captchaSolutionSecondsDataGridViewTextBoxColumn});
            this.dgwRucaptcha.DataSource = this.sedeRuCaptchaDataBindingSource;
            this.dgwRucaptcha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwRucaptcha.Location = new System.Drawing.Point(0, 0);
            this.dgwRucaptcha.Margin = new System.Windows.Forms.Padding(6);
            this.dgwRucaptcha.Name = "dgwRucaptcha";
            this.dgwRucaptcha.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwRucaptcha.Size = new System.Drawing.Size(1682, 983);
            this.dgwRucaptcha.TabIndex = 0;
            this.dgwRucaptcha.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwRucaptcha_CellFormatting);
            // 
            // ssRuCaptcha
            // 
            this.ssRuCaptcha.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ssRuCaptcha.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssmBalanceIcon,
            this.tsslBalanceValue});
            this.ssRuCaptcha.Location = new System.Drawing.Point(0, 946);
            this.ssRuCaptcha.Name = "ssRuCaptcha";
            this.ssRuCaptcha.Size = new System.Drawing.Size(1682, 37);
            this.ssRuCaptcha.TabIndex = 1;
            this.ssRuCaptcha.Text = "statusStrip1";
            // 
            // tsslBalanceValue
            // 
            this.tsslBalanceValue.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.tsslBalanceValue.Name = "tsslBalanceValue";
            this.tsslBalanceValue.Size = new System.Drawing.Size(113, 32);
            this.tsslBalanceValue.Text = "NO DATA";
            // 
            // tssmBalanceIcon
            // 
            this.tssmBalanceIcon.Image = global::Sede_Checker.Properties.Resources.Pay_per_Click__PPC__icon_icons_com_53742;
            this.tssmBalanceIcon.Name = "tssmBalanceIcon";
            this.tssmBalanceIcon.Size = new System.Drawing.Size(32, 32);
            // 
            // captchaStatusDataGridViewTextBoxColumn
            // 
            this.captchaStatusDataGridViewTextBoxColumn.DataPropertyName = "CaptchaStatus";
            this.captchaStatusDataGridViewTextBoxColumn.HeaderText = "STATUS";
            this.captchaStatusDataGridViewTextBoxColumn.Name = "captchaStatusDataGridViewTextBoxColumn";
            // 
            // captchaDateSendDataGridViewTextBoxColumn
            // 
            this.captchaDateSendDataGridViewTextBoxColumn.DataPropertyName = "CaptchaDateSend";
            this.captchaDateSendDataGridViewTextBoxColumn.HeaderText = "SEND DATE TIME";
            this.captchaDateSendDataGridViewTextBoxColumn.Name = "captchaDateSendDataGridViewTextBoxColumn";
            // 
            // captchaSolutionSecondsDataGridViewTextBoxColumn
            // 
            this.captchaSolutionSecondsDataGridViewTextBoxColumn.DataPropertyName = "CaptchaSolutionSeconds";
            this.captchaSolutionSecondsDataGridViewTextBoxColumn.HeaderText = "ANSWER TAKES, SECONDS";
            this.captchaSolutionSecondsDataGridViewTextBoxColumn.Name = "captchaSolutionSecondsDataGridViewTextBoxColumn";
            this.captchaSolutionSecondsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sedeRuCaptchaDataBindingSource
            // 
            this.sedeRuCaptchaDataBindingSource.DataSource = typeof(Sede_Checker.RucapthcaFormParams.Entities.SedeRuCaptchaData);
            // 
            // RuCaptchaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1682, 983);
            this.Controls.Add(this.ssRuCaptcha);
            this.Controls.Add(this.dgwRucaptcha);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "RuCaptchaForm";
            this.Text = "RuCaptcha Console | SEA PROJECT";
            ((System.ComponentModel.ISupportInitialize)(this.dgwRucaptcha)).EndInit();
            this.ssRuCaptcha.ResumeLayout(false);
            this.ssRuCaptcha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sedeRuCaptchaDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwRucaptcha;
        private System.Windows.Forms.StatusStrip ssRuCaptcha;
        private System.Windows.Forms.ToolStripStatusLabel tssmBalanceIcon;
        private System.Windows.Forms.ToolStripStatusLabel tsslBalanceValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn captchaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn captchaStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn captchaDateSendDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn captchaSolutionSecondsDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sedeRuCaptchaDataBindingSource;
    }
}