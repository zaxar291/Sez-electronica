using System;


namespace Sede_Checker.Abstract.Interfaces
{
    public interface IWebDriverResolverTask
    {
        bool IsSolved { get; set; }

        DateTime CreatedDateTime { get; }
        DateTime SolvedDateTime { get; }
    }
}
