using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GigHub.ViewModels;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public bool IsCanceled { get; private set; }

        public Genre Genre { get; set; }

        public ApplicationUser Artist { get; set; }

        //Benefit Of This Collection to Load All Related Objects
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {

            this.IsCanceled = true;

            //============= Cancel gig Notification =====
            //1 Initial notification
            Notification notification = new Notification(NotificationType.GigCanceled, this);


            //3 iterate through attendeesUser
            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                //invok Notify Function
                attendee.Notify(notification);
            }

        }


        public void Update(GigFormViewModel model)
        {

            //1.Get All Attendanee fro this gig 
            //var attendees = _context.Attendances.Where(a => a.GigId == gigInDb.Id).Select(a => a.Attendee).ToList();

            //2.Intial Notification for Update Case
            var notification = new Notification(NotificationType.GigUpdated, this);
            notification.OldDateTime = this.DateTime;
            notification.OldLocation = this.Location;


            //3.Send To All Attendee
            //"Attendances.Select(a => a.Attendee)" instead of Query statement  that get all Attendances stup no. '1'
            //Use Here Relationla collection object "Attendances" and Insert new object to every attendee
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                //invok Notify Function
                attendee.Notify(notification);
            }

            this.DateTime = model.GetDateTime();
            this.Location = model.Location;
            this.GenreId = model.GenreId;

        }
    }
}