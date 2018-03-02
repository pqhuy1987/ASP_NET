﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Framework;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class CateloryController : Controller
    {
        //
        // GET: /Admin/Catelory/

        public ActionResult Index()
        {
            var iplCate =  new CategoryModel();
            var model = iplCate.ListAll();
            return View(model);
        }

        //
        // GET: /Admin/Catelory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Catelory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Create

        [HttpPost]
        public ActionResult Create(Category collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Catelory/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Edit/5

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

        //
        // GET: /Admin/Catelory/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Catelory/Delete/5

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
