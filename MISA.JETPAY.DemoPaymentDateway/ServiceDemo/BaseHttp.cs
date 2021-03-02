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
        /// <summary>
        /// Hàm dùng chung các phương thức gọi api
        /// </summary>
        /// <param name="url">đường đẫn</param>
        /// <param name="method">tên phương thức</param>
        /// <param name="JsonObj">đối tượng truyền vào body</param>
        /// <param name="header">danh sách các header add vào phương thức</param>
        /// <returns></returns>
        /// createby : vhduong(28/2/2021)
        public static ServiceResult BaseHttpRestfull(string url, string method, string JsonObj, Dictionary<string, string> header)
        {
            var serviceResult = new ServiceResult();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            var methodRequest = method.ToUpper();
            switch (methodRequest)
            {
                case "POST":
                    httpWebRequest.Method = "POST";
                    foreach (var item in header)
                    {
                        httpWebRequest.Headers.Add(item.Key, item.Value);
                    }
                    httpWebRequest.ContentType = "application/json";
                    break;
                case "PUT":

                    break;
                case "GET":
                    break;
                case "DELETE":
                    break;

            }
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
