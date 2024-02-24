using HouseRentingSystem.Services.Data.Models.Statistics;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IHouseService houseService;

        public StatisticsApiController(IHouseService houseService)
        {
            this.houseService = houseService;   
        }


        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StatisticsServiceModel), 200)]
        [ProducesResponseType(400)]
        
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel =  await this.houseService.GetStatisctisForHouses();

                return this.Ok(serviceModel);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
