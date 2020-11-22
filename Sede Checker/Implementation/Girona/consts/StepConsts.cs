using System;

namespace Sede_Checker.Implementation.StepsResolvers.Girona.consts
{
    class StepsConsts
    {
        public const string DomFirstStepButton = "return document.getElementById('btnAceptar').click();";
        public const string DomCaptchaSolutionPath = "return f(arguments[0]);function f(k){document.getElementById('g-recaptcha-response').value=k;document.getElementById('btnEnviar').disabled = false;}";
        public const string DomCaptchaSuccessScript = "return document.getElementById('btnEnviar').click();";
        public const string DomThirdStep = "return document.getElementById('btnEntrar').click();";
        public const string DomNinethStepCheckbox1 = "return document.getElementById('enviarCorreo').click()";
        public const string DomNinethStepCheckbox2 = "return document.getElementById('chkTotal').click()";
        public const string DomNinethStepBtn = "return document.getElementById('btnConfirmar').click()";

        public const string DomIlesBalearsMallorcaProcedureDate = "return e();function e(){var a=document.getElementById('VistaMapa_Datatable').childNodes[3].childNodes;for(var i in a){if(a[i].childNodes != undefined && a[i].childNodes.length > 0){if(a[i].childNodes[3].innerText.replace(/\\s+/g, '') == 'LIBRE'){a[i].childNodes[3].children[0].click();return true;}}}}";

        public const string DomClickButtonScript = "return c(arguments[0]);function c(k){document.getElementById(k).click();};";

        public const string SitaUrl = "https://sede.administracionespublicas.gob.es/icpplus/index.html";
        public const string SitaCaptchaUrl = "https://sede.administracionespublicas.gob.es/icpplus/acEntrada";

        public const string SitaPdfPageFirstProvinceUrl = "https://sede.administracionespublicas.gob.es/icpplustiem/plantillas/printjustificante";
        public const string SitaPdfPageSixthrovinceUrl = "https://sede.administracionespublicas.gob.es/icpplus/plantillas/printjustificante";

        public const string ApiKey = "6Ld3FzoUAAAAANGzDQ-ZfwyAArWaG2Ae15CGxkKt";

        public const string MallorcaProcedure = "SOLICITUD AUTORIZACIONES - MALLORCA";

        public string SitaDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Sede/";
    }
}
