using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using techtalk_revised.Models;

namespace techtalk_revised.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class eventController : ApiController
    {
        private techtalkEntities db = new techtalkEntities();

        public IQueryable<tevent> GetAllEvent()
        {
            
            var allEvents = from s in db.tevents orderby s.scheduledOn descending select s;
            return allEvents;

        }

        public IQueryable<tevent> GetPastEvent()
        {
            var todaydate = DateTime.Now;
            var pastEvents = from s in db.tevents where s.scheduledOn < todaydate orderby s.scheduledOn descending select s;
            return pastEvents;

        }
        public IQueryable<tevent> GetUpcomingEvent()
        {
            var todaydate = DateTime.Now;
            var upcomingEvents = from s in db.tevents where s.scheduledOn > todaydate orderby s.scheduledOn descending select s;
            return upcomingEvents;

        }
        [ResponseType(typeof(tevent))]
        public IHttpActionResult PostEventTable(tevent eventTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.tevents.Add(eventTable);        
            

            try
            {
               
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventTableExists(eventTable.eventID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventTable.eventID }, eventTable);
        }
        // PUT: api/EventTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventTable(int id, tevent eventTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventTable.eventID)
            {
                return BadRequest();
            }

            db.Entry(eventTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTableExists(id))
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
        // DELETE: api/EventTables/5
        [ResponseType(typeof(tevent))]
        public IHttpActionResult DeleteEventTable(int id)
        {
            tevent eventTable = db.tevents.Find(id);
            if (eventTable == null)
            {
                return NotFound();
            }

            db.tevents.Remove(eventTable);
            db.SaveChanges();

            return Ok(eventTable);
        }
        private bool EventTableExists(int id)
        {
            return db.tevents.Count(e => e.eventID == id) > 0;
        }
    }
}
