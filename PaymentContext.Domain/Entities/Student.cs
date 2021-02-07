using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private readonly IList<Subscription> _subsciptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subsciptions = new List<Subscription>();
            AddNotifications(name, document, email);
        }

        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get{return _subsciptions.ToArray();} }

        public void AddSubscription(Subscription subscription) 
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subsciptions)
            {
                if(sub.Active)
                hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                //Certificando que o valor é false, caso seja acontece uma verdade logo não lança a notificação
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .AreNotEquals(0, subscription.Payments.Count, "Subscription.Payments", "Não há pagamentos vinculados a inscrição")
                .IsTrue(subscription.Active, "Subscription.Active", "Novas assinaturas não devem ser inativas")
            );

            _subsciptions.Add(subscription);
        }
        
    }
    
}