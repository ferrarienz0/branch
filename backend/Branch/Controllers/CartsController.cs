using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Branch.JWTProvider;
using Branch.Models;
using Branch.SearchAuxiliars;

namespace Branch.Controllers
{
    public class CartsController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("carts/user")]
        public IHttpActionResult UserCarts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var UserCarts = from Cart in SQLContext.Carts
                        where Cart.UserId == UserId
                        select Cart;

            var Response = new List<dynamic>();

            foreach(var Cart in UserCarts)
            {
                var ProductCarts = SQLContext.ProductCarts
                                                          .Where(x => x.CartId == Cart.Id)
                                                          .ToList();

                Response.Add(new { Cart, Products = ProductCarts });

            }

            return Ok(Response);
        }

        [HttpGet]
        [Route("carts/pro")]
        public IHttpActionResult ProCarts([FromUri] string AccessToken)
        {
            var ProId = TokenValidator.VerifyToken(AccessToken);

            var UserCarts = from Cart in SQLContext.Carts
                            where Cart.ProId == ProId
                            select new { Cart.Id };

            var Response = new List<dynamic>();

            foreach (var Cart in UserCarts)
            {
                var ProductCarts = from ProductCart in SQLContext.ProductCarts
                                   where Cart.Id == ProductCart.ProductId
                                   select new { ProductCart.Amount, ProductCart.Product };

                Response.Add(new { Cart, Products = ProductCarts });
            }

            return Ok(Response);
        }

        [HttpPost]
        [Route("cart/insert")]
        public IHttpActionResult AddProductToCart([FromUri] string AccessToken, [FromUri] int ProId, [FromBody] int ProductId, [FromBody] int Amount)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Cart = UserAuxiliar.StoreCart(UserId, ProId, SQLContext);

            if (Cart == default)
            {
                var NewCart = new Cart()
                {
                    ProId = ProId,
                    UserId = UserId, 
                };

                Cart = NewCart;
                SQLContext.Carts.Add(Cart);
            }

            SQLContext.SaveChanges();

            var ProductCart = new ProductCart()
            {
                ProductId = ProductId,
                CartId = Cart.Id,
                Amount = Amount
            };

            SQLContext.ProductCarts.Add(ProductCart);
            SQLContext.SaveChanges();

            return Ok(new { Cart, ProductCart });
        }

        [HttpPut]
        [Route("cart/update")]
        public IHttpActionResult ChangeProductAmountOnCart([FromUri] string AccessToken, [FromBody] int ProId, [FromBody] int ProductId, [FromBody] int NewAmount)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var StoreCart = UserAuxiliar.StoreCart(UserId, ProId, SQLContext);
            var ProductCart = SQLContext.ProductCarts.FirstOrDefault(x => x.CartId == StoreCart.Id && x.ProductId == ProductId);

            if(ProductCart == default)
            {
                return NotFound();
            }

            if(NewAmount == 0)
            {
                SQLContext.ProductCarts.Remove(ProductCart);

                var Message = "Product Removed";
                return Ok(new { Message });
            }
            else
            {
                ProductCart.Amount = NewAmount;
                SQLContext.Entry(ProductCart).State = EntityState.Modified;
            }

            SQLContext.SaveChanges();

            return Ok(new { StoreCart, ProductCart });
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