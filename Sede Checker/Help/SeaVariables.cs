using System;

namespace Sede_Checker.Help
{
    public class SeaVariables
    {
        public static string RootDirectoryPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Sede\\";
        public static string JsonDb = $"{RootDirectoryPath}users.json";
        public static string JsonStorageDb = $"{RootDirectoryPath}sede-storage.json";

        public static string ReCaptchaApiKey = "6Ld3FzoUAAAAANGzDQ-ZfwyAArWaG2Ae15CGxkKt";
        public static string SitaBaseUrl = "https://sede.administracionespublicas.gob.es/icpplus/";
        public static string SitaCaptchaUrl = $"{SitaBaseUrl}acEntrada";
    }
}