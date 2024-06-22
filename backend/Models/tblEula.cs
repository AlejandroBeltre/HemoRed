using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblEula
    {
        public int EulaID { get; set; }
        public required string Version { get; set; }
        public required DateTime UpdateDate { get; set; }
        public required string Content { get; set; }
    }
}