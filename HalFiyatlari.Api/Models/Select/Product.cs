using HalFiyatlari.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HalFiyatlari.Api.Models.Select
{
    public class Product : DataAccess
    {

        #region Veriables
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double OldMinPrice { get; set; }
        public double OldMaxPrice { get; set; }
        public double MinRateOfChange { get; set; }
        public double MaxRateOfChange { get; set; }
        public bool IsMinPriceRise { get; set; }//düşük alış fiyatı yükseliştemi
        public bool IsMaxPriceRise { get; set; }//yüksek alış fiyatı yükseliştemi

        #endregion
        public static List<Product> GetProductByCustomerId(int pCustomerId)
        {
            List<Product> list = new List<Product>();
            DataTable dt = DAL.GetProductByCustomerId(pCustomerId);
            foreach (DataRow row in dt.Rows)
            {
                Product product = new Product();
                product.Id = row.Field<int>("ID");
                product.Name = row.Field<string>("NAME");
                product.Category = row.Field<string>("CATEGORY");
                product.Unit = row.Field<string>("UNIT");
                product.MinPrice = row.Field<double>("MINPRICE");
                product.MaxPrice = row.Field<double>("MAXPRICE");
                product.OldMinPrice = row.Field<double>("OLDDAYMINPRICE");
                product.OldMaxPrice = row.Field<double>("OLDDAYMAXPRICE");
                product.Currency = row.Field<string>("CURRENCY");
                product.CreateDate = row.Field<DateTime>("CREATEDATE");
                product.CalculateProductPriceChange();
                list.Add(product);
            }
            return list;
        }
        public static List<Product> GetProductByCustomerIdAndTodayPrice(int pCustomerId)
        {
            List<Product> list = new List<Product>();
            DataTable dt = DAL.GetProductByCustomerIdAndTodayPrice(pCustomerId);
            foreach (DataRow row in dt.Rows)
            {
                Product product = new Product();
                product.Id = row.Field<int>("ID");
                product.Name = row.Field<string>("NAME");
                product.Category = row.Field<string>("CATEGORY");
                product.Unit = row.Field<string>("UNIT");
                product.MinPrice = row.Field<double>("MINPRICE");
                product.MaxPrice = row.Field<double>("MAXPRICE");
                product.OldMinPrice = row.Field<double>("OLDDAYMINPRICE");
                product.OldMaxPrice = row.Field<double>("OLDDAYMAXPRICE");
                product.Currency = row.Field<string>("CURRENCY");
                product.CreateDate = row.Field<DateTime>("CREATEDATE");
                product.CalculateProductPriceChange();
                list.Add(product);
            }
            return list;
        }
   

        private void CalculateProductPriceChange()
        {
            IsMinPriceRise = MinPrice > OldMinPrice;//yükselişte;
            IsMaxPriceRise = MaxPrice > OldMaxPrice;//yükselişte;
            MinRateOfChange = OldMinPrice > 0 && MinPrice > 0 ? (MinPrice / OldMinPrice - 1) * 100 : 0;
            MaxRateOfChange = OldMaxPrice > 0 && MaxPrice > 0 ? (MaxPrice / OldMaxPrice - 1) * 100 : 0;

        }
    }


}