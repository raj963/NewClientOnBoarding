using System.Configuration;

namespace ClientOnBoarding
{
    static class AppSettings
    {
        #region -- Variables --

        private static string _hostName;
        private static string _smtpPort;
        private static string _smtpUserName;
        private static string _smtpPassword;
        private static string _fromMail;
        private static string _enableSsl;
        private static string _mailSentTo;

        private static int _pageSize;
        
        #endregion

        #region -- Constructor

        static AppSettings()
        {
            _hostName = ConfigurationManager.AppSettings["HostName"];
            _smtpPort = ConfigurationManager.AppSettings["SMTPport"];
            _smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
            _smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            _fromMail = ConfigurationManager.AppSettings["FromMail"];
            _enableSsl = ConfigurationManager.AppSettings["EnableSsl"];
            _mailSentTo = ConfigurationManager.AppSettings["MailSentTo"];

            _pageSize = Common.ConvertToInt(ConfigurationManager.AppSettings["PageSize"]);
        }

        #endregion

        #region -- Properties --

        public static string HostName
        {
            get
            {
                return _hostName;
            }
        }
        public static string SMTPport
        {
            get
            {
                return _smtpPort;
            }
        }
        public static string SMTPUserName
        {
            get
            {
                return _smtpUserName;
            }
        }
        public static string SMTPPassword
        {
            get
            {
                return _smtpPassword;
            }
        }
        public static string FromMail
        {
            get
            {
                return _fromMail;
            }
        }
        public static string EnableSsl
        {
            get
            {
                return _enableSsl;
            }
        }
        public static string MailSentTo
        {
            get
            {
                return _mailSentTo;
            }
        }
        public static int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        #endregion
    }
}
