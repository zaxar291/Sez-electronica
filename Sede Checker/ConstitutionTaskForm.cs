using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.ConstitutionTaskFormParams.Properties;
using Sede_Checker.Entities.Collections.RegionProcedures;
using Sede_Checker.Entities.Collections.Regions;
using Sede_Checker.Entities.Converters;
using Sede_Checker.Entities.DTO;
using Sede_Checker.Enums;
using Sede_Checker.Implementation.Services.Tasks;
using Sede_Checker.TaskFormParams.converters;
using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker
{
    public partial class ConstitutionTaskForm : Form
    {

        private readonly IStorageService<SedeAppDto> _ss;

        private ConstitutionTaskDataService _tds;

        private List<SedeConstitutionDto> _us;

        private ConstitutionTaskData u;

        private bool isCreateMode;

        public ConstitutionTaskForm(IStorageService<SedeAppDto> storageService)
        {
            InitializeComponent();

            _ss = storageService;

            _tds = new ConstitutionTaskDataService(_ss);

        }

        private void ConstitutionTaskForm_Load(object sender, EventArgs e)
        {
            UpdateDataTable();

            u = new ConstitutionTaskData();

            pgTaskData.SelectedObject = u;

            pgTaskData.Enabled = false;
            btnSaveChanges.Enabled = false;
        }

        private void UpdateDataTable()
        {
            try
            {
                var data = _tds.GetObjects();

                if (ReferenceEquals(data, null)) return;
                
                this.dgwSeeTasks.AutoGenerateColumns = false;
                this.dgwSeeTasks.DataSource = data;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JsonException {ex.Message}");
                Close();
                return;
            }
        }

        private void BtnFieldsState_Click(object sender, EventArgs e)
        {
            ChangeEditFieldState();
        }

        private void ChangeEditFieldState()
        {
            pgTaskData.Enabled = !pgTaskData.Enabled;
            btnSaveChanges.Enabled = !btnSaveChanges.Enabled;
            btnFieldsState.Text = pgTaskData.Enabled ? "Lock" : "Unlock to edit";
        }

        private void BtnSaveChanges_Click(object sender, EventArgs e)
        {
            var data = pgTaskData.SelectedObject as ConstitutionTaskData;

            if (ReferenceEquals(data, null)) return;

            MessageBox.Show(this._tds.AddOrUpdate(data)
                ? $"{data.CustomerNameAndSurname} successfully created/updated!"
                : $"{data.CustomerNameAndSurname} can't create/update");

            UpdateDataTable();
        }

        private void NewTaskBtn_Click(object sender, EventArgs e)
        {
            u = new ConstitutionTaskData();

            pgTaskData.SelectedObject = u;

            if (!pgTaskData.Enabled)
                ChangeEditFieldState();
            isCreateMode = !isCreateMode;

            pgTaskData.Focus();
        }

        private void DgwSeeTasks_SelectionChanged(object sender, EventArgs e)
        {
            var r = dgwSeeTasks.CurrentRow;

            if (ReferenceEquals(r, null)) return;

            var so = (ConstitutionTaskData)r.DataBoundItem;
            this.pgTaskData.SelectedObject = so;
        }

        private void RmBtn_Click(object sender, EventArgs e)
        {
            var r = dgwSeeTasks.CurrentRow;

            if (ReferenceEquals(r, null)) return;

            var so = (ConstitutionTaskData)r.DataBoundItem;

            if (MessageBox.Show(null, $"Are you sure want to remove {so.CustomerNameAndSurname} from the tasks list?",
                    "Action confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            this._tds.Delete(so.TaskGuid);

            UpdateDataTable();
        }

        private void EditTaskBtn_Click(object sender, EventArgs e)
        {
            if (isCreateMode)
                isCreateMode = !isCreateMode;

            if (dgwSeeTasks.SelectedRows.Count > 0)
            {
                if (dgwSeeTasks.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Hey, we can't edit more than one user in a one time!");
                    return;
                }

                var selectedObject = (ConstitutionTaskData)dgwSeeTasks.CurrentRow.DataBoundItem;

                pgTaskData.SelectedObject = selectedObject;
                UpdateDataTable();
            }
            else
            {
                MessageBox.Show("Hey, you should select just a one row to edit it!");
            }
        }
    }
}
