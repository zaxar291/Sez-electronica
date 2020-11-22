using System;
using Newtonsoft.Json;
using Sede_Checker.Enums;

namespace Sede_Checker.Entities.DTO
{
    public class SedeConstitutionDto
    {
        [JsonProperty("TaskGuid")]
        public Guid TaskGuid { get; set; }

        [JsonProperty("TaskCreatedDateTime")]
        public DateTime TaskCreatedDateTime { get; set; }

        [JsonProperty("TaskNotes")]
        public string TaskNotes { get; set; }

        [JsonProperty("TaskStatus")]
        public TaskStatus TaskStatus;

        [JsonProperty("CustomerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("CustomerNameAndSurname")]
        public string CustomerNameAndSurname { get; set; }

        [JsonProperty("NIENumber")]
        public string NIENumber { get; set; }

        [JsonProperty("DateRequest")]
        public DateTime DateRequest { get; set; }

        [JsonProperty("CustomerDateBorn")]
        public DateTime CustomerDateBirth { get; set; }
    }
}
