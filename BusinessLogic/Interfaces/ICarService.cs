using DataBase.Entities;

public interface ICarService
{
    List<Brand> GetBrands();
    List<Car> Get(int[] ids);
    List<Car> GetAll();
    Car? Get(int id);
    void Create(Car car);
    void Edit(Car car);
    void Delete(int id);
    void AddFavorite(string userId, int id);
    void RemoveFavorite(string userId, int id);
    int GetFavoritesCount(string userId, int carId);
    List<Car> GetFavorites(string userId);
}