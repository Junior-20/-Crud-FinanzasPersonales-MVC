using System;
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
    public class EGRESO_TIPOController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: EGRESO_TIPO
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.EGRESO_TIPO.Where(p => Criterio == null || p.Descripcion.Contains(Criterio)).ToList()); ;
        }

        // GET: EGRESO_TIPO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_TIPO eGRESO_TIPO = db.EGRESO_TIPO.Find(id);
            if (eGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_TIPO);
        }

        // GET: EGRESO_TIPO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EGRESO_TIPO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Estado")] EGRESO_TIPO eGRESO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.EGRESO_TIPO.Add(eGRESO_TIPO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eGRESO_TIPO);
        }

        // GET: EGRESO_TIPO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_TIPO eGRESO_TIPO = db.EGRESO_TIPO.Find(id);
            if (eGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_TIPO);
        }

        // POST: EGRESO_TIPO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Estado")] EGRESO_TIPO eGRESO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eGRESO_TIPO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eGRESO_TIPO);
        }

        // GET: EGRESO_TIPO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_TIPO eGRESO_TIPO = db.EGRESO_TIPO.Find(id);
            if (eGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_TIPO);
        }

        // POST: EGRESO_TIPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EGRESO_TIPO eGRESO_TIPO = db.EGRESO_TIPO.Find(id);
            db.EGRESO_TIPO.Remove(eGRESO_TIPO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ExportaExcel()
        {
            string filename = "Egresos_Tipo.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportestra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("sep=,");
            sw.WriteLine("ID,Descripcion,Estado"); //Encabezado 
            foreach (var i in db.EGRESO_TIPO.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Descripcion + "," + i.Estado);
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
