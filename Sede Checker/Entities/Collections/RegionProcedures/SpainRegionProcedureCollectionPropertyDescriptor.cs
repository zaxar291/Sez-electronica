using System;
using System.ComponentModel;
using System.Text;

namespace Sede_Checker.Entities.Collections.RegionProcedures
{
    /// <summary>
    ///     Класс описания свойств коллекции объектов SpainRegionProcedure
    /// </summary>
    public class SpainRegionProcedureCollectionPropertyDescriptor : PropertyDescriptor
    {
        private readonly SpainRegionProcedureCollection _collection;
        private readonly int _index;

        public SpainRegionProcedureCollectionPropertyDescriptor(SpainRegionProcedureCollection coll, int idx) :
            base($"#{idx}", null)
        {
            _collection = coll;
            _index = idx;
        }

        public override AttributeCollection Attributes => new AttributeCollection(null);

        public override Type ComponentType => _collection.GetType();

        public override string DisplayName
        {
            get
            {
                var i = _collection[_index];
                return i.Name;
            }
        }

        public override string Description
        {
            get
            {
                var i = _collection[_index];
                var sb = new StringBuilder();
                sb.Append(i.Name);
                sb.Append(", ");
                sb.Append(_index + 1);

                return sb.ToString();
            }
        }

        public override bool IsReadOnly => false;

        public override string Name => $"#{_index}";

        public override Type PropertyType => _collection[_index].GetType();

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return _collection[_index];
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}