﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using techtalk_revised.Models;
using Newtonsoft.Json.Linq;


namespace techtalk_revised.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class userController : ApiController
    {
        //Db entity
        private techtalkEntities db = new techtalkEntities();
        [HttpPost]
        public IHttpActionResult LoginCheck(user user)
        {
            user foundUser = db.users.Where(a => a.username.Equals(user.username)&& a.password.Equals(user.password)).FirstOrDefault();
            if (foundUser == null)
                return null;
            else
            {

                if (foundUser.isAdmin == true)
                    return Ok(true);
                else
                    if(foundUser.isAdmin == false)
                    return Ok(false);
                else
                    return null;
            }
           
        }


        //GET all users
        [HttpGet]

        public JArray getUsers()
        {
            List<user> categories = db.users.ToList();
            JArray array = new JArray();

            foreach (var category in categories)
            {
                JObject obj = new JObject();
                obj["userid"] = category.userID;
                obj["name"] = category.name;
                obj["uname"] = category.username;
                obj["designation"] = category.designation;
                obj["cgicode"] = category.cgicode;
                array.Add(obj);
            }

            return array;
        }

        //GET users via ID
        [HttpGet]
        public String getUsersById(int id)
        {
            //user userID = db.users.Find(id);
            //if(userID == null)
            //{
            //    return NotFound();
            //}
            //return Ok(db.users);
            user userfound = db.users.Find(id);
            if (userfound == null)
            {
                BadRequest();
            }

            var selectedAll = (from s in db.users where s.userID == id select s.name).FirstOrDefault();
            return selectedAll;
            //List<user> userList = selectedAll.ToList();
            //JArray array = new JArray();
            //foreach (var ulist in userList)
            //{
            //    JObject obj = new JObject();
            //    obj["userid"] = ulist.userID;
            //    obj["name"] = ulist.name;
            //    obj["uname"] = ulist.username;
            //    obj["designation"] = ulist.designation;
            //    obj["cgicode"] = ulist.cgicode;
            //    array.Add(obj);
            //}

            //return array;

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
