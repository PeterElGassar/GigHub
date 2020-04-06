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
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gig = new Gig()
            {
                Location = model.Location,
                ArtistId = User.Identity.GetUserId(),
                GenreId = model.GenreId,

                DateTime = DateTime.Parse(String.Format("{0},{1}", model.Date, model.Time))
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("index", "Home");
        }


    }
}