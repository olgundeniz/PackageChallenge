using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Exceptions
{
    public static class ExceptionMessage
    {
        public const string MaxWeightOfPackage = "Max weight of a package can not be more than 100";
        public const string MaxItemCount = "Max item count can not be more than 15";
        public const string MaxWeightAndCostOfItem = "Max weight and cost of an item can not be more than 100";
    }
}
