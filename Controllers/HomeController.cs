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

            // ViewBag.LitePrice = ltc.price;
            // ViewBag.Litecoin = litecoin.data.@base;
            // return View();




            //  end new controller route
            List<FullCoin> fullCoin = new List<FullCoin>();

            List<Crypto> monthBt = new List<Crypto>();
            List<string> dates = new List<string>();
            string[] coins = { "BTC", "BCH", "ETH", "LTC" };
            List<List<Crypto>> allCoins = new List<List<Crypto>>();
            // foreach (var coin in coins)
            // {

            //     for (int i = 0; i < 30; i++)
            //     {
            //         DateTime today = DateTime.Now;
            //         today = today.AddDays(-i);
            //         string date = today.ToString("yyyy-MM-dd");
            //         // Console.WriteLine(date);
            //         if (dates.Count < 30)
            //         {
            //             dates.Insert(0, today.ToString("dd"));
            //         }
            //         var c = await Models.CryptoProxy.GetCryptoMonth(coin, date);
            //         monthBt.Add(c);
            //         FullCoin fc = new FullCoin();
            //         fc.price = float.Parse(c.data.amount,
            //   System.Globalization.CultureInfo.InvariantCulture);
            //         fc.Currency = c.data.@base;
            //         fullCoin.Add(fc);
            //         _context.Add(fc);
            //         _context.SaveChanges();

            //     }
            //     allCoins.Add(monthBt);
            //     monthBt = new List<Crypto>();
            // }
            //         _context.SaveChanges();


            //     foreach (var cB in allCoins)
            //     {
            //         foreach (var coin in cB)
            //         {

            //             prices.Add(float.Parse(coin.data.amount,
            //   System.Globalization.CultureInfo.InvariantCulture));


            //         }
            //         allFloatCoins.Add(prices);

            //     }


            int id = _context.ethereum.Last().id;
            List<Bitcoin> bitRecent = new List<Bitcoin>();
            List<BitCoinCash> cashRecent = new List<BitCoinCash>();
            List<Ethereum> ethRecent = new List<Ethereum>();
            List<Litecoin> liteRecent = new List<Litecoin>();
            List<string> hours = new List<string>();

            for (var i = 0; i < 6; i++)
            {

                hours.Insert(0, i.ToString());
                bitRecent.Insert(0, _context.bitcoin.SingleOrDefault(choose => choose.id == id));
                cashRecent.Insert(0, _context.bcash.SingleOrDefault(choose => choose.id == id));
                ethRecent.Insert(0, _context.ethereum.SingleOrDefault(choose => choose.id == id));
                liteRecent.Insert(0, _context.litecoin.SingleOrDefault(choose => choose.id == id));
                id--;
            }





            ViewBag.bitRecent = bitRecent;
            ViewBag.cashRecent = cashRecent;
            ViewBag.ethRecent = ethRecent;
            ViewBag.liteRecent = liteRecent;
            ViewBag.hours = hours;







            List<FullCoin> BTC = _context.lastMonthAllCoins.Where(b => b.Currency == "BTC").ToList();
            List<FullCoin> BCH = _context.lastMonthAllCoins.Where(b => b.Currency == "BCH").ToList();
            List<FullCoin> ETH = _context.lastMonthAllCoins.Where(b => b.Currency == "ETH").ToList();
            List<FullCoin> LTC = _context.lastMonthAllCoins.Where(b => b.Currency == "LTC").ToList();



            List<List<FullCoin>> allFullCoinsList = new List<List<FullCoin>>();
            allFullCoinsList.Add(BTC);
            allFullCoinsList.Add(BCH);
            allFullCoinsList.Add(ETH);
            allFullCoinsList.Add(LTC);

            List<List<float>> allFloatCoins = new List<List<float>>();
            List<float> prices = new List<float>();


            foreach (var eachList in allFullCoinsList)
            {
                foreach (var coin in eachList)
                {
                    prices.Add(coin.price);

                }
                allFloatCoins.Add(prices);
                prices = new List<float>();


            }




            ViewBag.BTC = allFloatCoins[0];
            ViewBag.BCH = allFloatCoins[1];
            ViewBag.ETH = allFloatCoins[2];
            ViewBag.LTC = allFloatCoins[3];

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


            }



            ViewBag.dates = dates;


            ViewBag.Bitcoin = "BTC";
            ViewBag.BCHCurr = "BCH";
            ViewBag.ETHCurr = "ETH";
            ViewBag.LTCCurr = "LTC";
            return View();
        }



    }
}