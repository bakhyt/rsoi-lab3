using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ModelLib;
using RestSharp;

namespace MvcApplication1.Controllers
{
    public class Deliveries2Controller : ApiController
    {
        public List<Delivery> Get(int page, int size)
        {
            MyOAuth.CheckAccessToken();
            // получаем данные из микросервиса покупателей
            var client = new RestClient("http://localhost:4444/CustomerAndDeliveryService/api");
            var request = new RestRequest(string.Format("deliveries?page={0}&size={1}", page, size), Method.GET);
            IRestResponse<List<Delivery>> response2 = client.Execute<List<Delivery>>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return null;
        }
    }
}