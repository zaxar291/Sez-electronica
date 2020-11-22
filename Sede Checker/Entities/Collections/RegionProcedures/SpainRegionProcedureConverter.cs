using System;
using System.ComponentModel;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Класс выполняющий преобразование типа SpainRegionProcedure для корректного отображения 
    ///     в редакторе коллекций. Если его не реализовать, то в окне редактора 
    ///     коллекции SpainRegionProcedure будут отображаться внутреннее имя класса SpainRegionProcedure
    /// </summary>
    public class SpainRegionProcedureConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, 
                                            System.Globalization.CultureInfo culture, 
                                                object value, 
                                                    Type destType)
        {
            if (destType != typeof(string) || !(value is SpainRegionProcedure))
                return base.ConvertTo(context, culture, value, destType);
            var sr = (SpainRegionProcedure)value;
            return sr.Name;
        }
    }
}