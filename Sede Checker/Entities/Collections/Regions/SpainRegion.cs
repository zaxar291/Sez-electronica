using System;
using System.ComponentModel;
using Sede_Checker.Entities.Collections.RegionProcedures;

namespace Sede_Checker.Entities.Collections.Regions
{
    /// <summary>
    ///     Объект, описывающий структурe региона Испании
    /// </summary>
    [TypeConverter(typeof(SpainRegionConverter))]
    public class SpainRegion
    {
        public SpainRegion()
        {
        }

        public SpainRegion(Guid id, string name, SpainRegionProcedureCollection procedures)
        {
            Id = id;
            Name = name;
            Procedures = procedures;
        }

        public Guid Id { get; }
        public string Name { get; }
        public SpainRegionProcedureCollection Procedures { set; get; }
    }
}