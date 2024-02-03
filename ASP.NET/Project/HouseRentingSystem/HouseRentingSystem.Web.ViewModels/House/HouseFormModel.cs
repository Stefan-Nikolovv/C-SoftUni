using HouseRentingSystem.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.House;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class HouseFormModel
    {

        public HouseFormModel()
        {
            this.Categories = new HashSet<HouseSelectCategoryFromModel>();
        }
        [Required]
        [StringLength(HouseTitleMax, MinimumLength = HouseTitleMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(HouseAddressMax, MinimumLength = HouseAddressMin)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(HouseDescriptionMax, MinimumLength = HouseDescriptionMin)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(HouseImageUrlLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), HousePricePerMonthMin, HousePricePerMonthMax)]
        [Display(Name = "Monthly Price")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseSelectCategoryFromModel> Categories { get; set; }
    }
}
