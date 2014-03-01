using Microsoft.AspNet.SignalR;
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
using System.Web.Http.Description;
using Temperature_Display.Models;

namespace Temperature_Display.Controllers
{
    public class TemperatureController : ApiController
    {
        private TemperatureDisplayContext db = new TemperatureDisplayContext();

        // GET api/Default1
        public IQueryable<TemperatureReading> GetTemperatureReadings()
        {
            return db.TemperatureReadings;
        }

        // GET api/Default1/5
        [ResponseType(typeof(TemperatureReading))]
        public async Task<IHttpActionResult> GetTemperatureReading(int id)
        {
            TemperatureReading temperaturereading = await db.TemperatureReadings.FindAsync(id);
            if (temperaturereading == null)
            {
                return NotFound();
            }

            return Ok(temperaturereading);
        }

        // PUT api/Default1/5
        public async Task<IHttpActionResult> PutTemperatureReading(int id, TemperatureReading temperaturereading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperaturereading.TemperatureReadingId)
            {
                return BadRequest();
            }

            db.Entry(temperaturereading).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureReadingExists(id))
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

        // POST api/Default1
        [ResponseType(typeof(TemperatureReading))]
        public async Task<IHttpActionResult> PostTemperatureReading(TemperatureReading temperaturereading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            temperaturereading.Date = System.DateTime.Now;
            db.TemperatureReadings.Add(temperaturereading);
            var context = GlobalHost.ConnectionManager.GetHubContext<Temperature_Display.TemperatureHub>();
            context.Clients.All.addNewTemperature(temperaturereading);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = temperaturereading.TemperatureReadingId }, temperaturereading);
        }

        // DELETE api/Default1/5
        [ResponseType(typeof(TemperatureReading))]
        public async Task<IHttpActionResult> DeleteTemperatureReading(int id)
        {
            TemperatureReading temperaturereading = await db.TemperatureReadings.FindAsync(id);
            if (temperaturereading == null)
            {
                return NotFound();
            }

            db.TemperatureReadings.Remove(temperaturereading);
            await db.SaveChangesAsync();

            return Ok(temperaturereading);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperatureReadingExists(int id)
        {
            return db.TemperatureReadings.Count(e => e.TemperatureReadingId == id) > 0;
        }
    }
}