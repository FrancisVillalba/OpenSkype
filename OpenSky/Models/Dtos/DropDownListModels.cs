using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class DropDownListModels
    {
        public int Values { get; set; }
        public string Opciones { get; set; }
    }

    public class SelectListModels
    {
        public Guid id { get; set; }
        public string name { get; set; }
    }
}