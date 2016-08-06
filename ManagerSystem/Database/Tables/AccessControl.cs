using System.Data;

namespace ManagerSystem.Database.Tables
{
    /// <summary>
    /// Represents the Access Control table of 'ManagerSystem' database.
    /// <remarks>
    /// This table stores a list of users and password to access control.
    /// </remarks>
    /// </summary>
    public static class AccessControl
    {
        #region Constants

        /// <summary>
        /// Holds the name of primary key column.
        /// </summary>
        public const string PrimaryKeyColumnName = "pkCode";

        /// <summary>
        /// Holds the name of the username column.
        /// </summary>
        public const string UserNameColumnName = "userName";

        /// <summary>
        /// Holds the name of the password column.
        /// </summary>
        public const string PasswordColumnName = "password";

        /// <summary>
        /// Holds the name of the modules column.
        /// </summary>
        public const string ModulesColumnName = "modules";

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new user on AccessControl table.
        /// </summary>
        /// <param name="userName">User's identification.</param>
        /// <param name="password">User's password.</param>
        /// <param name="allowedModules">User's allowed modules.</param>
        /// <returns>Returns whether the registry was correctly added.</returns>
        public static bool Insert(string userName, string password, ushort allowedModules)
        {
            long count = Access.Count(DbStrings.CountAccessControl);

            Access.ExecuteNonQuery(
                string.Format(
                    DbStrings.InsertAccessControl,
                    userName,
                    password,
                    allowedModules)
                );
            
            return Access.Count(DbStrings.CountColaborators) > count;
        }

        /// <summary>
        /// Selects a access control by user's identification.
        /// </summary>
        /// <param name="userName">User's identification.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataRow SelectByUserName(string userName)
        {
            DataTable dt = Access.LoadData(
                                string.Format(
                                    DbStrings.SelectAccessCodeByUserName,
                                    userName)
                              );

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Selects a access control by user's code.
        /// </summary>
        /// <param name="pkCode">User's code.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataRow SelectByCode(int pkCode)
        {
            DataTable dt = Access.LoadData(
                                string.Format(
                                    DbStrings.SelectAccessControlByCode,
                                    pkCode)
                              );

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Select all access controls.
        /// </summary>
        /// <returns>Returns the found access controls. Null if no records found.</returns>
        public static DataTable SelectAll()
        {
            DataTable dt = Access.LoadData(DbStrings.SelectAllAccessControl);

            if (dt.Rows.Count > 0)
                return dt;

            return null;
        }

        /// <summary>
        /// Update access control searching by reference code.
        /// </summary>
        /// <param name="userName">User's identification.</param>        
        /// <param name="password">User's password.</param>
        /// <param name="allowedModules">User's allowed modules.</param>
        /// <param name="pkCode">Access control code.</param>
        /// <returns>Return whether the access control was updated.</returns>
        public static bool Update(string userName, string password, ushort allowedModules, int pkCode)
        {
            return Access.ExecuteNonQuery(
                                string.Format(
                                    DbStrings.UpdateAccessControl,
                                    userName,
                                    password,
                                    allowedModules,
                                    pkCode)
                                ) > 0;
        }

        /// <summary>
        /// Delete an access control by user identification.
        /// </summary>
        /// <param name="userName">User identification.</param>
        /// <returns>Returns whether the registry was correctly delelted.</returns>
        public static bool DeleteByUserName(string userName)
        {
            long count = Access.Count(DbStrings.CountAccessControl);

            Access.ExecuteNonQuery(string.Format(DbStrings.DeleteAccessControlByUserName, userName));

            return count > Access.Count(DbStrings.CountAccessControl);
        }

        /// <summary>
        /// Delete an access control by primary key code.
        /// </summary>
        /// <param name="code">Primary key code.</param>
        /// <returns>Returns whether the registry was correctly delelted.</returns>
        public static bool DeleteByCode(long code)
        {
            long count = Access.Count(DbStrings.CountAccessControl);

            Access.ExecuteNonQuery(string.Format(DbStrings.DeleteAccessControlByCode, code));

            return count > Access.Count(DbStrings.CountAccessControl);
        }

        /// <summary>
        /// Delete all access control registries.
        /// </summary>
        /// <returns>Returns whether all access controls were deleted.</returns>
        public static bool DeleteAll()
        {
            Access.ExecuteNonQuery(DbStrings.DeleteAllAccessControl);

            return Access.Count(DbStrings.CountAccessControl) == 0;
        }

        #endregion
    }
}
