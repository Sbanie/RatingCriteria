using RatingCriteria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace RatingCriteria.Controllers
{
    public class criteriaCommentsController : Controller
    {
        // GET: criteriaComments
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArticlesComments

        public ActionResult Index()
        {
            return View(db.criteriaCommentss.ToList());
        }

        // GET: ArticlesComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criteriaComments articlesComment = db.criteriaCommentss.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
           var modId = int.Parse(form["ModuleId"]);
            var rating = int.Parse(form["Rating"]);

            criteriaComments artComment = new criteriaComments()
            {
                ModuleId = modId,
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now

            };

            db.criteriaCommentss.Add(artComment);
            db.SaveChanges();

            return RedirectToAction("Details", "Assessment", new { id = modId });
        }

        // POST: ArticlesComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] criteriaComments criteriaComments)
        {
            if (ModelState.IsValid)
            {
                db.criteriaCommentss.Add(criteriaComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(criteriaComments);
        }

        // GET: ArticlesComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criteriaComments articlesComment = db.criteriaCommentss.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] criteriaComments criteriaComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criteriaComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(criteriaComments);
        }

        // GET: ArticlesComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criteriaComments articlesComment = db.criteriaCommentss.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            criteriaComments articlesComment = db.criteriaCommentss.Find(id);
            db.criteriaCommentss.Remove(articlesComment);
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


        public ActionResult ConvertToPDF()
        {
            var printpdf = new ActionAsPdf("Index");
            return printpdf;
        }

    }
}