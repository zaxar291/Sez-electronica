using System;
using Newtonsoft.Json;
using Sede_Checker.Enums;

namespace Sede_Checker.Entities.DTO
{
    public class SedeCheckerUserDto
    {
        [JsonProperty("TaskGuid")]
        public Guid TaskGuid { get; set; }

        [JsonProperty("Province")]
        public string Province { get; set; }

        [JsonProperty("ProceduresByPersonalCode")]
        public string ProceduresByPersonalCode { get; set; }

        [JsonProperty("ProcedureForSelectedProvince")]
        public string ProcedureForSelectedProvince { get; set; }

        [JsonProperty("SitaMinRequiredDate")]
        public DateTime SitaMinRequiredDate { get; set; }

        [JsonProperty("SitaMaxRequiredDate")]
        public DateTime SitaMaxRequiredDate { get; set; }

        [JsonProperty("NumberToInsert")]
        public string NumberToInsert { get; set; }

        [JsonProperty("DateCardEnd")]
        public DateTime DateCardEnd { get; set; }

        [JsonProperty("DocumentTypeId")]
        public string DocumentTypeId { get; set; }

        [JsonProperty("Passport")]
        public string Passport { get; set; }

        [JsonProperty("NameSurname")]
        public string NameSurname { get; set; }
        
        [JsonProperty("CustomerDateOfBirth")]
        public DateTime CustomerDateOfBirth { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("NeadableCitaTown")]
        public string NeadableCitaTown { get; set; }

        [JsonProperty("MailToNotifications")]
        public string MailToNotifications { get; set; }

        [JsonProperty("ReasonToGetNIE")]
        public string ReasonToGetNIE { get; set; }

        [JsonProperty("DateRegistration")]
        public DateTime TaskCreatedDateTime { get; set; }

        [JsonProperty("SitaReceived")]
        public string SitaReceived { get; set; }

        [JsonProperty("TaskNotes")]
        public string TaskNotes { get; set; }

        [JsonProperty("TaskStatus")]
        public TaskStatus TaskStatus { get; set; }

        [JsonProperty("CitaIdentificationNumber")]
        public string CitaIdentificationNumber { get; set; }

        [JsonProperty("CitaDateTime")]
        public DateTime CitaDateTime { get; set; }

        [JsonProperty("CitaNotes")]
        public string CitaNotes { get; set; }

        [JsonProperty("FamiliarConnectionWithCitizen")]
        public string FamiliarConnectionWithCitizen { get; set; }

        [JsonProperty("FamiliarNameAndSurname")]
        public string FamiliarNameAndSurname { get; set; }

        [JsonProperty("FamiliarDocumentNumber")]
        public string FamiliarDocumentNumber { get; set; }

        [JsonProperty("FamiliarNationality")]
        public string FamiliarNationality { get; set; }

    }
}