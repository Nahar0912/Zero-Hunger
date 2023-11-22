using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.EF;

namespace Zero_Hunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UserLogin()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var db = new ZeroHungerEntities();
            var data = db.UserInfoes.ToList();
            return View(data);
        }
      
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo s)
        {
            var db = new ZeroHungerEntities();
            db.UserInfoes.Add(s);
            db.SaveChanges();
            return RedirectToAction("UserLogin");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ZeroHungerEntities();
            var ex = (from d in db.UserInfoes
                      where d.Id == id
                      select d).SingleOrDefault();
            return View(ex);

        }
        [HttpPost]
        public ActionResult Edit(UserInfo d)
        {
            var db = new ZeroHungerEntities();
            var exdata = db.UserInfoes.Find(d.Id);
            exdata.Name = d.Name;
            exdata.Email = d.Email;
            exdata.Password = d.Password;
            exdata.Phone= d.Phone;
            exdata.Address = d.Address;
            exdata.UserType = d.UserType;
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult Delete(int id)
        {
            var db = new ZeroHungerEntities(); 
            var exdata = db.UserInfoes.Find(id);
            db.UserInfoes.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }


    }
}