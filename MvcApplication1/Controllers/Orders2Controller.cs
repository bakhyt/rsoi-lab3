using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ModelLib;
using System.Web.Http;
using RestSharp;

namespace MvcApplication1.Controllers
{
    public class Orders2Controller : ApiController
    {
        public ICollection<Order> Get(int page, int size)
        {
            MyOAuth.CheckAccessToken();

            var client = new RestClient("http://localhost:8888/OrdersService/api");
            var request = new RestRequest(string.Format("orders?page={0}&size={1}", page, size), Method.GET);
            IRestResponse<List<Order>> response2 = client.Execute<List<Order>>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return null;
        }
        public int Post()
        {
            MyOAuth.CheckAccessToken();

            var client = new RestClient("http://localhost:8888/OrdersService/api");
            var request = new RestRequest("orders", Method.POST);
            IRestResponse<int> response2 = client.Execute<int>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return -1;
        }
        public Order Post(int orderId, int goodId, int customerId, int deliveryId)
        {
            MyOAuth.CheckAccessToken();

            var client = new RestClient("http://localhost:8888/OrdersService/api");
            var request = new RestRequest(string.Format("orders?orderId={0}&goodId={1}&customerId={2}&deliveryId={3}", orderId, goodId, customerId, deliveryId), Method.POST);
            /*request.AddParameter("orderId", orderId);
            request.AddParameter("goodId", goodId);
            request.AddParameter("customerId", customerId);
            request.AddParameter("deliveryId", deliveryId);
            */
            IRestResponse<Order> response2 = client.Execute<Order>(request);

            if (response2 != null && response2.Data != null)
            {
                return response2.Data;
            }
            return null;
        }
        /// <summary>
        /// Метод аггрегатора. Достаёт данные из 3 микросервисов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            string res = "";
            Order order = null;

            // получаем данные по заказу
            var client = new RestClient("http://localhost:8888/OrdersService/api");
            var request = new RestRequest(string.Format("orders?id={0}", id), Method.GET);
            IRestResponse<Order> orderResp = client.Execute<Order>(request);

            if (orderResp != null && orderResp.Data != null)
            {
                order = orderResp.Data;
            }
            if (order == null) return null;

            Good good = null;

            client = new RestClient("http://localhost:7777/GoodsService/api");
            request = new RestRequest(string.Format("goods?id={0}", order.GoodId), Method.GET);
            IRestResponse<Good> goodResp = client.Execute<Good>(request);

            if (goodResp != null && goodResp.Data != null)
            {
                good = goodResp.Data;
            }

            Customer customer = null;
            client = new RestClient("http://localhost:4444/CustomerAndDeliveryService/api");
            request = new RestRequest(string.Format("customers?id={0}", order.CustomerId), Method.GET);
            IRestResponse<Customer> customerResp = client.Execute<Customer>(request);

            if (customerResp != null && customerResp.Data != null)
            {
                customer = customerResp.Data;
            }

            Delivery delivery = null;
            client = new RestClient("http://localhost:4444/CustomerAndDeliveryService/api");
            request = new RestRequest(string.Format("deliveries?id={0}", order.DeliveryId), Method.GET);
            IRestResponse<Delivery> deliveryResp = client.Execute<Delivery>(request);

            if (deliveryResp != null && deliveryResp.Data != null)
            {
                delivery = deliveryResp.Data;
            }

            res = string.Format("Номер заказа: {0}; Товар: {1}; Покупатель: {2}; Доставка: {3}",
                id, (good == null ? "" : good.Title), (customer == null ? "" : customer.Title), (delivery == null ? "" : delivery.Title));

            return res;
        }
        [HttpDelete]
        public string Delete(int orderId)
        {
            MyOAuth.CheckAccessToken();

            bool res = false;

            var client = new RestClient("http://localhost:8888/OrdersService/api");
            var request = new RestRequest(string.Format("orders?orderId={0}", orderId), Method.DELETE);
            IRestResponse<bool> response2 = client.Execute<bool>(request);

            if (response2 != null && response2.Data != null)
            {
                res = response2.Data;
            }

            if (res)
                return "Удалено - " + orderId;
            else return "Не удалось удалить -" + orderId;
        }
    }
}