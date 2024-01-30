using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace APIChatPlus.Data
{
    public static class Logs
    {
        readonly static Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public static void WriteDebug (String message)  => logger.Debug(message);
    }
}
