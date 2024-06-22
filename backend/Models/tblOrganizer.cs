using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class tblOrganizer
    {
        public int OrganizerID { get; set; }
        public int? AddressID { get; set; }
        public required string OrganizerName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        public tblAddress? Address { get; set; }
    }
}