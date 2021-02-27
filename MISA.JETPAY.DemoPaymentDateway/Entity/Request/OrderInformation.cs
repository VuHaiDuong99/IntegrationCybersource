using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    /// <summary>
    /// Thông tin thanh toán 
    /// createby : vhduong(27/2/2021)
    /// </summary>
    public class OrderInformation
    {
        [JsonProperty("amountDetails")]
        public AmountDetail AmountDetails { get; set; }
        [JsonProperty("billTo")]
        public BillTo BillTo { get; set; }
    }
}
