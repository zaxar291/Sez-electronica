using System;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Entities;
using Sede_Checker.RucapthcaFormParams.Entities;

namespace Sede_Checker.Entities.Converters
{
    internal sealed class SedeRuCaptchaConverter : SedeBaseEntityDtoConverter<SedeRuCaptchaData, RuCaptchaTask>
    {
        public override RuCaptchaTask ConvertToDtoEntity(SedeRuCaptchaData uiEntity)
        {
            throw new NotImplementedException();
        }

        public override SedeRuCaptchaData ConvertToUiEntity(RuCaptchaTask dtoEntity)
        {
            var r = new SedeRuCaptchaData
            {
                CaptchaId = dtoEntity.RequestId,
                CaptchaSolution = dtoEntity.CaptchaSolution,
                CaptchaStatus = dtoEntity.Status,
                CaptchaDateSend = dtoEntity.CreatedDateTime,
                CaptchaDateSolution = dtoEntity.SolvedDateTime
            };

            return r;
        }
    }
}