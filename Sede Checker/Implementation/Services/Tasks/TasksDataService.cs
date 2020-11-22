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

namespace Sede_Checker.Implementation.Services.Tasks
{
    public class TasksDataService : BaseDataService<SedeTaskData>
    {
        private readonly List<SedeTaskData> _data;
        private readonly SedeTaskConverter _c;
        private readonly IStorageService<SedeAppDto> _s;
        private readonly IUserValidator _u;

        public TasksDataService(IStorageService<SedeAppDto> storage)
        {
            this._s = storage;

            var data = _s.GetData();

            _c = new SedeTaskConverter();

            _u = new UserValidatorService();

            if(!ReferenceEquals(data, null) &&
               !ReferenceEquals(data.Tasks, null) &&
               !data.Tasks.Length.Equals(0))
               _data = _c.ConvertToUiEntity(data.Tasks).ToList();
            else
                this._data = new List<SedeTaskData>();
        }

        public override bool AddOrUpdate(SedeTaskData data)
        {
            try
            {
                /*var _uResponse = _u.Validate(data);

                if (!_uResponse.IsSuccess)
                {
                    Console.WriteLine(_uResponse.Message);
                    return false;
                }*/

                if (data.TaskGuid.Equals(Guid.Empty)) {
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
        
        public override SedeTaskData[] GetObjects()
        {
            return this._data.OrderBy(el=>el.CustomerEmail).ToArray();
        }

        public override SedeTaskData GetObjectByGuid(Guid guid)
        {
            return this._data.FirstOrDefault(el => el.TaskGuid.Equals(guid));
        }

        public SedeTaskData[] GetInProgressTasks()
        {
            var r = this.GetObjects().Where(el => el.TaskStatus.Equals(TaskStatus.INPROGRESS))
                .OrderBy(el => el.CustomerEmail);

            return r.ToArray();
        }

        private void SaveInStorage()
        {
            var data = _s.GetData();
            if(ReferenceEquals(data, null))
                data = new SedeAppDto();
            data.Tasks = this._c.ConvertToDtoEntity(this._data.ToArray());
            this._s.UpdateData(data);
        }
    }
}