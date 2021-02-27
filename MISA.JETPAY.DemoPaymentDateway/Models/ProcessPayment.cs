using MISA.JETPAY.DemoPaymentGateway.Entity.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Models
{
    public class ProcessPayment
    {
        public class ProcessPaymentReq
        {
            [JsonProperty("clientReferenceInformation")]
            public ClientReferenceInformation ClientReferenceInformation { get; set; }
            [JsonProperty("paymentInformation")]
            public PaymentInformation PaymentInformation { get; set; }
            [JsonProperty("orderInformation")]
            public OrderInformation OrderInformation { get; set; }
        }
    }
}
