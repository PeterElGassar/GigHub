using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {

        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OldDateTime { get; private set; }

        public string OldLocation { get; private set; }


        public int GigId { get; set; }

        [Required]
        [ForeignKey("GigId")]
        public Gig Gig { get; set; }


        //public ICollection<User_Notification> UserNotification { get; set; }
        //================


        protected Notification()
        {
        }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                  throw new ArgumentNullException("gig");

            Type = type;
            DateTime = DateTime.Now;
            Gig = gig;
        }

        public static Notification GigCreate(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime oldDateTime, string oldLocation)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OldDateTime = oldDateTime;
            notification.OldLocation = oldLocation;

            return notification;

        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }


    }
}