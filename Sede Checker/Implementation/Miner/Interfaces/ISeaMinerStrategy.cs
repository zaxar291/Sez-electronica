using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker.Implementation.Miner.Interfaces
{
    public interface ISeaMinerStrategy
    {
        bool Excecute(SedeTaskData data);
    }
}