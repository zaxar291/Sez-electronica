namespace Sede_Checker.Abstract.Interfaces
{
    public interface IStorageService<TData>
    {
        TData GetData();
        bool UpdateData(TData data);
    }
}