using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using RestSharp;
using ModelLib;

namespace WebApiClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:5555/api");
            var request = new RestRequest("goods?id=1", Method.GET);
            IRestResponse<Good> response2 = client.Execute<Good>(request);

            /*
            request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            request.AddFile(path);
            */
            // execute the request
            // IRestResponse response = client.Execute(request);
            // var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
           // var name = response2.Data.Name;
            /*
            // easy async support
            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response.Content);
            });

            // async with deserialization
            var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            {
                Console.WriteLine(response.Data.Name);
            });

            // abort the request on demand
            asyncHandle.Abort();
             */
        }
    }
}
