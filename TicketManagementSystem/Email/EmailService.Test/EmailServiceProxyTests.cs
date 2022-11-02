namespace EmailService.Test
{
    [TestFixture]
    public class EmailServiceProxyTests
    {
        private readonly IEmailService _emailService = ClassFactory.EmailService;

        [Test]
        public void SendEmailToAdministrator_IncidentTitleIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => _emailService.SendEmailToAdministrator(null, null), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
