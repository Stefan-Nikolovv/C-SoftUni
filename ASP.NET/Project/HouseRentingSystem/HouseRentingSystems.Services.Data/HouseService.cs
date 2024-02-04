using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
using HouseRentingSystem.Web.ViewModels.House.Enums;
using HouseRentingSystems.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Web.ViewModels.Agent;

namespace HouseRentingSystems.Services.Data
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel)
        {
            IQueryable<House> housesQuery = this.dbContext.Houses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                housesQuery = housesQuery
                    .Where(h => h.Category.Name == queryModel.Category);
            }

            if(!string.IsNullOrWhiteSpace (queryModel.SearchString)) 
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                housesQuery = housesQuery
                   .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                               EF.Functions.Like(h.Address, wildCard) ||
                               EF.Functions.Like(h.Description, wildCard));
            }
            housesQuery = queryModel.HouseSorting switch
            {
                HouseSorting.Newest => housesQuery.OrderBy(h => h.CreatedOn),
                HouseSorting.Oldest => housesQuery.OrderByDescending(h => h.CreatedOn),
                HouseSorting.PriceAscending => housesQuery.OrderBy(h => h.PricePerMonth),
                HouseSorting.PriceDescending => housesQuery.OrderByDescending(h => h.PricePerMonth),
                _ => housesQuery.OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.CreatedOn)
            };

            IEnumerable<HouseAllViewModel> allHouses = await housesQuery
                                                       .Where(h => h.isActive)
                                                       .Skip((queryModel.CurrentPage - 1) * queryModel.HousePerPage)
                                                       .Take(queryModel.HousePerPage)
                                                       .Select(h => new HouseAllViewModel()
                                                       {
                                                           Id = h.Id.ToString(),
                                                           Title = h.Title,
                                                           Address = h.Address,
                                                           ImageUrl = h.ImageUrl,
                                                           PricePerMonth = h.PricePerMonth,
                                                           IsRenting = h.RenterId.HasValue
                                                       })
                                                       .ToArrayAsync();
            int totalHouses = housesQuery.Count();

            return new AllHousesFilteredAndPagedServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = allHouses
            };
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<HouseAllViewModel> allAgnetHouses = await this.dbContext.Houses
                                                            .Where(h => h.isActive)
                                                            .Where(h => h.AgentId.ToString() == agentId)
                                                            .Select(h => new HouseAllViewModel()
                                                            {
                                                                Id= h.Id.ToString(),
                                                                Title = h.Title,
                                                                Address = h.Address,
                                                                ImageUrl = h.ImageUrl,
                                                                PricePerMonth = h.PricePerMonth,
                                                                IsRenting= h.RenterId.HasValue
                                                            })
                                                            .ToArrayAsync();
            return allAgnetHouses;
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<HouseAllViewModel> allUserHouses = await this.dbContext.Houses
                                                          .Where(h => h.isActive)
                                                          .Where(h => h.RenterId.HasValue 
                                                          && h.RenterId.ToString() == userId)
                                                          .Select(h => new HouseAllViewModel()
                                                          {
                                                              Id = h.Id.ToString(),
                                                              Title = h.Title,
                                                              Address = h.Address,
                                                              ImageUrl = h.ImageUrl,
                                                              PricePerMonth = h.PricePerMonth,
                                                              IsRenting = h.RenterId.HasValue
                                                          })
                                                          .ToArrayAsync();
            return allUserHouses;
        }

        public async Task CreateAsync(HouseFormModel model, string agentId)
        {
            House modelForm = new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId  = model.CategoryId,
                AgentId = Guid.Parse(agentId),
               
            };
            await this.dbContext.Houses.AddAsync(modelForm);
             await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<HouseIndexViewModel>> GetAllHouseAsync()
        {
            IEnumerable<HouseIndexViewModel> result = await this.dbContext.Houses.Where(h => h.isActive).Select(x => new HouseIndexViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();
            return result;
        }

        public async Task<HouseDetailsViewModel?> GetHouseDetailsAsync(string houseId)
        {
            House? house = await this.dbContext
                   .Houses
                   .Include(h => h.Category)
                   .Include(h => h.Agent)
                   .ThenInclude(a => a.User)
                  .Where(h => h.isActive)
                  .FirstOrDefaultAsync(h => h.Id.ToString() == houseId);

            if(house == null)
            {
                return null;
            }

            return new HouseDetailsViewModel()
            {
                Id = house.Id.ToString(),
                Title = house.Title,
                ImageUrl = house.ImageUrl,
                Address = house.Address,
                PricePerMonth = house.PricePerMonth,
                IsRenting = house.RenterId.HasValue,
                Description = house.Description,
                Category = house.Category.Name,
                agentInfoForHouse = new AgentInfoForHouse()
                {
                    Email = house.Agent.User.Email,
                    PhoneNumber = house.Agent.PhoneNumber,
                }
            };
        }

        public async Task<IEnumerable<HouseIndexViewModel>> GetLastThreeHouseAsync()
        {
            IEnumerable<HouseIndexViewModel> result = await this.dbContext
                                                                .Houses
                                                                .Where(h => h.isActive)
                                                                .OrderByDescending(x => x.CreatedOn)
                                                                .Take(3)
                                                                .Select(x => new HouseIndexViewModel
                                                                {
                                                                    Id = x.Id.ToString(),
                                                                    Title = x.Title,
                                                                    ImageUrl = x.ImageUrl,
                                                                })
                                                                .ToArrayAsync();
            return result;
        }
    }
}
