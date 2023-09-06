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
        {

            get
            {
                InvalidIndex(index);
                return items[index];

            }
          set 
            {
                InvalidIndex(index);
                items[index] = value; 
            }
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
        public int RemoveAt(int index)
        {
            int removedItem = items[index];

            items[index] = default;

            ShiftLeft(index);

            Count--;

            if (index < 0 || index >= Count)
            {
                InvalidIndex(index);
            }
            if(items.Length / 4 == Count)
            {
                Shrink();
            }

            
            return removedItem;
        }

        public void InsertAt(int index, int value)
        {
            if (index < 0 || index >= Count)
            {
                InvalidIndex(index);
            }
            if (items.Length == Count)
            {
                Resize();
            }

            ShifToRight(index);
            items[index] = value;
            Count++;
        }
        public bool Contains(int element)
        {
            for (int i = 0; i < Count;i++)
            {
                if(items[i] == element)
                {
                    return true;
                }
            }
            return false;
        }
        public void Swap (int firstIndex, int secondIndex)
        {
            InvalidIndex(firstIndex);
            InvalidIndex(secondIndex);

            int temp = items[firstIndex];
            items[firstIndex] = items[secondIndex];
            items[secondIndex] = temp;
        }

        public int? Find(int element)
        {
            for (int i = 0; i < Count ; i++)
            {
                if (items[i] == element)
                {
                    return items[i];
                }
            }
            return null;
        }
        public int[] Reverse()
        {
            for(int i = 0;i < Count / InitialCapacity;i++)
            {
                int temp = items[i];
                items[i] = items[Count - 1 - i];
                items[Count - 1 - i] = temp;
            }
            return items;
        }

        public string toString()
        {
            string strings = string.Empty;
            for (int i = 0; i < Count;i++)
            {
                strings += ($"{items[i] + " "}");
            }
            return strings.TrimEnd();
        }

        private void ShifToRight(int index)
        {
           for( int i = Count; i > index; i--) 
            {
                items[i] = items[i - 1];
            }
        }

        private void ShiftLeft(int index)
        {
           

            for(int i = index; i < Count; i++)
            {
                items[i] = items[i + 1];
            }

        }

        private void Resize()
        {
            int[] copy = new int[items.Length * InitialCapacity];

            for(int i = 0; i < Count;i++)
            {
                copy[i] = items[i];
            }
            items = copy;
        }

        private void Shrink()
        {
            int[] copy = new int[items.Length / InitialCapacity];

            for (int i = 0;i < Count; i++)
            {
                copy[i] = items[i];
            }
            items = copy;

        }

        private void InvalidIndex(int index)
        {
            if(index < 0 || index >= items.Length)
            {
                throw new ArgumentOutOfRangeException($"Invalid {index}");
            }
            
        }
    }
}
