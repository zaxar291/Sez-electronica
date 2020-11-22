using System;
using System.ComponentModel;
using System.Globalization;
using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Класс преобразования типа SpainRegion
    ///     Выполняет преобразование к типу String и обратно в SpainRegion
    ///     Также, позволяет отображать в виде выпадающего списка коллекцию регионов Испании
    /// </summary>
    public class SpainRegionProcedureTypeConverter : ExpandableObjectConverter
    {
        /// <summary>
        ///     Возвращает значение, показывающее, поддерживает ли объект стандартный набор значений,
        ///     которые можно выбрать из списка.
        ///     Если не установить принудительно значение в true, есть вероятность,
        ///     что обработчик не будет воспринимать ваш метод GetStandardValues и вы не увидите
        ///     ваш список значений для выбора
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        ///     Данный метод связывает выбранное значение региона и возвращает доступные процедуры по данному региону
        ///     Недостаток - явная привязка к используемому типу данных SedeTaskData. Надо будет подумать как избежать этого
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private SpainRegionProcedureCollection GetCollection(ITypeDescriptorContext context)
        {
            var c = ((SedeTaskData) context.Instance).ProcedureSpainRegion;
            return ReferenceEquals(c, null) ? null : c.Procedures;
        }

        /// <summary>
        ///     Метод возвращает список значений из коллекции регионов Испании для отображения в выпадающем
        ///     списке значений PropertyGrid региона
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(GetCollection(context));
        }

        /// <summary>
        ///     Метод проверяет, можно ли преобразовывать полученное значение свойства от пользователя
        ///     в нужный нам тип
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        ///     Преобразование типа SpainRegionProcedure в читаемую строку для отображения значения в поле PropertyGrid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string) || !(value is SpainRegionProcedure))
                return base.ConvertTo(context, culture, value, destinationType);
            var item = (SpainRegionProcedure) value;
            return item.Name;
        }

        /// <summary>
        ///     Проверка на возможность обратного преобразования строкового представления в тип SpainRegionProcedure
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        ///     Преобразование строкового представления процедуры в тип SpainRegionProcedure.
        ///     Недостаток данного преобразования - правильность работы приведения типа зависит от формата
        ///     строкового представления типа SpainRegionProcedure, а именно, от дублирующихся значений.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (ReferenceEquals(value, null)) return null;
            
            if (value is string)
            {
                var itemSelected = GetCollection(context).Count.Equals(0)
                    ? new SpainRegionProcedure()
                    : GetCollection(context)[0];

                foreach (SpainRegionProcedure i in GetCollection(context))
                {
                    var sCraftName = i.Name;
                    if (sCraftName.Equals((string) value))
                        itemSelected = i;
                }
                return itemSelected;
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        ///     Возвращает значение, показывающее, является ли исчерпывающим списком коллекция стандартных значений,
        ///     возвращаемая методом.
        ///     false - данные можно вводить вручную
        ///     true - только выбор из списка
        ///     В нашем случае - вводить можно только из списка значений!
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}