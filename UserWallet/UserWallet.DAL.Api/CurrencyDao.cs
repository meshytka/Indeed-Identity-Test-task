using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UserWallet.Entities;

namespace UserWallet.DAL.Api
{
    public class CurrencyDao
    {
        string _path;

        public CurrencyDao(string path)
        {
            _path = path;
        }

        public List<(Currency currencyType, decimal rate)> GetRates()
        {
            var loadRate = LoadRates();

            if (!loadRate.Any())
            {
                return null;
            }

            var result = GetRatesFromXmlData(loadRate);

            return result;
        }

        private List<(string currency, decimal rate)> LoadRates()
        {
            var result = new List<(string currency, decimal rate)>();

            XmlDocument xml = new XmlDocument();

            xml.Load(_path);

            XmlNodeList nodes = xml.SelectNodes("//*[@currency]");

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var currency = node.Attributes["currency"].Value;
                    var rate = Decimal.Parse(node.Attributes["rate"].Value, NumberStyles.Any, new CultureInfo("en-Us"));

                    result.Add((currency, rate));
                }
            }

            return result;
        }

        private List<(Currency currencyType, decimal rate)> GetRatesFromXmlData(List<(string currency, decimal rate)> data)
        {
            var result = new List<(Currency currencyType, decimal rate)>();

            result.Add((Currency.EUR, 1));

            foreach (var item in data)
            {
                result.Add((GetCurrencyFromString(item.currency), item.rate));
            }

            return result;
        }

        private Currency GetCurrencyFromString(string currency)
        {
            try
            {
                Currency result = (Currency)Enum.Parse(typeof(Currency), currency);

                return result;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Bad Currency name");
            }
        }
    }
}