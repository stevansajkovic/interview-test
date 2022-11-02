using System;

namespace TicketManagementSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string IncidentTitle { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        public User AssignedUser { get; set; }
        public User AccountManager { get; set; }
        public DateTime CreatedDate { get; set; }
        public double PriceDollars { get; set; }
    }
}
