using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<User_Notification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<User_Notification>();

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


        // This Function Add To current user argument "notification"
        // and add it to relational collection objects "UserNotifications"
        public void Notify(Notification notification)
        {
            var user_Notifiaction = new User_Notification(this, notification);

            //هنا لما تضيف ال 
            //new object to collection then in controller SaveChanges  EF will add New Record To Database
            UserNotifications.Add(user_Notifiaction);
        }


      

    }


}