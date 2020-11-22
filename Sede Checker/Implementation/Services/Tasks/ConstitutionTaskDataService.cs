using System;
using System.Collections.Generic;
using System.Linq;
using Sede_Checker.Abstract;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Entities.Converters;
using Sede_Checker.Entities.DTO;
using Sede_Checker.Enums;
using Sede_Checker.TaskFormParams.Properties;
using Sede_Checker.Implementation.Services.Validator;
using Sede_Checker.ConstitutionTaskFormParams.Properties;

namespace Sede_Checker.Implementation.Services.Tasks
{
    class ConstitutionTaskDataService : BaseDataService<ConstitutionTaskData>
    {
        private readonly List<ConstitutionTaskData> _data;
        private readonly ConstitutionTaskConverter _c;
        private readonly IStorageService<SedeAppDto> _s;

        public ConstitutionTaskDataService(IStorageService<SedeAppDto> storage)
        {
            this._s = storage;

            var data = _s.GetData();

            _c = new ConstitutionTaskConverter();

            if (!ReferenceEquals(data, null) &&
                !ReferenceEquals(data.ConstitutionsTasks, null) &&
                !data.ConstitutionsTasks.Length.Equals(0))
                _data = _c.ConvertToUiEntity(data.ConstitutionsTasks).ToList();
            else
                this._data = new List<ConstitutionTaskData>();
        }

        public override bool AddOrUpdate(ConstitutionTaskData data)
        {
            try
            {
                if (data.TaskGuid.Equals(Guid.Empty))
                {
                    data.TaskGuid = Guid.NewGuid();
                    data.TaskCreatedDateTime = DateTime.Now;
                    this._data.Add(data);
                    this.SaveInStorage();
                    return true;
                }

                if (!Delete(data.TaskGuid)) return false;
                this._data.Add(data);
                this.SaveInStorage();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public override bool Delete(Guid taskGuid)
        {
            var e = GetObjectByGuid(taskGuid);

            if (ReferenceEquals(e, null))
            {
                //Exception, doesn't found task entity
                return false;
            }

            this._data.Remove(e);

            this.SaveInStorage();

            return true;
        }

        public override ConstitutionTaskData[] GetObjects()
        {
            return this._data.OrderBy(el => el.CustomerEmail).ToArray();
        }

        public override ConstitutionTaskData GetObjectByGuid(Guid guid)
        {
            return this._data.FirstOrDefault(el => el.TaskGuid.Equals(guid));
        }

        public ConstitutionTaskData[] GetInProgressTasks()
        {
            var r = this.GetObjects().Where(el => el.TaskStatus.Equals(TaskStatus.INPROGRESS))
                .OrderBy(el => el.CustomerEmail);

            return r.ToArray();
        }

        private void SaveInStorage()
        {
            var data = _s.GetData();
            if (ReferenceEquals(data, null))
                data = new SedeAppDto();
            data.ConstitutionsTasks = this._c.ConvertToDtoEntity(this._data.ToArray());
            this._s.UpdateData(data);
        }
    }
}
