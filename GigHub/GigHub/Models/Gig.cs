using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        public string ArtistId { get; set; }

        public byte GenreId { get; set; }


        [Required]
        public Genre Genre { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; }

    }
}