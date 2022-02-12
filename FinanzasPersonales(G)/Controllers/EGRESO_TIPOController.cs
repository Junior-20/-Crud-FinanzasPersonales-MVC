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
    public class EGRESO_TIPOController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: EGRESO_TIPO
        public ActionResult Index()
        {
            return View(db.EGRESO_TIPO.ToList());
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
