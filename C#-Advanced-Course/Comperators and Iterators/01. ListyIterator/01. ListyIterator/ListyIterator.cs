using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListyIteratorType
{
    public class ListyIterator<T>
    {
        private List<T> customList;
        private int index = 0;
        public ListyIterator(List<T> items)
        {
            this.customList = items;
        }

        public bool Move()
        {
            if(index < customList.Count - 1)
            {
                index++;
                return true;
            }
            return false;
        }

        public bool HasNext ()
        {
          return index < customList.Count - 1;
        }
        public void Print()
        {
            if(customList.Count == 0) 
            {
                throw new InvalidOperationException("Invalid Operation!");
            }
            Console.WriteLine(customList[index]);
        }
    }
}
