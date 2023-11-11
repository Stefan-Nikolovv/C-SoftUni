namespace AutomotiveRepairShop
{
    public class Vehicle
    {

        public Vehicle(string vin, int mymileage, string damage)
        {
            VIN = vin;
            MyMileage = mymileage;
            Damage = damage;
        }

        public override string ToString()
        {
            return $"Damage: {Damage}, Vehicle: {VIN} ({MyMileage} km)";
        }
        public string VIN { get; private set; }
        public int MyMileage { get; private set; }
        public string Damage { get; private set; }
    }
}
