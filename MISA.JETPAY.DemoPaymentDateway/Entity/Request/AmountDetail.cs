using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    public class AmountDetail
    {

        [JsonProperty("totalAmount")]
        public string TotalAmount { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }

        public AmountDetail(string totalAmount, string currency)
        {
            TotalAmount = totalAmount;
            Currency = currency;
        }
    }
}
