using GigHub.Models;
using GigHub.ViewModels;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }



        [Authorize]
        public ActionResult Create()
        {
            var GenresList = _context.Genres.ToList();
            GigFormViewModel model = new GigFormViewModel();
            model.Genres = GenresList;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View(model);
            }

            var gig = new Gig()
            {
                Location = model.Location,
                ArtistId = User.Identity.GetUserId(),
                GenreId = model.GenreId,
                DateTime = model.GetDateTime()
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("index", "Home");
        }


    }
}