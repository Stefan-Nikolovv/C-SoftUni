using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace VendingSystem
{
    public class VendingMachine
    {
        public int ButtonCapacity;
        public List<Drink> Drinks;
        public int GetCount
        {
            get { return Drinks.Count; }
        }
        

        public VendingMachine(int button)
        {
            this.ButtonCapacity = button;
            Drinks = new List<Drink>();
        }
        public void AddDrink(Drink drink)
        {
            if(Drinks.Count < ButtonCapacity)
            {
                Drinks.Add(drink);
            }
        }

        public bool RemoveDrink(string name)
        {
          
           bool isRemoved = Drinks.Remove(Drinks.FirstOrDefault(x => x.Name == name));
            return isRemoved;
        }
        

        public Drink GetLongest()
        {
            return Drinks.OrderByDescending(x => x.Volume).FirstOrDefault();
        }

        public Drink GetCheapest() 
        {
            return Drinks.OrderBy(x => x.Price).FirstOrDefault();
        }
        public string BuyDrink(string name) 
        {
            return Drinks.FirstOrDefault(x => x.Name == name).ToString().TrimEnd();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Drinks available:");
            foreach (var item in Drinks)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
