using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PaymentContext.Shared.Services;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    , IHandler<CreateCreditCardSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            this._repository = repository;
            this._emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if(command.Invalid)
            {   
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            if(_repository.DocumentExists(command.StudentDocument) || _repository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            if(_repository.EmailExists(command.StudentEmail) || _repository.EmailExists(command.PayerEmail))
                AddNotification($"Email", "O email informado já está em uso." );

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.StudentDocument, EDocumentType.CPF);
            var email = new Email(command.StudentEmail);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, EDocumentType.CPF);

            var student = new Student(document: document, name: name, email:email);
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate,
            command.ExpireDate, command.Total, command.TotalPaid, address, payerDocument, command.PayerName, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            AddNotifications(name, document, email, address, student, payment, subscription);

            if(Invalid){
                return new CommandResult(false, "Não foi possível cadastrar sua assinatura");
            }

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            _repository.CreateSubscrition(student);
            _emailService.SendEmail(student.Name.ToString(), student.Email.Address , "Seja bem-vindo a nossa plataforma de cursos");
               
            return new CommandResult(true, "Assinatura efetuada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            command.Validate();
            if(command.Invalid)
            {   
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            if(_repository.DocumentExists(command.StudentDocument) || _repository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            if(_repository.EmailExists(command.StudentEmail) || _repository.EmailExists(command.PayerEmail))
                AddNotification($"Email", "O email informado já está em uso." );

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.StudentDocument, EDocumentType.CPF);
            var email = new Email(command.StudentEmail);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, EDocumentType.CPF);

            var student = new Student(document: document, name: name, email:email);
            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber, command.PaidDate,
            command.ExpireDate, command.Total, command.TotalPaid, address, payerDocument, command.PayerName, email);
            var subscription = new Subscription(null);

            AddNotifications(name, document, email, address, student, payment, subscription);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            _repository.CreateSubscrition(student);
            _emailService.SendEmail(student.Name.ToString(), student.Email.Address , "Seja bem-vindo a nossa plataforma de cursos");
               
            return new CommandResult(true, "Assinatura efetuada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            command.Validate();
            if(command.Invalid)
            {   
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            if(_repository.DocumentExists(command.StudentDocument) || _repository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            if(_repository.EmailExists(command.StudentEmail) || _repository.EmailExists(command.PayerEmail))
                AddNotification($"Email", "O email informado já está em uso." );

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.StudentDocument, EDocumentType.CPF);
            var email = new Email(command.StudentEmail);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, EDocumentType.CPF);

            var student = new Student(document: document, name: name, email:email);
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate,
            command.ExpireDate, command.Total, command.TotalPaid, address, payerDocument, command.PayerName, email);
            var subscription = new Subscription(null);

            AddNotifications(name, document, email, address, student, payment, subscription);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            _repository.CreateSubscrition(student);
            _emailService.SendEmail(student.Name.ToString(), student.Email.Address , "Seja bem-vindo a nossa plataforma de cursos");
               
            return new CommandResult(true, "Assinatura efetuada com sucesso!");
        }
    }
}