using System;
using System.Data;
using System.Text;

namespace ManagerSystem.Database.Tables
{
    /// <summary>
    /// Represents the Access Control table of 'ManagerSystem' database.
    /// <remarks>
    /// This table stores a list of users and password to access control.
    /// </remarks>
    /// </summary>
    public static class Colaborator
    {
        #region Constants

        /// <summary>
        /// Holds the name of database colaborators table.
        /// </summary>
        public const string TableName = "colaborators";

        /// <summary>
        /// Holds the name of primary key column.
        /// </summary>
        public const string PrimaryKeyColumnName = "pkCode";

        /// <summary>
        /// Holds the name of the name column.
        /// </summary>
        public const string NameColumnName = "name";

        /// <summary>
        /// Holds the name of the sex column.
        /// </summary>
        public const string SexColumnName = "sex";

        /// <summary>
        /// Holds the name of the birth date column.
        /// </summary>
        public const string BirthDateColumnName = "birthDate";

        /// <summary>
        /// Holds the name of the rg column.
        /// </summary>
        public const string RgColumnName = "rg";

        /// <summary>
        /// Holds the name of the cpf column.
        /// </summary>
        public const string CpfColumnName = "cpf";

        /// <summary>
        /// Holds the name of the cnh column.
        /// </summary>
        public const string CnhColumnName = "cnh";

        /// <summary>
        /// Holds the name of the cnh category column.
        /// </summary>
        public const string CnhCategoryColumnName = "cnhCategory";

        /// <summary>
        /// Holds the name of the address column.
        /// </summary>
        public const string AddressColumnName = "address";

        /// <summary>
        /// Holds the name of the particular phone column.
        /// </summary>
        public const string ParticularPhoneColumnName = "privatePhone";

        /// <summary>
        /// Holds the name of the residencial phone column.
        /// </summary>
        public const string ResidencialPhoneColumnName = "residencialPhone";

        /// <summary>
        /// Holds the name of the email column.
        /// </summary>
        public const string EmailColumnName = "email";

        /// <summary>
        /// Holds the name of the nationality column.
        /// </summary>
        public const string NationalityColumnName = "nationality";

        /// <summary>
        /// Holds the name of the ctpd number column.
        /// </summary>
        public const string CtpsColumnName = "ctps";

        /// <summary>
        /// Holds the name of the position column.
        /// </summary>
        public const string PositionColumnName = "position";

        /// <summary>
        /// Holds the name of the admission date column.
        /// </summary>
        public const string AdmissionDateColumnName = "admissionDate";

        /// <summary>
        /// Holds the name of the termination date column.
        /// </summary>
        public const string TerminationDateColumnName = "terminationDate";

        /// <summary>
        /// Holds the name of the validity date column.
        /// </summary>
        public const string ValidityDateColumnName = "validityDate";

        /// <summary>
        /// Holds the name of the formation column.
        /// </summary>
        public const string FormationColumnName = "formation";

        /// <summary>
        /// Holds the name of the experience column.
        /// </summary>
        public const string ExperienceColumnName = "experience";

        /// <summary>
        /// Holds the name of the attachments column.
        /// </summary>
        public const string AttachmentsColumnName = "attachments";

        /// <summary>
        /// Holds the name of the photo column.
        /// </summary>
        public const string PhotoColumnName = "photo";

