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
    public class TRANSACIONController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();

        // GET: TRANSACION
        [Authorize(Users = "Admin3030@gmail.com")]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.TRANSACIONs.Where(p => Criterio == null || p.ID.ToString().Contains(Criterio)
                                                                || p.Usuario.ToString().StartsWith(Criterio)
                                                                || p.NO_Tarjeta_CR.ToString().StartsWith(Criterio)
                                                                 ).ToList());
        }

        // GET: TRANSACION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACION tRANSACION = db.TRANSACIONs.Find(id);
            if (tRANSACION == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACION);
        }

        // GET: TRANSACION/Create
        public ActionResult Create()
        {
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion");
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre");
            return View();
        }

        // POST: TRANSACION/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo_Transacion,Usuario,Evento,Tipo_Pago,Fecha_Transacion,Fecha_Registro,Monto_Transacion,NO_Tarjeta_CR,Comentario,Estado")] TRANSACION tRANSACION)
        {
            if (ModelState.IsValid)
            {
                db.TRANSACIONs.Add(tRANSACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", tRANSACION.Tipo_Pago);
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", tRANSACION.Usuario);
            return View(tRANSACION);
        }

        // GET: TRANSACION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACION tRANSACION = db.TRANSACIONs.Find(id);
            if (tRANSACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", tRANSACION.Tipo_Pago);
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", tRANSACION.Usuario);
            return View(tRANSACION);
        }

        // POST: TRANSACION/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tipo_Transacion,Usuario,Evento,Tipo_Pago,Fecha_Transacion,Fecha_Registro,Monto_Transacion,NO_Tarjeta_CR,Comentario,Estado")] TRANSACION tRANSACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANSACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_Pago = new SelectList(db.PAGO_TIPO, "ID", "Descripcion", tRANSACION.Tipo_Pago);
            ViewBag.Usuario = new SelectList(db.USUARIOs, "ID", "Nombre", tRANSACION.Usuario);
            return View(tRANSACION);
        }

        // GET: TRANSACION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACION tRANSACION = db.TRANSACIONs.Find(id);
            if (tRANSACION == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACION);
        }

        // POST: TRANSACION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRANSACION tRANSACION = db.TRANSACIONs.Find(id);
            db.TRANSACIONs.Remove(tRANSACION);
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
