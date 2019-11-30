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
    public class ProductInfo
    {
        public int ProId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

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

            var UserCartsSync = UserCarts.ToList();

            var Response = new List<dynamic>();

            foreach(var Cart in UserCartsSync)
            {
                var Products = SQLContext.ProductCarts
                                                      .Where(x => x.CartId == Cart.Id)
                                                      .ToList()
                                                      .Select(x => new { Product = FilterProduct(x.Product), x.Amount })
                                                      .ToList();
                                                      

                Response.Add(new { Cart, Products });
            }

            return Ok(Response);
        }

        [HttpGet]
        [Route("carts/pro")]
        public IHttpActionResult ProCarts([FromUri] string AccessToken)
        {
            var ProId = TokenValidator.VerifyToken(AccessToken);

            var _UserCarts = from Cart in SQLContext.Carts
                            where Cart.ProId == ProId
                            select new { Cart.Id };

            var UserCarts = _UserCarts.ToList();

            var Response = new List<dynamic>();

            foreach (var Cart in UserCarts)
            {
                var ProductCarts = from ProductCart in SQLContext.ProductCarts
                                   where Cart.Id == ProductCart.ProductId
                                   select new { ProductCart.Amount, Product = FilterProduct(ProductCart.Product) };

                Response.Add(new { Cart, Products = ProductCarts });
            }

            return Ok(Response);
        }

        [HttpPost]
        [Route("cart/insert")]
        public IHttpActionResult AddProductToCart([FromUri] string AccessToken, [FromBody] ProductInfo ProductInfo)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Cart = UserAuxiliar.StoreCart(UserId, ProductInfo.ProId, SQLContext);

            if (Cart == default)
            {
                var NewCart = new Cart()
                {
                    ProId = ProductInfo.ProId,
                    UserId = UserId, 
                };

                Cart = NewCart;
                SQLContext.Carts.Add(Cart);
            }

            SQLContext.SaveChanges();

            var ProductCart = new ProductCart()
            {
                ProductId = ProductInfo.ProductId,
                CartId = Cart.Id,
                Amount = ProductInfo.Amount
            };

            SQLContext.ProductCarts.Add(ProductCart);
            SQLContext.SaveChanges();

            var Product = SQLContext.Products.Find(ProductInfo.ProductId);

            return Ok(new { Cart, Product = FilterProduct(Product) });
        }

        [HttpPut]
        [Route("cart/update")]
        public IHttpActionResult ChangeProductAmountOnCart([FromUri] string AccessToken, [FromBody] ProductInfo UpdateInfo)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var StoreCart = UserAuxiliar.StoreCart(UserId, UpdateInfo.ProId, SQLContext);
            var ProductCart = SQLContext.ProductCarts.FirstOrDefault(x => x.CartId == StoreCart.Id && x.ProductId == UpdateInfo.ProductId);

            if(ProductCart == default)
            {
                return NotFound();
            }

            if(UpdateInfo.Amount == 0)
            {
                SQLContext.ProductCarts.Remove(ProductCart);

                var Message = "Product Removed";
                return Ok(new { Message });
            }
            else
            {
                ProductCart.Amount = UpdateInfo.Amount;
                SQLContext.Entry(ProductCart).State = EntityState.Modified;
            }

            SQLContext.SaveChanges();

            return Ok(new { StoreCart, Product = FilterProduct(ProductCart.Product) });
        }

        [HttpPut]
        [Route("cart/finish")]
        public IHttpActionResult FinishCart([FromUri] string AccessToken, [FromUri] int CartId)
        {
            _ = TokenValidator.VerifyToken(AccessToken);
            var Cart = SQLContext.Carts.Find(CartId);

            try
            {
                Cart.Finished = true;
                SQLContext.Entry(Cart).State = EntityState.Modified;
                SQLContext.SaveChanges();
            }
            catch
            {
                return Unauthorized();
            }
            
            if(!Cart.Finished)
            {
                return Unauthorized();
            }

            return Ok(Cart);
        }

        private dynamic FilterProducts(List<Product> Products)
        {
            return Products.Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.Stock,
                x.Price,
                x.MaxDiscount,
                x.CurrentDiscount,
                x.CreatedAt,
                x.UpdatedAt,
                x.Media,
            });
        }

        private dynamic FilterProduct(Product Product)
        {
            return new
            {
                Product.Id,
                Product.Name,
                Product.Description,
                Product.Stock,
                Product.Price,
                Product.MaxDiscount,
                Product.CurrentDiscount,
                Product.CreatedAt,
                Product.UpdatedAt,
                Product.Media,
            };
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