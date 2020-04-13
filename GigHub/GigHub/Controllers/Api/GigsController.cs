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
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpDelete]
        public IHttpActionResult GigCancel(int id)
        {
            string currintUserId = User.Identity.GetUserId();

            var gigInDb = _context.Gigs.SingleOrDefault(g => g.Id == id && g.ArtistId == currintUserId);

            if (gigInDb == null || gigInDb.IsCanceled)
                return NotFound();


            gigInDb.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
