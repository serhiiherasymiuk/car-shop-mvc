using BusinessLogic.Interfaces;
using DataBase;
using DataBase.Entities;
using DataBase.Interfaces;
using System.Linq;

namespace BusinessLogic.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IRepository<Car> carRepo;
        public AdvertisementService(
                                    IRepository<Car> carRepo)
        {
            this.carRepo = carRepo;
        }

        public List<Car> GetUserAdvertisements(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<Car> { };
            else
                return carRepo.Get(includeProperties: new[] { "Brand", "FavoriteUsers" }).Where(x => x.UserId.ToString() == userId).ToList();
        }
    }
}
