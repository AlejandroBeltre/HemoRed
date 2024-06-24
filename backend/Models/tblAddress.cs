using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblAddress
    {
        public int addressID { get; set; }
        public int municipalityID { get; set; }
        public int provinceID { get; set; }
        public required string street { get; set; }
        public required int buildingNumber { get; set; }

        public required tblMunicipality tblMunicipality { get; set; }
        public required tblProvince tblProvince { get; set; }
    }
}