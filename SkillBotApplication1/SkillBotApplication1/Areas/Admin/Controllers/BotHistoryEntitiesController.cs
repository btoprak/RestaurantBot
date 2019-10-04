using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkillBotApplication1.Areas.Admin.Attributes;
using SkillBotApplication1.EF;
using SkillBotApplication1.EF.Tables;

namespace SkillBotApplication1.Areas.Admin.Controllers
{

    [Admin]
    public class BotHistoryEntitiesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/BotHistoryEntities
        public ActionResult Index()
        {
            return View(db.BotHistories.ToList());
        }

        // GET: Admin/BotHistoryEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BotHistoryEntity botHistoryEntity = db.BotHistories.Find(id);
            if (botHistoryEntity == null)
            {
                return HttpNotFound();
            }
            return View(botHistoryEntity);
        }

        // GET: Admin/BotHistoryEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BotHistoryEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message,Source,UserId,CreateDate")] BotHistoryEntity botHistoryEntity)
        {
            if (ModelState.IsValid)
            {
                db.BotHistories.Add(botHistoryEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(botHistoryEntity);
        }

        // GET: Admin/BotHistoryEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BotHistoryEntity botHistoryEntity = db.BotHistories.Find(id);
            if (botHistoryEntity == null)
            {
                return HttpNotFound();
            }
            return View(botHistoryEntity);
        }

        // POST: Admin/BotHistoryEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message,Source,UserId,CreateDate")] BotHistoryEntity botHistoryEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(botHistoryEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(botHistoryEntity);
        }

        // GET: Admin/BotHistoryEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BotHistoryEntity botHistoryEntity = db.BotHistories.Find(id);
            if (botHistoryEntity == null)
            {
                return HttpNotFound();
            }
            return View(botHistoryEntity);
        }

        // POST: Admin/BotHistoryEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BotHistoryEntity botHistoryEntity = db.BotHistories.Find(id);
            db.BotHistories.Remove(botHistoryEntity);
            db.SaveChanges();
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
