using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalFiyatlari.Schedule.Models
{
   public class WebSiteHalData
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string Unit { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public DateTime ReadDate { get; set; }
        public string Currency { get; set; }
        public int CustomerId { get; set; }

        public WebSiteHalData()
        {
            ReadDate = DateTime.Now;
        }

    }

}
