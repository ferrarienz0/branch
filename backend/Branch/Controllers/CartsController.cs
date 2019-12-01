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
                            where Cart.UserId == UserId && !Cart.Finished
                            select Cart;

            var UserCartsSync = UserCarts.ToList();

            var Response = new List<dynamic>();

            foreach (var Cart in UserCartsSync)
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
                             select new { Cart.Id, Cart.Finished };

            var UserCarts = _UserCarts.ToList();

            var Response = new List<dynamic>();

            foreach (var Cart in UserCarts)
            {
                var ProductCarts = from ProductCart in SQLContext.ProductCarts
                                   where Cart.Id == ProductCart.ProductId && Cart.Finished
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

            if (Cart == default || Cart.Finished)
            {
                var NewCart = new Cart()
                {
                    ProId = ProductInfo.ProId,
                    UserId = UserId,
                    Finished = false
                };

                SQLContext.Carts.Add(NewCart);
                SQLContext.SaveChanges();

                try
                {
                    var ProductCart = new ProductCart()
                    {
                        ProductId = ProductInfo.ProductId,
                        CartId = NewCart.Id,
                        Amount = ProductInfo.Amount
                    };

                    SQLContext.ProductCarts.Add(ProductCart);
                    SQLContext.SaveChanges();
                }catch(DbUpdateException)
                {
                    return Content(HttpStatusCode.Forbidden, new { Message = "Estoque insuficente!" });
                }

                var Product = SQLContext.Products.Find(ProductInfo.ProductId);

                return Ok(new { NewCart, Product = FilterProduct(Product) });
            }

            var AlreadyExists = SQLContext.ProductCarts.FirstOrDefault(x => x.CartId == Cart.Id
                                                                && x.ProductId == ProductInfo.ProductId
                                                                && !x.Cart.Finished);

            if(AlreadyExists != default)
            {
                try
                {
                    AlreadyExists.Amount += ProductInfo.Amount;
                    SQLContext.Entry(AlreadyExists).State = EntityState.Modified;
                    SQLContext.SaveChanges();

                    return Ok(AlreadyExists);
                }catch(DbUpdateException)
                {
                    return Content(HttpStatusCode.Forbidden, new { Message = "Estoque insuficente!" });
                }
            }

            try
            {
                var NewProductCart = new ProductCart()
                {
                    ProductId = ProductInfo.ProductId,
                    CartId = Cart.Id,
                    Amount = ProductInfo.Amount
                };

                SQLContext.ProductCarts.Add(NewProductCart);
                SQLContext.SaveChanges();

                return Ok(NewProductCart);
            }
            catch (DbUpdateException)
            {
                return Content(HttpStatusCode.Forbidden, new { Message = "Estoque insuficente!" });
            }
        }

        [HttpPut]
        [Route("cart/finish")]
        public IHttpActionResult FinishCart([FromUri] string AccessToken, [FromUri] int CartId)
        {
            TokenValidator.VerifyToken(AccessToken);
            var Cart = SQLContext.Carts.Find(CartId);

            try
            {
                Cart.Finished = true;
                SQLContext.Entry(Cart).State = EntityState.Modified;
                SQLContext.SaveChanges();
            }catch (DbUpdateException Exception)
            {
                return Content(HttpStatusCode.Forbidden, new { Exception = Exception.Message, Message = "Não foi possível concluir a compra, tente novamente mais tarde!"});
            }

            return Ok(Cart);
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