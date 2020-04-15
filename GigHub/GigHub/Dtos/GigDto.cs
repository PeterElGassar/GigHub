using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }    
        public string Location { get; set; }
        public bool IsCanceled { get;  set; }

        public GenreDto Genre { get; set; }

        public UserDto Artist { get; set; }

    }
}