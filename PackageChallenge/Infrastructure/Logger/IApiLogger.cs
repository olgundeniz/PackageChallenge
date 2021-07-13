using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logger
{
    
    public interface IApiLogger
    {
        public ILogger _logger { get; set; }
        public void Error(Exception exception, string messageTemplate);
    }
}
