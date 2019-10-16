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
using pryArquitecturaMobike;

namespace pryArquitecturaMobike.Controllers
{
    public class BicicletasController : ApiController
    {
        private MobikeEntities db = new MobikeEntities();

        // GET: api/Bicicletas
        public IQueryable<Bicicleta> GetBicicleta()
        {
            return db.Bicicleta;
        }

        // GET: api/Bicicletas/5
        [ResponseType(typeof(Bicicleta))]
        public IHttpActionResult GetBicicleta(int id)
        {
            Bicicleta bicicleta = db.Bicicleta.Find(id);
            if (bicicleta == null)
            {
                return NotFound();
            }

            return Ok(bicicleta);
        }

        // PUT: api/Bicicletas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBicicleta(int id, Bicicleta bicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bicicleta.id)
            {
                return BadRequest();
            }

            db.Entry(bicicleta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BicicletaExists(id))
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

        // POST: api/Bicicletas
        [ResponseType(typeof(Bicicleta))]
        public IHttpActionResult PostBicicleta(Bicicleta bicicleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bicicleta.Add(bicicleta);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BicicletaExists(bicicleta.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bicicleta.id }, bicicleta);
        }

        // DELETE: api/Bicicletas/5
        [ResponseType(typeof(Bicicleta))]
        public IHttpActionResult DeleteBicicleta(int id)
        {
            Bicicleta bicicleta = db.Bicicleta.Find(id);
            if (bicicleta == null)
            {
                return NotFound();
            }

            db.Bicicleta.Remove(bicicleta);
            db.SaveChanges();

            return Ok(bicicleta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BicicletaExists(int id)
        {
            return db.Bicicleta.Count(e => e.id == id) > 0;
        }
    }
}