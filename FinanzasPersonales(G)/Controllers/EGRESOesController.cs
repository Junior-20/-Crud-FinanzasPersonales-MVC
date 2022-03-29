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
    public class EGRESOesController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: EGRESOes
        public ActionResult Index(string Criterio = null)
        {
            var eGRESOes = db.EGRESOes.Include(e => e.EGRESO_RENGLON).Include(e => e.EGRESO_TIPO).Include(e => e.PAGO_TIPO);
            return View(db.EGRESOes.Where(p => Criterio == null || p.Decripcion.Contains(Criterio)
                                                                            || p.Tipo_Egreso.ToString().StartsWith(Criterio)
                                                                            || p.Tipo_Pago.ToString().StartsWith(Criterio)
                                                                            || p.Renglon_Egreso.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: EGRESOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESOes.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO);
        }

        // GET: EGRESOes/Create
        public ActionResult Create()
        {
            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion");
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion");
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion");
            return View();
        }

        // POST: EGRESOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo_Egreso,Renglon_Egreso,Tipo_Pago,Decripcion,Estado")] EGRESO eGRESO)
        {
            if (ModelState.IsValid)
            {
                db.EGRESOes.Add(eGRESO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion", eGRESO.Renglon_Egreso);
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Egreso);
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Pago);
            return View(eGRESO);
        }

        // GET: EGRESOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESOes.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion", eGRESO.Renglon_Egreso);
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Egreso);
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Pago);
            return View(eGRESO);
        }

        // POST: EGRESOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tipo_Egreso,Renglon_Egreso,Tipo_Pago,Decripcion,Estado")] EGRESO eGRESO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eGRESO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion", eGRESO.Renglon_Egreso);
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Egreso);
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Pago);
            return View(eGRESO);
        }

        // GET: EGRESOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESOes.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO);
        }

        // POST: EGRESOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EGRESO eGRESO = db.EGRESOes.Find(id);
            db.EGRESOes.Remove(eGRESO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ExportaExcel()
        {
            string filename = "Egresos.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportes tra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.Writeline("sep=,");
            sw.WriteLine("ID,Tipo_Egreso,Renglon_Egreso,Tipo_Pago,Decripcion,Estado"); //Encabezado 
            foreach (var i in db.EGRESOes.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Tipo_Egreso + "," + i.Renglon_Egreso + "," + i.Tipo_Pago + "," + i.Decripcion + "," + i.Estado);
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
    }
}
