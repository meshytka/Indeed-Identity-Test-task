using System;
using System.Linq;
using UserWallet.BLL.Contracts;
using UserWallet.DAL.Api;
using UserWallet.DAL.Contracts;
using UserWallet.Entities;

namespace UserWallet.BLL.Logic
{
    public class WalletLogic : IWalletLogic
    {
        private IWalletDao _walletDao;
        private CurrencyDao _currencyDao;
        private Wallet _wallet;

        public WalletLogic(IWalletDao walletDao, CurrencyDao currencyDao)
        {
            _walletDao = walletDao;
            _currencyDao = currencyDao;
        }

        public Wallet GetUserWallet(Guid id)
        {
            return _walletDao.GetUserWallet(id);
        }

        public bool TopUpWallet(Guid id, Currency currency, decimal value)
        {
            _wallet = _walletDao.GetUserWallet(id);

            if (_wallet == null)
            {
                return false;
            }

            ChangeWalletBalance(currency, value);

            _walletDao.SaveUserWallet(_wallet);

            return true;
        }

        public bool WithdrawMoney(Guid id, Currency currency, decimal value)
        {
            _wallet = _walletDao.GetUserWallet(id);

            if (_wallet == null)
            {
                return false;
            }

            var negativeValue = value * -1;

            ChangeWalletBalance(currency, negativeValue);

            _walletDao.SaveUserWallet(_wallet);

            return true;
        }

        public bool TransferMoneyToAnotherCurrency(Guid id, Currency fromCurrency, Currency toCurrency, decimal value)
        {
            var wallet = _walletDao.GetUserWallet(id);

            if (wallet == null)
            {
                return false;
            }

            var rates = _currencyDao.GetRates();

            var rateFromCurrency = rates.FirstOrDefault(r => r.currencyType == fromCurrency).rate;
            var rateToCurrency = rates.FirstOrDefault(r => r.currencyType == toCurrency).rate;

            decimal koeff = rateFromCurrency / rateToCurrency;
            var addValue = value * koeff;

            if (!WithdrawMoney(id, fromCurrency, value) || !TopUpWallet(id, toCurrency, addValue))
            {
                return false;
            }

            return true;
        }

        private void ChangeWalletBalance(Currency currencyType, decimal value)
        {
            var currencyAccount = _wallet.CurrencyAccounts.FirstOrDefault(c => c.CurrencyType == currencyType);

            if (currencyAccount == null)
            {
                var newCurrency = new CurrencyAccount()
                {
                    CurrencyAccountId = Guid.NewGuid(),
                    CurrencyType = currencyType,
                    Value = value,
                    WalletId = _wallet.ClientId
                };

                _wallet.CurrencyAccounts.Add(newCurrency);
            }
            else
            {
                currencyAccount.Value += value;
            }
        }
    }
}