using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Following
    {
        [Key]
        public int Id { get; set; }



        [Required]
        public string FolloweeId { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public ApplicationUser Followee { get; set; }

        
        public ApplicationUser Follower { get; set; }


    }
}