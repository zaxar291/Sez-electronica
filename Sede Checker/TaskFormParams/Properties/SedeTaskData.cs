using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sede_Checker.Entities.Collections.RegionProcedures;
using Sede_Checker.Entities.Collections.Regions;
using Sede_Checker.Enums;

namespace Sede_Checker.TaskFormParams.Properties
{
    public class SedeTaskData
    {
        #region General Task Information Group
        [Browsable(false)]
        [ReadOnly(true)]
        [Category("General")]
        [Description("Task GUID")]
        [DisplayName("Task GUID")]
        [Display(Order = 1)]
        public Guid TaskGuid { get; set; }

        [Browsable(true)]
        [ReadOnly(true)]
        [Category("General")]
        [Description("Task created date & time")]
        [DisplayName("Created")]
        [Display(Order = 2)]
        public DateTime TaskCreatedDateTime { get; set; }
        
        [Browsable(true)]
        [Category("General")]
        [Description("Type some task notes here")]
        [DisplayName("Task Notes")]
        [Display(Order = 3)]
        public string TaskNotes { get; set; }

        [Browsable(true)]
        [Category("General")]
        [Description("Task status")]
        [DisplayName("Status")]
        [Display(Order = 3)]
        public TaskStatus TaskStatus { get; set; }
        #endregion 

        #region Procedure Location Group
        [Browsable(true)]
        [Category("Procedure")]
        [Description("Enter user neadable province")]
        [DisplayName("Region in Spain")]
        [Display(Order = 4)]
        public string ProcedureRegion { get; set; }

        #region New Region & Procedure Selection
        //todo: We need to add it to DTO separate field and transfer data. Then we can remove old fields!!!
        [Browsable(true)]
        [Category("Procedure V2")]
        [Description("Enter user neadable province")]
        [DisplayName("Region in Spain")]
        [Display(Order = 4)]
        [TypeConverter(typeof(SpainRegionTypeConverter))]
        public SpainRegion ProcedureSpainRegion { set; get; }

        [Browsable(true)]
        [Category("Procedure V2")]
        [Description("Select procedure in region")]
        [DisplayName("Procedure in region")]
        [Display(Order = 5)]
        [TypeConverter(typeof(SpainRegionProcedureTypeConverter))]
        public SpainRegionProcedure ProcedureSpainRegionProcedures { set; get; }

        [Browsable(false)]
        [Category("Procedure V2")]
        [Description("Enter user neadable province")]
        [DisplayName("Regions in Spain")]
        [Display(Order = 4)]
        [TypeConverter(typeof(SpainRegionCollectionConverter))]
        public SpainRegionCollection ProcedureRegionList { set; get; }
        #endregion

        [Browsable(true)]
        [Category("Procedure")]
        [Description("For Barcelona region we need to insert neadable tramite")]
        [DisplayName("Tramite in Barcelona region")]
        [Display(Order = 4)]
        public string ProcedureTramite { get; set; }

        [Browsable(true)]
        [Category("Procedure")]
        [Description("Type city or street name")]
        [DisplayName("City or Street (optional)")]
        [Display(Order = 5)]
        public string ProcedureCity { get; set; }
        
        [Browsable(true)]
        [Category("Procedure")]
        [Description("Enter procedure for province")]
        [DisplayName("Enter a full procedure name for province")]
        [Display(Order = 6)]
        public string ProcedureName { get; set; }

        [Browsable(true)]
        [Category("Procedure")]
        [Description("For some procedures (NIE), it is necessary to indicate the reason for applying for permission or number")]
        [DisplayName("Reason")]
        [Display(Order = 7)]
        public string ProcedureReason { get; set; }
    
        #endregion
        
        #region Customer infromation
        [Browsable(true)]
        [Category("Customer information")]
        [Description("Insert customer name and surname")]
        [DisplayName("Name & Surname")]
        [Display(Order = 8)]
        public string CustomerNameAndSurname { get; set; }

