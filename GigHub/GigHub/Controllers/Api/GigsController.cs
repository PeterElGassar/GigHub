using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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

            //Get Gig With All Related Attendances and Select From them Attendee
            var gigInDb = _context.Gigs
                .Include(g => g.Attendances.Select(x => x.Attendee))
                .SingleOrDefault(g => g.Id == id && g.ArtistId == currintUserId);

            if (gigInDb == null || gigInDb.IsCanceled)
                return NotFound();

            gigInDb.Cancel();

            _context.SaveChanges();

            return Ok();
        }



    }
}
