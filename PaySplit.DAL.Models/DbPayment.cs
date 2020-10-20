using System;
using System.Collections.Generic;
using PaySplit.Abstractions.Payment;

namespace PaySplit.DAL.Models
{
    public class DbPayment : IPayment, ILastModifiedTimestamp, ICreatedTimestamp
    {
        
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public IEnumerable<PaymentElement> PaymentElements { get; set; }

        public string Generator { get; set; }

        public DateTimeOffset Modified { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}