using HouseRentingSystem.Web.ViewModels.House;

namespace HouseRentingSystem.Areas.Admin.ViewModels.House
{
    public class MyHousesViewModel
    {
        public IEnumerable<HouseAllViewModel> AddedHouses { get; set; } = null!;
        public IEnumerable<HouseAllViewModel> RentedHouses { get; set; } = null!;
    }
}
