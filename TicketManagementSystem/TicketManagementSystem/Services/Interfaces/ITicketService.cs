using System;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Services.Interfaces
{
    public interface ITicketService
    {
        int CreateTicket(string incidentTitle, Priority priority, string assignedTo, string description, DateTime createdDate, bool isPayingCustomer, double price = 0, User accountManager = null);
        void AssignTicket(int id, string username);
        Priority EscalateTicket(Priority priority);
    }
}
