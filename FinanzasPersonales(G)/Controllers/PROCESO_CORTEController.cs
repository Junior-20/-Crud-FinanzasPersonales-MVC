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
    public class PROCESO_CORTEController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: PROCESO_CORTE
        [Authorize(Users = "Admin3030@gmail.com")]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.PROCESO_CORTE.Where(p => Criterio == null || p.ID.ToString().Contains(Criterio)
                                                                || p.Usuario.ToString().StartsWith(Criterio)
                                                                || p.Fecha_Corte.ToString().StartsWith(Criterio)
                                                                 ).ToList());
        }

        // GET: PROCESO_CORTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROCESO_CORTE pROCESO_CORTE = db.PROCESO_CORTE.Find(id);
            if (pROCESO_CORTE == null)
            {
                return HttpNotFound();
            }
            return View(pROCESO_CORTE);
        }

        // GET: PROCESO_CORTE/Create
        public ActionResult Create()
        {
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre");
            return View();
        }

        // POST: PROCESO_CORTE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ano,Mes,Fecha_Corte,Balance_Inicial,Total_ingreso,Total_Egreso,Balance_corte,Usuario")] PROCESO_CORTE pROCESO_CORTE)
        {
            if (ModelState.IsValid)
            {
                db.PROCESO_CORTE.Add(pROCESO_CORTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", pROCESO_CORTE.Usuario);
            return View(pROCESO_CORTE);
        }

        // GET: PROCESO_CORTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROCESO_CORTE pROCESO_CORTE = db.PROCESO_CORTE.Find(id);
            if (pROCESO_CORTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", pROCESO_CORTE.Usuario);
            return View(pROCESO_CORTE);
        }

        // POST: PROCESO_CORTE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ano,Mes,Fecha_Corte,Balance_Inicial,Total_ingreso,Total_Egreso,Balance_corte,Usuario")] PROCESO_CORTE pROCESO_CORTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROCESO_CORTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", pROCESO_CORTE.Usuario);
            return View(pROCESO_CORTE);
        }

        // GET: PROCESO_CORTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROCESO_CORTE pROCESO_CORTE = db.PROCESO_CORTE.Find(id);
            if (pROCESO_CORTE == null)
            {
                return HttpNotFound();
            }
            return View(pROCESO_CORTE);
        }

        // POST: PROCESO_CORTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROCESO_CORTE pROCESO_CORTE = db.PROCESO_CORTE.Find(id);
            db.PROCESO_CORTE.Remove(pROCESO_CORTE);
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
