using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Http;
using MvcApplication1;

namespace WebApi1
{
    /// <summary>
    /// Summary description for WebApi1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebApi1 : System.Web.Services.WebService
    {
        Entities context = new Entities();
        [WebMethod]
        [HttpGet]
        public string Echo(string input)
        {
            MyOAuth.CheckAccessToken();
            return string.Format("{0} - {1}", DateTime.Now, input);
        }

        [WebMethod]
        public List<Goods> GetAllGoods(int page, int size)
        {
            return context.Goods.OrderBy(x => x.ID).Skip(page)
                 .Take(size).ToList();
        }
        [WebMethod]
        [HttpGet]
        public Goods GetGood(int id)
        {
            return context.Goods.Where(x => x.ID == id).FirstOrDefault();
        }
        [WebMethod]
        [HttpGet]
        public string Me()
        {
            return "It is me";
        }
        [WebMethod]
        [HttpGet]
        public List<Orders> GetAllOrders(int page, int size)
        {
            return context.Orders.OrderBy(x => x.ID).Skip(page)
                 .Take(size).ToList();
        }

        [WebMethod]
        [HttpGet]
        public Orders GetOrder(int id)
        {
            return context.Orders.Where(x => x.ID == id).FirstOrDefault();
        }
        [WebMethod]
        public int CreateOrder()
        {
            int maxid = 0;
            if (context.Orders.Count() > 0)
            {
                maxid = context.Orders.Max(p => p == null ? 0 : p.ID);
            }
            Orders order = new Orders()
            {
                ID = Convert.ToInt32(maxid + 1)
            };
            context.Orders.AddObject(order);
            context.SaveChanges();
            return order.ID;
        }
        [WebMethod]
        public Orders AddGood(int orderId, int goodId)
        {
            Orders order = context.Orders.Where(x => x.ID == orderId).First();
            if (order != null)
            {
                order.GoodId = goodId;
            }
            context.SaveChanges();
            return order;
        }

        [WebMethod]
        [HttpDelete]
        public string DeleteOrder(int orderId)
        {
            Orders order = context.Orders.Where(x => x.ID == orderId).First();
            context.DeleteObject(order);
            context.SaveChanges();
            return "Удалено - " + orderId;
        }
        [WebMethod]
        [HttpGet]
        public string Billing()
        { 
            // для всех товаров, у которых нет Cost, проставить
            return "Billing - ok";
        }
    }
}
