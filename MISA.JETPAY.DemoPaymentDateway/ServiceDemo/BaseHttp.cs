using MISA.JETPAY.DemoPaymentGateway.Entity.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace MISA.JETPAY.DemoPaymentGateway.ServiceDemo
{
    public class BaseHttp
    {
        public static ServiceResult BaseHttpRestfull(string url, string method, string JsonObj, Dictionary<string, string> header)
        {
            var serviceResult = new ServiceResult();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            foreach (var item in header)
            {
                httpWebRequest.Headers.Add(item.Key, item.Value);
            }
            httpWebRequest.ContentType = "application/json";
            var httpResponse = new HttpWebResponse();
            
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
                    serviceResult.HttpResponse = "200";
                    //serviceResult.HttpResponse = httpResponse.StatusCode.ToString();
                    serviceResult.Data = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                //serviceResult.HttpResponse = httpResponse.StatusCode.ToString();
                serviceResult.Messenger = e.ToString();
                serviceResult.HttpResponse = "100";
            }

            return serviceResult;


        }
    }
}
