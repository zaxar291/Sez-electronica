using Sede_Checker.Entities.Settings.BrowserDrivers.Chrome;

namespace Sede_Checker.Entities.Settings.Controllers
{
    public class BaseControllerSettings : BaseSettings
    {
        public ChromeDriverSettings ChromeDriverSettings { set; get; }
    }
}