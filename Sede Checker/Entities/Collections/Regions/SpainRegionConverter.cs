using System;
using System.ComponentModel;

namespace Sede_Checker.Entities.Collections.Regions
{
    /// <summary>
    /// Класс выполняющий преобразование типа SpainRegion для корректного отображения в редакторе коллекций
    /// Если его не реализовать, то в окне редактора коллекции SpainRegion будут отображаться внутреннее имя
    /// класса SpainRegion
    /// </summary>
    public class SpainRegionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType != typeof(string) || !(value is SpainRegion))
                return base.ConvertTo(context, culture, value, destType);
            var sr = (SpainRegion)value;
            return sr.Name;
        }
    }
}