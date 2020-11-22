using System;
using Sede_Checker.Abstract.Interfaces;

namespace Sede_Checker.Abstract
{
    public abstract class BaseDataService<TEntity> : ITasksService<TEntity>
    {
        public abstract bool AddOrUpdate(TEntity data);
        public abstract bool Delete(Guid taskGuid);
        public abstract TEntity[] GetObjects();
        public abstract TEntity GetObjectByGuid(Guid guid);
    }
}