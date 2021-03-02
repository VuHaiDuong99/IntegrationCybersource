using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    public class AmountDetail
    {
        /// <summary>
        /// tổng số tiền đơn đặt hàng
        /// </summary>
        [JsonProperty("totalAmount")]
        public string TotalAmount { get; set; }
        /// <summary>
        /// Loại tiền tệ
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        #region Hàm khởi tạo 
        public AmountDetail() { }
        public AmountDetail(string totalAmount, string currency)
        {
            TotalAmount = totalAmount;
            Currency = currency;
        }
        #endregion
    }
}
