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
    public class USUARIOs1Controller : Controller
    {
        private FinanzasPerEntities1 db = new FinanzasPerEntities1();

        // GET: USUARIOs1
        public ActionResult Index()
        {
            var uSUARIO = db.USUARIO.Include(u => u.PROCESO_CORTE);
            return View(uSUARIO.ToList());
        }

        // GET: USUARIOs1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // GET: USUARIOs1/Create
        public ActionResult Create()
        {
            ViewBag.Fecha_Corte = new SelectList(db.PROCESO_CORTE, "ID", "Mes");
            return View();
        }

        // POST: USUARIOs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Cedula,Limite_Egreso,Tipo_Persona,Fecha_Corte,Estado")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.USUARIO.Add(uSUARIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fecha_Corte = new SelectList(db.PROCESO_CORTE, "ID", "Mes", uSUARIO.Fecha_Corte);
            return View(uSUARIO);
        }

        // GET: USUARIOs1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fecha_Corte = new SelectList(db.PROCESO_CORTE, "ID", "Mes", uSUARIO.Fecha_Corte);
            return View(uSUARIO);
        }

        // POST: USUARIOs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Cedula,Limite_Egreso,Tipo_Persona,Fecha_Corte,Estado")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fecha_Corte = new SelectList(db.PROCESO_CORTE, "ID", "Mes", uSUARIO.Fecha_Corte);
            return View(uSUARIO);
        }

        // GET: USUARIOs1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: USUARIOs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            db.USUARIO.Remove(uSUARIO);
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
