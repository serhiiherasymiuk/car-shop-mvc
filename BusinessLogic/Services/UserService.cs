using BusinessLogic.Interfaces;
using DataBase;
using DataBase.Entities;
using DataBase.Interfaces;
using System.Linq;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepo;
        public UserService(
                        IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        public User GetByID(string userId)
        {
            return userRepo.GetByID(userId);
        }
    }
}
