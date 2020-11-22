using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sede_Checker.Abstract.Interfaces;
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
    public partial class TaskForm : Form
    {
        private readonly IStorageService<SedeAppDto> _ss;

        private TasksDataService _tds;

        private bool isCreateMode;

        private readonly TableConverter TC = new TableConverter();

        private SedeTaskData u;

        private List<SedeTaskDataDto> UsersList;

        private SpainRegionCollection _regions;

        public TaskForm(IStorageService<SedeAppDto> storageService)
        {
            InitializeComponent();

            _ss = storageService;
            
            _tds = new TasksDataService(_ss);

            this._regions = this.GetRegions();
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            //UpdateTable();
            UpdateDataTable();

            u = new SedeTaskData();

            u.ProcedureRegionList = _regions;

            pgTaskData.SelectedObject = u;

            pgTaskData.Enabled = false;
            btnSaveChanges.Enabled = false;
        }

        private void newTaskBtn_Click(object sender, EventArgs e)
        {
            u = new SedeTaskData();

            u.ProcedureRegionList = _regions;
            pgTaskData.SelectedObject = u;

            if (!pgTaskData.Enabled)
                ChangeEditFieldState();
            isCreateMode = !isCreateMode;

            pgTaskData.Focus();
        }

        private void editTaskBtn_Click(object sender, EventArgs e)
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
                
                var selectedObject = (SedeTaskData)dgwSeeTasks.CurrentRow.DataBoundItem;

                pgTaskData.SelectedObject = selectedObject;
                UpdateDataTable();
            }
            else
            {
                MessageBox.Show("Hey, you should select just a one row to edit it!");
            }
        }

        private void rmBtn_Click(object sender, EventArgs e)
        {
            var r = dgwSeeTasks.CurrentRow;

            if (ReferenceEquals(r, null)) return;

            var so = (SedeTaskData)r.DataBoundItem;

            if (MessageBox.Show(null, $"Are you sure want to remove {so.CustomerNameAndSurname} from the tasks list?",
                    "Action confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            this._tds.Delete(so.TaskGuid);
            
            UpdateDataTable();
        }

        private void btnUnlockToEdit_Click(object sender, EventArgs e)
        {
            ChangeEditFieldState();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            var data = pgTaskData.SelectedObject as SedeTaskData;

            if(ReferenceEquals(data, null)) return;

            MessageBox.Show(this._tds.AddOrUpdate(data)
                ? $"{data.CustomerNameAndSurname} successfully created/updated!"
                : $"{data.CustomerNameAndSurname} can't create/update");

            UpdateDataTable();
        }

        private void UpdateDataTable()
        {
            try
            {
                var data = _tds.GetObjects();

                if(ReferenceEquals(data, null)) return;

                foreach (var v in data)
                {
                    v.ProcedureRegionList = _regions;
                }
                
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

        public void UpdateTableStyle()
        {
            if(this.dgwSeeTasks.Rows.Count.Equals(0)) return;

            const int taskStatusRow = 8;

            foreach (DataGridViewRow row in this.dgwSeeTasks.Rows)
            {
                var r = row.Cells[taskStatusRow].Value.ToString();

                var font = "Microsoft Sans Serif";
                var fontSize = 8;

                switch ((TaskStatus)Enum.Parse(typeof(TaskStatus),r))
                {
                    case TaskStatus.INPROGRESS:
                        row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        break;
                    case TaskStatus.POSTPONED:
                        row.DefaultCellStyle.BackColor = Color.Gray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                        break;
                    case TaskStatus.DONE:
                        row.DefaultCellStyle.BackColor = Color.DarkGreen;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Regular);
                        break;
                    case TaskStatus.NA:
                        row.DefaultCellStyle.BackColor = Color.FloralWhite;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Strikeout);
                        break;
                    case TaskStatus.WRONG:
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Yellow;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Underline);
                        break;
                    case TaskStatus.CITAAVAILABLE:
                        row.DefaultCellStyle.BackColor = Color.DarkGreen;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Italic);
                        break;
                    case TaskStatus.REMOVEAVAILABLECITA:
                        row.DefaultCellStyle.BackColor = Color.Firebrick;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Strikeout);
                        break;
                    case TaskStatus.EXCEPTION:
                        row.DefaultCellStyle.BackColor = Color.DarkMagenta;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.Font = new Font(font, fontSize, FontStyle.Underline);
                        break;
                    default:break;
                }
            }
        }

        
        
        
       
       
      



       


        private SpainRegionCollection GetRegions()
        {
            var madridProcedures = new SpainRegionProcedureCollection {
                new SpainRegionProcedure(Guid.Parse("536B1CD4-E06B-439F-8E9F-0DE07B6C9E04"), "Despliegue para ver trámites disponibles en esta provincia"),
                new SpainRegionProcedure(Guid.Parse("840C87ED-0AB9-4BCE-A8F2-36B82E8E9FD4"), "ASILO - OFICINA DE ASILO Y REFUGIO. Entrevista Trabajador/ a Social.Calle Pradillo"),
                new SpainRegionProcedure(Guid.Parse("D5432583-709C-49D9-AE0C-A3436349A550"), "ASILO - OFICINA DE ASILO Y REFUGIO. Expedición / Renovación de Documentos. Calle Pradillo"),
                new SpainRegionProcedure(Guid.Parse("C9444C3C-8FAC-4740-9139-E0E886B5526F"), "ASILO - OFICINA DE ASILO Y REFUGIO. Información / Entrega de Documentación. Calle Pradillo"),
                new SpainRegionProcedure(Guid.Parse("FC49984C-DB8A-4EAC-B5E4-48D781F9752D"), "AUT.DE RESIDENCIA TEMPORAL POR CIRCUNS.EXCEPCIONALES POR ARRAIGO"),
                new SpainRegionProcedure(Guid.Parse("00357E9D-3F06-417B-8745-56C80684C2BF"), "AUT.RES.TEMPORAL POR CIRCUNSTANCIAS EXCEPCIONALES POR RAZONES HUMANITARIAS"),
                new SpainRegionProcedure(Guid.Parse("6BE288E3-D69E-425A-9EEE-4AD79F3F9F19"), "AUTORIZACIÓN DE RESIDENCIA DE MENORES NACIDOS EN ESPAÑA, HIJOS DE RESIDENTES"),
                new SpainRegionProcedure(Guid.Parse("B064B67D-9133-4407-A6B1-E25658489143"), "AUTORIZACIONES DE TRABAJO POR ESTUDIOS"),
                new SpainRegionProcedure(Guid.Parse("A8955262-E3EB-46E8-A42F-EB7EEE8976B3"), "FAMILIARES DE RESIDENTES COMUNITARIOS"),
                new SpainRegionProcedure(Guid.Parse("540BAE0D-AA31-444D-9BB9-0B95855C5511"), "INFORMACIÓN"),
                new SpainRegionProcedure(Guid.Parse("FF9443EE-F270-42ED-B044-9E6BD1D90412"), "POLICIA - ASIGNACIÓN DE NIE"),
                new SpainRegionProcedure(Guid.Parse("DE48AA80-5D0C-4674-A935-81F0E571401D"), "POLICIA - AUTORIZACIONES DE REGRESO"),
                new SpainRegionProcedure(Guid.Parse("B5E535E3-71A2-49C0-9DE6-99608E70F76F"), "POLICIA - CERTIFICADOS(DE RESIDENCIA, DE NO RESIDENCIA Y DE CONCORDANCIA)"),
                new SpainRegionProcedure(Guid.Parse("1294B114-E4E3-4B82-AE04-F330B9B1E66C"), "POLICIA - CERTIFICADOS UE"),
                new SpainRegionProcedure(Guid.Parse("021F45BB-3650-4076-8E44-B2A647709815"), "POLICIA - DUPLICADO DE TARJETA DE IDENTIDAD DE EXTRANJERO"),
                new SpainRegionProcedure(Guid.Parse("9E1BCCB1-BAF1-47E0-80FA-952A158A6CB1"), "POLICÍA - EXPEDICIÓN DE TARJETAS CUYA AUTORIZACIÓN RESUELVE LA DIRECCIÓN GENERAL DE MIGRACIONES"),
                new SpainRegionProcedure(Guid.Parse("AA929B36-7CC0-42F3-8B48-BB5819F34F03"), "POLICIA - TOMA DE HUELLAS(EXPEDICIÓN DE TARJETA) Y RENOVACIÓN DE TARJETA DE LARGA DURACIÓN"),
                new SpainRegionProcedure(Guid.Parse("A6458258-A137-4C6D-8086-A7B0D480D469"), "PRORROGA DE ESTANCIA POR ESTUDIOS"),
                new SpainRegionProcedure(Guid.Parse("8935DF04-C54E-41EB-8175-0028A7194BC7"), "REAGRUPACIÓN FAMILIAR"),
                new SpainRegionProcedure(Guid.Parse("7BD1BA33-E121-4091-91C2-40FF9CA43A64"), "Recuperación de la autorización de larga duración"),
                new SpainRegionProcedure(Guid.Parse("5948445A-6B7F-45CC-8315-CCB5075B1220"), "Resid.Menores NO nacidos en España"),
                new SpainRegionProcedure(Guid.Parse("8D443A34-9F8B-4586-8BE0-4B6EA32CFE8A"), "TRABAJO Y RESIDENCIA INICIAL POR CUENTA AJENA"),
                new SpainRegionProcedure(Guid.Parse("30DE697A-B077-45A3-8754-9BDBAB7DEED8"), "TRABAJO Y RESIDENCIA INICIAL POR CUENTA PROPIA")
            };
            
            var illesBalearsProcedures = new SpainRegionProcedureCollection {
                new SpainRegionProcedure(Guid.Parse("28A334B6-89B8-4EF1-B717-803CC04E9871"), "INFORMACIÓN GENERAL - IBIZA"),
                new SpainRegionProcedure(Guid.Parse("2ADFFBC4-C494-49D3-9068-94E7C0EF77AE"), "POLICIA - ASIGNACIÓN DE NIE"),
                new SpainRegionProcedure(Guid.Parse("9798135D-6ACE-4C90-83A5-9E105EC73B4F"), "POLICIA - CERTIFICADOS UE"),
                new SpainRegionProcedure(Guid.Parse("466F1032-2794-4043-BE80-2A9AE6F722A1"), "POLICIA - TOMA DE HUELLAS(EXPEDICIÓN DE TARJETA) Y RENOVACIÓN DE TARJETA DE LARGA DURACIÓN"),
                new SpainRegionProcedure(Guid.Parse("C30FC942-9653-4D18-95EE-DEA044105418"), "SOLICITUD AUTORIZACIONES - IBIZA"),
                new SpainRegionProcedure(Guid.Parse("AF022EA3-F310-4CA1-8C58-32BBF5EB67C2"), "SOLICITUD AUTORIZACIONES - MALLORCA")
            };

            var c = new SpainRegionCollection
            {
                //new SpainRegion(Guid.Parse("EC52980B-BDBC-4A4B-8F74-1FE23333DBEC"), "A Coruña"),
                //new SpainRegion(Guid.Parse("CE241A95-D927-46A2-A6B2-B2990C25B095"), "Albacete"),
                //new SpainRegion(Guid.Parse("863081FF-CEF8-4603-8CDC-9200BB893656"), "Alicante"),
                //new SpainRegion(Guid.Parse("A0B4AD34-D05E-4BBA-B186-E1F9601188BB"), "Almeria"),
                //new SpainRegion(Guid.Parse("3916369E-94DD-467B-B9F5-D1449629C1A9"), "Araba"),
                //new SpainRegion(Guid.Parse("1BF10AD4-5EE4-4DDC-8F56-3BEC94BCBE37"), "Asturias"),
                //new SpainRegion(Guid.Parse("DE092C4A-E157-48C1-BEC5-E61EE912834D"), "Badajoz"),
                //new SpainRegion(Guid.Parse("A1048E9C-3E67-4DBD-86DC-7618A4153EE5"), "Barcelona"),
                //new SpainRegion(Guid.Parse("FFE0B38A-8A6C-45A9-B1AD-4650390275D7"), "Bizkaia"),
                //new SpainRegion(Guid.Parse("21FF460B-66DF-4874-9E00-CEA688E9BD57"), "Cáceres"),
                //new SpainRegion(Guid.Parse("44214841-5C0A-4100-B975-3D0752B16505"), "Cantabria"),
                //new SpainRegion(Guid.Parse("F8374B77-A283-4357-A826-391BD5A842A5"), "Castellon"),
                //new SpainRegion(Guid.Parse("62122163-0F4B-4CA0-AF3D-03C8176A340A"), "Ceuta"),
                //new SpainRegion(Guid.Parse("48D99692-7A03-46AE-BC79-8B052B8A8F41"), "Ciudad Real"),
                //new SpainRegion(Guid.Parse("AD19A9E6-2130-4763-BFF9-15A354C7C1DB"), "Cordoba"),
                //new SpainRegion(Guid.Parse("647887BC-FA61-43F8-B082-89CF939F595B"), "Cuenca"),
                //new SpainRegion(Guid.Parse("B156C591-E41D-4151-8243-827079598F9B"), "Girona"),
                //new SpainRegion(Guid.Parse("E926FC04-E373-4CA3-BC53-42F3048ED89A"), "Granada"),
                //new SpainRegion(Guid.Parse("C9B41578-CEF9-442E-A4BD-3B7C07A2EC5C"), "Guadalajara"),
                //new SpainRegion(Guid.Parse("EE648B27-AFA6-47B7-83D4-E2B46D6B00D3"), "Guipúzcoa"),
                //new SpainRegion(Guid.Parse("5DFD1C3E-BB9F-450D-BB69-6E93EEDA0195"), "Huelva"),
                //new SpainRegion(Guid.Parse("2EBFD771-7E75-44A0-A510-5D953C7DD7A3"), "Huesca"),
                new SpainRegion(Guid.Parse("DC663972-2972-492A-A6D6-82F8A4431CD5"), "Illes Balears", illesBalearsProcedures),
                //new SpainRegion(Guid.Parse("95D2051F-BA68-47C5-AE75-D4A638AB9438"), "Jaen"),
                //new SpainRegion(Guid.Parse("082BB66C-CE69-40CA-A86E-E6FE9A4419D6"), "Las Palmas"),
                //new SpainRegion(Guid.Parse("F2C89D19-F8A4-4A02-BE2E-0707DAD075FA"), "León"),
                //new SpainRegion(Guid.Parse("4F3DE250-5397-4762-A930-4A82AD5865E7"), "Lleida"),
                //new SpainRegion(Guid.Parse("3EE8E568-FB20-4841-89DA-07229AC8A7C6"), "Logroño"),
                new SpainRegion(Guid.Parse("5C3E5118-1A80-4DF4-8DCC-B8E5617B3DC1"), "Madrid", madridProcedures),
                //new SpainRegion(Guid.Parse("DD9D16D5-2024-4CAA-8BFB-D8581BB585E1"), "Malaga"),
                //new SpainRegion(Guid.Parse("640E8F0E-0547-4903-B677-BCC3A3488291"), "Melilla"),
                //new SpainRegion(Guid.Parse("442A357B-56E2-41C8-B76A-78C398BF0264"), "Murcia"),
                //new SpainRegion(Guid.Parse("B21D4BAF-F5F1-49A5-8C56-2081A234E19C"), "Navarra"),
                //new SpainRegion(Guid.Parse("B832D2A5-BC6E-4FF0-91EC-B1D93EAF9A2D"), "Orense"),
                //new SpainRegion(Guid.Parse("787ECDC7-7DC3-4CC0-8375-B0AD8E2B8639"), "S.Cruz Tenerife"),
                //new SpainRegion(Guid.Parse("CCE80906-1A42-4341-9E25-2250430D756A"), "Salamanca"),
                //new SpainRegion(Guid.Parse("8377C828-648A-4CD5-AB30-03941B5847AF"), "Sevilla"),
                //new SpainRegion(Guid.Parse("81FCF43D-B3DF-4FDD-BF7C-01C7FA192D67"), "Tarragona"),
                //new SpainRegion(Guid.Parse("642234F2-C414-49AD-B1F2-3D8827BC8D25"), "Teruel"),
                //new SpainRegion(Guid.Parse("9D6F19A5-FB8A-440B-AD08-1A98C697D267"), "Toledo"),
                //new SpainRegion(Guid.Parse("7F714B67-7009-4C69-AFD0-90FA7860791E"), "Valencia"),
                //new SpainRegion(Guid.Parse("D203391C-4308-48C1-ADCC-1E2883E5E0C6"), "Zamora"),
                //new SpainRegion(Guid.Parse("5EC96FD3-B99B-4B6C-B553-566BD312D3C6"), "Zaragoza")
            };
            return c;
        }

        private void ChangeEditFieldState()
        {
            pgTaskData.Enabled = !pgTaskData.Enabled;
            btnSaveChanges.Enabled = !btnSaveChanges.Enabled;
            btnUnlockToEdit.Text = pgTaskData.Enabled ? "Lock" : "Unlock to edit";
        }

        private void dgwSeeTasks_SelectionChanged(object sender, EventArgs e)
        {
            var r = dgwSeeTasks.CurrentRow;

            if (ReferenceEquals(r, null)) return;

            var so = (SedeTaskData)r.DataBoundItem;
            this.pgTaskData.SelectedObject = so;
        }

        private void dgwSeeTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            this.UpdateTableStyle();
        }
    }
}