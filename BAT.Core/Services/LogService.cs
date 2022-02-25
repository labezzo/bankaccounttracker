namespace BAT.Core.Services
{
    using log4net;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class LogService
    {
        private static readonly string messageDivider = ": ";

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogError(string msg, Exception ex, [CallerMemberName] string caller = null)
        {
            var completeMsg = GetCompleteMessage(caller, messageDivider, msg);
            if (ex != null)
            {
                logger.Error(completeMsg, ex);
            }
            else
            {
                LogWarn("ex is null on message: " + completeMsg);
            }
        }

        public static void LogInfo(string msg, [CallerMemberName] string caller = null)
        {
            var completeMsg = GetCompleteMessage(caller, messageDivider, msg);
            logger.Info(completeMsg);
        }

        public static void LogWarn(string msg, [CallerMemberName] string caller = null)
        {
            var completeMsg = GetCompleteMessage(caller, messageDivider, msg);
            logger.Warn(completeMsg);
        }

        private static string GetCompleteMessage(params string[] strings)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in strings)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
    }
}
