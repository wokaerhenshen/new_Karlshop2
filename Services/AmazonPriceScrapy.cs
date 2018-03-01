using HtmlAgilityPack;
using new_Karlshop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace new_Karlshop.Services
{
    public class AmazonPriceScrapy
    {
        private ApplicationDbContext _context;
        private string price;
        public AmazonPriceScrapy(ApplicationDbContext context)
        {
            _context = context;
           
        }


        public async Task AmazonPriceAsync(int id,string Asin)
        {
            var url = "https://www.amazon.ca/dp/" + Asin;
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync(url);
            Stream stream = await result.Content.ReadAsStreamAsync();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);
            var nodes = doc.DocumentNode.SelectNodes("//span");
           
            Goods good = _context.Goodses.Where(ID => ID.goods_id == id).FirstOrDefault();
            foreach (var node in nodes)
            {
                if (node.Attributes["id"] != null )
                {
                    //Console.WriteLine(node.Attributes["id"].Value);
                    if (node.Attributes["id"].Value == "priceblock_ourprice")
                    {
                        price = node.InnerText;
                        good.market_price = price;
                        _context.SaveChanges();
                        break;
                    }
                    //good.market_price = price;
                    //_context.SaveChanges();
                }

                if (node.Attributes["class"] != null)
                {
                    if (node.Attributes["class"].Value == "a-color-price")
                    {
                        price = node.InnerText;
                        good.market_price = price;
                        _context.SaveChanges();
                        break;
                    }
                }


            }
            if (good.market_price == "")
            {
                good.market_price = "Not_Available";
                _context.SaveChanges();
            }

            //if (nodes != null) {

            //    good.market_price = price;
            //    _context.SaveChanges();
            //    stream.Close();

            //}
            //else
            //{
            //    good.market_price = "Not_Available";
            //    _context.SaveChanges();
            //    stream.Close();
            //}


            //if (web.Load(url) is HtmlDocument document)
            //{
            //    var nodes = document.GetElementbyId("priceblock_ourprice").InnerHtml;
            //    return nodes.ToString();
            //}
            //return null;
        }
    }
}
