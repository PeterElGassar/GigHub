using GigHub.Models;
using System.Collections;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }


    }
}