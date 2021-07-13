using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logger
{
    public class ApiLogger : IApiLogger
    {
        public ApiLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("./logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public ILogger _logger { get ; set; }
        
        public void Error(Exception exception, string messageTemplate)
        {
            _logger.Error(exception, messageTemplate);
        }
    }
}
