using Sede_Checker.Delegates;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Entities;

namespace Sede_Checker.Abstract.Interfaces
{
    public interface IRecaptchaV2Resolver
    {
        string Name { get; }
        string Descriptions { get; }
        string CaptchaApiToken { get; }
        string VictimGoogleApiKey { get; }
        string VictimPageUrl { get; }
        string RequestUrl { get; }

        RuCaptchaTask[] Tasks { get; }

        IRecaptchaV2ResolverTask SendTaskToResolve();

        bool Report(string CaptchaId, string action);

        event RecaptchaV2TaskCallback OnRecaptchaV2TaskCallback;
        event RecaptchaV2ServiceStatusInfoCallback OnServiceStatusInfoCallback;
    }
}
