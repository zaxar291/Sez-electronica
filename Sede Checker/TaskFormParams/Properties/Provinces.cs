using System.ComponentModel;
using System.Collections.Generic;

public class ProvincesConverter : StringConverter
{
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
    public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        List<string> list = new List<string>();
        list.Add("A Coruña");
        list.Add("Albacete");
        list.Add("Alicante");
        list.Add("Almeria");
        list.Add("Araba");
        list.Add("Asturias");
        list.Add("Badajoz");
        list.Add("Barcelona");
        list.Add("Bizkaia");
        list.Add("Cáceres");
        list.Add("Cantabria");
        list.Add("Castellon");
        list.Add("Ceuta");
        list.Add("Ciudad Real");
        list.Add("Cordoba");
        list.Add("Cuenca");
        list.Add("Girona");
        list.Add("Granada");
        list.Add("Guadalajara");
        list.Add("Guipúzcoa");
        list.Add("Huelva");
        list.Add("Huesca");
        list.Add("Illes Balears");
        list.Add("Jaen");
        list.Add("Las Palmas");
        list.Add("León");
        list.Add("Lleida");
        list.Add("Logroño");
        list.Add("Madrid");
        list.Add("Malaga");
        list.Add("Melilla");
        list.Add("Murcia");
        list.Add("Orense");
        list.Add("S.Cruz Tenerife");
        list.Add("Salamanca");
        list.Add("Sevilla");
        list.Add("Tarragona");
        list.Add("Teruel");
        list.Add("Toledo");
        list.Add("Valencia");
        list.Add("Zamora");
        list.Add("Zaragoza");
        return new StandardValuesCollection(list);
    }
}