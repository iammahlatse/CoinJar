using Azure.Core;
using CoinJarApi.Data;
using CoinJarApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CoinJarApi.Repository
{
    public class CoinJarRepos : ICoinJar
    {
        private readonly CoinJarDBContext _db;

        public CoinJarRepos(CoinJarDBContext db)
        {
            _db = db;

        }

        public void AddCoin(ICoin coin)
        {
            var MaxVolume = _db.Coins.Select(t => t.Volume).Sum();
            //string coinVal = coin.Amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));


            
            var cns = new Coins
            {
                Amount = coin.Amount,
                Volume = coin.Volume
            };
            _db.Coins.Add(cns);
            _db.SaveChanges();
             

        }

        public decimal GetTotalAmount()
        {

            decimal total = 0;
            var lst = _db.Coins.ToList();


            foreach (var coin in lst)
            {

                total += (coin.Volume * coin.Amount);

            }

            return total;
        }

        public void Reset()
        {
            var lst = _db.Coins.ToList();


            foreach (var coin in lst)
            {

                _db.Coins.Remove(coin);

            }

            _db.SaveChanges();
        }


    }
}
