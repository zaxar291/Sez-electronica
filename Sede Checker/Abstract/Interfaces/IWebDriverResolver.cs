using System;
using Sede_Checker.Delegates;
using Sede_Checker.Entities.DTO;

namespace Sede_Checker.Abstract.Interfaces
{
    public interface IWebDriverResolver : IDisposable
    {   
        bool UpdateFieldData(string element, string text);

        bool SelectElementInList(string element, string selectable, bool useRegularExpression = false,
            bool selectByDefault = false);

        bool ExecuteScript(string script);
        bool ExecuteScript(string script, object obj);
        
        bool InvokeMemberClick(string element);

        bool ElementExists(string element);

        bool SolveAlertWindow();

        string GetPageSource();

        string GetDataFromPage(string element, string script = null);

        void Navigate(string url);

        bool Initialize();
    }
}