using HouseRentingSystem.Web.ViewModels.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystems.Services.Data.Interfaces
{
    public interface IAgentService
    {
        Task<bool> AgentExistsByUserId (string userId);
        Task<bool> AgentExistsPhoneNumberAsync(string phoneNumber);
        Task<bool> UserHasRentsAsync(string userId);

        Task Create(string userId, BecomeAgentFromModel model);
        Task<string> GetAgentIdByUserIdAsync(string userId);
    }
}
