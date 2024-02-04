using HouseRentingSystem.Web.ViewModels.House.Enums;

using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class AllHousesQueryModel
    {
        public AllHousesQueryModel()
        {
            CurrentPage = 1;
            HousePerPage = 3;
            Categories = new HashSet<string>();
            Houses = new HashSet<HouseAllViewModel>();
        }
        public string? Category { get; set; }
        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }
        [Display(Name ="Sort Houses by")]
        public HouseSorting HouseSorting { get; set; }

        public int CurrentPage { get; set; }

        public int HousePerPage { get; set; }
        public int TotalHouses { get; set;}

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<HouseAllViewModel> Houses { get; set; }
    }
}
