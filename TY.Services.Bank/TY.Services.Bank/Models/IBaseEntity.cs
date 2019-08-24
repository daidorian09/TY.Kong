using System;

namespace TY.Services.Bank.Models
{
    public interface IBaseEntity
    {
        string Id { get; }

        DateTime? CreatedAt { get; }

        DateTime? UpdatedAt { get; }
    }
}