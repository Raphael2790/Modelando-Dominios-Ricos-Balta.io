using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Queries
{
    public static class SubscriptionQuery
    {
        public static Expression<Func<Subscription, bool>> GetStudentSubscriptionActive()
        {
            return x => x.Active;
        }
    }
}