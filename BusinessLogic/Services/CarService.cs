using Core.Interfaces;
using DataBase;
using DataBase.Entities;
using DataBase.Interfaces;
using System.Linq;

namespace BusinessLogic.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepo;
        private readonly IRepository<Brand> brandRepo;
        private readonly IRepository<User> userRepo;
        private readonly IFileService fileService;
        public CarService(IRepository<Car> productRepo,
                               IRepository<Brand> categoryRepo,
                                    IRepository<User> userRepo,
                                        IFileService fileService)
        {
            this.carRepo = productRepo;
            this.brandRepo = categoryRepo;
            this.userRepo = userRepo;
            this.fileService = fileService;
        }
        public void Create(Car car)
        {
            //string imagePath = fileService.SaveCarImage(car.Image).Result;
            carRepo.Insert(car);
            brandRepo.Save();
        }

        public void Delete(int id)
        {
            var car = Get(id);
            if (car == null) return;
            carRepo.Delete(car);
            brandRepo.Save();
        }

        public void Edit(Car car)
        {
            carRepo.Update(car);
            brandRepo.Save();
        }

        public List<Car> GetAll()
        {
            return carRepo.Get(includeProperties: new[] { "Brand", "User" }).ToList();
        }

        public List<Brand> GetBrands()
        {
            return brandRepo.Get().ToList();
        }

        public Car? Get(int id)
        {
            if (id < 0) return null;
            return carRepo.GetByID(id);
        }
        public List<Car> Get(int[] ids)
        {
            return carRepo.Get(x => ids.Contains(x.Id)).ToList();
        }

        public void AddFavorite(string userId, int id)
        {
            User user = userRepo.GetByID(userId);
            Car car = carRepo.GetByID(id);
            user.FavoriteCars.Add(car);
            car.FavoriteUsers.Add(user);
            userRepo.Save();
            carRepo.Save();
        }
        public List<Car> GetFavorites(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<Car> { };
            else
            {
                User user = userRepo.GetByID(userId);
                //return carRepo.Get(x => x.FavoriteUsers.Contains(user)).ToList();
                return carRepo.Get(includeProperties: new[] { "Brand", "FavoriteUsers", "User" }).Where(x => x.FavoriteUsers.Contains(user)).ToList();
            }
        }

        public void RemoveFavorite(string userId, int id)
        {
            User user = userRepo.Get(includeProperties: new[] { "FavoriteCars" }).ToList().Where(x => x.Id == userId).FirstOrDefault();
            Car car = carRepo.Get(includeProperties: new[] { "FavoriteUsers", "User" }).ToList().Where(x => x.Id == id).FirstOrDefault();
            user.FavoriteCars.Remove(car);
            car.FavoriteUsers.Remove(user);
            userRepo.Save();
            carRepo.Save();
        }

        public int GetFavoritesCount(string userId, int carId)
        {
            if (string.IsNullOrEmpty(userId))
                return 0;
            else
            {
                User user = userRepo.GetByID(userId);
                return carRepo.Get(includeProperties: new[] { "Brand", "FavoriteUsers", "User" }).Where(x => x.FavoriteUsers.Contains(user) && x.Id == carId).ToList().Count();
            }
        }
    }
}
