using System.Data;
using System.Web.Http;
using HalFiyatlari.Business;
using System.Web.Http.Results;
using System.Collections.Generic;
using HalFiyatlari.Api.Models.Select;

namespace HalFiyatlari.Api.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/values
        public JsonResult<List<Customer>> GetCustomer()
        {
            List<Customer> list = new List<Customer>();
            DataTable dt = DataAccess.DAL.GetCustomer();

            foreach (DataRow row in dt.Rows)
            {
                Customer customer = new Customer();
                {
                    customer.Id = row.Field<int>("ID");
                    customer.Code = row.Field<string>("CODE");
                    customer.Name = row.Field<string>("NAME");
                }
                list.Add(customer);
            }

            return Json<List<Customer>>(list);
        }
    }
}