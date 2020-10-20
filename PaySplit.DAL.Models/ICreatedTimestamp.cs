using System;

namespace PaySplit.DAL.Models
{
    public interface ICreatedTimestamp
    {
        DateTimeOffset Created { get; set; }
    }
}