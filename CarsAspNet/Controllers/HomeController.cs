using BusinessLogic.Services;
using CarsAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataBase.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Interfaces;
using Org.BouncyCastle.Bcpg;

namespace CarsAspNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService carService;
        private readonly IAdvertisementService advertisementService;
        private readonly IUserService userService;
        public HomeController(ICarService carService, IAdvertisementService advertisementService, IUserService userService)
        {
            this.carService = carService;
            this.advertisementService = advertisementService;
            this.userService = userService;
        }
        public void LoadBrands()
        {
            ViewBag.BrandList = new SelectList(carService.GetBrands(), nameof(Brand.Id), nameof(Brand.Name));
        }
        public IActionResult Index()
        {
            ViewBag.FavoriteCars = carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(carService.GetAll());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult VINDecoder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            var searchInput = form["searchInput"];
            ViewBag.FavoriteCars = carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(carService.GetAll().Where(c => (c.Brand.Name + c.Model).ToLower().Contains(searchInput.ToString().ToLower())).ToList());
        }
        [Authorize]
        public IActionResult Favorite()
        {
            return View(carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [Authorize]
        public IActionResult AddFavorite(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
                carService.AddFavorite(userId, id);
            return RedirectToAction("Index");
        }



        [Authorize]
        public IActionResult CarAdvertisements()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.CarService = carService;
            ViewBag.UserId = userId;
            return View(advertisementService.GetUserAdvertisements(userId));
        }






        public IActionResult Create()
        {
            LoadBrands();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                LoadBrands();
                return View(car);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); ;
            User user = userService.GetByID(userId);
            car.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            user.CarsAdvertisements.Add(car);
            carService.Create(car);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            LoadBrands();
            return View(carService.Get(id));
        }
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (!ModelState.IsValid)
            {
                LoadBrands();
                return View(car);
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); ;
            User user = userService.GetByID(userId);
            car.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            user.CarsAdvertisements.Add(car);
            carService.Edit(car);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            carService.Delete(id);
            return RedirectToAction(nameof(Index));
            //return View("Index", context.Cars.ToList());
        }




        [Authorize]
        public IActionResult RemoveFavorite(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
                carService.RemoveFavorite(userId, id);
            return RedirectToAction("Index");
        }
        public IActionResult FilterFavorites()
        {
            ViewBag.FavoriteCars = carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View("Index", carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public IActionResult FilterExpensive()
        {
            ViewBag.FavoriteCars = carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View("Index", carService.GetAll().OrderByDescending(x => x.Price).ToList());
        }
        public IActionResult FilterCheap()
        {
            ViewBag.FavoriteCars = carService.GetFavorites(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View("Index", carService.GetAll().OrderBy(x => x.Price).ToList());
        }
    }
}