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
    public class LugarHistoricoesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: LugarHistoricoes
        public async Task<ActionResult> Index()
        {
            var lugarHistoricoes = db.LugarHistoricoes.Include(l => l.categoriafk);
            return View(await lugarHistoricoes.ToListAsync());
        }

        // GET: LugarHistoricoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            if (lugarHistorico == null)
            {
                return HttpNotFound();
            }
            return View(lugarHistorico);
        }

        // GET: LugarHistoricoes/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat");
            return View();
        }

        // POST: LugarHistoricoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LugarHistoricoVista lugarHistorico) 
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/imagenTurismo";

                if (lugarHistorico.fotoFilelugar != null)
                {
                    pic = FilesHelper.UploadPhoto(lugarHistorico.fotoFilelugar, folder);
                    pic = $"{folder}/{pic}";

                }

                var lugaresHistoricos = this.ToLugarHistorico(lugarHistorico, pic);

                db.LugarHistoricoes.Add(lugaresHistoricos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", lugarHistorico.idCategoria);
            return View(lugarHistorico);
        }

        private LugarHistorico ToLugarHistorico(LugarHistoricoVista lugarHistorico, string pic)
        {
            return new LugarHistorico
            {
                idLugarHistorico=lugarHistorico.idLugarHistorico,
                foto=pic,
                nombreLugarH=lugarHistorico.nombreLugarH,
                descripcionLugarH=lugarHistorico.descripcionLugarH,
                calle=lugarHistorico.calle,
                numero=lugarHistorico.numero,
                telefonoLugarH=lugarHistorico.telefonoLugarH,
                latitudLugarH=lugarHistorico.latitudLugarH,
                longitudLugarH=lugarHistorico.longitudLugarH,
                idCategoria=lugarHistorico.idCategoria,
            };
        }

        // GET: LugarHistoricoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            if (lugarHistorico == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", lugarHistorico.idCategoria);
            var view = this.ToView(lugarHistorico);




                
            return View(view);
        }

        private LugarHistoricoVista ToView (LugarHistorico lugarHistorico)
        {
            return new LugarHistoricoVista
            {
                idLugarHistorico = lugarHistorico.idLugarHistorico,
                foto = lugarHistorico.foto,
                nombreLugarH = lugarHistorico.nombreLugarH,
                descripcionLugarH = lugarHistorico.descripcionLugarH,
                calle = lugarHistorico.calle,
                numero = lugarHistorico.numero,
                telefonoLugarH = lugarHistorico.telefonoLugarH,
                latitudLugarH = lugarHistorico.latitudLugarH,
                longitudLugarH = lugarHistorico.longitudLugarH,
                idCategoria = lugarHistorico.idCategoria,


            };

        }



        // POST: LugarHistoricoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LugarHistoricoVista lugarHistorico )
        {
            if (ModelState.IsValid)
            {
                var pic = lugarHistorico.foto;
                var folder = "~/Content/imagenTurismo";

                if (lugarHistorico.fotoFilelugar != null)
                {
                    pic = FilesHelper.UploadPhoto(lugarHistorico.fotoFilelugar, folder);
                    pic = $"{folder}/{pic}";

                }

                var lugaresHistoricos = this.ToLugarHistorico(lugarHistorico, pic);
                db.Entry(lugaresHistoricos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombreCat", lugarHistorico.idCategoria);
            return View(lugarHistorico);
        }

        // GET: LugarHistoricoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            if (lugarHistorico == null)
            {
                return HttpNotFound();
            }
            return View(lugarHistorico);
        }

        // POST: LugarHistoricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            db.LugarHistoricoes.Remove(lugarHistorico);
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
