using System;
using System.Collections;
using System.ComponentModel;

namespace Sede_Checker.Entities.Collections.Regions
{
    /// <summary>
    ///     Реализация коллекции объектов - регионов Испании
    /// </summary>
    [DisplayName("Region in Spain")]
    [Description("Regions in Spain")]
    [Category("Region")]
    public class SpainRegionCollection : CollectionBase, ICustomTypeDescriptor
    {
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            var pds = new PropertyDescriptorCollection(null);

            for (var i = 0; i < List.Count; i++)
            {
                var pd = new SpainRegionCollectionPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #region Методы коллекции

        public void Add(SpainRegion item)
        {
            List.Add(item);
        }

        public void Remove(SpainRegion item)
        {
            List.Remove(item);
        }

        public SpainRegion this[int index] => (SpainRegion) List[index];

        #endregion
    }
}