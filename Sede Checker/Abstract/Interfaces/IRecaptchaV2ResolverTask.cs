using Sede_Checker.Implementation.Captcha.RuCaptcha.Enums;
using System;

namespace Sede_Checker.Abstract.Interfaces
{
    public interface IRecaptchaV2ResolverTask
    {
        string RequestId { get; }
        string RequestUrl { set; get; }
        string CaptchaSolution { set; get; }
        RuCaptchaTaskStatus Status { set; get; }
        DateTime CreatedDateTime { get; }
        DateTime SolvedDateTime { get; }
    }
}
