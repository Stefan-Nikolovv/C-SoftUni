using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Repositories;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {

            var boothId = booths.Models.Count + 1;
            var boot = new Booth(boothId, capacity);
           
            booths.AddModel(boot);

            return $"Added booth number {boothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if(cocktailTypeName != "MulledWine" && cocktailTypeName != "Hibernation")
            {
                return $"Cocktail type {cocktailTypeName} is not supported in our application!";
            }

            if(size != "Small" && size != "Middle" && size != "Large")
            {
                return $"{size} is not recognized as valid cocktail size!";
            }
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            var coctail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == cocktailName && c.Size == size);

            if(coctail != null)
            {
                return $"{size} {cocktailName} is already added in the pastry shop!";
            }

            if(cocktailTypeName == "MulledWine")
            {
                 coctail = new MulledWine(cocktailName, size);
            }
            else if (cocktailTypeName == "Hibernation") 
            {
                coctail = new Hibernation(cocktailName, size);
            }
            booth.CocktailMenu.AddModel(coctail);
            return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            //var delicacy = new 
            if(delicacyTypeName != "Stolen" && delicacyTypeName != "Gingerbread")
            {
                return $"Delicacy type {delicacyTypeName} is not supported in our application!";
            }
            var bootid = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            var delicacy = bootid.DelicacyMenu.Models.FirstOrDefault(n => n.Name == delicacyName);

            if(delicacy != null)
            {
                return $"{delicacyName} is already added in the pastry shop!";
            }

           if(delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else if(delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
           bootid.DelicacyMenu.AddModel(delicacy);

            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
        }

        public string BoothReport(int boothId)
        {
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

           return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            var sb = new StringBuilder();
            var booth = booths.Models.Where(b => b.BoothId == boothId)
                .FirstOrDefault();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            booth.Charge();
            booth.ChangeStatus();
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var boot = booths.Models.
                Where(r => !r.IsReserved && r.Capacity >= countOfPeople)
                .OrderBy(r => r.Capacity)
                .ThenByDescending(r => r.BoothId)
                .FirstOrDefault();

            if (boot == null)
            {
                return $"No available booth for {countOfPeople} people!";
            }
          
                boot.ChangeStatus();
            return $"Booth {boot.BoothId} has been reserved for {countOfPeople} people!";
        }

        public string TryOrder(int boothId, string order)
        {
            string[] ordersCommands = order.Split('/');
            string itemTypeName = ordersCommands[0];
            string itemName = ordersCommands[1];
            int orderCount = int.Parse(ordersCommands[2]);
            string? size = ordersCommands[3];

            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (itemTypeName != "MulledWine" && itemTypeName != "Hibernation"
                && itemTypeName != "Stolen" && itemTypeName != "Gingerbread")
            {
                return $"{itemTypeName} is not recognized type!";
            }

            if(itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                if(!booth.CocktailMenu.Models.Any(c => c.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }
                var coctail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);
                if (coctail == null)
                {
                    return $"There is no {size} {itemName} available!";
                }
                booth.UpdateCurrentBill(orderCount * coctail.Price);

                return $"Booth {boothId} ordered {orderCount} {itemName}!";
            } 
            else
            {
                if (!booth.DelicacyMenu.Models.Any(c => c.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }
                var delicates = booth.DelicacyMenu.Models.FirstOrDefault(c => c.Name == itemName );
                if (delicates == null)
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }

                booth.UpdateCurrentBill(orderCount * delicates.Price);

                return $"Booth {boothId} ordered {orderCount} {itemName}!";
            }




        }
    }
}