        /// <summary>
        /// Holds the name of the access controle first key column.
        /// </summary>
        public const string AccessControlFkColumnName = "fkAccessControl";

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new colaborator registry.
        /// </summary>
        /// <param name="name">Colaborator's name. Is not allowed NULL or Empty.</param>
        /// <param name="sex">Colaborator's sex. Male as default.</param>
        /// <param name="birthDate">Colaborator's birth date. Is not allowed NULL or Empty.</param>
        /// <param name="rg">Colaborator's identity number. Is not allowed NULL or Empty.</param>
        /// <param name="cpf">Colaborator's CPF number. Is not allowed NULL or Empty.</param>
        /// <param name="cnh">Colaborator's CNH number. It is optional.</param>
        /// <param name="cnhCategory">Colaborator's CNH category. Is just necessary whether the CNH number is not Empty.</param>
        /// <param name="address">Colaborator's address street. It is optional.</param>
        /// <param name="privatePhone">Colaborator's particular phone. It is optional.</param>
        /// <param name="residencialPhone">Colaborator's residencial phone. It is optional.</param>
        /// <param name="email">Colaborator's particular e-mail. It is optional.</param>
        /// <param name="nationality">Colabotaor's nationality. It is optional.</param>
        /// <param name="ctps">Colaborator's CTPS number. Is not allowed NULL or Emprty.</param>
        /// <param name="position">Colaborator's position. Is not allowed NULL or Empty.</param>
        /// <param name="admissionDate">Colaborator's addmission date. Is not allowed NULL or Empty.</param>        
        /// <param name="validityDate">Colaborator's contract validity date. If is an effective contract, it must be Empty.</param>
        /// <param name="formation">Colaborator's formation decription. It is optional.</param>
        /// <param name="experience">Colaborator's experience description. It is optional.</param>
        /// <param name="fkAccessControl">Code identification of colaborator's access control.</param>
        /// <returns>Returns whether the registry was correctly added.</returns>
        public static long Insert(string name, string sex, string birthDate, string rg, string cpf, string cnh, string cnhCategory,
                                  string address, string privatePhone, string residencialPhone, string email, string nationality,
                                  string ctps, string position, string admissionDate, string validityDate, string formation,
                                  string experience, string fkAccessControl)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            columns.Append(string.Format(DbStrings.InsertColumn, NameColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, name) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, SexColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, sex) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, BirthDateColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, birthDate) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, RgColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, rg) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, CpfColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, cpf) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, CtpsColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, ctps) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, PositionColumnName) + ", ");
            values.Append(string.Format(DbStrings.InsertValue, position) + ", ");

            columns.Append(string.Format(DbStrings.InsertColumn, AdmissionDateColumnName));
            values.Append(string.Format(DbStrings.InsertValue, admissionDate));

            if (!string.IsNullOrWhiteSpace(cnh))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, CnhColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, cnh));

                columns.Append(", " + string.Format(DbStrings.InsertColumn, CnhCategoryColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, cnhCategory));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, AddressColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, address));
            }

            if (!string.IsNullOrWhiteSpace(privatePhone))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, ParticularPhoneColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, privatePhone));
            }

            if (!string.IsNullOrWhiteSpace(residencialPhone))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, ResidencialPhoneColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, residencialPhone));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, EmailColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, email));
            }

            if (!string.IsNullOrWhiteSpace(nationality))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, NationalityColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, nationality));
            }

            if (!string.IsNullOrWhiteSpace(validityDate))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, ValidityDateColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, validityDate));
            }

            if (!string.IsNullOrWhiteSpace(formation))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, FormationColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, formation));
            }

            if (!string.IsNullOrWhiteSpace(experience))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, ExperienceColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, experience));
            }

            if (!string.IsNullOrWhiteSpace(fkAccessControl))
            {
                columns.Append(", " + string.Format(DbStrings.InsertColumn, AccessControlFkColumnName));
                values.Append(", " + string.Format(DbStrings.InsertValue, fkAccessControl));
            }

            string command = string.Format(
                    DbStrings.InsertFormat,
                    TableName,
                    columns.ToString(),
                    values.ToString());

            Access.ExecuteNonQuery(
                string.Format(
                    DbStrings.InsertFormat,
                    TableName,
                    columns.ToString(),
                    values.ToString())
                );

            DataRow dt = SelectColaborator(Colaborator.RgColumnName, Convert.ToInt64(rg));

            if (dt != null)
                return Convert.ToInt64(dt[PrimaryKeyColumnName]);

            return 0;
        }

        /// <summary>
        /// Selects the data of a colaborator.
        /// </summary>
        /// <param name="name">Colaborator's name. Is not allowed NULL or Empty.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataTable SelectColaborator(string name)
        {
            return Access.LoadData(
                            string.Format(
                                DbStrings.SelectColaborator,
                                    string.Format(
                                        DbStrings.WhereWithOneVariable,
                                        NameColumnName,
                                        name)
                                    )
                                );
        }

        /// <summary>
        /// Selects the data of a colaborator.
        /// </summary>
        /// <param name="registryType">Type of registry that will be used to find the colaborator, indentified by column name.</param>
        /// <param name="registryNumber">Number of registry to compare.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataRow SelectColaborator(string registryType, long registryNumber)
        {
            DataTable dt = Access.LoadData(
                                string.Format(
                                    DbStrings.SelectColaborator,
                                        string.Format(
                                            DbStrings.WhereWithOneVariable,
                                            registryType,
                                            registryNumber)
                                        )
                                    );

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Selects the data of a colaborator.
        /// </summary>
        /// <param name="registryType1">Type of registry that will be used to find the colaborator, indentified by column name.</param>
        /// <param name="registryNumber1">Number of registry to compare.</param>
        /// /// <param name="registryType2">Type of registry that will be used to find the colaborator, indentified by column name.</param>
        /// <param name="registryNumber2">Number of registry to compare.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataRow SelectColaborator(string registryType1, long registryNumber1, string registryType2, long registryNumber2)
        {
            DataTable dt = Access.LoadData(
                                string.Format(
                                    DbStrings.SelectColaborator,
                                        string.Format(
                                            DbStrings.WhereWithTwoVariables,
                                            registryType1,
                                            registryNumber1,
                                            registryType2,
                                            registryNumber2)
                                        )
                                    );

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Selects the data of a colaborator.
        /// </summary>
        /// <param name="rg">Colaborator's identity number. Is not allowed NULL or Empty.</param>
        /// <param name="cpf">Colaborator's CPF number. Is not allowed NULL or Empty.</param>
        /// <param name="ctps">Colaborator's CTPS number. Is not allowed NULL or Emprty.</param>
        /// <returns>Returns the found records. Null if no records found.</returns>
        public static DataRow SelectColaborator(long rg, long cpf, long ctps)
        {
            DataTable dt = Access.LoadData(
                                string.Format(
                                    DbStrings.SelectColaborator,
                                        string.Format(
                                            DbStrings.WhereWithThreeVariables,
                                            RgColumnName,
                                            rg,
                                            CpfColumnName,
                                            cpf,
                                            CtpsColumnName,
                                            ctps)
                                        )
                                    );

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Select all colaborators registries.
        /// </summary>
        /// <returns>Returns the found access controls. Null if no records found.</returns>
        public static DataTable SelecAll()
        {
            return Access.LoadData(DbStrings.SelectAllColaborators);
        }

        /// <summary>
        /// Updates a colaborator registry.
        /// </summary>
        /// <param name="name">Colaborator's name. Is not allowed NULL or Empty.</param>
        /// <param name="sex">Colaborator's sex. Male as default.</param>
        /// <param name="birthDate">Colaborator's birth date. Is not allowed NULL or Empty.</param>
        /// <param name="cnh">Colaborator's CNH number. It is optional.</param>
        /// <param name="cnhCategory">Colaborator's CNH category. Is just necessary whether the CNH number is not Empty.</param>
        /// <param name="address">Colaborator's address street. It is optional.</param>
        /// <param name="privatePhone">Colaborator's private phone. It is optional.</param>
        /// <param name="residencialPhone">Colaborator's residencial phone. It is optional.</param>
        /// <param name="email">Colaborator's particular e-mail. It is optional.</param>
        /// <param name="nationality">Colabotaor's nationality. It is optional.</param>
        /// <param name="position">Colaborator's position. Is not allowed NULL or Empty.</param>
        /// <param name="admissionDate">Colaborator's addmission date. Is not allowed NULL or Empty.</param>        
        /// <param name="terminationDate">Colaborator's termination date. If the contract isn't finished, it must be Empty.</param>
        /// <param name="validityDate">Colaborator's contract validity date. If is an effective contract, it must be Empty.</param>
        /// <param name="formation">Colaborator's formation decription. It is optional.</param>
        /// <param name="experience">Colaborator's experience description. It is optional.</param>
        /// <param name="fkAccessControl">Code identification of colaborator's access control.</param>
        /// <returns>Returns whether the registry was correctly added.</returns>
        public static bool UpdateColaborator(string name, string sex, string birthDate, string cnh, string cnhCategory, string address,
                                             string privatePhone, string residencialPhone, string email, string nationality,
                                             string position, string admissionDate, string terminationDate, string validityDate,
                                             string formation, string experience, string fkAccessControl, string rg, string cpf,
                                             string ctps)
        {
            StringBuilder updateCommand = new StringBuilder();

            return Access.ExecuteNonQuery(
                string.Format(
                    DbStrings.UpdateColaborator,
                    updateCommand,
                    rg,
                    cpf,
                    ctps)) > 0;
        }

        /// <summary>
        /// Deletes a colaborator registry.
        /// </summary>
        /// <param name="command">Command containing the columns which will be used to find the colaborator registry.</param>
        /// <returns>Returns whether the colaborator registry was corectly deleted.</returns>
        public static bool Delete(string command)
        {
            long count = Access.Count(DbStrings.CountColaborators);

            Access.ExecuteNonQuery(command);

            return count > Access.Count(DbStrings.CountColaborators);
        }

        /// <summary>
        /// Deletes all colaborators registries.
        /// </summary>
        /// <returns>Returns whether the colaborators registries were correctly deleted.</returns>
        public static bool DeleteAll()
        {
            Access.ExecuteNonQuery(DbStrings.DeleteAllColaborators);

            return Access.Count(DbStrings.CountColaborators) == 0;
        }

        #endregion
    }
}
