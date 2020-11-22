using System;
using System.ComponentModel;
using System.Globalization;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Класс преобразователя стандартного отображения коллекции в PropertyGrid
    ///     Заменяет обозначение "(Collection)" на "(Select available procedures in region...)"
    /// </summary>
    public class SpainRegionProcedureCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destType)
        {
            if (destType == typeof(string) && value is SpainRegionProcedureCollection)
                return "(Select available procedures in region...)";
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}