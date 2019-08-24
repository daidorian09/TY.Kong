using System;

namespace TY.Services.Bank.Models.Request
{
    public interface IBaseRequest
    {
        string Id { get; }

        DateTime? CreatedAt { get; }

        DateTime? UpdatedAt { get; }
    }
}