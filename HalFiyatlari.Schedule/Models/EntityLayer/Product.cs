using HalFiyatlari.Business;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace HalFiyatlari.Schedule.Models
{
    public class Product : DataAccess
    {
        public static bool InsertProduct(List<WebSiteHalData> list)
        {
            foreach (var item in list)
            {
                DAL.InsertProduct(item.ProductName.ToUpper(), item.ProductCategory, item.Unit, item.MinPrice, item.MaxPrice, item.Currency, item.CustomerId);
            }

            return true;
        }
    }
   
}
