using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class FollowersController : ApiController
    {

        private readonly ApplicationDbContext _context;

        public FollowersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {

            string CurrintUserId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeId == dto.FolloweeId && f.FollowerId == CurrintUserId))
            {
                return BadRequest();
            }
            var Follow = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = CurrintUserId
            };


            _context.Followings.Add(Follow);
            _context.SaveChanges();

            return Json(new { ArtistId = dto.FolloweeId });
        }

    }
}
