using System.Drawing;
using Sede_Checker.Implementation.Driver.Enums;

namespace Sede_Checker.Entities.Settings.BrowserDrivers.Chrome
{
    public class ChromeDriverSettings
    {
        public bool LaunchBrowserIncognitoMode { set; get; }
        public bool UseProxy { set; get; }
        public bool DisableInfoBars { set; get; }
        public bool IgnoreCertificateErrors { set; get; }
        public bool DisablePopupBlocking { set; get; }
        public bool EnablePreciseMemoryInfo { set; get; }
        public bool DisableDefaultApps { set; get; }

        /// <summary>
        ///     Launch driver without browser window (works faster), recommended False if debug!
        /// </summary>
        public bool IsHeadless { set; get; }
        
        public bool CloseAlianPages { set; get; }

        public BrowserWindowState WindowState { set; get; }

        public Size BrowserWindowsSize { set; get; }

        /// <summary>
        ///     Page Load Timeour, in seconds
        /// </summary>
        public int PageLoadTimeout { set; get; }
        
    }
}