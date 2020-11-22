using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Sede_Checker;

class ProvinceConverter : TypeConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        List<string> l = new List<string>();
        l.Add("Firstly select procedure");
        return new StandardValuesCollection(l);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        if (value is string)
        {
            return String.Empty;
        }
        return base.ConvertFrom(context, culture, value);
    }
}
