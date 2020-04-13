using GigHub.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }


        [Required]
        public string Location { get; set; }
        [Required]
        [FutrueDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }


        public string Heading { get; set; }

        public string Action { get; set; }


        public DateTime GetDateTime()
        {
            //first convert string date to DateTime
            DateTime endDate = DateTime.ParseExact(Convert.ToString(Date), "dd/MM/yyyy",CultureInfo.CurrentCulture);
            TimeSpan endTime = TimeSpan.Parse(Time);
            //Second Add To It time 
            DateTime combine = endDate + endTime;

            return combine;

            //return DateTime.Parse(string.Format("{0},{1}", Date, Time));

        }
    }
}