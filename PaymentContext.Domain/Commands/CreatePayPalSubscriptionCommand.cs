using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string StudentDocument { get; set; }
        public string StudentEmail { get; set; } 
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerName { get; set; }
        public string PayerEmail { get; set; }

        public void Validate()
        {
             AddNotifications(new Contract()
                .Requires()
                .HasMaxLen(FirstName, 20, "Name.FirsName", "O primeiro nome deve conter no máximo 20 caracteres")
                .HasMaxLen(LastName, 20, "Name.LastName", "O sobrenome deve conter no máximo 20 caracteres")
                .IsNotNullOrEmpty(StudentDocument, "CreatePayPalSubscription.StudentDocument", "O documento do estudante não pode ser nulo ou vazio")
                .IsNotNullOrEmpty(PayerDocument, "CreatePayPalSubscription.PayerDocument", "O documento informado para pagamento não pode ser nulo ou vazio")
                .IsEmail(StudentEmail, "CreatePayPalSubscription.StudentEmail", "O e-mail informado para o estudante é inválido")
                .IsNotNullOrEmpty(TransactionCode, "CreatePayPalSubscription.TransactionCode", "O nome para o cartão de crédito não pode ser nulo ou vazio")
            );
        }
    }
}