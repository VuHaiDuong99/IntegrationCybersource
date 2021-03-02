using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    public class Card
    {
        #region Hàm khởi tạo
        public Card() { }
        public Card(string number, string expirationMonth, string expirationYear)
        {
            Number = number;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
        }
        #endregion
        /// <summary>
        /// Số thẻ
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }
        /// <summary>
        /// Tháng hết hạn 
        /// </summary>
        [JsonProperty("expirationMonth")]
        public string ExpirationMonth { get; set; }
        /// <summary>
        /// Năm hết hạn 
        /// </summary>
        [JsonProperty("expirationYear")]
        public string ExpirationYear { get; set; }
    }
}
