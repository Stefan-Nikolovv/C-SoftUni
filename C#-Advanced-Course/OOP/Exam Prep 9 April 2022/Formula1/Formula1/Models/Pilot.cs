using System;
using System.Reflection;
using Formula1.Models.Contracts;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullname;
        private IFormulaOneCar car;
        private int numberOfWins;
        private bool canRace;

        public Pilot(string fullName)
        {
            FullName = fullName;
            canRace = false;
        }

        public string FullName
        {
            get { return fullname; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid pilot name: {fullname}.");
                }
                fullname = value;
            }
        }
        public IFormulaOneCar Car
        {

            get { return car; }
            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException($"Pilot car can not be null.");
                }
                car = value;

            }
        }

        public int NumberOfWins
        {
            get => numberOfWins;
            private set => numberOfWins = value;
        }

        public bool CanRace
        {
            get => canRace; private set => canRace = value;
        }


        public void AddCar(IFormulaOneCar car)
        {
           Car = car;
            CanRace = true;
        }

        public void WinRace()
        {

            NumberOfWins++;
        }

        public override string ToString()
        {

            return $"Pilot {fullname} has {numberOfWins} wins.";
        }
    }
}
