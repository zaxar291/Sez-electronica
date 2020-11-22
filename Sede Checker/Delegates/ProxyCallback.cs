using System;

namespace Sede_Checker.Delegates
{
    public delegate void ProxyCallback(object sender, ProxyCallbackResult eventArgs);

    public class ProxyCallbackResult : EventArgs
    {

        public ProxyCallbackResult(bool isOnline)
        {
            IsOnline = isOnline;
        }

        public bool IsOnline { get; }
    }
}
