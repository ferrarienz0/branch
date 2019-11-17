using Branch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Branch.Controllers
{
    public class ProductController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("products")]
        public List<Product> GetProducts()
        {
            return SQLContext.Products.ToList();
        }

        [HttpGet]
        [Route("product")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult ProductById(int ProductId)
        {
            Product Product = SQLContext.Products.Find(ProductId);

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        [HttpPost]
        [Route("product/create")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult CreateProduct(Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserExists = SQLContext.Users.Find(Product.ProId) != null;

            if(!UserExists)
            {
                return NotFound();
            }

            if(Product.Name.Contains(" "))
            {
                Product.Name = Product.Name.Replace(' ', '_');
            }

            SQLContext.Products.Add(Product);
            SQLContext.SaveChanges();

            return Ok(User);
        }
    }
}
