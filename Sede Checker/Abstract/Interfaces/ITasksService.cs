using System;

namespace Sede_Checker.Abstract.Interfaces
{
    public interface ITasksService<TEntity>
    {
        bool AddOrUpdate(TEntity data);
        bool Delete(Guid taskGuid);
        TEntity[] GetObjects();
        TEntity GetObjectByGuid(Guid guid);
    }
}