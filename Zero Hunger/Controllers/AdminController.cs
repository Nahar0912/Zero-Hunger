﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin a)
        {
            var db = new ZeroHungerEntities();
            var user = (from d in db.UserInfoes
                        where d.Email == a.Email && d.Password == a.Password
                        select d).SingleOrDefault();
            if (user != null)
            {
                //db.Restuarants.Add(a);
                //db.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                TempData["Msg"] = "Username or Passwordd Invalide";
            }
            return View();
        }

        public ActionResult AdminDashboard()
        {
            var db = new ZeroHungerEntities();
            var data = db.NGOSystems.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ZeroHungerEntities();
            var ex = (from d in db.NGOSystems
                      where d.Id == id
                      select d).SingleOrDefault();
            return View(ex);

        }
        [HttpPost]
        public ActionResult Edit(NGOSystem d)
        {
            var db = new ZeroHungerEntities();
            var exdata = db.NGOSystems.Find(d.Id);
            exdata.R_Name = d.R_Name;
            exdata.R_Address = d.R_Address;
            exdata.FoodCollectionTime = d.FoodCollectionTime;
            exdata.R_Status = d.R_Status;

            db.SaveChanges();
            return RedirectToAction("RestDashboard");
        }

        public ActionResult Delete(int id)
        {
            var db = new ZeroHungerEntities();
            var exdata = db.NGOSystems.Find(id);
            db.NGOSystems.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("RestDashboard");
        }
    }
}