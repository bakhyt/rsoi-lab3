using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using ModelLib;
using NLog;
using Newtonsoft.Json;

namespace CustomerAndDeliveryService
{
    public class DeliveriesController : ApiController
    {
        private static Logger _logger = LogManager.GetLogger("CustomerAndDeliveryService");

        public Delivery Get(int id)
        {
            _logger.Debug(string.Format("вызов deliveries?id={0}", id));

            using (CustomersContext context = new CustomersContext())
            {
                return context.Deliveries.Where(x => x.ID == id).FirstOrDefault();
            }
        }
        public List<Delivery> Get(int page, int size)
        {
            _logger.Debug(string.Format("вызов deliveries?page={0}&size={1}", page, size));

            using (CustomersContext context = new CustomersContext())
            {
                return context.Deliveries.OrderBy(x => x.ID).Skip((page - 1) * size).Take(size).ToList<Delivery>();
            }
        }
    }
}
