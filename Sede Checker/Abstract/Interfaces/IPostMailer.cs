namespace Sede_Checker.Abstract.Interfaces
{
    interface IPostMailer
    {
        bool SendReport(string customerName, string[] attachments = null);
    }
}
