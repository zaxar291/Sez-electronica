using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Entities.Settings.BrowserDrivers.Chrome;
using Sede_Checker.Implementation.Driver.Chrome.Entities;
using Sede_Checker.Implementation.Driver.Enums;

namespace Sede_Checker.Implementation.Driver.Chrome
{
    public class ChromeDriverResolver : IWebDriverResolver
    {
        #region Constructor

        public ChromeDriverResolver(ChromeDriverTask task, ILogger lg, ChromeDriverSettings settings)
        {
            _lg = lg;
            _prefix = $"[ChromeDriver(Task:[{task.Name}]; ID:[{task.Guid}])] -";
            _settings = settings;
            _task = task;
        }

        #endregion

        #region Private Methods

        private ChromeOptions GetDriverOptions()
        {
            var o = new ChromeOptions();
            
            if(this._settings.LaunchBrowserIncognitoMode)
                o.AddArgument("--incognito"); //Launch browsers in incognito mode!

            if(this._settings.IgnoreCertificateErrors)
                o.AddArgument("ignore-certificate-errors"); //Ignore if website has wrong ssl certificate

            if (this._settings.UseProxy)
            {
                if (ReferenceEquals(this._task.Proxy, null))
                {
                    this._lg.Error($"{_prefix} proxy is not initialized! Please, add proxy to driver options or change driver settings!");
                    return null;
                }
                
                var proxy = $"{_task.Proxy.Ip}:{_task.Proxy.Host}";

                _lg.Info($"{_prefix} will procceed the job via [{proxy}] proxy...");

                var p = new Proxy {
                    Kind = ProxyKind.Manual,
                    IsAutoDetect = false,
                    SslProxy = proxy
                };

                o.Proxy = p;
            }

            //o.AddArgument("--start-minimized"); //Launch browser minimized, disable it for debug

            if(this._settings.DisableInfoBars)
                o.AddArgument("disable-infobars"); //Disable infobar with message that Chrome a controlled by automatic software

            if(this._settings.IsHeadless)
                o.AddArgument("--headless");

            return o;
        }

        public bool Initialize()
        {   
            try
            {
                var o = this.GetDriverOptions();

                if (ReferenceEquals(o, null))
                {
                    this._lg.Error($"{_prefix} can't initialize driver options! Dispose driver!");
                    this.Dispose();
                    return false;
                }

                _cd = new ChromeDriver(o);
                
                //todo: Test this, seems doesn't work
                if(!this._settings.PageLoadTimeout.Equals(0))
                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(this._settings.PageLoadTimeout);
                
                //todo: Test this
                switch (this._settings.WindowState)
                {
                    case BrowserWindowState.Normal:
                        break;
                    case BrowserWindowState.Maximized:
                        this.Driver.Manage().Window.Maximize();
                        break;
                    case BrowserWindowState.Minimized:
                        this.Driver.Manage().Window.Minimize();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _lg.Info($"{_prefix} Navigate to target url -> {_task.TargetUrl}");

                Driver.Navigate().GoToUrl(_task.TargetUrl);

                //todo: add information about page load time...
                _lg.Info($"{_prefix} was successfully initialized & targer url loaded!");

                return true;
            }
            catch (WebDriverException e)
            {
                _lg.Error($"{_prefix} While trying to initialize, exception occured!");
                _lg.Exception(e);
                this.Dispose();
                return false;
            }
        }

        #endregion

        #region Fields

        private ChromeDriver _cd;

        private readonly ILogger _lg;

        private readonly string _prefix;

        private readonly ChromeDriverSettings _settings;

        private readonly ChromeDriverTask _task;

        #endregion

        #region Properties

        public ChromeDriver Driver
        {
            get
            {
                if (ReferenceEquals(this._cd, null)) return null;

                //todo: Test this
                //If browser window is visible & we have option to close alian pages (which opens via some strange proxies)
                if (!this._settings.IsHeadless && 
                    this._settings.CloseAlianPages)
                    this.CloseAnotherTabs();

                return this._cd;
            }
        }
      
        #endregion

        #region Public Methods

        public string GetPageSource()
        {
            return ReferenceEquals(Driver, null) ? null : Driver.PageSource;
        }

        public string GetDataFromPage(string element, string script = null)
        {
            By se = null;
            _lg.Info($"{_prefix} Trying to select text from element {element}");

            if(element != null)
            {
                se = GetElement(element);

                if (ReferenceEquals(se, null))
                {
                    _lg.Error($"{_prefix} can't find element {element}");
                    return string.Empty;
                }
            }

            try
            {
                if(script != null)
                {
                    return (string)Driver.ExecuteScript(script);
                }
                return Driver.FindElement(se).Text;
            }
            catch (StaleElementReferenceException e)
            {
                _lg.Error(
                    $"{_prefix} Can't get valid text from element {element}, exception details: {e.Message}");

                _lg.Exception(e);

                return string.Empty;
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);

                return string.Empty;
            }
        }

