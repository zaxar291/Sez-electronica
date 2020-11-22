using Sede_Checker.Entities.DTO;
using Sede_Checker.ConstitutionTaskFormParams.Properties;

namespace Sede_Checker.Entities.Converters
{
   internal sealed class ConstitutionTaskConverter : SedeBaseEntityDtoConverter<ConstitutionTaskData, SedeConstitutionDto>
   {
        public override SedeConstitutionDto ConvertToDtoEntity(ConstitutionTaskData uiEntity)
        {
            var r = new SedeConstitutionDto
            {
                TaskGuid = uiEntity.TaskGuid,
                TaskCreatedDateTime = uiEntity.TaskCreatedDateTime,
                TaskNotes = uiEntity.TaskNotes,
                TaskStatus = uiEntity.TaskStatus,
                CustomerEmail = uiEntity.CustomerEmail,
                CustomerNameAndSurname = uiEntity.CustomerNameAndSurname,
                NIENumber = uiEntity.NIENumber,
                DateRequest = uiEntity.DateRequest,
                CustomerDateBirth = uiEntity.CustomerDateBirth
            };

            return r;
        }

        public override ConstitutionTaskData ConvertToUiEntity(SedeConstitutionDto dtoEntity)
        {
            var r = new ConstitutionTaskData {
                TaskGuid = dtoEntity.TaskGuid,
                TaskCreatedDateTime = dtoEntity.TaskCreatedDateTime,
                TaskNotes = dtoEntity.TaskNotes,
                CustomerEmail = dtoEntity.CustomerEmail,
                CustomerNameAndSurname = dtoEntity.CustomerNameAndSurname,
                NIENumber = dtoEntity.NIENumber,
                DateRequest = dtoEntity.DateRequest,
                CustomerDateBirth = dtoEntity.CustomerDateBirth
            };

            return r;
        }
    }
}
