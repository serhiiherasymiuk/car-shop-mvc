using DataBase.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IAdvertisementService
    {
        List<Car> GetUserAdvertisements(string userId);
    }
}