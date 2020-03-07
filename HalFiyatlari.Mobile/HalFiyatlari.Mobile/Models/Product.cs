using HalFiyatlari.Mobile.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HalFiyatlari.Mobile.Models
{
    public class Product : BaseModel
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
        public string MinPriceStr { get { return MinPrice + " " + Currency; } }
        public string MaxPriceStr { get { return MaxPrice + " " + Currency; } }

        public string MinPriceImage { get { return IsMinPriceRise ? "up.png" : "down.png"; } }
        public string MaxPriceImage { get { return IsMaxPriceRise ? "up.png" : "down.png"; } }
        #endregion

        public static List<Product> GetTodayPriceByCustomerId(int customerId)
        {
            try
            {
                using (var c = new HttpClient())
                {
                    var client = new System.Net.Http.HttpClient();
                    //var jsonRequest = new { email = "myemailid", password = "mypassword" };

                    //var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                    //HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                    var response = client.GetAsync(new Uri(GlobalConst.BaseUrl + "halfiyatlari/getproductbytodayprice?id="+customerId)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<List<Product>>(response.Content.ReadAsStringAsync().Result);
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
