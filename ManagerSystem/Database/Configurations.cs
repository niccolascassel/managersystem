using System.Net;

namespace ManagerSystem.Database
{
    /// <summary>
    /// Implements the access to the Instramed database configurations access.
    /// </summary>
    public static class Configurations
    {
        #region Fields

        /// <summary>
        /// Holds the IP address of database server.
        /// </summary>
        private static IPAddress serverAddress = IPAddress.Parse("127.0.0.1");             // localhost

        /// <summary>
        /// Holds the access port of database server.
        /// </summary>
        private static string port = "5432";

        /// <summary>
        /// Holds the user name to access the database.
        /// </summary>
        private static string userName = "postgres";

        /// <summary>
        /// Holds the password to access the database.
        /// </summary>
        private static string password = "P@triots2014";

        /// <summary>
        /// Holds the database name.
        /// </summary>
        private static string databaseName = "managersystem";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the database server IP address.
        /// </summary>
        public static IPAddress ServerAddress
        {
            get { return serverAddress; }
            set { serverAddress = value; }
        }

        /// <summary>
        /// Gets or sets the port to access the database server.
        /// </summary>
        public static string Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Gets or sets the user name to access the database server.
        /// </summary>
        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Gets or sets the password to access the database server.
        /// </summary>
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public static string DataBaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        #endregion
    }
}
