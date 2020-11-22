using Sede_Checker.Entities.Properties;
using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker.Abstract.Interfaces
{
    interface IUserValidator
    {
        ValidatorResponse Validate(SedeTaskData task);
    }
}
