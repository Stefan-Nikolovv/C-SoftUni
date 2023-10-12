using System.Text;

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
            if (Cloths.Count < Capacity)
            {
                Cloths.Add(cloth);
            }
        }

        public bool RemoveCloth(string color)
        {
            Cloth searchedColor = GetCloth(color);
            if (Cloths.Remove(searchedColor))
            {
                return true;
            }
            return false;
        }

        public Cloth GetSmallestCloth()
        {
            return Cloths.MinBy(c => c.Size);
        }

        public Cloth GetCloth(string color)
        {
            return Cloths.Find(c => c.Color == color);
        }
        public int GetClothCount

        { get { return Cloths.Count; } }


        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Type} magazine contains:");

            foreach (var cloth in Cloths.OrderBy(c => c.Size))
            {
                sb.AppendLine(cloth.ToString());
            }
            return sb.ToString().TrimEnd();
        }

    }
}
