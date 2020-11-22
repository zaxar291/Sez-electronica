using System;
using System.ComponentModel;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Объект, описывающий структуру процедуры в регионе
    /// </summary>
    [TypeConverter(typeof(SpainRegionProcedureConverter))]
    public class SpainRegionProcedure
    {
        public SpainRegionProcedure() { }

        public SpainRegionProcedure(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}