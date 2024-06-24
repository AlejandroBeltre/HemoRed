using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblMunicipality
    {
        public int municipalityID { get; set; }
        public int provinceID { get; set; }
        public required string municipalityName { get; set; }

        public required tblProvince tblProvince { get; set; }
    }
}