using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {

            var upComingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs
                    .Where(
                    g => g.Genre.Name.Contains(query)
                    || g.Artist.Name.Contains(query)
                    || g.Location.Contains(query));
            }
            string currentUserId = User.Identity.GetUserId();
            // return hashTable it key = GigId
            var attendance = _context.Attendances
                .Where(a => a.AttendeeId == currentUserId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpComeingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "All Upcomeing Gigs",
                SearchTerm = query,
                Attendances = attendance
            };


            return View("GigsList", viewModel);
        }




    }
}