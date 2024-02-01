using HouseRentingSystem.Models;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystems.Services.Data;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly IHouseService houseService;
        public HomeController(IHouseService houseService)
        {
           this.houseService = houseService;    
        }

        public async Task<IActionResult> Index()
        {
          IEnumerable<HouseIndexViewModel> houses = 
                await this.houseService.GetLastThreeHouseAsync();
            return View(houses);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
