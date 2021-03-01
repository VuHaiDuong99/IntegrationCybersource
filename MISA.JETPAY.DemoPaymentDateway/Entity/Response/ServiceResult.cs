using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Response
{
    /// <summary>
    /// Trả về kết quả của Service
    /// </summary>
    /// CreatedBy : vhduong(1/3/2021)
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Mã trả về
        /// </summary>
        public string HttpResponse { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Messenger { get; set; }
    }
}
