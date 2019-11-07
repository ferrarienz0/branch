using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Branch.Models;

namespace Branch.Controllers
{
    public class AddressController : ApiController
    {
        private SQLContext db = new SQLContext();

        [HttpGet]
        [Route("address")]
        public IQueryable<Address> GetAddresses()
        {
            return db.Addresses;
        }

        [HttpGet]
        [Route("address")]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> GetAddress(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPut]
        [Route("address")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            db.Entry(address).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("address")]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User User = await db.Users.FindAsync(address.UserId);
            address.User = User;

            City City = await db.Cities.FindAsync(address.CityId);
            address.City = City;

            Estate Estate = await db.States.FindAsync(City.EstateId);
            
            if(User == null || City == null || Estate == null)
            {
                return NotFound();
            }

            db.Addresses.Add(address);
            await db.SaveChangesAsync();

            return Ok(address);
        }

        [HttpDelete]
        [Route("address")]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
           
            if (address == null)
            {
                return NotFound();
            }

            db.Addresses.Remove(address);
            await db.SaveChangesAsync();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return db.Addresses.Any(e => e.Id == id);
        }
    }
}