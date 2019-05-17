using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Tesis.Comun.Modelo;
using Tesis.Dominio.Modelo;

namespace Tesis.API.Controllers
{
    public class LugarHistoricoesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/LugarHistoricoes
        public IQueryable<LugarHistorico> GetLugarHistoricoes()
        {
            return db.LugarHistoricoes;
        }

        // GET: api/LugarHistoricoes/5
        [ResponseType(typeof(LugarHistorico))]
        public async Task<IHttpActionResult> GetLugarHistorico(int id)
        {
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            if (lugarHistorico == null)
            {
                return NotFound();
            }

            return Ok(lugarHistorico);
        }

        // PUT: api/LugarHistoricoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLugarHistorico(int id, LugarHistorico lugarHistorico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lugarHistorico.idLugarHistorico)
            {
                return BadRequest();
            }

            db.Entry(lugarHistorico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LugarHistoricoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LugarHistoricoes
        [ResponseType(typeof(LugarHistorico))]
        public async Task<IHttpActionResult> PostLugarHistorico(LugarHistorico lugarHistorico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LugarHistoricoes.Add(lugarHistorico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lugarHistorico.idLugarHistorico }, lugarHistorico);
        }

        // DELETE: api/LugarHistoricoes/5
        [ResponseType(typeof(LugarHistorico))]
        public async Task<IHttpActionResult> DeleteLugarHistorico(int id)
        {
            LugarHistorico lugarHistorico = await db.LugarHistoricoes.FindAsync(id);
            if (lugarHistorico == null)
            {
                return NotFound();
            }

            db.LugarHistoricoes.Remove(lugarHistorico);
            await db.SaveChangesAsync();

            return Ok(lugarHistorico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LugarHistoricoExists(int id)
        {
            return db.LugarHistoricoes.Count(e => e.idLugarHistorico == id) > 0;
        }
    }
}