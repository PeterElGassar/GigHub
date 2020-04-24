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
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _cotext;

        public AttendancesController()
        {
            _cotext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            string UserId = User.Identity.GetUserId();

            //Check If Current User has Attended  This Gig Before Or Not
            if (_cotext.Attendances.Any(a => a.GigId == dto.GigId && a.AttendeeId == UserId))
                return BadRequest("The Attendance already Exists.");

            var AttendUser = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = UserId
            };

            _cotext.Attendances.Add(AttendUser);
            _cotext.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            string UserId = User.Identity.GetUserId();

            var atteandForDelete = _cotext.Attendances
                .Where(a => a.AttendeeId == UserId && a.GigId == id)
                .SingleOrDefault();

            if (atteandForDelete == null)
            {
                return NotFound();
            }
            _cotext.Attendances.Remove(atteandForDelete);
            _cotext.SaveChanges();

            return Ok("Delete Attendance No. "+ id + " Successfully");
        }
    }
}
