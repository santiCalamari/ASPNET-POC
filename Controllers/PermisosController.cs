using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Permisos_khensys.Models;

namespace Permisos_khensys.Controllers
{
    public class PermisosController : Controller
    {
        private KhensysEntities1 db = new KhensysEntities1();

        // GET: Permisos
        public ActionResult Index()
        {
            var permisos = db.Permisos.Include(p => p.TipoPermisos);
            return View(permisos.ToList());
        }

        // GET: Permisos/Create
        public ActionResult Create()
        {
            ViewBag.tipoPermisoId = new SelectList(db.TipoPermisos, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombreEmpleado,apellidoEmpleado,fecha,tipoPermisoId")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipoPermisoId = new SelectList(db.TipoPermisos, "Id", "Descripcion", permisos.tipoPermisoId);
            return View(permisos);
        }

        
        // GET: Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permisos permisos = db.Permisos.Find(id);
            db.Permisos.Remove(permisos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}
