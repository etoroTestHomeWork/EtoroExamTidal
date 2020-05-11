using log4net;
using log4net.Config;

namespace core.logger
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");

        public static void InitLogger()
        {
            var log = LogManager.GetLogger("LOGGER");
            XmlConfigurator.Configure();
        }
    }
}
