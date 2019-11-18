using Branch.Models;
using Branch.SearchAuxiliars;
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

        [HttpGet]
        [Route("pro/products")]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult ProductsByPro(int ProId)
        {
            var User = SQLContext.Users.Find(ProId);

            if(User == null || !User.IsPro)
            {
                return NotFound();
            }

            return Ok(UserSearchAuxiliar.Products(ProId, SQLContext));
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

            var User = SQLContext.Users.Find(Product.ProId);

            if(User == null || !User.IsPro)
            {
                return NotFound();
            }

            if(Product.Name.Contains(" "))
            {
                Product.Name = Product.Name.Replace(' ', '_');
            }

            SQLContext.Products.Add(Product);
            SQLContext.SaveChanges();

            return Ok(Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SQLContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
