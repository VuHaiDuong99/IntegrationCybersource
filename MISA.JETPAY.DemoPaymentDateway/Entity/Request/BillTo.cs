using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.JETPAY.DemoPaymentGateway.Entity.Request
{
    public class BillTo
    {
        /// <summary>
        /// 
        /// </summary>

        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("address1")]
        public string Address1 { get; set; }
        [JsonProperty("locality")]
        public string Locality { get; set; }
        [JsonProperty("administrativeArea")]
        public string AdministrativeArea { get; set; }
        [JsonProperty("postalCode")]

        public string PostalCode { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="add1"></param>
        /// <param name="location"></param>
        /// <param name="adminArea"></param>
        /// <param name="postCode"></param>
        /// <param name="country"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public BillTo(string firstName, string lastName, string add1, string location, string adminArea, string postCode, string country, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address1 = add1;
            Locality = location;
            AdministrativeArea = adminArea;
            PostalCode = postCode;
            Country = country;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
