using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public Guid ProfileId { get; set; }
        public Guid ChargesId { get; set; }
        public Guid BranchCompanyId { get; set; }
        public int BranchCompanyNumber { get; set; }
    }
}