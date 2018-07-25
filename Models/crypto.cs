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
namespace crypto.Models
{
    public class CryptoProxy
    {
        public async static Task<Crypto> GetCryptoPrice()
        {
            var http = new HttpClient();
            var url = String.Format("https://api.coinbase.com/v2/prices/BTC-USD/spot?date=2017-12-20");
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Crypto));  
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Crypto) serializer.ReadObject(ms);    
            return data;  
        }
    }
    public class Data
    {
        public string @base { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class Warning
    {
        public string id { get; set; }
        public string message { get; set; }
        public string url { get; set; }
    }

    public class Crypto
    {
        public Data data { get; set; }
        public List<Warning> warnings { get; set; }
    }
}