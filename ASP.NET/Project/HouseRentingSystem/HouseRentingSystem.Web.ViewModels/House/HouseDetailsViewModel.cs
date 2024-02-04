using HouseRentingSystem.Web.ViewModels.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class HouseDetailsViewModel : HouseAllViewModel
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public AgentInfoForHouse agentInfoForHouse { get; set; }
    }
}
