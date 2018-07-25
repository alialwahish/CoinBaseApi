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
            var bitcoin = await Models.CryptoProxy.GetCryptoPrice();
            ViewBag.BitPrice = bitcoin.data.amount;
            ViewBag.Bitcoin = bitcoin.data.currency;
            return View("Request");
        }
    }
}