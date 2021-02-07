using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;  
            LastUpdate = DateTime.Now;
            Active = true;
            ExpireDate = expireDate;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get {return _payments.ToArray();}}

        public void InactivateSub()
        {
            Active = false;
            LastUpdate = DateTime.Now;
        }

        public void ActivateSub()
        {
            Active = true;
            LastUpdate = DateTime.Now;
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payment.PaidDate", "A data do pagamento deve ser futura!")
                );
            
            if(Valid)
            _payments.Add(payment);
        }

    }
    
}