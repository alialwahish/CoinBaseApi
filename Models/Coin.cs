using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace crypto.Models
{
    public abstract class BaseEntity{}
    public class Coin : BaseEntity
    {
        [Key]
        public int id {get;set;}
        [Required]
        public decimal price {get;set;}
        public DateTime created_at {get;set;}
        public Coin()
        {
            created_at = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }
    }
    public class Bitcoin : Coin {}
    public class BitCoinCash : Coin {}
    public class Ethereum : Coin {}
    public class Litecoin : Coin {}
}