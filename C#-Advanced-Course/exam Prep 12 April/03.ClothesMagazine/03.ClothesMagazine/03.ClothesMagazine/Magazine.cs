using System.Drawing;

namespace ClothesMagazine
{
    public class Magazine
    {
        private string Type { get; set; }
        private int Capacity { get; set; }

        private List<Cloth> Cloths;

        public Magazine(string type, int capacity)
        {
            Type = type;
            Capacity = capacity;
            Cloths = new List<Cloth>();
        }

        public void AddCloth(Cloth cloth)
        {
            if(Cloths.Count < Capacity) 
            { 
                Cloths.Add(cloth);
            }
        }

        public bool RemoveCloth(string color)
        {
            Cloth searchedColor = GetColor(color);
           bool isRemoved = Cloths.Remove(searchedColor);
            return isRemoved;
        }

        public Cloth GetSmallestCloth()
        {
            return Cloths;
        }

        private Cloth GetColor(string color)
        {
            return Cloths.Find(c => c.Color == color);
        }
    }
}
