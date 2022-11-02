using System;
using TicketManagementSystem.Exceptions;
using TicketManagementSystem.Helpers;
using TicketManagementSystem.Models;
using TicketManagementSystem.Services.Interfaces;

namespace TicketManagementSystem
{
    public class TicketService : ITicketService
    {
        public TicketService()
        {
        }

        public int CreateTicket(string incidentTitle, Priority priority, string assignedTo, string description, DateTime createdDate, bool isPayingCustomer, double price = 0, User accountManager = null)
        {
            if (string.IsNullOrWhiteSpace(incidentTitle) || string.IsNullOrWhiteSpace(description))
                throw new InvalidTicketException("Incident title or description were null");

            User user = null;
            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                using var userRepository = ClassFactory.UserRepository;
                user = userRepository.GetUser(assignedTo);
            }

            if (user == null)
                throw new UnknownUserException("User " + assignedTo + " not found");

            var priorityRaised = PriorityHelper.RaisePriority(incidentTitle, priority, createdDate);

            if (priorityRaised)
                priority = EscalateTicket(priority);

            if (priority == Priority.High)
                PriorityHelper.SendHighPriorityEmail(incidentTitle, assignedTo);

            if (isPayingCustomer)
            {
                // Only paid customers have an account manager.
                using var userRepository = ClassFactory.UserRepository;
                accountManager = userRepository.GetAccountManager();
                price = priority == Priority.High ? 100 : 50;
            }

            var ticket = new Ticket()
            {
                IncidentTitle = incidentTitle,
                AssignedUser = user,
                Priority = priority,
                Description = description,
                CreatedDate = createdDate,
                PriceDollars = price,
                AccountManager = accountManager
            };

            var ticketId = TicketRepository.CreateTicket(ticket);

            return ticketId;
        }

        public void AssignTicket(int ticketId, string username)
        {
            User user = null;
            if (!string.IsNullOrWhiteSpace(username))
            {
                using var userRepository = ClassFactory.UserRepository;
                user = userRepository.GetUser(username);
            }

            var ticket = TicketRepository.GetTicket(ticketId);

            if (ticket == null)
                throw new InvalidTicketException("No ticket found for id " + ticketId);

            ticket.AssignedUser = user ?? throw new UnknownUserException("User not found");

            TicketRepository.UpdateTicket(ticket);
        }

        public Priority EscalateTicket(Priority priority) =>
            priority switch
            {
                Priority.Low => Priority.Medium,
                Priority.Medium => Priority.High,
                _ => priority
            };
    }
}
