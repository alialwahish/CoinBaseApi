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

        


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            //Trim lat and lon

            List<Crypto> monthBt = new List<Crypto>();
            List<string> dates = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                DateTime today = DateTime.Now;
                today = today.AddDays(-i);


                string date = today.ToString("yyyy-MM-dd");
                // Console.WriteLine(date);
                dates.Insert(0,today.ToString("dd"));
                var c = await Models.CryptoProxy.GetCryptoPrice(date);
                monthBt.Add(c);
            }
            List<float> prices = new List<float>();

            foreach (var bt in monthBt)
            {
                prices.Add(float.Parse(bt.data.amount,
      System.Globalization.CultureInfo.InvariantCulture));
            }



            float min = prices.First();
            float max = min;
            foreach (var p in prices)
            {
                if (min > p)
                {
                    min = p;
                }
                if (max < p)
                {
                    max = p;
                }
            }
            
            ViewBag.lastWeekMin=prices.Last();
            for (int i=23;i< 30;i++){
               if(ViewBag.lastWeekMin>prices[i]){
                   ViewBag.lastWeekMin=prices[i];
               }
                   

                }


            ViewBag.max = max;
            ViewBag.min = min;
            ViewBag.dates = dates;
            ViewBag.btCoins = monthBt;
            ViewBag.BitPrices = prices;
            
            ViewBag.Bitcoin = monthBt.First().data.currency;

            return View();
        }



    }
}