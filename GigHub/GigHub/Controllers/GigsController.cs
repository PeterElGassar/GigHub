using GigHub.Models;
using GigHub.ViewModels;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;

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

        [Authorize]
        public ActionResult Attending()
        {
            string currentUserId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == currentUserId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .OrderBy(a => a.DateTime)
                .ToList();
            var viewModel = new GigsViewModel()
            {
                UpComeingGigs = gigs,
                ShowActions = false,
                Heading= "Gigs I'm Attending"
            };


            return View("GigsList", viewModel);
        }
    }
}