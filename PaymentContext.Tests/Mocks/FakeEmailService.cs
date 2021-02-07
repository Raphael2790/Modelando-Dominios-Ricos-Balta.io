using PaymentContext.Shared.Services;

namespace PaymentContext.Tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            
        }
    }
}