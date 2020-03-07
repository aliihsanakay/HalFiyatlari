using HalFiyatlari.Mobile.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HalFiyatlari.Mobile.Models
{
  public  class ProductPrice
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

        public string MinPriceStr { get { return MinPrice + " " + Currency; } }
        public string MaxPriceStr { get { return MaxPrice + " " + Currency; } }

        #endregion

        public static List<ProductPrice> GetProductPriceByProductId(int productId)
        {
            try
            {
                using (var c = new HttpClient())
                {
                    var client = new System.Net.Http.HttpClient();
                    //var jsonRequest = new { email = "myemailid", password = "mypassword" };

                    //var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                    //HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                    var response = client.GetAsync(new Uri(GlobalConst.BaseUrl + "halfiyatlari/getproductpricebyproductId?id=" + productId)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<List<ProductPrice>>(response.Content.ReadAsStringAsync().Result);
                        return result;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
