using System;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories.Interfaces
{
    public interface ITicketRepository : IDisposable
    {
        int CreateTicket(Ticket ticket);
        Ticket GetTicket(int id);
        void UpdateTicket(Ticket ticket);
    }
}
