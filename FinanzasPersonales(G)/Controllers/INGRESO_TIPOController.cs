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
    public class INGRESO_TIPOController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: INGRESO_TIPO
        [Authorize]
        public ActionResult Index()
        {
            return View(db.INGRESO_TIPO.ToList());
        }

        // GET: INGRESO_TIPO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO_TIPO iNGRESO_TIPO = db.INGRESO_TIPO.Find(id);
            if (iNGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO_TIPO);
        }

        // GET: INGRESO_TIPO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INGRESO_TIPO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Estado")] INGRESO_TIPO iNGRESO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.INGRESO_TIPO.Add(iNGRESO_TIPO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iNGRESO_TIPO);
        }

        // GET: INGRESO_TIPO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO_TIPO iNGRESO_TIPO = db.INGRESO_TIPO.Find(id);
            if (iNGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO_TIPO);
        }

        // POST: INGRESO_TIPO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Estado")] INGRESO_TIPO iNGRESO_TIPO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNGRESO_TIPO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iNGRESO_TIPO);
        }

        // GET: INGRESO_TIPO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESO_TIPO iNGRESO_TIPO = db.INGRESO_TIPO.Find(id);
            if (iNGRESO_TIPO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESO_TIPO);
        }

        // POST: INGRESO_TIPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INGRESO_TIPO iNGRESO_TIPO = db.INGRESO_TIPO.Find(id);
            db.INGRESO_TIPO.Remove(iNGRESO_TIPO);
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
