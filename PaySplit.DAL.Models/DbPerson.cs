using System;
using System.Collections.Generic;
using PaySplit.Abstractions.PaySources;

namespace PaySplit.DAL.Models
{
    public class DbPerson : Person, ILastModifiedTimestamp, ICreatedTimestamp
    {
        public DateTimeOffset Modified { get; set; }
        public DateTimeOffset Created { get; set; }

        public IEnumerable<PaymentElement> PaymentElements { get; set; }
    }
}
