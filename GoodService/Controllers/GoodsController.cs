using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using ModelLib;
using NLog;
namespace GoodsService
{
    public class GoodsController : ApiController
    {
        private static Logger _logger = LogManager.GetLogger("GoodsService");
        
        public Good Get(int id)
        {
            _logger.Debug(string.Format("вызов goods?id={0}", id));

            using (GoodsContext context = new GoodsContext())
            {
                return context.Goods.Where(x => x.ID == id).FirstOrDefault();
            }
        }
        public List<Good> Get(int page, int size)
        {
            _logger.Debug(string.Format("вызов goods?page={0}&size={1}", page, size));

            using (GoodsContext context = new GoodsContext())
            {
                return context.Goods.OrderBy(x => x.ID).Skip((page - 1) * size)
                      .Take(size).ToList<Good>();
                /*
                return JsonConvert.SerializeObject(context.Goods.OrderBy(x => x.ID).Skip((page - 1) * size)
                      .Take(size).ToList());
                */
            }
        }
    }
}
