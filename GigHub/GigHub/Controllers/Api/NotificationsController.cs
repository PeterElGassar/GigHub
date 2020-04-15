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

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string currentUserId = User.Identity.GetUserId();

            var notifications = _context.User_Notifications
                .Where(un => un.UserId == currentUserId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();



            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
