using System;

using System.Text;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int capacity;
        private DelicacyRepository delicacyRepository;
        private CocktailRepository cocktailRepository;


        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            CurrentBill = 0;
            Turnover = 0;
            IsReserved = false;
            delicacyRepository = new DelicacyRepository();
            cocktailRepository = new CocktailRepository();
        }

        public int BoothId { get; private set; }

        public int Capacity 
        {
            get { return capacity; } 
             private set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Capacity has to be greater than 0!");
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyRepository;

        public IRepository<ICocktail> CocktailMenu => cocktailRepository;

        public double CurrentBill { get; private set; }

        public double Turnover { get; private set; }

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            if (IsReserved)
            {
                this.IsReserved = false;
            }
            else
            {
                this.IsReserved = true;
            }
        }

        public void Charge()
        {
            this.Turnover += CurrentBill;
            this.CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine("-Cocktail menu:");
            foreach( var item in this.CocktailMenu.Models )
            {
                sb.AppendLine($"--{item.ToString()}");
            }
            sb.AppendLine($"-Delicacy menu:");
            foreach (var item in delicacyRepository.Models)
            {
                sb.AppendLine($"--{item.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
