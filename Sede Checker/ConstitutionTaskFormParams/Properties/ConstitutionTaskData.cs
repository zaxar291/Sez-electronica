using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sede_Checker.Entities.Collections.RegionProcedures;
using Sede_Checker.Entities.Collections.Regions;
using Sede_Checker.Enums;

namespace Sede_Checker.ConstitutionTaskFormParams.Properties
{
    public class ConstitutionTaskData
    {
        #region General Task Information Group
        [Browsable(true)]
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
        [Display(Order = 4)]
        public TaskStatus TaskStatus { get; set; }
        #endregion 

        #region Contacts Group

        [Browsable(true)]
        [Category("Customer")]
        [Description("Type customer email")]
        [DisplayName("Email")]
        [Display(Order = 5)]
        public string CustomerEmail { get; set; }
        #endregion

        #region Customer Information
        [Browsable(true)]
        [Category("Customer")]
        [Description("Type customer name && surname")]
        [DisplayName("Name and Surname")]
        [Display(Order = 7)]
        public string CustomerNameAndSurname { get; set; }
        #endregion

        #region Document Group
        [Browsable(true)]
        [Category("Document")]
        [Description("Type customer document serie && number")]
        [DisplayName("Serie and Number")]
        [Display(Order = 8)]
        public string NIENumber { get; set; }

        [Browsable(true)]
        [Category("Document")]
        [Description("Type customer date of the request")]
        [DisplayName("Date of request")]
        [Display(Order = 9)]
        public DateTime DateRequest { get; set; }

        [Browsable(true)]
        [Category("Document")]
        [Description("Type customer date of born")]
        [DisplayName("Date of born")]
        [Display(Order = 10)]
        public DateTime CustomerDateBirth { get; set; }
        #endregion
    }
}
