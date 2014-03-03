using System;
using System.Text;
using PX.Library.Common;
using log4net;

namespace PX.Library.Logging
{
    public class Logger
    {
        private readonly ILog _logger;

        public Logger(Type type)
        {
            _logger = LogManager.GetLogger(type);
        }

        #region Info Log

        public void Info(string message)
        {
            _logger.Info(message);
        }

        #endregion

        #region Warning Log

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(Exception exception)
        {
            _logger.Warn(exception);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        #endregion

        #region Debug Log

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        #endregion

        #region Error Log

        public void Error(string message)
        {
            _logger.Error(message);
            SendEmail(message, message);
        }

        public void Error(Exception exception)
        {
            Error(LogUtility.BuildExceptionMessage(exception));
            SendEmail(exception.Message, LogUtility.BuildExceptionMessage(exception));
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
            SendEmail(message, LogUtility.BuildExceptionMessage(exception));
        }

        #endregion

        #region Fatal Log

        public void Fatal(string message)
        {
            _logger.Fatal(message);
            SendEmail(message, message);
        }

        public void Fatal(Exception exception)
        {
            Fatal(LogUtility.BuildExceptionMessage(exception));
            SendEmail(exception.Message, LogUtility.BuildExceptionMessage(exception));
        }

        public void Fatal(string message, Exception exception)
        {
            Fatal(LogUtility.BuildExceptionMessage(exception));
            SendEmail(message, LogUtility.BuildExceptionMessage(exception));
        }

        #endregion

        /// <summary>
        /// Send logging email
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string subject, string body)
        {
            subject = "WebEd 8 Error Logging";
            var mailService = new MailUtilities();
            mailService.SendEmail("abc@abc.com", "nam.le@saigontechnology.vn", subject, body);
        }
    }

    public class LogUtility
    {
        public static string BuildExceptionMessage(Exception exception)
        {

            var logException = exception;
            if (exception.InnerException != null)
                logException = exception.InnerException;

            var sb = new StringBuilder();

            try
            {
                sb.AppendLine("\n Error in Path :" + System.Web.HttpContext.Current.Request.Path);

                // Get the QueryString along with the Virtual Path
                sb.AppendLine("\n Raw Url :" + System.Web.HttpContext.Current.Request.RawUrl);
            }
            catch
            {
            }

            // Get the error message
            sb.AppendLine("\n Message :" + logException.Message);

            // Source of the message
            sb.AppendLine("\n Source :" + logException.Source);

            // Stack Trace of the error

            sb.AppendLine("\n Stack Trace :" + logException.StackTrace);

            // Method where the error occurred
            sb.AppendLine("\n TargetSite :" + logException.TargetSite);
            return sb.ToString();
        }
    }
}
