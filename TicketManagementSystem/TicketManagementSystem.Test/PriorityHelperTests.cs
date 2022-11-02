namespace TicketManagementSystem.Test
{
    public class PriorityHelperTests
    {
        [Test]
        [AutoData]
        public void RaisePriority_TitleIsNullOrWhiteSpace_ThrowsArgumentNullException(Priority priority, DateTime createdDate)
        {
            var incidentTitle = "";

            Assert.That(() => PriorityHelper.RaisePriority(incidentTitle, priority, createdDate), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [AutoData]
        public void RaisePriority_ContainsKeywordsAndNotHighPriority_ReturnsTrue(DateTime createdDate)
        {
            var incidentTitle = "System Crash";
            var priority = Priority.Medium;

            var expectedResult = true;
            var actualResult = PriorityHelper.RaisePriority(incidentTitle, priority, createdDate);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [AutoData]
        public void RaisePriority_CreatedDateMinusOneHour_ReturnsTrue(string incidentTitle)
        {
            var priority = Priority.Low;
            var createdDate = DateTime.UtcNow.AddHours(-2);

            var expectedResult = true;
            var actualResult = PriorityHelper.RaisePriority(incidentTitle, priority, createdDate);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [AutoData]
        public void RaisePriority_HighPriorityInput_ReturnsFalse(string incidentTitle, DateTime createdDate)
        {
            var priority = Priority.High;

            var expectedResult = false;
            var actualResult = PriorityHelper.RaisePriority(incidentTitle, priority, createdDate);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [AutoData]
        public void SendHighPriorityEmail_IncidentTitleIsNullOrWhiteSpace_ThrowsArgumentNullException(string assignedTo)
        {
            var incidentTitle = "";

            Assert.That(() => PriorityHelper.SendHighPriorityEmail(incidentTitle, assignedTo), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [AutoData]
        public void SendHighPriorityEmail_AssignedToIsNullOrWhiteSpace_ThrowsArgumentNullException(string incidentTitle)
        {
            var assignedTo = "";

            Assert.That(() => PriorityHelper.SendHighPriorityEmail(incidentTitle, assignedTo), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
