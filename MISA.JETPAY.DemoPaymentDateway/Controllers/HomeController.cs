using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using MISA.JETPAY.DemoPaymentDateway.Models;
using MISA.JETPAY.DemoPaymentGateway.Entity.Request;
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
        public IActionResult test()
        
        {
            string SecretKey = "+JjOOTqdIkMptZ460yaqzYtLgIeLyOEqbqzDJGL2I1g=";
            string KeyId = "7a3712c7 - 4688 - 430c - ad9a - 3101cf9ced5a";
            string MerchantId = "test_jetpay";
            var clientReferenceInformation = new ClientReferenceInformation("TC50171_3");
            var paymentInformation = new PaymentInformation()
            {
                Cart = new Cart("4111111111111111", "12", "2031")
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

            /*var buffer = System.Text.Encoding.UTF8.GetBytes(objReqPaymentJson);
            var byteContent = new ByteArrayContent(buffer);*/
            StringContent httpContent = new StringContent(objReqPaymentJson, Encoding.UTF8, "application/json");
            // đường dẫn url
            string url = "https://apitest.cybersource.com/pts/v2/payments";
            // date
           // var date = new DateTime(DateTime.Now.).toUTCString();
            
            // sinh digest
            var digest = GenerateDigest(objReqPaymentJson);
            // mã bí mật
            //var secretKey = "+JjOOTqdIkMptZ460yaqzYtLgIeLyOEqbqzDJGL2I1g=";
            string signatureParams = 
                "host: apitest.cybersource.com \n"+
                "date: Thu, 18 Jul 2019 00:18:03 GMT\n" +
                "(request - target): post /pts/v2/payments/ \n" +
                $"digest:{digest}\n" +
                "v-c-merchant-id: test_jetpay";
            // sinh mã Signature
            var generateSignatureFromParams = GenerateSignatureFromParams(signatureParams, SecretKey);

            var Signature = 'keyid="7a3712c7 - 4688 - 430c - ad9a - 3101cf9ced5a", algorithm="HmacSHA256", headers="host date(request-target) v - c - merchant - id", signature="lAJZEekdnpG5OrhJqcHA + xvH0kiX3CgoJsT1yKMcqoQ = "';
            // giá trị header
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("host", "apitest.cybersource.com");
            header.Add("v-c-merchant-id", "test_jetpay");
            header.Add("Date", "");
            header.Add("Content-Type", "");
            header.Add("Digest", digest);
            header.Add("Signature", signatureParams);

            MISA.JETPAY.DemoPaymentGateway.ServiceDemo.BaseHttp.BaseHttpRestfull(url,"POST", httpContent, header);

          


            return test();
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
        private static string GenerateDigest(string payload)
        {
            var digest = "";
            
            using (var sha256hash = SHA256.Create())
            {
                byte[] payloadBytes = sha256hash
                    .ComputeHash(Encoding.UTF8.GetBytes(payload));
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
        private static string GenerateSignatureFromParams(string signatureParams, string secretKey)
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
