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
        public int Post()
        {
            MyOAuth.CheckAccessToken();

            var client = new RestClient("http://localhost:7777/GoodsService/api");
            var request = new RestRequest("goods", Method.POST);
            IRestResponse<int> response2 = client.Execute<int>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return -1;
        }
        public Good Post(string goodName, double goodCost)
        {
            MyOAuth.CheckAccessToken();

            var client = new RestClient("http://localhost:7777/GoodsService/api");
            var request = new RestRequest(string.Format("Название: {0}; Цена: {1}", goodName, goodCost), Method.POST);

            IRestResponse<Good> response2 = client.Execute<Good>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return null;
        }
        [HttpDelete]
        public string Delete(int goodId)
        {
            MyOAuth.CheckAccessToken();

            bool res = false;

            var client = new RestClient("http://localhost:7777/GoodsService/api");
            var request = new RestRequest(string.Format("goods?orderId={0}", goodId), Method.DELETE);
            IRestResponse<bool> response2 = client.Execute<bool>(request);

            if (response2 != null && response2.Data != null)
            {
                res = response2.Data;
            }

            if (res)
                return "Удалено - " + goodId;
            else return "Не удалось удалить -" + goodId;
        }
    }
}