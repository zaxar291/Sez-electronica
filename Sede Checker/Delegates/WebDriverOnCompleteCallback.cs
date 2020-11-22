using Sede_Checker.Abstract.Interfaces;
using System;

namespace Sede_Checker.Delegates
{
    public delegate void WebDriverOnCompleteCallback(object sender, WebDriverResultEvent eventArgs);

    public class WebDriverResultEvent : EventArgs
    {
        public WebDriverResultEvent(IWebDriverResolverTask task)
        {
            Task = task;
        }

        public IWebDriverResolverTask Task { get; }
    }
}
