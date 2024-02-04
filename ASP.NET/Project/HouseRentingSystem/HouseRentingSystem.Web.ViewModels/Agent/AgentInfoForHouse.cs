using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Web.ViewModels.Agent
{
    public class AgentInfoForHouse
    {
        public string Email { get; set; }
        [Display(Name = "Phone")]

        public string PhoneNumber { get; set; }
    }
}
