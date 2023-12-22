

using System;
using Formula1.Core;
using Formula1.Models.Contracts;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsepower;
        private double enginedisplacement;

        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
        }

        public string Model 
        { 
            get { return model; }
           private set 
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException($"Invalid car model: {model}.");
                }
                model = value; 
            }
        }

        public int Horsepower
        {
            get { return horsepower; }
            private set
            {
                if (value < 900 || value > 1050)
                {
                    throw new ArgumentException($"Invalid car horsepower: {horsepower}.");
                }
                horsepower = value;
            }
        }

        public double EngineDisplacement
        {
            get { return enginedisplacement; }
            private set
            {
                if (value < 1.60 || value > 2.00)
                {
                    throw new ArgumentException($"Invalid car engine displacement: {enginedisplacement}.");
                }
                enginedisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
        {
           return enginedisplacement / horsepower * laps;
        }
    }
}
