using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    public class ClientReferenceInformation
    {
        public ClientReferenceInformation() { }
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        public ClientReferenceInformation(string code)
        {
            Code = code;
        }
    }
}
