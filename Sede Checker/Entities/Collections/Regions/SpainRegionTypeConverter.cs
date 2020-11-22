using System;
using System.ComponentModel;
using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker.Entities.Collections.Regions
{
    /// <summary>
    /// Класс преобразования типа SpainRegion
    /// Выполняет преобразование к типу String и обратно в SpainRegion
    /// Также, позволяет отображать в виде выпадающего списка коллекцию должностей
    /// </summary>
    public class SpainRegionTypeConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Возвращает значение, показывающее, поддерживает ли объект стандартный набор значений, 
        /// которые можно выбрать из списка.
        /// Если не установить принудительно значение в true, есть вероятность, 
        /// что обработчик не будет воспринимать ваш метод GetStandardValues и вы не увидите 
        /// ваш список значений для выбора
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Этот метод можно исключить, но в перспективе он позволит использовать данный класс конвертора 
        /// для вызова из разных классов.
        /// Для этого достаточно добавить в секцию case имя другого класса, который должен содержать свойство 
        /// SpainRegion с функцией связи с выпадающим списком
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private SpainRegionCollection GetCollection(System.ComponentModel.ITypeDescriptorContext context)
        {
            SpainRegionCollection c = new SpainRegionCollection();
            //switch (context.Instance.GetType().Name)
            //{
            //    //case "Employee":
            //    //    c = ((Employee)context.Instance).SpainRegions;
            //    //    break;
            //    //default:
            //    //    c = ((Employee)context.Instance).SpainRegions;
            //    //    break;
            //}

            c = ((SedeTaskData)context.Instance).ProcedureRegionList;
            return c;
        }

        /// <summary>
        /// Метод возвращает список значений из коллекции должностей для отображения в выпадающем 
        /// списке значений PropertyGrid для должности сотрудника
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(GetCollection(context));
        }

        /// <summary>
        /// Метод проверяет, можно ли преобразовывать полученное значение свойства от пользователя 
        /// в нужный нам тип
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// Преобразование типа SpainRegion в строку для отображения значения в поле PropertyGrid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string) || !(value is SpainRegion))
                return base.ConvertTo(context, culture, value, destinationType);
            SpainRegion item = (SpainRegion)value;
            return item.Name;
        }

        /// <summary>
        /// Проверка на возможность обратного преобразования строкового представления в тип SpainRegion
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// Преобразование строкового представления должности в тип SpainRegion.
        /// Недостаток данного преобразования - правильность работы приведения типа зависит от формата 
        /// строкового представления типа SpainRegion, а именно, от дублирующихся значений.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                SpainRegion itemSelected = GetCollection(context).Count.Equals(0) ?
                    new SpainRegion() : GetCollection(context)[0];

                foreach (SpainRegion Item in GetCollection(context))
                {
                    string sCraftName = Item.Name;
                    if (sCraftName.Equals((string)value))
                    {
                        itemSelected = Item;
                    }
                }
                return itemSelected;
            }
            else
                return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Возвращает значение, показывающее, является ли исчерпывающим списком коллекция стандартных значений, 
        /// возвращаемая методом.
        /// 
        /// false - данные можно вводить вручную
        /// true - только выбор из списка
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Конструктор метода
        /// </summary>
        public SpainRegionTypeConverter() { }
    }
}
