using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblMunicipality
    {
        public int MunicipalityID { get; set; }
        public int ProvinceID { get; set; }
        public required string MunicipalityName { get; set; }

        public required tblProvince Province { get; set; }
    }
}