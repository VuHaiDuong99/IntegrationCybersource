using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace MISA.JETPAY.DemoPaymentGateway.ServiceDemo
{
    public class BaseHttp
    {
        public static void BaseHttpRestfull(string url, string method, string JsonObj, Dictionary<string, string> header)
        {
            /* using var httpClient = new HttpClient();
             foreach (var item in header)
             {
                 httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
             }

             HttpResponseMessage response =  httpClient.PostAsync(url,JsonObj).Result;*/





            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://apitest.cybersource.com/pts/v2/payments");
            httpWebRequest.Method = "POST";
            foreach (var item in header)
            {
                httpWebRequest.Headers.Add(item.Key, item.Value);
            }
            httpWebRequest.ContentType = "application/json";
            var httpResponse = new HttpWebResponse();
            string res = "";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonObj);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    res = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(httpResponse.ToString());
            }

            Console.Write(res);


        }
    }
}
