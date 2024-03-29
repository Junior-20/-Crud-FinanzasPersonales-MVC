﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanzasPersonales_G_.Models;

namespace FinanzasPersonales_G_.Controllers
{
    public class PAGO_TIPOController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: PAGO_TIPO
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.PAGO_TIPO.Where(p => Criterio == null || p.Descripcion.Contains(Criterio)).ToList());
        }

        // GET: PAGO_TIPO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_TIPO pAGO_TIPO = db.PAGO_TIPO.Find(id);
            if (pAGO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_TIPO);
        }

        // GET: PAGO_TIPO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PAGO_TIPO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Estado")] PAGO_TIPO pAGO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.PAGO_TIPO.Add(pAGO_TIPO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAGO_TIPO);
        }

        // GET: PAGO_TIPO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_TIPO pAGO_TIPO = db.PAGO_TIPO.Find(id);
            if (pAGO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_TIPO);
        }

        // POST: PAGO_TIPO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Estado")] PAGO_TIPO pAGO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGO_TIPO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pAGO_TIPO);
        }

        // GET: PAGO_TIPO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_TIPO pAGO_TIPO = db.PAGO_TIPO.Find(id);
            if (pAGO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_TIPO);
        }

        // POST: PAGO_TIPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAGO_TIPO pAGO_TIPO = db.PAGO_TIPO.Find(id);
            db.PAGO_TIPO.Remove(pAGO_TIPO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult exportaExcel()
        {
            string filename = "Reportespagos.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportestra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("sep=,");
            sw.WriteLine("ID,Descripcion,Estado");
            foreach (var i in db.PAGO_TIPO.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Descripcion+ "," + i.Estado);
            }
            sw.Close();
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
