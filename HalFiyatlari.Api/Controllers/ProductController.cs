using HalFiyatlari.Api.Models.Select;
using HalFiyatlari.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Results;

namespace HalFiyatlari.Api.Controllers
{

    public class ProductController : ApiController
    {
        // GET api/values



        [HttpGet, Route("api/halfiyatlari/getproductbycustomerid")]
        public JsonResult<List<Product>> GetProductByCustomerId(int id)
        {
            var list = Product.GetProductByCustomerId(id);
            return Json<List<Product>>(list);
        }



    }
}
