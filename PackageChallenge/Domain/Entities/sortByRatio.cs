using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class sortByRatio : IComparer<Item> {
        public int Compare(Item a, Item b)
        {
            bool temp = a.Cost / a.Weight > b.Cost / b.Weight;
            return temp ? -1 : 1;
        }
    }
}
