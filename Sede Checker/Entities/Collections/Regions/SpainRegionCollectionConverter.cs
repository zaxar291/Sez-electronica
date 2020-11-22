using System;
using System.ComponentModel;
using System.Globalization;
using Sede_Checker.Entities.Collections.RegionProcedures;

namespace Sede_Checker.Entities.Collections.Regions
{
    /// <summary>
    ///     Класс преобразователя стандартного отображения коллекции в PropertyGrid
    ///     Заменяет обозначение "(Collection)" на "(Select region in Spain...)"
    /// </summary>
    public class SpainRegionCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destType)
        {
            if (destType == typeof(string) && value is SpainRegionCollection)
                return "(Select region in Spain...)";
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}