using System;

using Sede_Checker.Implementation.Captcha.RuCaptcha.Enums;

namespace Sede_Checker.RucapthcaFormParams.Entities
{
    public class SedeRuCaptchaData
    {
        public string CaptchaId { get; set; }

        public string CaptchaSolution { get; set; }

        public RuCaptchaTaskStatus CaptchaStatus { get; set; }

        public DateTime CaptchaDateSend { get; set; }

        public DateTime CaptchaDateSolution { get; set; }

        public TimeSpan SolutionTimeSpan => CaptchaDateSolution - CaptchaDateSend;

        public string CaptchaSolutionSeconds => !string.IsNullOrEmpty(this.CaptchaSolution) ? $"{Math.Round(SolutionTimeSpan.TotalSeconds, 0)} sec." : "NOT READY";
    }
}
