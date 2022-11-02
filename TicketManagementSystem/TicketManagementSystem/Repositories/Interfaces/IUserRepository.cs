using System;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        User GetUser(string username);
        User GetAccountManager();
    }
}
