using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketManagementSystem.Helpers
{
    public class PriorityHelper
    {
        private static readonly List<string> _keyWords = new() { "crash", "important", "failure" };

        public static bool RaisePriority(string incidentTitle, Priority priority, DateTime createdDate)
        {
            if (string.IsNullOrWhiteSpace(incidentTitle))
                throw new ArgumentNullException(nameof(incidentTitle), "Incident title is null");

            var priorityRaised = false;

            if (createdDate < DateTime.UtcNow.AddHours(-1))
            {
                if (priority != Priority.High)
                {
                    priorityRaised = true;
                }
            }

            if ((!priorityRaised || priority != Priority.High) && _keyWords.Any(kw => incidentTitle.ToLower().Contains(kw)))
            {
                priorityRaised = true;
            }

            return priorityRaised;
        }

        public static void SendHighPriorityEmail(string incidentTitle, string assignedTo)
        {
            if (string.IsNullOrWhiteSpace(incidentTitle) || string.IsNullOrEmpty(assignedTo))
                throw new ArgumentNullException("Incident title or assigned to were null");

            ClassFactory.EmailService.SendEmailToAdministrator(incidentTitle, assignedTo);
        }
    }
}
