using System;
using UserWallet.Entities;

namespace UserWallet.BLL.Contracts
{
    public interface IWalletLogic
    {
        Wallet GetUserWallet(Guid id);
        bool TopUpWallet(Guid id, Currency currency, decimal value);
        bool WithdrawMoney(Guid id, Currency currency, decimal value);
        bool TransferMoneyToAnotherCurrency(Guid id, Currency fromCurrency, Currency toCurrency, decimal value);
    }
}
