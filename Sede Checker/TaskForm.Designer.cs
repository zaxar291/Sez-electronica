using Sede_Checker.Entities.DTO;

namespace Sede_Checker
{
    partial class TaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.pgTaskData = new System.Windows.Forms.PropertyGrid();
            this.dgwSeeTasks = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newTaskBtn = new System.Windows.Forms.ToolStripButton();
            this.editTaskBtn = new System.Windows.Forms.ToolStripButton();
            this.rmBtn = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbProperty = new System.Windows.Forms.GroupBox();
            this.btnUnlockToEdit = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.customerNameAndSurnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerDateOfBirthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documentTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documentNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procedureRegionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procedureCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tieExpiredDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.citaIdentificationNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isCitaReceivedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCreatedDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sedeTaskDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSeeTasks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sedeTaskDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pgTaskData
            // 
            this.pgTaskData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTaskData.Location = new System.Drawing.Point(6, 19);
            this.pgTaskData.Name = "pgTaskData";
            this.pgTaskData.Size = new System.Drawing.Size(266, 465);
            this.pgTaskData.TabIndex = 0;
            // 
            // dgwSeeTasks
            // 
            this.dgwSeeTasks.AutoGenerateColumns = false;
            this.dgwSeeTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwSeeTasks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwSeeTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSeeTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerNameAndSurnameDataGridViewTextBoxColumn,
            this.customerDateOfBirthDataGridViewTextBoxColumn,
            this.documentTypeDataGridViewTextBoxColumn,
            this.documentNumberDataGridViewTextBoxColumn,
            this.countryDataGridViewTextBoxColumn,
            this.procedureRegionDataGridViewTextBoxColumn,
            this.procedureCityDataGridViewTextBoxColumn,
            this.tieExpiredDateDataGridViewTextBoxColumn,
            this.taskStatusDataGridViewTextBoxColumn,
            this.citaIdentificationNumberDataGridViewTextBoxColumn,
            this.isCitaReceivedDataGridViewTextBoxColumn,
            this.taskCreatedDateTimeDataGridViewTextBoxColumn});
            this.dgwSeeTasks.DataSource = this.sedeTaskDataBindingSource;
            this.dgwSeeTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwSeeTasks.Location = new System.Drawing.Point(3, 16);
            this.dgwSeeTasks.Name = "dgwSeeTasks";
            this.dgwSeeTasks.ReadOnly = true;
            this.dgwSeeTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSeeTasks.Size = new System.Drawing.Size(927, 500);
            this.dgwSeeTasks.TabIndex = 1;
            this.dgwSeeTasks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwSeeTasks_CellFormatting);
            this.dgwSeeTasks.SelectionChanged += new System.EventHandler(this.dgwSeeTasks_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTaskBtn,
            this.editTaskBtn,
            this.rmBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1260, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newTaskBtn
            // 
            this.newTaskBtn.Image = global::Sede_Checker.Properties.Resources.Bid_icon_icons_com_53709;
            this.newTaskBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTaskBtn.Name = "newTaskBtn";
            this.newTaskBtn.Size = new System.Drawing.Size(98, 36);
            this.newTaskBtn.Text = "NEW TASK";
            this.newTaskBtn.Click += new System.EventHandler(this.newTaskBtn_Click);
            // 
            // editTaskBtn
            // 
            this.editTaskBtn.Image = ((System.Drawing.Image)(resources.GetObject("editTaskBtn.Image")));
            this.editTaskBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTaskBtn.Name = "editTaskBtn";
            this.editTaskBtn.Size = new System.Drawing.Size(88, 36);
            this.editTaskBtn.Text = "Edit Task";
            this.editTaskBtn.Visible = false;
            this.editTaskBtn.Click += new System.EventHandler(this.editTaskBtn_Click);
            // 
            // rmBtn
            // 
            this.rmBtn.Image = global::Sede_Checker.Properties.Resources.Dead_Link_icon_icons_com_53721;
            this.rmBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rmBtn.Name = "rmBtn";
            this.rmBtn.Size = new System.Drawing.Size(110, 36);
            this.rmBtn.Text = "DELETE TASK";
            this.rmBtn.Click += new System.EventHandler(this.rmBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgwSeeTasks);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 519);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tasks";
            // 
            // gbProperty
            // 
            this.gbProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProperty.Controls.Add(this.btnUnlockToEdit);
            this.gbProperty.Controls.Add(this.btnSaveChanges);
            this.gbProperty.Controls.Add(this.pgTaskData);
            this.gbProperty.Location = new System.Drawing.Point(3, 3);
            this.gbProperty.Name = "gbProperty";
            this.gbProperty.Size = new System.Drawing.Size(278, 519);
            this.gbProperty.TabIndex = 4;
            this.gbProperty.TabStop = false;
            this.gbProperty.Text = "Task Properties";
            // 
            // btnUnlockToEdit
            // 
            this.btnUnlockToEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnlockToEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUnlockToEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnlockToEdit.Location = new System.Drawing.Point(6, 490);
            this.btnUnlockToEdit.Name = "btnUnlockToEdit";
            this.btnUnlockToEdit.Size = new System.Drawing.Size(141, 23);
            this.btnUnlockToEdit.TabIndex = 2;
            this.btnUnlockToEdit.Text = "UNLOCK TO EDIT";
            this.btnUnlockToEdit.UseVisualStyleBackColor = true;
            this.btnUnlockToEdit.Click += new System.EventHandler(this.btnUnlockToEdit_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveChanges.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveChanges.Location = new System.Drawing.Point(153, 490);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(119, 23);
            this.btnSaveChanges.TabIndex = 1;
            this.btnSaveChanges.Text = "SAVE CHANGES";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbProperty);
            this.splitContainer1.Size = new System.Drawing.Size(1239, 565);
            this.splitContainer1.SplitterDistance = 939;
            this.splitContainer1.TabIndex = 5;
            // 
            // customerNameAndSurnameDataGridViewTextBoxColumn
            // 
            this.customerNameAndSurnameDataGridViewTextBoxColumn.DataPropertyName = "CustomerNameAndSurname";
            this.customerNameAndSurnameDataGridViewTextBoxColumn.HeaderText = "Name & Surname";
            this.customerNameAndSurnameDataGridViewTextBoxColumn.Name = "customerNameAndSurnameDataGridViewTextBoxColumn";
            this.customerNameAndSurnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // customerDateOfBirthDataGridViewTextBoxColumn
            // 
            this.customerDateOfBirthDataGridViewTextBoxColumn.DataPropertyName = "CustomerDateOfBirth";
            this.customerDateOfBirthDataGridViewTextBoxColumn.HeaderText = "Date of birth";
            this.customerDateOfBirthDataGridViewTextBoxColumn.Name = "customerDateOfBirthDataGridViewTextBoxColumn";
            this.customerDateOfBirthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // documentTypeDataGridViewTextBoxColumn
            // 
            this.documentTypeDataGridViewTextBoxColumn.DataPropertyName = "DocumentType";
            this.documentTypeDataGridViewTextBoxColumn.HeaderText = "Document Type";
            this.documentTypeDataGridViewTextBoxColumn.Name = "documentTypeDataGridViewTextBoxColumn";
            this.documentTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // documentNumberDataGridViewTextBoxColumn
            // 
            this.documentNumberDataGridViewTextBoxColumn.DataPropertyName = "DocumentNumber";
            this.documentNumberDataGridViewTextBoxColumn.HeaderText = "Serie and Number";
            this.documentNumberDataGridViewTextBoxColumn.Name = "documentNumberDataGridViewTextBoxColumn";
            this.documentNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countryDataGridViewTextBoxColumn
            // 
            this.countryDataGridViewTextBoxColumn.DataPropertyName = "Country";
            this.countryDataGridViewTextBoxColumn.HeaderText = "Nationality";
            this.countryDataGridViewTextBoxColumn.Name = "countryDataGridViewTextBoxColumn";
            this.countryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // procedureRegionDataGridViewTextBoxColumn
            // 
            this.procedureRegionDataGridViewTextBoxColumn.DataPropertyName = "ProcedureRegion";
            this.procedureRegionDataGridViewTextBoxColumn.HeaderText = "Region in Spain";
            this.procedureRegionDataGridViewTextBoxColumn.Name = "procedureRegionDataGridViewTextBoxColumn";
            this.procedureRegionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // procedureCityDataGridViewTextBoxColumn
            // 
            this.procedureCityDataGridViewTextBoxColumn.DataPropertyName = "ProcedureCity";
            this.procedureCityDataGridViewTextBoxColumn.HeaderText = "City or Street (optional)";
            this.procedureCityDataGridViewTextBoxColumn.Name = "procedureCityDataGridViewTextBoxColumn";
            this.procedureCityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tieExpiredDateDataGridViewTextBoxColumn
            // 
            this.tieExpiredDateDataGridViewTextBoxColumn.DataPropertyName = "TieExpiredDate";
            this.tieExpiredDateDataGridViewTextBoxColumn.HeaderText = "TIE expired date";
            this.tieExpiredDateDataGridViewTextBoxColumn.Name = "tieExpiredDateDataGridViewTextBoxColumn";
            this.tieExpiredDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskStatusDataGridViewTextBoxColumn
            // 
            this.taskStatusDataGridViewTextBoxColumn.DataPropertyName = "TaskStatus";
            this.taskStatusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.taskStatusDataGridViewTextBoxColumn.Name = "taskStatusDataGridViewTextBoxColumn";
            this.taskStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // citaIdentificationNumberDataGridViewTextBoxColumn
            // 
            this.citaIdentificationNumberDataGridViewTextBoxColumn.DataPropertyName = "CitaIdentificationNumber";
            this.citaIdentificationNumberDataGridViewTextBoxColumn.HeaderText = "Cita Number";
            this.citaIdentificationNumberDataGridViewTextBoxColumn.Name = "citaIdentificationNumberDataGridViewTextBoxColumn";
            this.citaIdentificationNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isCitaReceivedDataGridViewTextBoxColumn
            // 
            this.isCitaReceivedDataGridViewTextBoxColumn.DataPropertyName = "IsCitaReceived";
            this.isCitaReceivedDataGridViewTextBoxColumn.HeaderText = "Cita Status";
            this.isCitaReceivedDataGridViewTextBoxColumn.Name = "isCitaReceivedDataGridViewTextBoxColumn";
            this.isCitaReceivedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskCreatedDateTimeDataGridViewTextBoxColumn
            // 
            this.taskCreatedDateTimeDataGridViewTextBoxColumn.DataPropertyName = "TaskCreatedDateTime";
            this.taskCreatedDateTimeDataGridViewTextBoxColumn.HeaderText = "Task Created";
            this.taskCreatedDateTimeDataGridViewTextBoxColumn.Name = "taskCreatedDateTimeDataGridViewTextBoxColumn";
            this.taskCreatedDateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sedeTaskDataBindingSource
            // 
            this.sedeTaskDataBindingSource.DataSource = typeof(Sede_Checker.TaskFormParams.Properties.SedeTaskData);
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 552);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TASKS | SEA PROJECT";
            this.Load += new System.EventHandler(this.TaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSeeTasks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbProperty.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sedeTaskDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgTaskData;
        private System.Windows.Forms.DataGridView dgwSeeTasks;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newTaskBtn;
        private System.Windows.Forms.ToolStripButton editTaskBtn;
        private System.Windows.Forms.ToolStripButton rmBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbProperty;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnUnlockToEdit;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerNameAndSurnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerDateOfBirthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn documentTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn documentNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn procedureRegionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn procedureCityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tieExpiredDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn citaIdentificationNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isCitaReceivedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCreatedDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sedeTaskDataBindingSource;
    }
}