using System;

namespace Sede_Checker.Abstract.Interfaces
{
    public interface ILogger
    {
        bool IsDebug { get; set; }
        
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Exception(Exception exc);
    }
}