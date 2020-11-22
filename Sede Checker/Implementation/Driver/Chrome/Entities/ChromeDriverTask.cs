using System;
using Sede_Checker.Entities.DTO;

namespace Sede_Checker.Implementation.Driver.Chrome.Entities
{
    public class ChromeDriverTask
    {
        public Guid Guid => Guid.NewGuid();

        public string TargetUrl { set; get; }
        public string Name { set; get; }

        public SedeCheckerProxyAdressDTO Proxy { set; get; }
    }
}