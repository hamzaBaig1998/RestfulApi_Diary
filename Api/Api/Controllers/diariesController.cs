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
using Api.Models;

namespace Api.Controllers
{
    public class diariesController : ApiController
    {
        private diaryEntities db = new diaryEntities();

        // GET: api/diaries
        public IQueryable<diary> Getdiaries()
        {
            return db.diaries;
        }

        // GET: api/diaries/5
        [ResponseType(typeof(diary))]
        public IHttpActionResult Getdiary(int id)
        {
            diary diary = db.diaries.Find(id);
            if (diary == null)
            {
                return NotFound();
            }

            return Ok(diary);
        }

        // PUT: api/diaries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdiary(int id, diary diary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diary.Id)
            {
                return BadRequest();
            }

            db.Entry(diary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!diaryExists(id))
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

        // POST: api/diaries
        [ResponseType(typeof(diary))]
        public IHttpActionResult Postdiary(diary diary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.diaries.Add(diary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = diary.Id }, diary);
        }

        // DELETE: api/diaries/5
        [ResponseType(typeof(diary))]
        public IHttpActionResult Deletediary(int id)
        {
            diary diary = db.diaries.Find(id);
            if (diary == null)
            {
                return NotFound();
            }

            db.diaries.Remove(diary);
            db.SaveChanges();

            return Ok(diary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool diaryExists(int id)
        {
            return db.diaries.Count(e => e.Id == id) > 0;
        }
    }
}