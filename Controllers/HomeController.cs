using Microsoft.AspNetCore.Mvc;
using crypto.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
namespace crypto.Controllers


{
    public class CryptoController : Controller
    {

        private Context _context;
        public CryptoController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            //Trim lat and lon

            // new controller route
            var bitcoin = await Models.CryptoProxy.GetCryptoPrice("BTC");
            Bitcoin btc = new Bitcoin();
            btc.price = Convert.ToDecimal(bitcoin.data.amount);
            _context.Add(btc);
            _context.SaveChanges();
            // ViewBag.BitPrice = btc.price;
            // ViewBag.Bitcoin = bitcoin.data.@base;
            var bcash = await Models.CryptoProxy.GetCryptoPrice("BCH");
            BitCoinCash bch = new BitCoinCash();
            bch.price = Convert.ToDecimal(bcash.data.amount);
            _context.Add(bch);
            _context.SaveChanges();
            // ViewBag.BcashPrice = bch.price;
            // ViewBag.Bcash = bcash.data.@base;
            var ethereum = await Models.CryptoProxy.GetCryptoPrice("ETH");
            Ethereum eth = new Ethereum();
            eth.price = Convert.ToDecimal(ethereum.data.amount);
            _context.Add(eth);
            _context.SaveChanges();
            // ViewBag.EthPrice = eth.price;
            // ViewBag.Ethereum = ethereum.data.@base;
            var litecoin = await Models.CryptoProxy.GetCryptoPrice("LTC");
            Litecoin ltc = new Litecoin();
            ltc.price = Convert.ToDecimal(litecoin.data.amount);
            _context.Add(ltc);
            _context.SaveChanges();
            // ViewBag.LitePrice = ltc.price;
            // ViewBag.Litecoin = litecoin.data.@base;
            // return View();




            //  end new controller route

            List<Crypto> monthBt = new List<Crypto>();
            List<string> dates = new List<string>();
            string[] coins = { "BTC", "BCH", "ETH", "LTC" };
            List<List<Crypto>> allCoins = new List<List<Crypto>>();
            foreach (var coin in coins)
            {

                for (int i = 0; i < 30; i++)
                {
                    DateTime today = DateTime.Now;
                    today = today.AddDays(-i);
                    string date = today.ToString("yyyy-MM-dd");
                    // Console.WriteLine(date);
                    if (dates.Count < 30)
                    {

                        dates.Insert(0, today.ToString("dd"));
                    }
                    var c = await Models.CryptoProxy.GetCryptoMonth(coin, date);
                    monthBt.Add(c);
                }
                allCoins.Add(monthBt);
                monthBt = new List<Crypto>();
            }

            List<List<float>> allFloatCoins = new List<List<float>>();
            List<float> prices = new List<float>();

            foreach (var cB in allCoins)
            {
                foreach (var coin in cB)
                {

                    prices.Add(float.Parse(coin.data.amount,
          System.Globalization.CultureInfo.InvariantCulture));
                }
                allFloatCoins.Add(prices);
                prices = new List<float>();

            }



            // float min = allFloatCoins[0].First();
            // float max = min;
            // foreach (var p in allFloatCoins[0])
            // {
            //     if (min > p)
            //     {
            //         min = p;
            //     }
            //     if (max < p)
            //     {
            //         max = p;
            //     }
            // }

            // ViewBag.lastWeekMin = allFloatCoins[0].Last();
            // for (int i = 23; i < 30; i++)
            // {
            //     if (ViewBag.lastWeekMin > allFloatCoins[0][i])
            //     {
            //         ViewBag.lastWeekMin = allFloatCoins[0][i];
            //     }


            // }




            ViewBag.BTC = allFloatCoins[0];
            ViewBag.BCH = allFloatCoins[1];
            ViewBag.ETH = allFloatCoins[2];
            ViewBag.LTC = allFloatCoins[3];

            ViewBag.dates = dates;

            // ViewBag.max = max;
            // ViewBag.min = min;
            //ViewBag.btCoins = monthBt;
            //ViewBag.BitPrices = prices;

            ViewBag.Bitcoin = "BTC";
            ViewBag.BCHCurr = "BCH";
            ViewBag.ETHCurr ="ETH";
            ViewBag.LTCCurr="LTC";
            return View();
        }



    }
}