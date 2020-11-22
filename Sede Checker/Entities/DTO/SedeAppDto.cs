using Newtonsoft.Json;

namespace Sede_Checker.Entities.DTO
{
    public class SedeAppDto
    {
        [JsonProperty("SeaTasks")]
        public SedeCheckerUserDto[] Tasks { set; get; }

        [JsonProperty("ConstitutionTasks")]
        public SedeConstitutionDto[] ConstitutionsTasks { get; set; }
    }
}