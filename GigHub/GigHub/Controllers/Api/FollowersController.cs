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
                return BadRequest("Following alredy Exist..");


            var Follow = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = CurrintUserId
            };


            _context.Followings.Add(Follow);
            _context.SaveChanges();

            return Json(new { ArtistId = dto.FolloweeId });
        }



        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            string currentUserId = User.Identity.GetUserId();

            var FollowForDelete = _context.Followings
                .Where(f => f.Followee.Id == id && f.Follower.Id == currentUserId)
                .SingleOrDefault();

            if (FollowForDelete == null)
                return NotFound();


            _context.Followings.Remove(FollowForDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
