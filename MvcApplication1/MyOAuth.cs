using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MvcApplication1.Models;
using ModelLib;
using RestSharp;

namespace MvcApplication1
{
    public class MyOAuth
    {
        /// <summary>
        /// Проверить токен авторизации
        /// </summary>
        /// <returns></returns>
        public static bool CheckAccessToken()
        {

            string auth = HttpContext.Current.Request.Headers["Authorization"];

            var client = new RestClient("http://localhost:5555/MyOAuthService/api");
            var request = new RestRequest("MyOAuth?auth=" + auth, Method.GET);
            IRestResponse<bool> response2 = client.Execute<bool>(request);

            if (response2 != null && !response2.Data)
            {
                throw new Exception("Недостаточно прав для выполнения операции");
            }
            return true;
        }

        //grant_type=authorization_code&client_id=464119&client_secret=deadbeef&code=DoRieb0y&
        //redirect_uri=http%3A%2F%2Fexample.com%2Fcb%2F123
    }
    public class TokenParams
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string refresh_token { get; set; }
    }
}