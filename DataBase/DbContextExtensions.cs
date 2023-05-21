using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class DbContextExtensions
    {
        public static void SeedBrands(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(new[]
            {
                new Brand() { Id = 1, Name = "Ford" },
                new Brand() { Id = 2, Name = "BMW" },
                new Brand() { Id = 3, Name = "Toyota" },
                new Brand() { Id = 4, Name = "Honda" },
                new Brand() { Id = 5, Name = "Volvo" },
                new Brand() { Id = 6, Name = "Audi" },
                new Brand() { Id = 7, Name = "Kia" },
                new Brand() { Id = 8, Name = "Volkswagen" },
            });
        }

        public static void SeedCars(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(new[]
            {
                new Car()
                {
                    Id = 1,
                    Model = "Focus",
                    Color = "Red",
                    ReleaseYear = 2018,
                    Price = 9000,
                    BrandId = 1,
                    ImagePath = "https://avtopark.dp.ua/wp-content/uploads/2022/02/14/77433/77433-img_4298-1024x670.jpg",
                    UserId = "24d26aa7-20cf-4ab2-a496-195cc18c6159",
                },
                new Car()
                {
                    Id = 2,
                    Model = "X5",
                    Color = "White",
                    ReleaseYear = 2019,
                    Price = 55000,
                    BrandId = 2,
                    ImagePath = "https://cdn3.riastatic.com/photosnew/auto/photo/bmw_x5__477082513f.jpg",
                    UserId = "24d26aa7-20cf-4ab2-a496-195cc18c6159",
                },
                new Car()
                {
                    Id = 3,
                    Model = "Polo",
                    Color = "White",
                    ReleaseYear = 2017,
                    Price = 8000,
                    BrandId = 8,
                    ImagePath = "https://auto.bigmir.net/i/52/33/98/1/5233981/55ec34c4e0b17dea415ef0ceff4ef6d2-resize_crop_1Xquality_100Xallow_enlarge_0Xw_1200Xh_630.jpg",
                    UserId = "24d26aa7-20cf-4ab2-a496-195cc18c6159",
                },
                new Car()
                {
                    Id = 4,
                    Model = "A7",
                    Color = "Gray",
                    ReleaseYear = 2018,
                    Price = 40000,
                    BrandId = 6,
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/2018_Audi_A7_S_Line_40_TDi_S-A_2.0.jpg/2560px-2018_Audi_A7_S_Line_40_TDi_S-A_2.0.jpg",
                    UserId = "24d26aa7-20cf-4ab2-a496-195cc18c6159",
                },
                new Car()
                {
                    Id = 5,
                    Model = "Corolla",
                    Color = "White",
                    ReleaseYear = 2018,
                    Price = 13000,
                    BrandId = 3,
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/d/dd/2018_Toyota_Corolla_%28ZRE172R%29_Ascent_sedan_%282018-08-27%29.jpg",
                    UserId = "24d26aa7-20cf-4ab2-a496-195cc18c6159",
                }
            });
        }
    }
}
