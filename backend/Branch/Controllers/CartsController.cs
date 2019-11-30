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
using Branch.Models;

namespace Branch.Controllers
{
    public class CartsController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("carts")]
        public List<Cart> GetCarts()
        {
            return SQLContext.Carts.ToList();
        }

        [HttpGet]
        [Route("cart")]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCart([FromUri] int Id)
        {
            Cart Cart = SQLContext.Carts.Find(Id);

            if (Cart == null)
            {
                return NotFound();
            }

            return Ok(Cart);
        }

        [HttpPut]
        [Route("cart")]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PutCart([FromUri] int Id, [FromBody] Cart Cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != Cart.Id)
            {
                return BadRequest();
            }

            SQLContext.Entry(Cart).State = EntityState.Modified;

            try
            {
                SQLContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Cart);
        }

        // POST: api/Carts
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostCart([FromBody] Cart Cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SQLContext.Carts.Add(Cart);
            SQLContext.SaveChanges();

            return Ok(Cart);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart([FromUri] int Id)
        {
            Cart Cart = SQLContext.Carts.Find(Id);
            
            if (Cart == null)
            {
                return NotFound();
            }

            SQLContext.Carts.Remove(Cart);
            SQLContext.SaveChanges();

            return Ok(Cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SQLContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int Id)
        {
            return SQLContext.Carts.Any(e => e.Id == Id);
        }
    }
}