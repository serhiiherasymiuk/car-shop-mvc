using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int ReleaseYear { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<User> FavoriteUsers { get; set; }
        public int OwnersNumber { get; set; }
        public int Run { get; set; }
        public Car()
        {
            FavoriteUsers = new List<User>();
        }
    }
}
