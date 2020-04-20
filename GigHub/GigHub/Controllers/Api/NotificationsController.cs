using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Apisite
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetNewNotifications()
        {
            string currentUserId = User.Identity.GetUserId();

            //changed to get all Notifications Not just new Notifications
            var notifications = _context.User_Notifications
                .Where(un => un.UserId == currentUserId)
                .Select(un => un.Notification)
                .OrderBy(n=> n.DateTime)
                .Include(n => n.Gig.Artist)
                .ToList();

            //get all new Notifications 
            var countNewNotifications = _context.User_Notifications
                .Where(un => un.UserId == currentUserId && un.IsRead == false)
                .Select(un => un.Notification).ToArray();

            int count = countNewNotifications.Length;
            


            return Json(new { count = count, notifications = notifications.Select(Mapper.Map<Notification, NotificationDto>) });
        }

        [HttpPost]
        public IHttpActionResult ReadNotifications()
        {

            string currentUserId = User.Identity.GetUserId();

            var notificationsInDb = _context.User_Notifications
                .Where(un => un.UserId == currentUserId && !un.IsRead)
                .ToList();

            foreach (var notifiy in notificationsInDb)
            {

                notifiy.Read();
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
