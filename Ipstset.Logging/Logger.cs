using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Logging
{
    public class Logger:ILogger
    {
        private ILogRepository _repository;
        public Logger(string connection)
        {
            _repository = new LogRepository(connection);
        }

        public void Log(LogEntry logEntry)
        {
            _repository.Save(logEntry);
        }

    }
}
