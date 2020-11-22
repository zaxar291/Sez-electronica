using Sede_Checker.TaskFormParams.Properties;
using Sede_Checker.Abstract.Interfaces;
using System;

namespace Sede_Checker.Delegates
{

    public delegate void StepsControllerMailCallback(object sender, StepsControllerMailCallbackEventArgs eventArgs);

    public class StepsControllerMailCallbackEventArgs : EventArgs
    {
        public StepsControllerMailCallbackEventArgs(string user, string attachment)
        {
            User = user;
            Attachment = attachment;
        }

        public string User { get; }
        public string Attachment { get; }
    }

    public delegate void StepsControllerCitaStatusCallback(object sender, StepsControllerCitaStatusCallbackEventArgs eventArgs);

    public class StepsControllerCitaStatusCallbackEventArgs : EventArgs
    {
        public StepsControllerCitaStatusCallbackEventArgs(SedeTaskData data, string citaNumber)
        {
            Data = data;
            CitaNumber = citaNumber;
        }

        public SedeTaskData Data { get; }

        public string CitaNumber { get; }
    }

    public delegate void StepsControllerCaptchaCallback(object sender, StepsControllerCaptchaCallbackEventArgs eventArgs);

    public class StepsControllerCaptchaCallbackEventArgs : EventArgs
    {
        public StepsControllerCaptchaCallbackEventArgs(IRecaptchaV2ResolverTask task)
        {
            RucaptchaResponse = task;
        }

        public IRecaptchaV2ResolverTask RucaptchaResponse { get; }
    }
}
