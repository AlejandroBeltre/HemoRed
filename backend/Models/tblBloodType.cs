using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    public class tblBloodType
    {
        public int bloodTypeID { get; set; }
        public BloodType bloodType { get; set; }
    }
}