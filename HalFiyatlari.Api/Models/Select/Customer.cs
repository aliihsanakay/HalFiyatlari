using HalFiyatlari.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HalFiyatlari.Api.Models.Select
{
    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }


        public static List<Customer> GetCustomers()
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
                    customer.Content = row.Field<string>("CONTENT");
                }
                list.Add(customer);
            }
            return list;
        }



    }
}