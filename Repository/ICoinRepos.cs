using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinJarApi.Repository
{
    public interface ICoinJar
    {
        void AddCoin(ICoin coin);
        decimal GetTotalAmount();
        void Reset();
    }

    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }

    }
}
