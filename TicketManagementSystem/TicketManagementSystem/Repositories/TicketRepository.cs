using TicketManagementSystem.Models;
using TicketManagementSystem.Repositories.Interfaces;

namespace TicketManagementSystem
{
    public static class TicketRepository
    {
        private static ITicketRepository GetInstance() => ClassFactory.TicketRepository;

        public static int CreateTicket(Ticket ticket)
        {
            using var repos = GetInstance();
            return repos.CreateTicket(ticket);
        }

        public static void UpdateTicket(Ticket ticket)
        {
            using var repos = GetInstance();
            repos.UpdateTicket(ticket);
        }

        public static Ticket GetTicket(int id)
        {
            using var repos = GetInstance();
            return repos.GetTicket(id);
        }
    }
}
