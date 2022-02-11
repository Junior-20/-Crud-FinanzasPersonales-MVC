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
    public class EGRESOes1Controller : Controller
    {
        private FinanzasPerEntities1 db = new FinanzasPerEntities1();

        // GET: EGRESOes1
        public ActionResult Index()
        {
            var eGRESO = db.EGRESO.Include(e => e.EGRESO_RENGLON).Include(e => e.EGRESO_TIPO).Include(e => e.PAGO_TIPO);
            return View(eGRESO.ToList());
        }

        // GET: EGRESOes1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESO.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO);
        }

        // GET: EGRESOes1/Create
        public ActionResult Create()
        {
            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion");
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion");
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion");
            return View();
        }

        // POST: EGRESOes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo_Egreso,Renglon_Egreso,Tipo_Pago,Decripcion,Estado")] EGRESO eGRESO)
        {
            if (ModelState.IsValid)
            {
                db.EGRESO.Add(eGRESO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion", eGRESO.Renglon_Egreso);
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Egreso);
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Pago);
            return View(eGRESO);
        }

        // GET: EGRESOes1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESO.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Renglon_Egreso = new SelectList(db.EGRESO_RENGLON, "ID", "Descripcion", eGRESO.Renglon_Egreso);
            ViewBag.Tipo_Egreso = new SelectList(db.EGRESO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Egreso);
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", eGRESO.Tipo_Pago);
            return View(eGRESO);
        }

        // POST: EGRESOes1/Edit/5
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

        // GET: EGRESOes1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EGRESO eGRESO = db.EGRESO.Find(id);
            if (eGRESO == null)
            {
                return HttpNotFound();
            }
            return View(eGRESO);
        }

        // POST: EGRESOes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EGRESO eGRESO = db.EGRESO.Find(id);
            db.EGRESO.Remove(eGRESO);
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
