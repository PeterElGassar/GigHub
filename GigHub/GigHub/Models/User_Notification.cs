using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class User_Notification
    {

        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }


        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }


        public bool IsRead { get; private set; }



        //Default constructor to Entity Framwork 
        protected User_Notification()
        {

        }

        public User_Notification(ApplicationUser user, Notification notification)
        {

            //Related Objects not ForeignKey
            if (user == null)
                   throw new ArgumentNullException("user");

            //short cut to if condition if null throw Exception
            if (notification == null)
                throw new ArgumentNullException("notification");
            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}