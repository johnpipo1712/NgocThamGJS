using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using W_GJS.Models;
namespace W_GJS.Controllers
{
    public class EmployeeController : Controller
    {
        //

        // GET: /Employee/
        public GJSEntities Db_gsj;
        public ActionResult Index()
        {
            Db_gsj = new GJSEntities();
            return View(Db_gsj.M_EMPLOYEE.OrderByDescending(x => x.CREATEDATE));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
   
        public ActionResult Create(M_EMPLOYEE EMPLOYEE)
        {

            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                EMPLOYEE.ACTIVE = true;
                EMPLOYEE.STATUS = 0;
                EMPLOYEE.CREATEDATE = DateTime.Now;
                Db_gsj.Entry(EMPLOYEE).State = EntityState.Added;
                Db_gsj.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(EMPLOYEE);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? EMPLOYEE_CD)
        {
            if (EMPLOYEE_CD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            M_EMPLOYEE EMPLOYEE_edit = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == EMPLOYEE_CD);
            if (EMPLOYEE_edit == null)
                return HttpNotFound();
            return View(EMPLOYEE_edit);

        }

        [HttpPost]
   
        public ActionResult Edit(M_EMPLOYEE EMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                Db_gsj = new GJSEntities();
                M_EMPLOYEE EMPLOYEE_edit = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == EMPLOYEE.EMPLOYEE_CD);
                EMPLOYEE_edit.EMPLOYEE_CODE = EMPLOYEE.EMPLOYEE_CODE;
                EMPLOYEE_edit.EMPLOYEE_NAME = EMPLOYEE.EMPLOYEE_NAME;
                EMPLOYEE_edit.EMAIL = EMPLOYEE.EMAIL;
                EMPLOYEE_edit.ADDRESS = EMPLOYEE.ADDRESS;
                Db_gsj.SaveChanges();
                return RedirectToAction("Edit", new { EMPLOYEE_CD = 1 });

            }
            else
            {
                return View(EMPLOYEE);
            }
        }


        public ActionResult Deactive([Bind(Include = "EMPLOYEE_CD")]M_EMPLOYEE EMPLOYEE)
        {
            if (EMPLOYEE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            EMPLOYEE = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == EMPLOYEE.EMPLOYEE_CD);
            EMPLOYEE.ACTIVE = false;
            Db_gsj.Entry(EMPLOYEE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Active([Bind(Include = "EMPLOYEE_CD")]M_EMPLOYEE EMPLOYEE)
        {
            if (EMPLOYEE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Db_gsj = new GJSEntities();
            EMPLOYEE = Db_gsj.M_EMPLOYEE.Single(t => t.EMPLOYEE_CD == EMPLOYEE.EMPLOYEE_CD);
            EMPLOYEE.ACTIVE = true;
            Db_gsj.Entry(EMPLOYEE).State = EntityState.Modified;
            Db_gsj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}