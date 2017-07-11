using RestauranteApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestauranteApp.Controllers
{
    public class PratoController : ApiController
    {
        private RestauranteModel db = new RestauranteModel();

        //GET: api/pratos
        public IQueryable<Pratos> GetPrato()
        {
            return db.Pratos;
        }

        [HttpGet]
        [Route("api/pratos/name/{name}")]
        // GET: api/pratos
        public IQueryable<Pratos> GetPratosByName(string name)
        {
            return db.Pratos.Where(x => x.namePrato.Contains(name));
        }

        // GET: api/Pratos/5
        [ResponseType(typeof(Pratos))]
        public IHttpActionResult GetPratos(int id)
        {
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return NotFound();
            }

            return Ok(pratos);
        }

        // PUT: api/Pratos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPratos(int id, Pratos pratos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pratos.pratoId)
            {
                return BadRequest();
            }

            db.Entry(pratos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PratosExists(id))
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

        // POST: api/pratos
        [ResponseType(typeof(Pratos))]
        public IHttpActionResult PostPratos(Pratos pratos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //busca o restaurante 
            pratos.restaurante = db.Restaurante.Find(pratos.restauranteId);

            if (pratos.restaurante == null)
            {
                return BadRequest(ModelState);
            }

            db.Pratos.Add(pratos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pratos.pratoId }, pratos);
        }

        // DELETE: api/pratos/5
        [ResponseType(typeof(Pratos))]
        public IHttpActionResult DeletePratos(int id)
        {
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return NotFound();
            }

            db.Pratos.Remove(pratos);
            db.SaveChanges();

            return Ok(pratos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PratosExists(int id)
        {
            return db.Pratos.Count(e => e.pratoId == id) > 0;
        }
    }
}
