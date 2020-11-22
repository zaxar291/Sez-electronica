using Newtonsoft.Json;

namespace Sede_Checker.DTO
{
    public class RucaptchaTaskResponseDto
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("request")]
        public string Data;
    }
}
