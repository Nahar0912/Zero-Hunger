using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.EF;
using Zero_Hunger.DTO;

namespace Zero_Hunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpLogin(EmployeeLoginDTO e)
        {
            var db = new ZeroHungerEntities();
            var user = (from d in db.UserInfoes
                        where d.Email == e.Email && d.Password == e.Password
                        select d).SingleOrDefault();
            if (user != null)
            {
                return RedirectToAction("EmpDashboard");
            }
            else
            {
                ModelState.AddModelError("Email", "Invalid user");
            }
            return View();
        }

        public ActionResult EmpDashboard()
        {
            var db = new ZeroHungerEntities();
            var data = db.NGOSystems.ToList();
            return View(data);
        }

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
            return RedirectToAction("EmpDashboard");
        }
    }
}