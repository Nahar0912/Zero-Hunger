using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.EF;
using Zero_Hunger.DTO;



namespace Zero_Hunger.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant

        [HttpGet]
        public ActionResult RestLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RestLogin(RestaurantLoginDTO r)
        {
            var db = new ZeroHungerEntities();
            var user = (from d in db.UserInfoes
                      where d.Email == r.Email && d.Password == r.Password && string.Equals(d.UserType, "Restaurant")
                        select d).SingleOrDefault();
            if (user!=null)
            {
               
                return RedirectToAction("RestDashboard");
            }
            else
            {
                TempData["Msg"] = "Username or Passwordd or Usertype Invalide";
            }
            return View();
        }

        public ActionResult RestDashboard()
        {
            var db = new ZeroHungerEntities();
            var data = db.NGOSystems.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult RestCreateRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RestCreateRequest(NGOSystem n)
        {
            var db = new ZeroHungerEntities();
            db.NGOSystems.Add(n);
            db.SaveChanges();
            return RedirectToAction("RestDashboard");
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
            exdata.FoodItem = d.FoodItem;
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