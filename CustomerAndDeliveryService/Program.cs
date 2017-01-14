using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using ModelLib;
using Newtonsoft.Json;
using System.Web.Http.SelfHost;
using System.Web.Http;

namespace CustomerAndDeliveryService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateAndOpenHost("http://localhost:4444/CustomerAndDeliveryService");
        }
        private static void CreateAndOpenHost(string baseAddress)
        {
            var selfHostConfiguraiton = new HttpSelfHostConfiguration(baseAddress);

            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}",
                defaults: null
            );
            using (var server = new HttpSelfHostServer(selfHostConfiguraiton))
            {
                server.OpenAsync().Wait();

                Console.WriteLine("CustomerAndDeliveryService запущен. baseAddress = " + baseAddress);
                Console.WriteLine("Нажмите <Enter> для остановки сервиса");
                Console.ReadLine();
            }
        }
    }
}
