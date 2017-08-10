using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Logging
{
    public interface ILogRepository
    {
        bool Save(LogEntry logEntry);
    }
}
