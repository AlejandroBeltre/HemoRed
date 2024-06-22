using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblUserEula
    {
        public int EulaID { get; set; }
        public required string UserDocument { get; set; }
        public bool AcceptedStatus { get; set; }

        public required tblEula Eula { get; set; }
        public required tblUser User { get; set; }  
    }
}