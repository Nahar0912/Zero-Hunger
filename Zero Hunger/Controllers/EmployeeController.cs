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

        [HttpGet]
        public ActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpLogin(EmployeeLoginDTO e)
        {
            var db = new ZeroHungerEntities();
            var user = (from d in db.UserInfoes
                        where d.Email == e.Email && d.Password == e.Password && string.Equals(d.UserType, "Employee")
                        select d).SingleOrDefault();
            if (user != null)
            {
                return RedirectToAction("EmpDashboard");
            }
            else
            {
                TempData["Msg"] = "Username or Passwordd or Usertype Invalide";
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
            exdata.E_Status = d.E_Status;
           

            db.SaveChanges();
            return RedirectToAction("EmpDashboard");
        }
    }
}