using HalFiyatlari.Business.Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HalFiyatlari.Business
{
    public class DatabaseContext
    {
        public static DataTable ExecuteReader(CommandType commandType, string commandText)
        {
            try
            {
                return Eryaz.Utility.MySqlHelper.ExecuteDataTable(GlobalSettings.ConnectionString, commandType, commandText);
            }
            catch (Exception ex)
            {
               // Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return new DataTable();
            }
        }

        public static DataTable ExecuteReader(CommandType commandType, string commandText, ParameterInfo[] parameterNames, object[] parameterValues)
        {
            try
            {
                return Eryaz.Utility.MySqlHelper.ExecuteDataTable(GlobalSettings.ConnectionString, commandType, commandText, GenerateParam(parameterNames, parameterValues));


            }
            catch (Exception ex)
            {
              //  Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return new DataTable();
            }
        }
        public static DataTable ExecuteReader(CommandType commandType, string commandText, object[] parameterNames, object[] parameterValues)
        {
            try
            {
                return Eryaz.Utility.MySqlHelper.ExecuteDataTable(GlobalSettings.ConnectionString, commandType, commandText, GenerateParamMysql(parameterNames, parameterValues));
            }
            catch (Exception ex)
            {

               // Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return new DataTable();
            }
        }



        public static DataSet ExecuteReaderDs(CommandType commandType, string commandText)
        {
            try
            {
                return Eryaz.Utility.MySqlHelper.ExecuteDataset(GlobalSettings.ConnectionString, CommandType.StoredProcedure, commandText);
            }
            catch (Exception ex)
            {
             //   Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return new DataSet();
            }
        }

        public static DataSet ExecuteReaderDs(CommandType commandType, string commandText, ParameterInfo[] parameterNames, object[] parameterValues)
        {
            try
            {
                return Eryaz.Utility.MySqlHelper.ExecuteDataset(GlobalSettings.ConnectionString, CommandType.StoredProcedure, commandText, GenerateParam(parameterNames, parameterValues));
            }
            catch (Exception ex)
            {
              //  Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return new DataSet();
            }
        }

        public static bool ExecuteNonQuery(CommandType commandType, string commandText, ParameterInfo[] parameterNames, object[] parameterValues)
        {
            try
            {
                Eryaz.Utility.MySqlHelper.ExecuteNonQuery(GlobalSettings.ConnectionString, CommandType.StoredProcedure, commandText, GenerateParam(parameterNames, parameterValues));
                return true;
            }
            catch (Exception ex)
            {
               // Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return false;
            }
        }
        public static bool ExecuteNonQuery(CommandType commandType, string commandText, object[] parameterNames, object[] parameterValues)
        {
            try
            {
                Eryaz.Utility.MySqlHelper.ExecuteNonQuery(GlobalSettings.ConnectionString, commandType, commandText, GenerateParamMysql(parameterNames, parameterValues));
                return true;

            }
            catch (Exception ex)
            {
              //  Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return false;
            }
        }
        public static int ExecuteScalar(CommandType commandType, string commandText, ParameterInfo[] parameterNames, object[] parameterValues)
        {
            try
            {
                return Convert.ToInt16(Eryaz.Utility.MySqlHelper.ExecuteScalar(GlobalSettings.ConnectionString, CommandType.StoredProcedure, commandText, GenerateParam(parameterNames, parameterValues)));
            }
            catch (Exception ex)
            {
             //   Logger.LogGeneral(LogGeneralErrorType.Error, ClientType.None, commandText.Length > 50 ? commandText.Substring(0, 49) : commandText, ex, string.Empty);
                return -2;
            }
        }


   

        //public static SqlParameter[] GenerateParam(ParameterInfo[] parameterNames, params object[] parameterValues)
        //{
        //    int length = parameterNames.Length;
        //    SqlParameter[] sqlParams = new SqlParameter[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        SqlParameter parameter = new SqlParameter();
        //        parameter.ParameterName = "@" + parameterNames[i].Name;
        //        parameter.Value = parameterValues[i] == null ? "" : parameterValues[i];
        //        sqlParams[i] = parameter;
        //    }

        //    return sqlParams;
        //}

        //public static SqlParameter[] GenerateParam(object[] parameterNames, params object[] parameterValues)
        //{
        //    int length = parameterNames.Length;
        //    SqlParameter[] sqlParams = new SqlParameter[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        SqlParameter parameter = new SqlParameter();
        //        parameter.ParameterName = "@" + parameterNames[i];
        //        parameter.Value = parameterValues[i] == null ? "" : parameterValues[i];
        //        sqlParams[i] = parameter;
        //    }

        //    return sqlParams;
        //}

        public static string GenerateParamStr(ParameterInfo[] parameterNames, params object[] parameterValues)
        {
            int length = parameterNames.Length;
            string sqlParams = string.Empty;

            for (int i = 0; i < length; i++)
            {
                sqlParams += string.Format("\r\n{0}: {1}", parameterNames[i].Name, parameterValues[i] == null ? "" : parameterValues[i]);
            }

            return sqlParams;
        }



        public static MySqlParameter[] GenerateParam(ParameterInfo[] parameterNames, params object[] parameterValues)
        {
            int length = parameterNames.Length;
            MySqlParameter[] sqlParams = new MySqlParameter[length];

            for (int i = 0; i < length; i++)
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@" + parameterNames[i].Name;
                parameter.Value = parameterValues[i];
                sqlParams[i] = parameter;
            }

            return sqlParams;
        }

        public static MySqlParameter[] GenerateParamMysql(object[] parameterNames, params object[] parameterValues)
        {
            int length = parameterNames.Length;
            MySqlParameter[] sqlParams = new MySqlParameter[length];

            for (int i = 0; i < length; i++)
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@" + parameterNames[i];
                parameter.Value = parameterValues[i];
                sqlParams[i] = parameter;
            }

            return sqlParams;
        }

        public static MySqlParameter[] GenerateMysqlParamByArray(string[] parameterNames, string[] types, object[] parameterValues)
        {
            if (parameterNames == null) return null;

            int length = parameterNames.Length;
            MySqlParameter[] sqlParams = new MySqlParameter[length];

            object val = new object();

            for (int i = 0; i < length; i++)
            {
                var dType = DbType.String;
                if (types[i].Contains("checkbox"))
                {
                    dType = DbType.Int16;
                    val = Convert.ToInt16(Convert.ToBoolean(parameterValues[i]));
                }
                else if (types[i].Contains("datetime"))
                {
                    dType = DbType.DateTime;
                    val = Convert.ToDateTime(parameterValues[i]);
                }
                else
                {
                    dType = DbType.String;
                    val = parameterValues[i];
                }

                MySqlParameter parameter = new MySqlParameter
                {
                    ParameterName = "@" + parameterNames[i],
                    DbType = dType,
                    Value = val
                };
                sqlParams[i] = parameter;
            }

            return sqlParams;
        }


       
    }
}
