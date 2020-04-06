using GigHub.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse(string.Format("{0},{1}", Date, Time));
            }
        }
    }
}