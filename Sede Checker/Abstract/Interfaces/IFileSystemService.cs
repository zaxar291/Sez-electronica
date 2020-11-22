namespace Sede_Checker.Abstract.Interfaces
{
    interface IFileSystemService
    {
        bool Create(string file, string data);
        bool Delete(string file);

        bool CreateDirectory(string directory);

        string Load(string file);
    }
}
