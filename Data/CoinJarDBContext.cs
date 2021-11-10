using CoinJarApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinJarApi.Data
{
    public class CoinJarDBContext: DbContext
    {
        public CoinJarDBContext(DbContextOptions<CoinJarDBContext> options) : base(options)
        {

        }

        public DbSet<Coins> Coins { get; set; }

    }
}
