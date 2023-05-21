using DataBase.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User GetByID(string userId);
    }
}