using Branch.Auxiliars;
using Branch.JWTProvider;
using Branch.Models;
using Branch.SearchAuxiliars;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            return Ok(UserAuxiliar.Products(ProId, SQLContext));
        }

        [HttpPost]
        [Route("product/create")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult CreateProduct([FromUri] string AccessToken, [FromBody] Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);
            var User = SQLContext.Users.Find(UserId);

            if(User == null || !User.IsPro)
            {
                return NotFound();
            }

            if(Product.Name.Contains(" "))
            {
                Product.Name = Product.Name.Replace(' ', '_');
            }

            Product.ProId = UserId;

            SQLContext.Products.Add(Product);
            SQLContext.SaveChanges();

            return Ok(Product);
        }

        [HttpGet]
        [Route("products/recommended")]
        public IHttpActionResult RecommendedProducs([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Recommendation = GraphAuxiliar.OrderProductsByAffinity(UserId, SQLContext.Products.ToList(), SQLContext);

            return Ok(Recommendation);
        }

        [HttpPut]
        [Route("product/discount")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult ApplyDiscountToProduct([FromUri] string AccessToken, [FromUri] int ProductId, [FromUri] float Discount)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            var User = SQLContext.Users.Find(UserId);

            var Product = SQLContext.Products.Find(ProductId);

            if(!User.IsPro || Product.ProId != UserId)
            {
                return Content(HttpStatusCode.Forbidden, new {Message = "Usuário não é Pro"});
            }

            if(Discount > Product.MaxDiscount)
            {
                return Content(HttpStatusCode.Forbidden, new { Message = "Desconto maior que o máximo." });
            }

            Product.CurrentDiscount = Discount;

            SQLContext.Entry(Product).State = EntityState.Modified;
            SQLContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("product/discount")]
        [ResponseType(typeof(List<dynamic>))]
        public IHttpActionResult RecommendedDiscounts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            var User = SQLContext.Users.Find(UserId);

            if(!User.IsPro)
            {
                return NotFound();
            }

            var RecommendedDiscounts = DBAuxiliar.RecommendedProDiscounts(UserId);

            return Ok(RecommendedDiscounts);
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
