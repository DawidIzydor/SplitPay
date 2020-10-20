using System;

namespace PaySplit.DAL.Models
{
    public interface ILastModifiedTimestamp
    {
        DateTimeOffset Modified { get; set; }
    }
}