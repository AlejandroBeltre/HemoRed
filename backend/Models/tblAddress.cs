using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblAddress
    {
        public int AddressID { get; set; }
        public int MunicipalityID { get; set; }
        public int ProvinceID { get; set; }
        public required string Street { get; set; }
        public required int BuildingNumber { get; set; }

        public required tblMunicipality Municipality { get; set; }
        public required tblProvince Province { get; set; }
    }
}