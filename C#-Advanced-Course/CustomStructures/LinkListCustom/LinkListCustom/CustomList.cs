using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkListCustom
{
    public class CustomList
    {
        private const int InitialCapacity = 2;
        private int[] items;
        public CustomList()
        {
            items = new int[InitialCapacity];
        }
        public int Count { get; private set; }
        public int this[int index]
        { get 
            { return items[index]; }
            set { items[index] = value; }
        }
        public void Add(int value)
        {
            if(items.Length == Count)
            {
                Resize();
            }
            items[Count] = value;
            Count++;
        }

        private void Resize()
        {
            int[] copy = new int[items.Length * InitialCapacity];

            for(int i = 0; i < Count-1;i++)
            {
                copy[i] = items[i];
            }
            items = copy;
        }

        private void InvalidIndex (int index)
        {
            if(index < 0 || index >= items.Length)
            {
                throw new ArgumentOutOfRangeException($"Invalid {index}");
            }
            
        }
    }
}
