using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using MISA.JETPAY.DemoPaymentDateway.Models;
using MISA.JETPAY.DemoPaymentGateway.Entity.Request;
using MISA.JETPAY.DemoPaymentGateway.Entity.Response;
using Newtonsoft.Json;

using static MISA.JETPAY.DemoPaymentGateway.Models.ProcessPayment;

namespace MISA.JETPAY.DemoPaymentDateway.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult gateway()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Result(Card card)
        {
            string url = "https://apitest.cybersource.com/pts/v2/payments";
            string SecretKey = "dlPkKIsuwoLnh8UobuS/mlAUwMIMUANS0PD1D8zMlQA=";
            string KeyId = "079ded53-cd32-400e-83ed-948bfa0d9811";
            string merchantId = "test_gateway";
            var clientReferenceInformation = new ClientReferenceInformation("TC50171_3");
            var paymentInformation = new PaymentInformation()
            {
                Card = new Card(card.Number, card.ExpirationMonth, card.ExpirationYear)
            };
            var orderInformation = new OrderInformation()
            {
                AmountDetails = new AmountDetail("102.21", "USD"),
                BillTo = new BillTo("John", "Doe", "1 Market St", "san francisco", "CA", "94105", "US", "test@cybs.com", "4158880000")
            };

            var objReqPayment = new ProcessPaymentReq()
            {
                ClientReferenceInformation = clientReferenceInformation,
                PaymentInformation = paymentInformation,
                OrderInformation = orderInformation
            };
            var objReqPaymentJson = JsonConvert.SerializeObject(objReqPayment);
            var digest = GenerateDigest(objReqPaymentJson);
            var signatureParm = "host: apitest.cybersource.com\n(request-target): post /pts/v2/payments/\ndigest: " + digest + "\nv-c-merchant-id: " + merchantId;
            var signatureHash = GenerateSignatureFromParams(signatureParm, SecretKey);
            Console.WriteLine(signatureHash);
            var date = DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'");
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("v-c-merchant-id", merchantId);
            header.Add("host", "apitest.cybersource.com");
            header.Add("v-c-date", date);
            header.Add("Digest", digest);
            header.Add("Signature", "keyid=\"" + KeyId + "\", algorithm=\"HmacSHA256\", headers=\"host (request-target) digest v-c-merchant-id\", signature=\"" + "Rbbrh0llAlguhXG4xMHAgnCIk+JbHK/g3dVEf5WNeno=" + "\"");
            header.Add("ContentType", "application/json");
            var serviceResult = new ServiceResult();
            serviceResult =  MISA.JETPAY.DemoPaymentGateway.ServiceDemo.BaseHttp.BaseHttpRestfull(url, "POST", objReqPaymentJson, header);
            var mes = "hihih";
            if(serviceResult.HttpResponse == "200")
            {
                return View("Result");
            }
            else
            {
                mes = "Thông tin nhập sai! Vui lòng nhập lại!";
                ViewBag.mess = mes;
                return View("gateway");
            }
           
        }
      
        public IActionResult cybersource()
        {
            
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Private function
        /// <summary>
        /// Hàm tính giá trị digest
        /// created by: vhduong (27/02/2021)
        /// </summary>
        /// <param name="payload"> String body dưới dạng json request</param>
        /// <returns></returns>
        public static string GenerateDigest(string bodyText)
        {
            var digest = "";

            using (var sha256hash = SHA256.Create())
            {
                byte[] payloadBytes = sha256hash
                    .ComputeHash(Encoding.UTF8.GetBytes(bodyText));
                digest = Convert.ToBase64String(payloadBytes);
                digest = "SHA-256=" + digest;
            }
            return digest;
        }
        /// <summary>
        /// Hàm tính signature 
        /// created by: vhduong(27/2/2021)
        /// </summary>
        /// <param name="signatureParams"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string GenerateSignatureFromParams(string signatureParams, string secretKey)
        {
            var sigBytes = Encoding.UTF8.GetBytes(signatureParams);
            var decodedSecret = Convert.FromBase64String(secretKey);
            var hmacSha256 = new HMACSHA256(decodedSecret);
            var messageHash = hmacSha256.ComputeHash(sigBytes);
            return Convert.ToBase64String(messageHash);
        }

        #endregion

    }
}
