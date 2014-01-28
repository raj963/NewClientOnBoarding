using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ClientOnBoarding
{
    public static class DataAccess
    {
        private const string ERROR_NUM = "@errNum";
        private const string ERROR_DESC = "@errDesc";
        private const string IDENTITY = "@Identity";        
        public static string connectionString;
        public static string provider;

        private static MySqlCommand command;

        private static List<MySqlParameter> sqlParms = new List<MySqlParameter>();

        static DataAccess()
        {
            connectionString = DatabaseConnection.SQLConnectionString;
            provider = DatabaseConnection.SQLProvider;
        }

        public static void resetParams()
        {
            sqlParms = new List<MySqlParameter>();
        }

        public static void ExecuteQuery(string strQuery)
        {            
            MySqlConnection con = new MySqlConnection();;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                con.ConnectionString = connectionString;

                cmd.Connection = con;
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    ((IDisposable)cmd).Dispose();

                if (con != null)
                    ((IDisposable)con).Dispose();
            }
        }

        public static DataTable ExecuteAdapter(string strQuery)
        {            
            MySqlConnection con = new MySqlConnection();;
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable();

            try
            {
                con.ConnectionString = connectionString;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = strQuery;

                if (con.State == ConnectionState.Closed)
                    con.Open();

                MySqlDataAdapter Adp = new MySqlDataAdapter();
                Adp.SelectCommand = cmd;
                Adp.Fill(_dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    ((IDisposable)cmd).Dispose();

                if (con != null)
                    ((IDisposable)con).Dispose();
            }

            return _dt;
        }

        public static DataSet ExecuteDataSet(string storedProcName, ref int errorNum, ref string errorDesc)
        {
            DataSet ds = new DataSet();
            try
            {
                addSqlParam(ERROR_NUM, ParameterDirection.Output, 16, MySqlDbType.Int32);
                addSqlParam(ERROR_DESC, ParameterDirection.Output, 225, MySqlDbType.VarChar);
                command = PrepareSPCommand(storedProcName, sqlParms.ToArray());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(ds);

                errorNum = Convert.ToInt32(command.Parameters[ERROR_NUM].Value.ToString());
                errorDesc = command.Parameters[ERROR_DESC].Value.ToString();
            }
            catch (Exception ex)
            {
                errorNum = -999;
                errorDesc = ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
            }

            return ds;
        }

        public static void ExecuteNonQuery(string storedProcName, ref int errorNum, ref string errorDesc)
        {
            addSqlParam(ERROR_NUM, ParameterDirection.Output, 16, MySqlDbType.Int32);
            addSqlParam(ERROR_DESC, ParameterDirection.Output, 225, MySqlDbType.VarChar);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                command = PrepareSPCommand(storedProcName, sqlParms.ToArray(), conn);

                conn.Open();

                Int32 rowsAffected = command.ExecuteNonQuery();

                errorNum = Convert.ToInt32(command.Parameters[ERROR_NUM].Value.ToString());
                errorDesc = command.Parameters[ERROR_DESC].Value.ToString();

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public static void ExecuteNonQuery(string storedProcName, ref int errorNum, ref string errorDesc, ref int identity)
        {
            addSqlParam(ERROR_NUM, ParameterDirection.Output, 16, MySqlDbType.Int32);
            addSqlParam(ERROR_DESC, ParameterDirection.Output, 225, MySqlDbType.VarChar);
            addSqlParam(IDENTITY, ParameterDirection.Output, 16, MySqlDbType.Int32);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                command = PrepareSPCommand(storedProcName, sqlParms.ToArray(), conn);
                conn.Open();
                command.ExecuteNonQuery();

                errorNum = Convert.ToInt32(command.Parameters[ERROR_NUM].Value.ToString());
                errorDesc = command.Parameters[ERROR_DESC].Value.ToString();
                identity = Convert.ToInt32(command.Parameters[IDENTITY].Value.ToString());
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private static MySqlCommand PrepareSPCommand(string storedProcName, MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcName, new MySqlConnection(connectionString));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            return command;
        }

        private static MySqlCommand PrepareSPCommand(string storedProcName, MySqlParameter[] parameters, MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand(storedProcName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);

            return command;
        }

        public static void addSqlParam(string name, ParameterDirection direction, int size, MySqlDbType type, Object value)
        {
            MySqlParameter sqlParam = new MySqlParameter();
            sqlParam.ParameterName = name;
            sqlParam.Direction = direction;
            sqlParam.Size = size;
            sqlParam.MySqlDbType = type;
            sqlParam.Value = value;
            sqlParms.Add(sqlParam);
        }

        public static void addSqlParam(string name, ParameterDirection direction, int size, MySqlDbType type)
        {
            MySqlParameter sqlParam = new MySqlParameter();
            sqlParam.ParameterName = name;
            sqlParam.Direction = direction;
            sqlParam.Size = size;
            sqlParam.MySqlDbType = type;
            sqlParms.Add(sqlParam);
        }

        public static object getSQLParam(string paramName)
        {
            return command.Parameters[paramName].Value;
        }
    }
}
