using System;

namespace PaySplit.DAL.Models
{
    public class PaymentElement : ILastModifiedTimestamp, ICreatedTimestamp
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public DbPerson Person { get; set; }
        public decimal Amount { get; set; }
        public int PaymentId { get; set; }
        public DbPayment Payment { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}