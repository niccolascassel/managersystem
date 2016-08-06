using Npgsql;
using System;
using System.Data;

namespace ManagerSystem.Database
{
    /// <summary>
    /// Implemements the access to Instramed database.
    /// </summary>
    public static class Access
    {
        #region Fields

        /// <summary>
        /// Holds the connection string of database.
        /// </summary>
        private static NpgsqlConnection pgsqlConnection =
                            new NpgsqlConnection(
                                String.Format(
                                    DbStrings.ConnectionString,
                                    Configurations.ServerAddress,
                                    Configurations.Port,
                                    Configurations.UserName,
                                    Configurations.Password,
                                    Configurations.DataBaseName));

        #endregion

        #region Methods

        /// <summary>
        /// Executes a command with non query.
        /// </summary>
        /// <param name="command">Command that will be executed.</param>
        public static int ExecuteNonQuery(string command)
        {
            try
            {
                pgsqlConnection.Open();

                NpgsqlCommand pgsqlcommand = new NpgsqlCommand(command, pgsqlConnection);
                return pgsqlcommand.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        /// <summary>
        /// Loads the entry of a table according with the command.
        /// </summary>
        /// <param name="command">Command that will be executed.</param>
        /// <returns>Returns the found entries.</returns>
        public static DataTable LoadData(string command)
        {
            try
            {
                pgsqlConnection.Open();

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command, pgsqlConnection);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (NpgsqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        /// <summary>
        /// Executes a count command.
        /// </summary>
        /// <param name="command">Command that will be executed.</param>
        /// <returns>Returns the value of lines selected.</returns>
        public static Int64 Count(string command)
        {
            try
            {
                pgsqlConnection.Open();

                NpgsqlCommand pgsqlcommand = new NpgsqlCommand(command, pgsqlConnection);
                return (Int64)pgsqlcommand.ExecuteScalar();
            }
            catch (NpgsqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        #endregion
    }
}
