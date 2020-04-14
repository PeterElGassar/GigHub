using System;
using System.Collections.Generic;
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

        public DateTime? OldDateTime { get; set; }

        public string OldLocation { get; set; }


        public int GigId { get; set; }

        [Required]
        [ForeignKey("GigId")]
        public Gig Gig { get; set; }

        public Notification()
        {

        }

        public Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;

            //if (type == NotificationType.GigUpdated)
            //{
            //    OldDateTime = gig.DateTime;
            //    OldLocation = gig.Location;
            //}
        }

    }
}