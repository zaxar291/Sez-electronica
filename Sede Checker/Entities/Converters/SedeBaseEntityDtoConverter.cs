using System.Linq;

namespace Sede_Checker.Entities.Converters
{
    public abstract class SedeBaseEntityDtoConverter<TUiEntity, TDtoEntity>
    {
        public abstract TUiEntity ConvertToUiEntity(TDtoEntity dtoEntity);
        public abstract TDtoEntity ConvertToDtoEntity(TUiEntity uiEntity);


        public virtual TUiEntity[] ConvertToUiEntity(TDtoEntity[] dtoEntities)
        {
            return dtoEntities.Select(ConvertToUiEntity).ToArray();
        }

        public virtual TDtoEntity[] ConvertToDtoEntity(TUiEntity[] uiEntities)
        {
            return uiEntities.Select(ConvertToDtoEntity).ToArray();
        }
    }
}