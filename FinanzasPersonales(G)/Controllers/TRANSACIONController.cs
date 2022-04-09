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
    public class TRANSACIONController : Controller
    {
        private FinanzasPerEntities2 db = new FinanzasPerEntities2();
        private object tRANSACION;

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
                USUARIO user = (from r in db.USUARIOs.Where
               (a => a.ID == tRANSACION.Usuario)

                                     select r).FirstOrDefault();

                user.Limite_Egreso = (int)((user.Limite_Egreso) - (tRANSACION.Monto_Transacion));
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
            USUARIO user = (from r in db.USUARIOs.Where
              (a => a.ID == tRANSACION.Usuario)

                            select r).FirstOrDefault();

            user.Limite_Egreso = (int)((user.Limite_Egreso) - (tRANSACION.Monto_Transacion));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult exportaExcel()
        {
            string filename = "ReportesTransacion.csv";
            string filepath = @"C:\Users\Alex Junior Valera\Desktop\reportestra" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("sep=,");
            sw.WriteLine("ID,Tipo_Transacion,Usuario,Evento,Tipo_Pago," +
                "Fecha_Transacion,Fecha_Registro,Monto_Transacion,NO_Tarjeta_CR," +
                "Cometario,Estado"); //Encabezado 
            foreach (var i in db.TRANSACIONs.ToList())
            {
                sw.WriteLine(i.ID.ToString() + "," + i.Tipo_Transacion + "," + i.Usuario +
                    "," + i.Evento +
                    "," + i.Tipo_Pago +
                    "," + i.Fecha_Transacion +
                    "," + i.Fecha_Registro +
                    "," + i.Monto_Transacion +
                    "," + i.NO_Tarjeta_CR +
                    "," + i.Comentario +
                    "," + i.Estado);
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
