using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class CategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } 
    }

    public class CategoriesListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}