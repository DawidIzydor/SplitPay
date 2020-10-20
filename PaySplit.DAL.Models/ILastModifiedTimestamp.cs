using System;

namespace PaySplit.DAL.Models
{
    public interface ILastModifiedTimestamp
    {
        DateTimeOffset LastModified { get; set; }
    }
}