using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class WaterController : Controller
    {


        // GET: Water
        public ActionResult Water()
        {
            return View();
        }



        [HttpGet]
        public JsonResult getRemoni (String someId)
        {

           
            try
            {
                return Json("Text", JsonRequestBehavior.AllowGet);
            }

            catch
            {
                throw new System.ArgumentException("Json not success");
            }


        }



        // GET: Water/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Water/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Water/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Water/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Water/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Water/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Water/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
