using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class FolloweeViewModel
    {
        public IEnumerable<ApplicationUser> ArtistIamFollowing { get; set; }

    }
}