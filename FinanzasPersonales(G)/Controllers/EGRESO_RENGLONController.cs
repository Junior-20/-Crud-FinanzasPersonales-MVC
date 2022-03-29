using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanzasPersonales_G_.Models;

namespace FinanzasPersonales_G_.Controllers
{
 
    public class EGRESO_RENGLONController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: EGRESO_RENGLON
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.EGRESO_RENGLON.Where(p => Criterio == null || p.Descripcion.Contains(Criterio)).ToList()); ;
        }

        // GET: EGRESO_RENGLON/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_RENGLON eGRESO_RENGLON = db.EGRESO_RENGLON.Find(id);
            if (eGRESO_RENGLON == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_RENGLON);
        }

        // GET: EGRESO_RENGLON/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EGRESO_RENGLON/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Estado")] EGRESO_RENGLON eGRESO_RENGLON)
        {
            if (ModelState.IsValid)
            {
                db.EGRESO_RENGLON.Add(eGRESO_RENGLON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eGRESO_RENGLON);
        }

        // GET: EGRESO_RENGLON/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_RENGLON eGRESO_RENGLON = db.EGRESO_RENGLON.Find(id);
            if (eGRESO_RENGLON == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_RENGLON);
        }

        // POST: EGRESO_RENGLON/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Estado")] EGRESO_RENGLON eGRESO_RENGLON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eGRESO_RENGLON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eGRESO_RENGLON);
        }

        // GET: EGRESO_RENGLON/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO_RENGLON eGRESO_RENGLON = db.EGRESO_RENGLON.Find(id);
            if (eGRESO_RENGLON == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO_RENGLON);
        }

        // POST: EGRESO_RENGLON/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EGRESO_RENGLON eGRESO_RENGLON = db.EGRESO_RENGLON.Find(id);
            db.EGRESO_RENGLON.Remove(eGRESO_RENGLON);
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
            string filename = "Egresos_Renglon.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportes tra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.Writeline("sep=,");
            sw.WriteLine("ID,Descripcion,Estado"); //Encabezado 
            foreach (var i in db.EGRESOes.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Decripcion + "," + i.Estado);
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
