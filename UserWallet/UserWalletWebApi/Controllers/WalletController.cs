using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserWallet.BLL.Contracts;
using UserWallet.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserWalletWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        IWalletLogic _walletLogic;

        public WalletController(IWalletLogic walletLogic)
        {
            _walletLogic = walletLogic;
        }

        // GET api/<WalletController>/5
        [HttpGet]
        public Wallet Get(Guid id)
        {
            return _walletLogic.GetUserWallet(id);
        }

        // PUT api/<WalletController>/5
        [HttpPut("TopUpWallet")]
        public bool TopUpWallet(Guid id, Currency currencyType, decimal value)
        {
            return _walletLogic.TopUpWallet(id, currencyType, value);
        }

        // PUT api/<WalletController>/5
        [HttpPut("WithdrawMoney")]
        public bool WithdrawMoney(Guid id, Currency currencyType, decimal value)
        {
            return _walletLogic.WithdrawMoney(id, currencyType, value);
        }

        // PUT api/<WalletController>/5
        [HttpPut("WithdrawMoney")]
        public bool TransferMoneyToAnotherCurrency(Guid id, Currency fromCurrency, Currency toCurrency, decimal value)
        {
            return _walletLogic.TransferMoneyToAnotherCurrency(id, fromCurrency, toCurrency, value);
        }
    }
}
