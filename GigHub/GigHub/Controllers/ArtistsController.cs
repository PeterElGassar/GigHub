using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ArtistsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Artists
        public ActionResult Following()
        { 
            string currentUserId = User.Identity.GetUserId();

            var Artists = _context.Followings
                .Where(f => f.FollowerId == currentUserId)
                .Select(f => f.Followee)
                .Include(f => f.Followees).ToList();

            var viewModel = new FolloweeViewModel() { ArtistIamFollowing = Artists };
            return View(viewModel);
        }
    }
}