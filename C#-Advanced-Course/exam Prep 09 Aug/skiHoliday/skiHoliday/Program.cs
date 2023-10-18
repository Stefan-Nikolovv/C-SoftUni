namespace SkiHoliday
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int stayingDays = int.Parse(Console.ReadLine());
            string roomType = Console.ReadLine();
            string assessment = Console.ReadLine();

            double price = 0;

            switch (roomType)
            {
                case "room for one person":
                    price = 118.00; 
                    break;
                case "apartment":
                    price = 155.00;
                    if (stayingDays < 10)
                    { 
                        price *= 0.7; 
                    }
                    else if (stayingDays >= 10 && stayingDays <= 15)
                    {
                        price *= 0.65; 
                    }
                    else if (stayingDays > 15)
                    { 
                        price *= 0.5; 
                    }
                    break;
                case "president apartment":
                    price = 235.00;
                    if (stayingDays < 10)
                    {
                        price *= 0.9; 
                    }
                    else if (stayingDays >= 10 && stayingDays <= 15)
                    {
                        price *= 0.85;
                    }
                    else if (stayingDays > 15)
                    {
                        price *= 0.80;
                    }
                    break;
            }

            double totalStayPrice = (stayingDays - 1) * price;
            if (assessment == "positive")
            {
                totalStayPrice *= 1.25;
                
            }
            else if (assessment == "negative")
            {
                totalStayPrice *= 0.90;
            }
            Console.WriteLine(totalStayPrice.ToString("F2"));

        }
    }
}