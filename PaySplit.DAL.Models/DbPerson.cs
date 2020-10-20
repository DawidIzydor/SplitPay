using System;
using PaySplit.Abstractions.PaySources;

namespace PaySplit.DAL.Models
{
    public class DbPerson : Person, ILastModifiedTimestamp, ICreatedTimestamp
    {
        public DateTimeOffset LastModified { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
