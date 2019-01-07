using DataBase.Models;
using EbayAPIService.Models;
using PullOrderTransaction.Base;
using PullOrderTransaction.Base.Models;
using ServiceReference1;
using System;
using System.Threading.Tasks;
using static ServiceReference1.eBayAPIInterfaceClient;

namespace DataBase
{
    public class CompleteSalesService : CompleteSalesBase, IDisposable
    {
        private EbayAPISettings EbayApiSettings;
        private ApiUserPlatformToken ApiUser;
        private eBayAPIInterfaceClient service;
        public LogPullOrder EbayApilog;
        public CompleteSalesService(ApiUserPlatformToken apiuser,string TransactionSiteID)
        {
            EbayApiSettings = SetEbayAPIConfig();
            this.ApiUser = apiuser;
            string requestURL = "https://api.ebay.com/wsapi"
              + "?callname=" + "CompleteSale"
              + "&siteid=" + EbayApiSettings.SiteID
              + "&appid=" + EbayApiSettings.AppID
              + "&version=" + EbayApiSettings.Version
              + "&routing=new";
            service = new eBayAPIInterfaceClient(EndpointConfiguration.eBayAPI, requestURL);
        }

        public async Task<CallCompleteSalesResponse> CallCompleteSalesAsync(CallCompleteSalesRequest request)
        {

            CallCompleteSalesResponse response = new CallCompleteSalesResponse();
            try
            {
                CompleteSaleRequestType CompleteSalerequest = new CompleteSaleRequestType()
                {
                    ItemID = request.ItemId,
                    TransactionID = request.TransactionId,
                    Shipped = request.IsShipped,
                    ShippedSpecified=true,
                    Shipment = new ShipmentType
                    {
                        ShipmentTrackingDetails = new ShipmentTrackingDetailsType[1]
                    },        
                    Version = EbayApiSettings.Version,
                };

                if (!string.IsNullOrEmpty(request.Courier) && !string.IsNullOrEmpty(request.TrackingNumber))
                {
                    ShipmentTrackingDetailsType shipmentTrackingDetailsType = new ShipmentTrackingDetailsType
                    {
                        ShipmentTrackingNumber = request.TrackingNumber,
                        ShippingCarrierUsed = request.Courier
                    };
                    CompleteSalerequest.Shipment.ShipmentTrackingDetails[0] = shipmentTrackingDetailsType;
                }

                CompleteSalerequest.Shipment.ShippedTime = request.ShipDate;
                CompleteSalerequest.WarningLevel = WarningLevelCodeType.High;
                CompleteSalerequest.WarningLevelSpecified = true;


                CustomSecurityHeaderType requesterCredentials = new CustomSecurityHeaderType
                {
                    eBayAuthToken = ApiUser.AccessToken,
                    Credentials = new UserIdPasswordType()
                    {
                        AppId = EbayApiSettings.AppID,
                        DevId = EbayApiSettings.DevID,
                        AuthCert = EbayApiSettings.AuthCert,
                    },
                };

         CompleteSaleResponseType result = (await service.CompleteSaleAsync(requesterCredentials, CompleteSalerequest)).CompleteSaleResponse1;

                if (result.Ack == AckCodeType.Success)
                    response.IsSuccess = true;
                else
                    response.IsSuccess = false;

                response.Message = result.Message;

                int errorcount = 1;
                foreach(ErrorType e in result.Errors)
                {
                    response.Errors = errorcount.ToString() + " " + e.ShortMessage;
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                SetFailLog(ex.ToString());
                response.IsSuccess = false;
            }

            return response;
        }

        public void Dispose()
        {
        }
    }
}
