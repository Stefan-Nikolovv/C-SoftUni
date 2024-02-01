
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.Agent;

namespace HouseRentingSystem.Web.ViewModels.Agent
{
    public class BecomeAgentFromModel
    {
        [Required]
        [StringLength(AgentPhoneNumberMax, MinimumLength = AgentPhoneNumberMin)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
    }
}
