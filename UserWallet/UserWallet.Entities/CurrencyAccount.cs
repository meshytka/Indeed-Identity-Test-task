using System;
using System.Collections.Generic;
using System.Text;

namespace UserWallet.Entities
{
    public enum Currency
    {
        EUR,
        USD,
        JPY,
        BGN,
        CZK,
        DKK,
        GBP,
        HUF,
        PLN,
        RON,
        SEK,
        CHF,
        ISK,
        NOK,
        HRK,
        RUB,
        TRY,
        AUD,
        BRL,
        CAD,
        CNY,
        HKD,
        IDR,
        ILS,
        INR,
        KRW,
        MXN,
        MYR,
        NZD,
        PHP,
        SGD,
        THB,
        ZAR
    }

    public class CurrencyAccount
    {
        public Guid CurrencyAccountId { get; set; }
        public Currency CurrencyType { get; set; }
        public decimal Value { get; set; }

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
