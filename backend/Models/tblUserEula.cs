using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblUserEula
    {
        public int eulaID { get; set; }
        public required string userDocument { get; set; }
        public bool acceptedStatus { get; set; }

        public required tblEula tblEula { get; set; }
        public required tblUser tblUser { get; set; }  
    }
}