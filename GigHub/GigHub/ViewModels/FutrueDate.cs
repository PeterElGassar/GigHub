﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class FutrueDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // An object that supplies culture-specific formatting information about Date
            //CultureInfo enUS = new CultureInfo("en-US");
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                 "d M yyyy",
                 CultureInfo.CurrentCulture,
                 DateTimeStyles.None,
                 out dateTime);

            // If both Condition is true state is Valid
            return (isValid && dateTime > DateTime.Now);

        }
    }

}