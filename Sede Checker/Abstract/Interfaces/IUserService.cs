using Sede_Checker.Entities.DTO;

namespace Sede_Checker.Abstract.Interfaces
{
    internal interface IUserService
    {
        bool AddUser(SedeCheckerUserDto data);
        bool RemoveUser(int UserId);
        bool UpdateUser(SedeCheckerUserDto user);
        int GetUserId();
        string GetUsers();
        SedeCheckerUserDto GetUserById(int id);
    }
}