        [Browsable(true)]
        [Category("Customer information")]
        [Description("Insert customer date of birth")]
        [DisplayName("Date of birth")]
        [Display(Order = 9)]
        public DateTime CustomerDateOfBirth { get; set; }

        [Browsable(true)]
        [Category("Customer information")]
        [Description("Insert customer mother country")]
        [DisplayName("Nationality")]
        [Display(Order = 10)]
        public string Country { get; set; }
        #endregion

        #region Contacts Group
        [Browsable(true)]
        [Category("Customer contacts")]
        [Description("Type customer spanish phone number")]
        [DisplayName("Phone number (+34)")]
        [Display(Order = 11)]
        public string CustomerPhoneNumber { get; set; }

        [Browsable(true)]
        [Category("Customer contacts")]
        [Description("Type customer email")]
        [DisplayName("Email")]
        [Display(Order = 12)]
        public string CustomerEmail { get; set; }
        #endregion

        #region Document Group
        [Browsable(true)]
        [Category("Document")]
        [Description("Enter a expired date of customer available residential card (Tarjeta de identidad de extranjero) (use '/' instead of '.')")]
        [DisplayName("TIE expired date")]
        [Display(Order = 13)]
        public DateTime TieExpiredDate { get; set; }

        [Browsable(true)]
        [Category("Document")]
        [Description("Select available document type")]
        [DisplayName("Type")]
        [Display(Order = 14)]
        public DocumentType DocumentType { get; set; }
        
        [Browsable(true)]
        [Category("Document")]
        [Description("Type customer document serie & number")]
        [DisplayName("Serie and Number")]
        [Display(Order = 15)]
        public string DocumentNumber { get; set; }
        #endregion

        #region Cita details
        
        [Browsable(true)]
        [Category("Cita details")]
        [Description("Insert cita identification number")]
        [DisplayName("Identification number")]
        [Display(Order = 16)]
        public string CitaIdentificationNumber { get; set; }

        [Browsable(true)]
        [Category("Cita details")]
        [Description("Insert cita date & time of visiting")]
        [DisplayName("Date and Time")]
        [Display(Order = 17)]
        public DateTime CitaDateTime { get; set; }

        [Browsable(true)]
        [Category("Cita details")]
        [Description("Insert cita minimum date & time")]
        [DisplayName("Minimum date")]
        [Display(Order = 18)]
        public DateTime SitaMinRequiredDate { get; set; }

        [Browsable(true)]
        [Category("Cita details")]
        [Description("Insert cita maximum date & time")]
        [DisplayName("Maximum date")]
        [Display(Order = 18)]
        public DateTime SitaMaxRequiredDate { get; set; }

        [Browsable(true)]
        [Category("Cita details")]
        [Description("Type some notes about cita here")]
        [DisplayName("Notes")]
        [Display(Order = 20)]
        public string CitaNotes { get; set; }

        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Cita details")]
        [Description("Status of cita search")]
        [DisplayName("Status")]
        [Display(Order = 21)]
        public string IsCitaReceived => !string.IsNullOrEmpty(CitaIdentificationNumber) ? "FOUNDED" : "NOT FOUND";

        #endregion

        #region Family reunification

        [Browsable(true)]
        [Category("Family reunification")]
        [Description("Familiar name and surname")]
        [DisplayName("Familiar name and surname")]
        [Display(Order = 23)]
        public string FamiliarNameAndSurname { get; set; }

        [Browsable(true)]
        [Category("Family reunification")]
        [Description("Familiar document number")]
        [DisplayName("Familiar document number")]
        [Display(Order = 24)]
        public string FamiliarDocumentNumber { get; set; }

        [Browsable(true)]
        [Category("Family reunification")]
        [Description("Familiar nationality")]
        [DisplayName("Familiar nationality")]
        [Display(Order = 25)]
        public string FamiliarNationality { get; set; }

        [Browsable(true)]
        [Category("Family reunification")]
        [Description("Connection with citizen")]
        [DisplayName("Connection")]
        [Display(Order = 26)]
        public string FamiliarConnectionWithCitizen { get; set; }

        #endregion
    }
}