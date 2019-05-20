using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tesis.Comun.Modelo;
using Tesis.Web.Models;
using Tesis.Web.Helpers;

namespace Tesis.Web.Controllers
{
    public class LocalsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Locals
        public async Task<ActionResult> Index()
        {
            var locals = db.Locals.Include(l => l.categoriafk);
            return View(await locals.OrderBy(l => l.nombreLocal).ToListAsync());
        }

        // GET: Locals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = await db.Locals.FindAsync(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat");
            return View();
        }

        // POST: Locals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        //SE INCORPORA EL LOCALVISTA POR LA PROPIEDAD DE LA FOTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( LocalVista vistaLocal)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Imagenes";

                if (vistaLocal.fotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(vistaLocal.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }
                //almaceno los datos en la variable local
                var local = this.ToLocal(vistaLocal,pic);
                //agrego los datos almacenados en la variable local a la base de datos
                db.Locals.Add(local);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", vistaLocal.idCategoria);
            return View(vistaLocal);
        }
        //pasando los datos y foto a la bd
        private Local ToLocal(LocalVista vistaLocal,string pic)
        {
            return new Local
            {

                idLocal=vistaLocal.idLocal,
                foto = pic,
                nombreLocal = vistaLocal.nombreLocal,
                pagWeb=vistaLocal.pagWeb,
                descripcion = vistaLocal.descripcion,
                idCategoria=vistaLocal.idCategoria
            };
        }

        // GET: Locals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = await db.Locals.FindAsync(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", local.idCategoria);
            var view = this.ToView(local);
            return View(view);
        }

        private LocalVista ToView(Local local)
        {
            return new LocalVista
            {
                idLocal= local.idLocal,
                foto = local.foto,
                nombreLocal = local.nombreLocal,
                pagWeb = local.pagWeb,
                descripcion = local.descripcion,
                idCategoria = local.idCategoria
            };
        }

        // POST: Locals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( LocalVista vistaLocal)
        {
            if (ModelState.IsValid)
            {
                var pic = vistaLocal.foto;
                var folder = "~/Content/Imagenes";

                if (vistaLocal.fotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(vistaLocal.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var local = this.ToLocal(vistaLocal, pic);

                this.db.Entry(local).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", vistaLocal.idCategoria);
            return View(vistaLocal);
        }

        // GET: Locals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = await db.Locals.FindAsync(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Local local = await db.Locals.FindAsync(id);
            db.Locals.Remove(local);
            await db.SaveChangesAsync();
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
