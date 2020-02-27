using HalFiyatlari.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HalFiyatlari.Api.Models.Select
{
    public class ProductPrice:DataAccess
    {
        #region Veriables
    
        public string CustomerName { get; set; }   
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
        public static List<ProductPrice> GetProductPriceByProductId(int pProductId)
        {
            List<ProductPrice> list = new List<ProductPrice>();
            DataTable dt = DAL.GetProductPriceByProductId(pProductId);
            foreach (DataRow row in dt.Rows)
            {
                ProductPrice price = new ProductPrice();
               price.CustomerName = row.Field<string>("CUSTOMERNAME");
                price.MinPrice = row.Field<double>("MINPRICE");
                price.MaxPrice = row.Field<double>("MAXPRICE");
                price.OldMinPrice = row.Field<double>("OLDDAYMINPRICE");
                price.OldMaxPrice = row.Field<double>("OLDDAYMAXPRICE");
                price.Currency = row.Field<string>("CURRENCY");
                price.CreateDate = row.Field<DateTime>("CREATEDATE");
                price.CalculateProductPriceChange();
                list.Add(price);
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