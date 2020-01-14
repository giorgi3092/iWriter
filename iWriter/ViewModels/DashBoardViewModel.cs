using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static iWriter.Models.AccountLevel;

namespace iWriter.ViewModels
{
    public class DashBoardViewModel
    {
        public string Name { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLogin { get; set; }
        public decimal AccountBalance { get; set; }
        public int NewProjectsCount { get; set; }
        public int PendingProjectsCount { get; set; }
        public int CompletedProjectsCount { get; set; }
        public string LatestProjectID { get; set; }
        public string AccountManagerName { get; set; }
        public int TicketCount { get; set; }
        public int TicketReplyCount { get; set; }
    }
}
