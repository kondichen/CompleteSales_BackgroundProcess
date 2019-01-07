using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class EbayOrder
    {
        public string EbayOrderId { get; set; }
        public string ApiId { get; set; }
        public string ApiUserId { get; set; }
        public long ApiUserPlatformTokenId { get; set; }
        public string MarketPlaceOrderNumber { get; set; }
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderSubtotal { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderCreateAt { get; set; }
        public string ShippingName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingStreet1 { get; set; }
        public string ShippingStreet2 { get; set; }
        public string ShippingCityName { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCountryName { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingAddressId { get; set; }
        public string ShippingService { get; set; }
        public int? ShippingTimeMin { get; set; }
        public int? ShippingTimeMax { get; set; }
        public string BuyerEbayUserId { get; set; }
        public DateTime? PaymentTime { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
