using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Car> FavoriteCars { get; set; }
        public ICollection<Car> CarsAdvertisements { get; set; }
        public User()
        {
            FavoriteCars = new List<Car>();
            CarsAdvertisements = new List<Car>();
        }

    }
}