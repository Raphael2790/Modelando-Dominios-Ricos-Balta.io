using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
     public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, string cardNumber, string lastTransactionNumber, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, Document document, string payer, Email email) :
        base(paidDate,expireDate,total,totalPaid,address,document,payer,email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
             AddNotifications(new Contract()
                .IsNotNullOrEmpty(CardHolderName, "PayPalPayment.CardHolderName", "O nome do dono do cartão não deve ser nulo ou vazio")
                .IsNotNullOrEmpty(CardNumber, "PayPalPayment.CardNumber", "O numero do cartão não deve ser nulo ou vazio")
            );
        }

        public string CardHolderName { get;private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}