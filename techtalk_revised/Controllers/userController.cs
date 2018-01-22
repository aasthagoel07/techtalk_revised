using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using techtalk_revised.Models;


namespace techtalk_revised.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class userController : ApiController
    {
        //Db entity
        private techtalkEntities db = new techtalkEntities();

        //GET all users
        [HttpGet]

        public IHttpActionResult getUsers()
        {
            return Ok(db.users);
        }

        //GET users via ID
        [HttpGet]
        public IHttpActionResult getUsers(int id)
        {
            user userID = db.users.Find(id);
            if(userID == null)
            {
                return NotFound();
            }
            return Ok(db.users);
        }
       
        //POST Insert User
        [HttpPost]

        public IHttpActionResult postUser(user user1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user1);
            try
            {
                db.SaveChanges();
                return Ok(user1);
            }
            catch(Exception e)
            {
                return Ok(e.StackTrace);
            }
        }






    }
}
