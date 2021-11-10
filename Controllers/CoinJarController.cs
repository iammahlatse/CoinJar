using CoinJarApi.Data;
using CoinJarApi.Models;
using CoinJarApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoinJarApi.Controllers
{

    [Route("api/v{version:apiVersion}/coinJar")]
    [ApiController]
    public class CoinJarController : ControllerBase
    {
        private ICoinJar _npRepo;
        private readonly CoinJarDBContext _db;


        public CoinJarController(ICoinJar npRepo, CoinJarDBContext db)
        {
            _db = db; 
            _npRepo = npRepo;

        }


        [HttpGet(Name = "GetTotalAmount")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalAmount()
        {
            var total = _npRepo.GetTotalAmount();

            return Ok(total);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Coin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddCoin([FromBody] Coin coin)
        {
            if (coin == null)
            {
                return BadRequest(ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var MaxVolume = _db.Coins.Select(t => t.Volume).Sum();
            string coinVal = coin.Amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            if (MaxVolume + coin.Volume < 42)
            {
                if (coinVal.StartsWith("$") == true)
                {
                    _npRepo.AddCoin(coin);
                }
                else
                {
                    return NotFound("Only US Coinage is acceptible");
                }
            }
            else
            {
                return NotFound("Jar Full");
            }

            return CreatedAtRoute("GetTotalAmount", new { Version = HttpContext.GetRequestedApiVersion().ToString() });

        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Reset()
        {
            _npRepo.Reset();

            return Ok();
        }



    }
}
