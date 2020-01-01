using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HalFiyatlari.Business
{
    public partial class DataAccessLayer
    {
        public DataAccessLayer() { }

        public bool InsertProduct(string pProductName, string pProductCategory, string pUnit, double pMinPrice, double pMaxPrice, string pCurrency, int pCustomerId)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "INSERT_PRODUCT_DATA", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pProductName, pProductCategory, pUnit, pMinPrice, pMaxPrice, pCurrency, pCustomerId });
        }

        public DataTable GetCustomer()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "GetList_Customer");
        }
        public DataTable GetProductByCustomerId(int pCustomerId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "GetList_ProductByCustomerId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pCustomerId });
        }
    }

    public sealed class MySQLParameterGeneratorEx
    {
        private const string ReturnValueParameterName = "RETURN_VALUE";

        public static MySqlParameter[] GenerateParam(ParameterInfo[] methodParameters, params object[] Values)
        {
            int length = methodParameters.Length;
            MySqlParameter[] sqlParams = new MySqlParameter[length];

            for (int i = 0; i < length; i++)
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@" + methodParameters[i].Name;
                parameter.Value = Values[i];
                sqlParams[i] = parameter;
            }

            return sqlParams;
        }
    }
}
