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
    public class INGRESOesController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: INGRESOes
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {
            var iNGRESOes = db.INGRESOes.Include(i => i.INGRESO_TIPO);
            return View(db.INGRESOes.Where(p => Criterio == null  || p.Tipo_Ingreso.ToString().StartsWith(Criterio)
                                                                || p.Descripcion.Contains(Criterio)
                                                                || p.Tipo_Obtencion.Contains(Criterio)).ToList());
        }

        // GET: INGRESOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESOes.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO);
        }

        // GET: INGRESOes/Create
        public ActionResult Create()
        {
            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion");
            return View();
        }

        // POST: INGRESOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo_Ingreso,Descripcion,Tipo_Obtencion,Estado")] INGRESO iNGRESO)
        {
            if (ModelState.IsValid)
            {
                db.INGRESOes.Add(iNGRESO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion", iNGRESO.Tipo_Ingreso);
            return View(iNGRESO);
        }

        // GET: INGRESOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESOes.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion", iNGRESO.Tipo_Ingreso);
            return View(iNGRESO);
        }

        // POST: INGRESOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tipo_Ingreso,Descripcion,Tipo_Obtencion,Estado")] INGRESO iNGRESO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNGRESO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion", iNGRESO.Tipo_Ingreso);
            return View(iNGRESO);
        }

        // GET: INGRESOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESOes.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO);
        }

        // POST: INGRESOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INGRESO iNGRESO = db.INGRESOes.Find(id);
            db.INGRESOes.Remove(iNGRESO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult exportaExcel()
        {
            string filename = "ReportesIngreso.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportestra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("sep=,");
            sw.WriteLine("ID,Tipo_Obtencion,Estado,Tipo_Ingreso"); //Encabezado 
            foreach (var i in db.INGRESOes.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Tipo_Obtencion + "," + i.Estado +
                    "," + i.Tipo_Ingreso);
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
