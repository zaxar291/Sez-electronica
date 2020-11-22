using Sede_Checker.Delegates;
using Sede_Checker.Entities.DTO;

namespace Sede_Checker.Abstract.Interfaces
{
    interface IProxyService
    {
        int NeadableProxyesToWork { get; }

        void LaunchCheckAdressesState();

        SedeCheckerProxyAdressDTO GetAvailibleAdress();

        event ProxyCallback OnProxyCallback;
    }
}
