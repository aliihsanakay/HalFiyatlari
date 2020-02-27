using HalFiyatlari.Mobile.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HalFiyatlari.Mobile.Models
{
    public  class Customer : BaseModel
    {
       
        public string Code { get; set; }
        public string Name { get; set; }

        public static  List<Customer> GetCustomer()
        {
            try
            {
                using (var c = new HttpClient())
                {
                    var client = new System.Net.Http.HttpClient();
                    //var jsonRequest = new { email = "myemailid", password = "mypassword" };

                    //var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                    //HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                    var response =  client.GetAsync(new Uri(GlobalConst.BaseUrl + "Customer")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);
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
