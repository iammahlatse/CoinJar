using CoinJarApi.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoinJarApi.Models
{
    public class Coins 
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; } = 0;
        public decimal Volume { get; set; } = 0;

    }


    public class Coin : ICoin
    {
        public decimal Amount { get; set; } = 0;
        public decimal Volume { get; set; } = 0;
    }


}
