using RatingCriteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RatingCriteria.Controllers
{
    public class CriterionsController : Controller
    {
        // GET: Criterions

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.criterias.ToList());
        }


        public ActionResult SingleCriteria(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criteria article = db.criterias.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = id.Value;

            var comments = db.criteriaCommentss.Where(d => d.ModuleId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.criteriaCommentss.Where(d => d.ModuleId.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(article);
        }


        
    }
}