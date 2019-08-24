using System;

namespace TY.Services.Bank.Models.Transaction
{
    public class Transaction : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Account.Account Account{ get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
    }
}