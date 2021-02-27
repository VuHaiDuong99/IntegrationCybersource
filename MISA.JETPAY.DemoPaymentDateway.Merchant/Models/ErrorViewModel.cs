using System;

namespace MISA.JETPAY.DemoPaymentDateway.Merchant.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
