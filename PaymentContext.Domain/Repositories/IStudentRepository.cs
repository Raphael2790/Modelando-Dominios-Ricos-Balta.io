using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        void CreateStudent();
        void SaveStudent();
        void CreateSubscrition(Student student);
        bool EmailExists(string email);
        bool DocumentExists(string document);
    }
}