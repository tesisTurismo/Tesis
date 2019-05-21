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
    public class CategoriasController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaVista datosCategoria)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder= "~/Content/catImagen";

                if (datosCategoria.fotoFileCat != null)
                {
                    pic = FilesHelper.UploadPhoto(datosCategoria.fotoFileCat, folder);
                    pic = $"{folder}/{pic}";

                }

                var categorias = this.ToCategorias(datosCategoria, pic);


                db.Categorias.Add(categorias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(datosCategoria);
        }

        private Categoria ToCategorias(CategoriaVista datosCategoria, string pic)
        {
            return new Categoria {
                idCategoria = datosCategoria.idCategoria,
                nombreCat=datosCategoria.nombreCat,
                fotoCategoria=pic,
                
            };
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(categoria);
            return View(view);
        }

        private CategoriaVista ToView(Categoria categorias)
        {
            return new CategoriaVista
            {
                idCategoria = categorias.idCategoria,
                nombreCat = categorias.nombreCat,
                fotoCategoria = categorias.fotoCategoria

            };
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoriaVista datosCategoria)
        {
            if (ModelState.IsValid)
            {
                var pic = datosCategoria.fotoCategoria;
                var folder = "~/Content/catImagen";

                if (datosCategoria.fotoFileCat != null)
                {
                    pic = FilesHelper.UploadPhoto(datosCategoria.fotoFileCat, folder);
                    pic = $"{folder}/{pic}";

                }
                var categorias = this.ToCategorias(datosCategoria, pic);
                db.Entry(categorias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(datosCategoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            db.Categorias.Remove(categoria);
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
