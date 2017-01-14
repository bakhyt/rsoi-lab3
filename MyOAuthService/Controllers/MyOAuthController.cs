using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using ModelLib;
using NLog;
using Newtonsoft.Json;

namespace MyOAuthService
{
    public class MyOAuthController : ApiController
    {
        private static Logger _logger = LogManager.GetLogger("MyOAuthService");

        /// <summary>
        /// Проверить токен авторизации
        /// </summary>
        /// <returns></returns>
        public bool Get(string auth)
        {
            _logger.Debug(string.Format("вызов MyOAuth?auth={0}", auth));

            if (string.IsNullOrEmpty(auth))
            {
                return false;
            }
            AuthParams authPars = JsonConvert.DeserializeObject<AuthParams>(auth);

            using (UsersContext context = new UsersContext())
            {
                UserProfile profile = context.UserProfiles.Where(x => x.Token == authPars.access_token).FirstOrDefault();

                // токер просрочен
                if (profile == null || profile.ExpirationDate < DateTime.Now)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
