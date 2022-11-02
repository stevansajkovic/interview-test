using EmailService;
using TicketManagementSystem.Repositories;
using TicketManagementSystem.Repositories.Interfaces;
using TicketManagementSystem.Services.Interfaces;

namespace TicketManagementSystem
{
    public static class ClassFactory
    {
        public static ITicketRepository TicketRepository => new TicketRepositoryImp();
        public static IUserRepository UserRepository => new UserRepository();
        public static IEmailService EmailService => new EmailServiceProxy();
        public static ITicketService TicketService => new TicketService();
    }
}
