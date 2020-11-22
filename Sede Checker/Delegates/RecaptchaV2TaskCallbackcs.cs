using Sede_Checker.Abstract.Interfaces;
using System;

namespace Sede_Checker.Delegates
{
    public delegate void RecaptchaV2TaskCallback(object sender, RecaptchaV2TaskResultEventArgs eventArgs);
    public delegate void RecaptchaV2ServiceStatusInfoCallback(object sender, CaptchaServiceInfoEventArgs eventArgs);


    public delegate void LoggerCallback(object sender, LoggerCallbackEventArgs eventArgs);

    public class RecaptchaV2TaskResultEventArgs : EventArgs
    {
        public RecaptchaV2TaskResultEventArgs(IRecaptchaV2ResolverTask task)
        {
            Task = task;
        }

        public IRecaptchaV2ResolverTask Task { get; }
    }

    public class CaptchaServiceInfoEventArgs : EventArgs
    {
        public double Balance { set; get; }
    }

    public class LoggerCallbackEventArgs : EventArgs
    {
        public LoggerCallbackEventArgs(string message, string type)
        {
            LogMessage = message;
            LogType = type;
        }

        public string LogMessage { get; }
        public string LogType { get; }
    }
}