        public bool ExecuteScript(string script)
        {
            try
            {
                _lg.Info($"{_prefix} Execute script -> {script}");

                Driver.ExecuteScript(script);

                _lg.Info($"{_prefix} Script was successfully executed!");

                return true;
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);

                return false;
            }
        }

        public bool ExecuteScript(string script, object obj)
        {   
            try
            {
                _lg.Info($"{_prefix} Execute script -> {script} with params");

                Driver.ExecuteScript(script, obj);

                _lg.Info($"{_prefix} Script was successfully executed!");

                return true;
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);

                return false;
            }
        }

        public bool InvokeMemberClick(string element)
        {
            _lg.Info($"{_prefix} Click on element -> {element}");

            By se = GetElement(element);

            if (ReferenceEquals(se, null))
            {
                _lg.Error($"{_prefix} Can't find element {element}");
                return false;
            }
            try
            {
                Driver.FindElement(se).Click();

                _lg.Info($"{_prefix} Click on {element} was successfull!");

                return true;
            }
            catch (StaleElementReferenceException e)
            {
                _lg.Exception(e);

                return false;
            }
            catch (NoSuchElementException e)
            {
                _lg.Exception(e);

                return false;
            }
            catch (ArgumentNullException e)
            {
                _lg.Exception(e);

                return false;
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);

                return false;
            }
        }

        public bool SelectElementInList(string element, 
                                        string selectable, 
                                        bool useRegularExpression = false,
                                        bool selectByDefault = false)
        {
            _lg.Info($"{_prefix} Select element -> [{selectable}] in list [{element}]; RegularExpression:[{useRegularExpression}]; SelectByDefault:[{selectByDefault}]");

            if (!useRegularExpression)
            {
                By se = GetElement(element);

                if (ReferenceEquals(se, null))
                {
                    _lg.Error($"{_prefix} Can't select [{selectable}] in [{element}]");
                    return false;
                }

                try
                {
                    new SelectElement(Driver.FindElement(se)).SelectByText(selectable);

                    _lg.Info($"{_prefix} Select [{selectable}] in [{element}] was successfull!");

                    return true;
                }
                catch (UnexpectedTagNameException e)
                {
                    _lg.Exception(e);

                    return false;
                }
                catch (NoSuchElementException e)
                {
                    _lg.Exception(e);

                    return false;
                }
                catch (ArgumentNullException e)
                {
                    _lg.Exception(e);

                    return false;
                }

                catch (WebDriverException e)
                {
                    _lg.Exception(e);
                    return false;
                }
            }

            //object[] p = { element, selectable };
            object p = new object[] { element, selectable };

            try
            {
                string script;
                if (!selectByDefault)
                    script =
                        "return s(arguments[0][0], arguments[0][1]);function s(a,b){var l = document.getElementById(a).options; for(var i in l){if(l[i].innerHTML == undefined){continue;}if(l[i].innerHTML.toLowerCase().indexOf(b) + 1){l[i].selected=true;return true;}}return false;}";
                else
                    script =
                        "return s(arguments[0][0], arguments[0][1]);function s(a,b){var l = document.getElementById(a).options; for(var i in l){if(l[i].innerHTML == undefined){continue;}if(l[i].innerHTML.toLowerCase().indexOf(b) + 1){l[i].selected=true;return true;}}return true;}";

                return (bool) Driver.ExecuteScript(script, p);
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);
                return false;
            }
        }

        public bool UpdateFieldData(string element, string text)
        {
            _lg.Info($"{_prefix} Attemp to update data [{text}] in element [{element}]...");

            By se = GetElement(element);

            if (ReferenceEquals(se, null))
            {
                _lg.Error($"{_prefix} Can't find element -> [{element}]");
                return false;
            }

            try
            {
                var e = Driver.FindElement(se);

                e.Clear();

                e.SendKeys(text);

                _lg.Info($"{_prefix} Value [{text}] was successfully updated in element [{element}]");

                return true;
            }
            catch (StaleElementReferenceException e)
            {
                _lg.Exception(e);

                return false;
            }
            catch (InvalidElementStateException e)
            {
                _lg.Exception(e);

                return false;
            }
            catch (WebDriverException e)
            {
                _lg.Exception(e);
                return false;
            }
        }

        public bool SolveAlertWindow()
        {
            try
            {
                _lg.Info($"{_prefix} Attemp to accept alert windows");

                Driver.SwitchTo().Alert().Accept();

                _lg.Info($"{_prefix} Alert window was successfully accepted!");

                return true;
            }
            catch (WebDriverException e)
            {
                _lg.Error($"{_prefix} While trying to accept alert window, exception occured!");
                _lg.Exception(e);
                return false;
            }
        }

        public bool ElementExists(string element)
        {
            By se = GetElement(element);

            if (ReferenceEquals(se, null)) return false;

            try
            {
                _lg.Info($"{_prefix} Attemp to check [{element}] if it's exist on the page...");
                
                Driver.FindElement(se);

                _lg.Info($"{_prefix} [{element}] is exist on the page!");

                return true;
            }
            catch (NoSuchElementException)
            {
                _lg.Warning($"{_prefix} [{element}] doesn't exist on the page!");
                return false;
            }
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Dispose()
        {
            try
            {
                this._cd?.Quit();
            }
            catch (Exception e)
            {
                _lg.Exception(e);
                this._cd?.Dispose();
            }
        }

        /// <summary>
        /// Close all another tabs
        /// </summary>
        public void CloseAnotherTabs()
        {
            try
            {
                if (ReferenceEquals(_cd, null) ||
                    ReferenceEquals(_cd.WindowHandles, null) ||
                    _cd.WindowHandles.Count <= 1) return;

                string dt = _cd.WindowHandles[0]; //default tab

                for (var i = 1; i < _cd.WindowHandles.Count; i++)
                {
                    string wh = _cd.WindowHandles[i];
                    _cd.SwitchTo().Window(wh);
                    _cd.Close();
                }

                _cd.SwitchTo().Window(dt);
            }
            catch (Exception e)
            {
                _lg.Exception(e);
            }
        }

        private By GetElement(string s)
        {
            if (s.IndexOf(".", StringComparison.Ordinal) != -1)
                return By.ClassName(s.Remove(0, s.IndexOf(".", StringComparison.Ordinal) + 1));
            if (s.IndexOf("#", StringComparison.Ordinal) != -1)
                return By.Id(s.Remove(0, s.IndexOf("#", StringComparison.Ordinal) + 1));
            if (s.IndexOf("$", StringComparison.Ordinal) != -1)
                return By.XPath(s.Remove(0, s.IndexOf("$", StringComparison.Ordinal) + 1));
            return null;
        }

        #endregion
    }
}