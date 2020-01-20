using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static iWriter.Models.AccountLevel;

namespace iWriter.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the iWriterUser class
    public class iWriterUser : IdentityUser
    {
        public string Name { get; set; }


        public AccountType AccountType { get; set; }
        public string Profession { get; set; } 
        public string Website { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLogin { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AccountBalance { get; set; }

        // project related properties
        public int NewProjectsCount { get; set; }
        public int PendingProjectsCount { get; set; }
        public int CompletedProjectsCount { get; set; }
        public string LatestProjectID { get; set; }

        // support related properties
        public string AccountManagerID { get; set; }
        public int TicketCount { get; set; }
        public int TicketReplyCount { get; set; }
    }
}
