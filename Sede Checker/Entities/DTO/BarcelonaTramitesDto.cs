namespace Sede_Checker.Entities.DTO
{
    public class BarcelonaTramitesDto
    {
        public const string AutorizacionDeRegreso = "//*[@id='idMensajeBarcelona']/p[2]/a[1]";
        public const string CertificatesUe = "//*[@id='idMensajeBarcelona']/p[2]/a[2]";
        public const string ExpeditionDeTarjetaDeIdentitadDeExtranjero = "//*[@id='idMensajeBarcelona']/p[2]/a[3]";
        public const string CertificadosYAsicnacionNie = "//*[@id='idMensajeBarcelona']/p[2]/a[4]";
        public const string ExpedicionDeDocumentosDeSoliciantesDeAsilo = "//*[@id='idMensajeBarcelona]/p[2]/a[5]";
    }
}
