using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logger
{
    public static class ApiLoggerFactory
    {
        public static IApiLogger CreateLogger()
        {
            return new ApiLogger();
        }
    }
}
