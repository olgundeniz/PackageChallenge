using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class sortByC : IComparer<Node>
    {
        public int Compare(Node a, Node b)
        {
            bool temp = a.lb > b.lb;
            return temp ? 1 : -1;
        }
    }
}
