using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, Document document, string payer, Email email) : 
        base (paidDate,expireDate,total,totalPaid,address,document,payer,email)
        {
            this.TransactionCode = transactionCode;
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(TransactionCode, "PayPalPayment.TransactionCode", "Código de transação não deve ser nulo ou vazio")
            );
        }

        public string TransactionCode { get; private set; }
    }
}