﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {

        public Gig Gig { get; set; }
        public ApplicationUser Artist { get; set; }
        public bool ShowActions { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsAttending { get; set; }

    }
}