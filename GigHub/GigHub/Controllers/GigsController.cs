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
            GigFormViewModel model = new GigFormViewModel()
            {
                Heading = "Add",
                Action = "Create",
                Genres = _context.Genres.ToList()
            };

            return View("GigForm", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("GigForm", model);
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
            return RedirectToAction("MyUpcomeingGigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            string currentUserId = User.Identity.GetUserId();

            var gig = _context.Gigs.SingleOrDefault(g => g.Id == id && g.ArtistId == currentUserId);
            if (gig == null)
            {
                return HttpNotFound();
            }

            GigFormViewModel viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Location = gig.Location,
                GenreId = gig.GenreId,
                Date = gig.DateTime.ToString("dd/MM/yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Heading = "Update",
                //Change it from here to Domian Model 
                Action = "Edit",
            };

            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("GigForm", model);
            }

            string currentUserId = User.Identity.GetUserId();

            //get gig in Database To Updated
            var gigInDb = _context.Gigs.Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == model.Id && g.ArtistId == currentUserId);

            if (gigInDb == null)
            {
                return HttpNotFound();
            }

            gigInDb.Update(model, gigInDb);

            _context.SaveChanges();

            return RedirectToAction("MyUpcomeingGigs");
        }

        public ActionResult Details(int id)
        {
            var gigInDb = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Single(g => g.Id == id);

            if (gigInDb == null)
                return HttpNotFound();

            string currentUserId = User.Identity.GetUserId();

            var viewModel = new GigDetailsViewModel()
            {
                Gig = gigInDb,
                ShowActions = User.Identity.IsAuthenticated,
                IsAttending = _context.Attendances.Any(a => a.GigId == gigInDb.Id && a.AttendeeId == currentUserId),
                Artist=gigInDb.Artist,
                IsFollowing = _context.Followings.Any(f => f.FolloweeId == gigInDb.ArtistId && f.FollowerId == currentUserId)
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult MyUpcomeingGigs()
        {
            string currentUserId = User.Identity.GetUserId();

            var Gigslist = _context.Gigs
                .Where(g =>
                g.ArtistId == currentUserId &&
                g.DateTime > DateTime.Now &&
                g.IsCanceled == false)
                .Include(g => g.Genre)
                .ToList();

            return View(Gigslist);
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

            var attendance = _context.Attendances
                .Where(a => a.AttendeeId == currentUserId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpComeingGigs = gigs,
                ShowActions = false,
                Heading = "Gigs I'm Attending",
                Attendances = attendance
            };


            return View("GigsList", viewModel);
        }
    }
}