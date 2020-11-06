using Microsoft.AspNetCore.Mvc;
using System;
using UserWallet.BLL.Contracts;
using UserWallet.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserWalletWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        private IWalletLogic _walletLogic;

        public WalletController(IWalletLogic walletLogic)
        {
            _walletLogic = walletLogic;
        }

        // GET api/<WalletController>/5
        [HttpGet]
        public JsonResult Get(Guid id)
        {
            var userWallet = _walletLogic.GetUserWallet(id);

            if (userWallet == null)
            {
                return ErrorResponse("No such user!");
            }

            return MessageResult(userWallet);
        }

        // PUT api/<WalletController>/5
        [HttpPut("TopUpWallet")]
        public JsonResult TopUpWallet(Guid id, Currency currencyType, decimal value)
        {
            var result = _walletLogic.TopUpWallet(id, currencyType, value);

            if (!result)
            {
                return ErrorResponse("Error while replenishing the wallet");
            }

            return MessageResult("Wallet has been successfully replenished");
        }

        // PUT api/<WalletController>/5
        [HttpPut("WithdrawMoney")]
        public JsonResult WithdrawMoney(Guid id, Currency currencyType, decimal value)
        {
            var result = _walletLogic.WithdrawMoney(id, currencyType, value);

            if (!result)
            {
                return ErrorResponse("Error when withdrawing money from the wallet!");
            }

            return MessageResult("Money has been successfully withdrawn from the wallet");
        }

        // PUT api/<WalletController>/5
        [HttpPut("WithdrawMoney")]
        public JsonResult TransferMoneyToAnotherCurrency(Guid id, Currency fromCurrency, Currency toCurrency, decimal value)
        {
            var result = _walletLogic.TransferMoneyToAnotherCurrency(id, fromCurrency, toCurrency, value);

            if (!result)
            {
                return ErrorResponse("Something went wrong");
            }

            return MessageResult("Money was successfully transferred to another currency");
        }
    }
}