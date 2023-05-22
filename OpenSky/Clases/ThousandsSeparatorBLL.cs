using OpenSky.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace OpenSky.Clases
{
    public class ThousandsSeparatorBLL
    { 
        public string ThousandsSeparator(double price, string currencyType)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            culture.NumberFormat.NumberGroupSeparator = ".";
            culture.NumberFormat.NumberDecimalSeparator = ",";
            var data =  price.ToString("N", culture);


            switch (currencyType)
            {
                case "PYG":
                    data = data.Replace(",00", "");
                    break; 
                default: 
                    break;
            }

            return data;
        }
    }
}