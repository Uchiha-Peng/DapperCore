using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DapperCore.Controllers
{
    public class HomeController : Controller
    {
        public DbContext db;
        public HomeController(IOptions<DbContext> options)
        {
            db = options.Value;
        }
        public IActionResult Index()
        {
            List<User> list = db.SelectAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Detail(int? Id)
        {
            User user = db.SelectUser(Id ?? 0);
            if (user != null)
            {

                return View(user);
            }
            else
            {
                return View();
            }
        }


        public IActionResult Edit(int Id)
        {
            User user = db.SelectUser(Id);
            if (user != null)
            {

                return View(user);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Edit(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int? Id)
        {
            User user = db.SelectUser(Id ?? 0);
            if (user != null)
            {

                return View(user);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                db.Delete(Id);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }
}