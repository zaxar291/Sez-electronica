using System;
using Sede_Checker.Delegates;

namespace Sede_Checker.Abstract.Interfaces
{
    interface IStepsController : IDisposable
    {
        void SolveSteps();
        void SolveStepsAsync();

        event StepsControllerMailCallback OnStepsControllerMailCallback;
        event StepsControllerCitaStatusCallback OnStepsControllerCitaStatusCallback;
    }
}
