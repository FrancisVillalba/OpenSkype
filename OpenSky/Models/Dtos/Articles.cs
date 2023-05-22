using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class Articles
    {
        public Guid Id { get; set; }
        public string ReferenceInternal { get; set; }
        public string Name { get; set; }
        public int? InitialAmount { get; set; }
        public Guid Iva_Id { get; set; }
        public string Iva_name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double? Price { get; set; }
        public string Categorie { get; set; }
        public Guid Categorie_id { get; set; }
        public int? MinimumAmount { get; set; }
        public bool Status { get; set; }
    }

    public class ArticlesDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }

    public class ArticlesResponseDto
    {
        public Guid Id { get; set; }
        public string ReferenceInternal { get; set; }
        public string Name { get; set; }
        public int? InitialAmount { get; set; }
        public Guid Iva { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double? Price { get; set; }
        public Guid categorie_id { get; set; }
        public int? MinimumAmount { get; set; }
    }

    public class ArticlesListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ArticlesPriceResponseDto
    {
        public string Price { get; set; }
    }
}