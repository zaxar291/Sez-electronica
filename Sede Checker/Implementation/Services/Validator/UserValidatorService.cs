using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Entities.Properties;
using Sede_Checker.TaskFormParams.Properties;
using System.Windows.Forms;

namespace Sede_Checker.Implementation.Services.Validator
{
    class UserValidatorService : IUserValidator
    {
        public ValidatorResponse Validate(SedeTaskData task)
        {
            if (ReferenceEquals(task, null))
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have any definitions!"
                };
            if (task.CustomerNameAndSurname.Equals(string.Empty))
            {
                return new ValidatorResponse {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for user name & surname"
                };
            }

            if (task.Country.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for country"
                };
            }

            if (task.ProcedureRegion.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for province"
                };
            }

            if (task.ProcedureName.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for procedure name"
                };
            }

            if (task.ProcedureCity.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for sita town"
                };
            }

            if (task.CustomerEmail.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for user e-mail"
                };
            }

            if(task.CustomerPhoneNumber.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for user phone number"
                };
            }

            if(task.DocumentNumber.Equals(string.Empty))
            {
                return new ValidatorResponse
                {
                    IsSuccess = false,
                    Message = "Task doesn't have definition for user document series/number"
                };
            }

            return new ValidatorResponse {
                IsSuccess = true
            };
        }
    }
}
