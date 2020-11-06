using System;
using UserWallet.Entities;

namespace UserWallet.DAL.Contracts
{
    public interface IWalletDao
    {
        public Wallet GetUserWallet(Guid id);
        public void SaveUserWallet(Wallet wallet);
    }
}
