using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using ModelLib;
using NLog;
using Newtonsoft.Json;

namespace OrdersService
{
    public class OrdersController : ApiController
    {
        private static Logger _logger = LogManager.GetLogger("OrdersService");

        public Order Get(int id)
        {
            _logger.Debug(string.Format("вызов orders?id={0}", id));

            using (OrdersContext context = new OrdersContext())
            {
                return context.Orders.Where(x => x.ID == id).FirstOrDefault();
            }
        }

        public List<Order> Get(int page, int size)
        {
            _logger.Debug(string.Format("вызов orders?page={0}&size={1}", page, size));

            using (OrdersContext context = new OrdersContext())
            {
                return context.Orders.OrderBy(x => x.ID).Skip((page - 1) * size)
                      .Take(size).ToList();
            }
        }
        public int Post()
        {
            _logger.Debug("вызов orders (создание через post)");

            int maxid = 0;
            using (OrdersContext context = new OrdersContext())
            {
                if (context.Orders.Count() > 0)
                {
                    maxid = context.Orders.Max(p => p == null ? 0 : p.ID);
                }
                Order order = new Order()
                {
                    ID = Convert.ToInt32(maxid + 1)
                };
                context.Orders.Add(order);
                context.SaveChanges();
                return order.ID;
            }
            return -1;
        }
        public Order Post(int orderId, int goodId, int customerId, int deliveryId)
        {
            _logger.Debug(string.Format("вызов orders (обновление через post). orderid={0}, goodId={1}, customerId={2}, deliveryId={3}", orderId, goodId, customerId, deliveryId));

            using (OrdersContext context = new OrdersContext())
            {
                Order order = context.Orders.Where(x => x.ID == orderId).First();
                if (order != null)
                {
                    order.GoodId = goodId;
                    order.CustomerId = customerId;
                    order.DeliveryId = deliveryId;
                }
                context.SaveChanges();
                return order;
            }
        }
        public bool Delete(int orderId)
        {
            _logger.Debug(string.Format("вызов orders?id={0} удаление через DELETE", orderId));

            try
            {
                using (OrdersContext context = new OrdersContext())
                {
                    Order order = context.Orders.Where(x => x.ID == orderId).First();
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
