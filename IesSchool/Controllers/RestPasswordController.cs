using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IesSchool.Controllers
{

    [Route("api/[controller]/[action]")]

    public class RestPasswordController : Controller
    {
        // GET: RestPasswordController

        public ActionResult Index()
        {
            return View();
        }

        // GET: RestPasswordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestPasswordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestPasswordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RestPasswordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestPasswordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RestPasswordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestPasswordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
