using System;
using System.Collections;
using System.ComponentModel;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Реализация коллекции объектов - процерур в регионах Испании
    /// </summary>
    [DisplayName("Procedures in Spain region")]
    [Description("Procedures in Spain region")]
    [Category("Procedures")]
    public class SpainRegionProcedureCollection : CollectionBase, ICustomTypeDescriptor
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
                var pd = new SpainRegionProcedureCollectionPropertyDescriptor(this, i);
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

        public void Add(SpainRegionProcedure item)
        {
            List.Add(item);
        }

        public void Remove(SpainRegionProcedure item)
        {
            List.Remove(item);
        }

        public SpainRegionProcedure this[int index] => (SpainRegionProcedure) List[index];

        #endregion
    }
}