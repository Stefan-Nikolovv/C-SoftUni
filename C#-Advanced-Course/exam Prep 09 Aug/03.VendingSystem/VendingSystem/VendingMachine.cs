
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace VendingSystem
{
    public class VendingMachine

    {

        public VendingMachine(int ButtonCapacity)
        {
            this.ButtonCapacity = ButtonCapacity;
            Drinks = new List<Drink>();
        }

        public int ButtonCapacity { get; set; }
        public List<Drink> Drinks { get; set; }
        public int GetCount => Drinks.Count;
    

        
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
