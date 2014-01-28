
namespace ClientOnBoarding
{

    public static class DatabaseConnection
    {
        #region -- Variables --

        private static string _SQLConnectionString;
        private static string _SQLProvider;

        #endregion -- Variables --

        #region -- Constructor --

        static DatabaseConnection()
        {
            _SQLConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FlexisConnection"].ConnectionString;
            _SQLProvider = System.Configuration.ConfigurationManager.ConnectionStrings["FlexisConnection"].ProviderName;
        }

        #endregion

        #region -- Methods --

        public static string SQLConnectionString
        {
            get
            {
                return _SQLConnectionString;
            }
        }

        public static string SQLProvider
        {
            get
            {
                return _SQLProvider;
            }
        }

        #endregion
    }
}
