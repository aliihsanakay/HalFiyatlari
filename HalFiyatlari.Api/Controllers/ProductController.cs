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
        [HttpGet, Route("api/halfiyatlari/getproductpricebyproductId")]
        public JsonResult<List<ProductPrice>> GetProductPriceByProductId(int id)
        {
            var list = ProductPrice.GetProductPriceByProductId(id);
            return Json<List<ProductPrice>>(list);
        }

        [HttpGet, Route("api/halfiyatlari/getproductbytodayprice")]
        public JsonResult<List<Product>> GetProductByCustomerIdTodayPrice(int id)
        {
            var list = Product.GetProductByCustomerIdAndTodayPrice(id);
            return Json<List<Product>>(list);
        }


    }
}
