using System;
using System.Collections.Generic;

namespace UserWallet.Entities
{
    public class Wallet
    {
        public Guid ClientId { get; set; }
        public List<CurrencyAccount> CurrencyAccounts { get; set; }
    }
}