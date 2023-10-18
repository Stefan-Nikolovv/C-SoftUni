using System.Text;

namespace AutomotiveRepairShop
{
    public class RepairShop
    {

        private int capacity;
        private List<Vehicle> cars;

        public RepairShop(int capacity)
        {
            Cars = new List<Vehicle>();
            Capacity = capacity;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (Cars.Count < this.Capacity)
            {
                Cars.Add(vehicle);
            }
        }
        public bool RemoveVehicle(string vin)
        {
            Vehicle vehicle = Cars.FirstOrDefault(x => x.VIN == vin);
            if (vehicle != null)
            {
                Cars.Remove(vehicle);
                return true;
            }
            return false;
        }
        public int GetCount()
        {
            return Cars.Count;
        }

        public Vehicle GetLowestMileage()
        {
            return Cars.MinBy(x => x.MyMileage);
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Vehicles in the preparatory");

            foreach (var car in Cars)
            {
                sb.AppendLine(car.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public List<Vehicle> Cars
        {
            get { return cars; }
            set { cars = value; }
        }
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
    }
}
