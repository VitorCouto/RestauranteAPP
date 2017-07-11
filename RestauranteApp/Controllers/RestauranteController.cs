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
    public class RestauranteController : ApiController
    {
        private RestauranteModel db = new RestauranteModel();

        //GET: api/restaurante
        public IQueryable<Restaurante> GetRestaurante()
        {
            return db.Restaurante;
        }

        [HttpGet]
        [Route("api/restaurante/name/{name}")]
        // GET: api/restaurante
        public IQueryable<Restaurante> GetRestauranteByName(string name)
        {
            return db.Restaurante.Where(x => x.nameRestaurante.Contains(name));
        }

        // GET: api/restaurante/5
        [ResponseType(typeof(Restaurante))]
        public IHttpActionResult GetRestaurante(int id)
        {
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return Ok(restaurante);
        }

        // PUT: api/restaurante/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestaurante(int id, Restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurante.restauranteId)
            {
                return BadRequest();
            }

            db.Entry(restaurante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestauranteExists(id))
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

        // POST: api/restaurante
        [ResponseType(typeof(Restaurante))]
        public IHttpActionResult PostRestaurante(Restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurante.Add(restaurante);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = restaurante.restauranteId }, restaurante);
        }

        // DELETE: api/restaurante/5
        [ResponseType(typeof(Restaurante))]
        public IHttpActionResult DeleteRestaurante(int id)
        {
            Restaurante restaurante = db.Restaurante.Find(id);
            if (restaurante == null)
            {
                return NotFound();
            }

            db.Restaurante.Remove(restaurante);
            db.SaveChanges();

            return Ok(restaurante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestauranteExists(int id)
        {
            return db.Restaurante.Count(e => e.restauranteId == id) > 0;
        }
    }
}
