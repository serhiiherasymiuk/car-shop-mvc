using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using CarsAspNet.Models;
using DataBase;
using DataBase.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace CarsAspNet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IUserService userService;
        public CarsController(ICarService carService, IUserService userService)
        {
            this.carService = carService;
            this.userService = userService;
        }
        public void LoadBrands()
        {
            ViewBag.BrandList = new SelectList(carService.GetBrands(), nameof(Brand.Id), nameof(Brand.Name));
        }
        //[AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.BrandList = carService.GetBrands();
            return View(carService.GetAll());
        }
        [AllowAnonymous]
        public IActionResult Details(int id, string returnUrl = null)
        {
            if (id < 0) return BadRequest();
            Car car = carService.GetAll().Where(x => x.Id == id).First();
            if (car == null) return NotFound();
            ViewBag.ReturnUrl = returnUrl;
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            carService.Delete(id);
            return RedirectToAction(nameof(Index));
            //return View("Index", context.Cars.ToList());
        }
        public IActionResult Create(string returnUrl = null)
        {
            LoadBrands();
            ViewBag.ReturnUrl = returnUrl;
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = userService.GetByID(userId);
            car.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            user.CarsAdvertisements.Add(car);
            carService.Edit(car);
            return RedirectToAction(nameof(Index));
        }
    }
}
