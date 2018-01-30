﻿using Newtonsoft.Json.Linq;
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
        
        [HttpGet]
        public JArray GetAllEvents()
        {
            List<tevent> categories = db.tevents.ToList();
            JArray array = new JArray();

            foreach (var category in categories)
            {
                JObject obj = new JObject();
                obj["eventid"] = category.eventID;
                obj["ename"] = category.ename;
                obj["edate"] = category.scheduledOn;
                obj["edes"] = category.edescription;
                obj["eurl"] = category.presentationURL;
                obj["userid"] = category.userID;
                array.Add(obj);
            }

            return array;

        }
        [HttpGet]
        public JArray GetAllEventsbyID(int id)
        {
            tevent eventfound = db.tevents.Find(id);
            if (eventfound == null)
            {
                BadRequest();
            }

            var selectedAll = from s in db.tevents where s.eventID == id select s;
            List<tevent> eventList = selectedAll.ToList();
            JArray array = new JArray();
            foreach (var elist in eventList)
            {
                JObject obj = new JObject();
                obj["eventid"] = elist.eventID;
                obj["ename"] = elist.ename;
                obj["edate"] = elist.scheduledOn;
                obj["edes"] = elist.edescription;
                obj["uid"] = elist.userID;
                array.Add(obj);
            }

            return array;

        }



        /*public IQueryable<tevent> GetAllEvent()
        {

         
        
        


            var allEvents = from s in db.tevents orderby s.scheduledOn descending select s;
            return allEvents;

        }*/

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

        [HttpPut]
        public IHttpActionResult updateEvent(int id, tevent ev)
        {

            var entity = db.tevents.FirstOrDefault(e => e.eventID == id);
            entity.ename = ev.ename;
            entity.edescription = ev.edescription;
            entity.scheduledOn = ev.scheduledOn;
            entity.userID = entity.userID;
            db.SaveChanges();
            return Ok();

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

        [HttpPost]
        public int getusername(user user)
        {
            var a = (from s in db.users where user.username == s.username orderby s.userID select s.userID).FirstOrDefault();
           
            return a;

        }
        [HttpPost]
        public IHttpActionResult PostEventUser(tevent_users teuser)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var a = from s in db.users where user.username == s.username orderby s.userID select s.userID;

            //tevent_users euser = new tevent_users();
            //euser.userID = Convert.ToInt32(a.FirstOrDefault());
            //euser.eventID = Convert.ToInt32(id);
            db.tevent_users.Add(teuser);

            db.SaveChanges();

            return Ok();
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
