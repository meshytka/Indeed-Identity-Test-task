using System;
using System.Linq;
using UserWallet.DAL.Contracts;
using UserWallet.Entities;

namespace UserWallet.DAL.BD
{
    public class WalletDao : IWalletDao
    {
        EntityDaoContext _entityDaoContext;

        public WalletDao(EntityDaoContext entityDaoContext)
        {
            _entityDaoContext = entityDaoContext;
        }

        public Wallet GetUserWallet(Guid id)
        {
            return _entityDaoContext.Wallets.FirstOrDefault(w => w.ClientId == id);
        }

        public void SaveUserWallet(Wallet wallet)
        {
            if (_entityDaoContext.Wallets.Any(w => w.ClientId == wallet.ClientId))
            {
                _entityDaoContext.Wallets.Update(wallet);
            }
            else
            {
                _entityDaoContext.Wallets.Add(wallet);
            }

            _entityDaoContext.SaveChanges();

        }
    }
}
