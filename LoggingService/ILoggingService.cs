using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingService
{
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
