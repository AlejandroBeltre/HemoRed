using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblEula
    {
        public int eulaID { get; set; }
        public required string version { get; set; }
        public required DateOnly updateDate { get; set; }
        public required string content { get; set; }
    }
}