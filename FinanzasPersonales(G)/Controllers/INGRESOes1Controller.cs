using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanzasPersonales_G_;

namespace FinanzasPersonales_G_.Controllers
{
    public class INGRESOes1Controller : Controller
    {
        private FinanzasPerEntities1 db = new FinanzasPerEntities1();

        // GET: INGRESOes1
        public ActionResult Index()
        {
            var iNGRESO = db.INGRESO.Include(i => i.INGRESO_TIPO);
            return View(iNGRESO.ToList());
        }

        // GET: INGRESOes1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESO.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO);
        }

        // GET: INGRESOes1/Create
        public ActionResult Create()
        {
            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion");
            return View();
        }

        // POST: INGRESOes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo_Ingreso,Descripcion,Tipo_Obtencion,Estado")] INGRESO iNGRESO)
        {
            if (ModelState.IsValid)
            {
                db.INGRESO.Add(iNGRESO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion", iNGRESO.Tipo_Ingreso);
            return View(iNGRESO);
        }

        // GET: INGRESOes1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESO.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Ingreso = new SelectList(db.INGRESO_TIPO, "ID", "Descripcion", iNGRESO.Tipo_Ingreso);
            return View(iNGRESO);
        }

        // POST: INGRESOes1/Edit/5
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

        // GET: INGRESOes1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO iNGRESO = db.INGRESO.Find(id);
            if (iNGRESO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO);
        }

        // POST: INGRESOes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INGRESO iNGRESO = db.INGRESO.Find(id);
            db.INGRESO.Remove(iNGRESO);
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
    }
}
