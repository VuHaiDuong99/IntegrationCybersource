using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.ServiceDemo
{
    public class BaseHttp
    {
        public static void BaseHttpRestfull(string url, string method, StringContent objData, Dictionary<string, string> header)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);


            client.DefaultRequestHeaders.Accept.Clear();

            foreach (var item in header)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Add an Accept header for JSON format.
            /*client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));*/
            /* foreach (var item in header)
             {
                 client.DefaultRequestHeaders.Add(item.Key, item.Value);
             }*/
            HttpResponseMessage response = new HttpResponseMessage();

            if (method.ToUpper().Equals("POST"))
                response = client.PostAsync(url,  objData).Result;
            else
                response = client.GetAsync("").Result;
            // List data response.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }
    }
}
