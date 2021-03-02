using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    /// <summary>
    /// Thông tin thẻ
    /// </summary>
    public class PaymentInformation
    {

        [JsonProperty("card")]
        public Card Card { get; set; }
        
    }
}
