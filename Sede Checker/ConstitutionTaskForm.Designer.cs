namespace Sede_Checker
{
    partial class ConstitutionTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConstitutionTaskForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgwSeeTasks = new System.Windows.Forms.DataGridView();
            this.CustomerNameAndSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NieNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerDateBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskCreatedDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnFieldsState = new System.Windows.Forms.Button();
            this.pgTaskData = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newTaskBtn = new System.Windows.Forms.ToolStripButton();
            this.rmBtn = new System.Windows.Forms.ToolStripButton();
            this.editTaskBtn = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSeeTasks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgwSeeTasks);
            this.splitContainer1.Panel1.Controls.Add(this.splitter1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveChanges);
            this.splitContainer1.Panel2.Controls.Add(this.btnFieldsState);
            this.splitContainer1.Panel2.Controls.Add(this.pgTaskData);
            this.splitContainer1.Size = new System.Drawing.Size(1304, 506);
            this.splitContainer1.SplitterDistance = 985;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgwSeeTasks
            // 
            this.dgwSeeTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSeeTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerNameAndSurname,
            this.NieNumber,
            this.DateRequest,
            this.CustomerDateBirth,
            this.TaskStatus,
            this.TaskCreatedDateTime});
            this.dgwSeeTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwSeeTasks.Location = new System.Drawing.Point(3, 0);
            this.dgwSeeTasks.Name = "dgwSeeTasks";
            this.dgwSeeTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSeeTasks.Size = new System.Drawing.Size(982, 506);
            this.dgwSeeTasks.TabIndex = 1;
            this.dgwSeeTasks.SelectionChanged += new System.EventHandler(this.DgwSeeTasks_SelectionChanged);
            // 
            // CustomerNameAndSurname
            // 
            this.CustomerNameAndSurname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CustomerNameAndSurname.DataPropertyName = "CustomerNameAndSurname";
            this.CustomerNameAndSurname.HeaderText = "Name & surname";
            this.CustomerNameAndSurname.Name = "CustomerNameAndSurname";
            this.CustomerNameAndSurname.ReadOnly = true;
            // 
            // NieNumber
            // 
            this.NieNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NieNumber.DataPropertyName = "NIENumber";
            this.NieNumber.HeaderText = "Nie number";
            this.NieNumber.Name = "NieNumber";
            this.NieNumber.ReadOnly = true;
            // 
            // DateRequest
            // 
            this.DateRequest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateRequest.DataPropertyName = "DateRequest";
            this.DateRequest.HeaderText = "Date of request";
            this.DateRequest.Name = "DateRequest";
            this.DateRequest.ReadOnly = true;
            // 
            // CustomerDateBirth
            // 
            this.CustomerDateBirth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CustomerDateBirth.DataPropertyName = "CustomerDateBirth";
            this.CustomerDateBirth.HeaderText = "Date of birth";
            this.CustomerDateBirth.Name = "CustomerDateBirth";
            this.CustomerDateBirth.ReadOnly = true;
            // 
            // TaskStatus
            // 
            this.TaskStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TaskStatus.DataPropertyName = "TaskStatus";
            this.TaskStatus.HeaderText = "Status";
            this.TaskStatus.Name = "TaskStatus";
            this.TaskStatus.ReadOnly = true;
            // 
            // TaskCreatedDateTime
            // 
            this.TaskCreatedDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TaskCreatedDateTime.DataPropertyName = "TaskCreatedDateTime";
            this.TaskCreatedDateTime.HeaderText = "Task created";
            this.TaskCreatedDateTime.Name = "TaskCreatedDateTime";
            this.TaskCreatedDateTime.ReadOnly = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 506);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveChanges.Location = new System.Drawing.Point(162, 463);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(150, 31);
            this.btnSaveChanges.TabIndex = 2;
            this.btnSaveChanges.Text = "Save changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.BtnSaveChanges_Click);
            // 
            // btnFieldsState
            // 
            this.btnFieldsState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFieldsState.Location = new System.Drawing.Point(3, 463);
            this.btnFieldsState.Name = "btnFieldsState";
            this.btnFieldsState.Size = new System.Drawing.Size(158, 31);
            this.btnFieldsState.TabIndex = 1;
            this.btnFieldsState.Text = "Unlock to edit";
            this.btnFieldsState.UseVisualStyleBackColor = true;
            this.btnFieldsState.Click += new System.EventHandler(this.BtnFieldsState_Click);
            // 
            // pgTaskData
            // 
            this.pgTaskData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTaskData.Location = new System.Drawing.Point(3, 3);
            this.pgTaskData.Name = "pgTaskData";
            this.pgTaskData.Size = new System.Drawing.Size(309, 454);
            this.pgTaskData.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTaskBtn,
            this.rmBtn,
            this.editTaskBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1304, 39);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newTaskBtn
            // 
            this.newTaskBtn.Image = global::Sede_Checker.Properties.Resources.Bid_icon_icons_com_53709;
            this.newTaskBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTaskBtn.Name = "newTaskBtn";
            this.newTaskBtn.Size = new System.Drawing.Size(98, 36);
            this.newTaskBtn.Text = "NEW TASK";
            this.newTaskBtn.Click += new System.EventHandler(this.NewTaskBtn_Click);
            // 
            // rmBtn
            // 
            this.rmBtn.Image = global::Sede_Checker.Properties.Resources.Dead_Link_icon_icons_com_53721;
            this.rmBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rmBtn.Name = "rmBtn";
            this.rmBtn.Size = new System.Drawing.Size(110, 36);
            this.rmBtn.Text = "DELETE TASK";
            this.rmBtn.Click += new System.EventHandler(this.RmBtn_Click);
            // 
            // editTaskBtn
            // 
            this.editTaskBtn.Image = ((System.Drawing.Image)(resources.GetObject("editTaskBtn.Image")));
            this.editTaskBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTaskBtn.Name = "editTaskBtn";
            this.editTaskBtn.Size = new System.Drawing.Size(88, 36);
            this.editTaskBtn.Text = "Edit Task";
            this.editTaskBtn.Visible = false;
            this.editTaskBtn.Click += new System.EventHandler(this.EditTaskBtn_Click);
            // 
            // ConstitutionTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 549);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ConstitutionTaskForm";
            this.Text = "CONSTITUTION TASKS | SEA PROJECT";
            this.Load += new System.EventHandler(this.ConstitutionTaskForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSeeTasks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DataGridView dgwSeeTasks;
        private System.Windows.Forms.PropertyGrid pgTaskData;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnFieldsState;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newTaskBtn;
        private System.Windows.Forms.ToolStripButton editTaskBtn;
        private System.Windows.Forms.ToolStripButton rmBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNameAndSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn NieNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerDateBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskCreatedDateTime;
    }
}