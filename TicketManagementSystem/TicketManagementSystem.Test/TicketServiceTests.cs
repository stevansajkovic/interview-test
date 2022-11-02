namespace TicketManagementSystem.Test
{
    public class TicketServiceTests
    {
        private readonly ITicketService _ticketService = ClassFactory.TicketService;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [AutoData]
        public void CreateTicket_TitleIsNull_ThrowsInvalidTicketException(Priority priority, string assignedTo, string description, DateTime createdDate, bool isPayingCustomer)
        {
            var incidentTitle = "";

            Assert.That(() => _ticketService.CreateTicket(incidentTitle, priority, assignedTo, description, createdDate, isPayingCustomer), Throws.TypeOf<InvalidTicketException>());
        }

        [Test]
        [AutoData]
        public void CreateTicket_UserIsNull_ThrowsUnknownUserException(string incidentTitle, Priority priority, string description, DateTime createdDate, bool isPayingCustomer)
        {
            var invalidAssignedTo = "UserNotInDatabase";

            Assert.That(() => _ticketService.CreateTicket(incidentTitle, priority, invalidAssignedTo, description, createdDate, isPayingCustomer), Throws.TypeOf<UnknownUserException>());
        }

        [Test]
        public void CreateTicket_ReturnTicketId()
        {
            var incidentTitle = "System Crash";
            var priority = Priority.Medium;
            var assignedTo = "Johan";
            var description = "Test description";
            var createdDate = DateTime.UtcNow;
            var isPayingCustomer = false;

            Assert.That(() => _ticketService.CreateTicket(incidentTitle, priority, assignedTo, description, createdDate, isPayingCustomer), Is.TypeOf<int>());
            //test will fail as user will not return from GetUser method as there is no database
            //could also test both paths for isPayingCustomer true or false
            //could also store result and test expected vs actual by asserting that they are equal
        }

        [Test]
        public void AssignTicket_TicketIsNull_ThrowsInvalidTicketException()
        {
            var username = "Johan";
            var invalidTicketId = 0;

            Assert.That(() => _ticketService.AssignTicket(invalidTicketId, username), Throws.TypeOf<InvalidTicketException>());
            //no database so will not find ticket regardless
        }

        [Test]
        public void AssignTicket_AssignedUserIsNull_ThrowsUnknownUserException()
        {
            var validTicketId = 1;
            var invalidUsername = "UserNotInDatabase";
            
            Assert.That(() => _ticketService.AssignTicket(validTicketId, invalidUsername), Throws.TypeOf<UnknownUserException>());
            //test will fail at first part due to no database implementation
        }

        [Test]
        public void EscalateTicket_LowPriorityInput_ReturnsMediumPriority()
        {
            var priority = Priority.Low;

            var expectedPriority = Priority.Medium;
            var actualPriority = _ticketService.EscalateTicket(priority);

            Assert.That(actualPriority, Is.EqualTo(expectedPriority));
        }

        [Test]
        public void EscalateTicket_MediumPriorityInput_ReturnsHighPriority()
        {
            var priority = Priority.Medium;

            var expectedPriority = Priority.High;
            var actualPriority = _ticketService.EscalateTicket(priority);

            Assert.That(actualPriority, Is.EqualTo(expectedPriority));
        }


        [Test]
        public void EscalateTicket_HighPriorityInput_ReturnsHighPriority()
        {
            var priority = Priority.High;

            var expectedPriority = Priority.High;
            var actualPriority = _ticketService.EscalateTicket(priority);

            Assert.That(actualPriority, Is.EqualTo(expectedPriority));
        }
    }
}