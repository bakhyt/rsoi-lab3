using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLib;
using Newtonsoft.Json;
using System.Web.Http;
using RestSharp;

namespace MvcApplication1.Controllers
{
    public class Goods2Controller : ApiController
    {
        public string Get(int id)
        {
            MyOAuth.CheckAccessToken();
            // получаем данные из микросервиса товаров

            var client = new RestClient("http://localhost:7777/GoodsService/api");
            var request = new RestRequest(string.Format("goods?id={0}", id), Method.GET);
            IRestResponse<Good> response2 = client.Execute<Good>(request);

            if (response2 != null && response2.Data != null)
            {
                Good good = response2.Data;
                return string.Format("Название: {0}; Цена: {1} ", good.Title, good.Cost);
            }
            return "Нет данных";
        }
        public List<Good> Get(int page, int size)
        {
            var client = new RestClient("http://localhost:7777/GoodsService/api");
            var request = new RestRequest(string.Format("goods?page={0}&size={1}", page, size), Method.GET);
            IRestResponse<List<Good>> response2 = client.Execute<List<Good>>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return null;
        }
    }
}