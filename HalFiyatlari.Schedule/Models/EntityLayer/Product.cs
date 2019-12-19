using System.Data;
using System.Reflection;

namespace HalFiyatlari.Schedule.Models
{
    public class Product : DataAccess
    {
        public static bool ChangeStartScreen(string startScreen)
        {
            return DAL.ChangeStartScreen(startScreen);
        }
    }
    public partial class DataAccessLayer
    {
        public bool ChangeStartScreen(string pStartScreen)
        {
            return DatabaseContext.ExecuteNonQuery(CommandType.StoredProcedure, "_Admin_Update_B2BRuleStartScreen", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pStartScreen });
        }
    }
}
