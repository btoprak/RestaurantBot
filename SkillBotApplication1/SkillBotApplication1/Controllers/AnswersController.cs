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
using SkillBotApplication1.EF;
using SkillBotApplication1.EF.Tables;


namespace DiyetisyenimBot.Controllers
{
    public class AnswersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Answers
        public IQueryable<Booking> GetAnswers()
        {
            return db.Bookings;
        }

        // GET: api/Answers/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetAnswers(int id)
        {
            Booking bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        // PUT: api/Answers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnswers(int id, Booking bookings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookings.ID)
            {
                return BadRequest();
            }

            db.Entry(bookings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswersExists(id))
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

        // POST: api/Answers
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostAnswers(Booking bookings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bookings.Add(bookings);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bookings.ID }, bookings);
        }

        // DELETE: api/Answers/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(bookings);
            db.SaveChanges();

            return Ok(bookings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswersExists(int id)
        {
            return db.Bookings.Count(e => e.ID == id) > 0;
        }
    }
}