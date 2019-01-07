using CompleteSales.Models;
using DataBase;
using DataBase.Models;
using EbayAPIService.Models;
using PullOrderTransaction.Base;
using PullOrderTransaction.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullOrderTransaction
{
    public class CompleteSalesWorking : CompleteSalesBase
    {
        #region prorerty
        private ApiUserKey userKey;
        private CompleteSalesRequest completeSalesRequest;
        private List<CompleteSalesRequestItem> completeSalesRequestItems;
        private Dictionary<long, ApiUserPlatformToken> userPlatformTokens;
        private Dictionary<Guid, EbayOrderTransaction> ebayOrderTransactions;
        private readonly QueueCompleteSales PayLoad;
        public LogPullOrder workinglog;
        #endregion
        public CompleteSalesWorking(QueueCompleteSales payload)
        {
            this.PayLoad = payload;
        }

        public LogPullOrder Working()
        {
            this.PullOrderLog = workinglog;
            try
            {
                PullOrderLog = CompleteSales();
                if (PullOrderLog.ProcessStatus == (int)EnumLogStatus.Fail)
                    return PullOrderLog;

                PullOrderLog.ProcessEndTime = DateTime.UtcNow;
                PullOrderLog.ProcessStatus = (int)EnumLogStatus.Success;
            }
            catch (Exception ex)
            {
                this.SetFailLog(ex.ToString());
            }
            return PullOrderLog;
        }

        private LogPullOrder CompleteSales()
        {
            try
            {
                using (mardevContext context = new mardevContext())
                {
                    GetUserKey(context, PayLoad.ApiUserId);
                    GetUserPlatformTokens(context);

                    GetRequest(context);
                    if (completeSalesRequest == null)
                    {
                        SetFailLog("completeSalesRequest is empty with CompleteSalesRequestID: " + PayLoad.CompleteSalesRequestId);
                        return PullOrderLog;
                    }

                    if (completeSalesRequestItems == null || completeSalesRequestItems.Count == 0)
                    {
                        SetFailLog("completeSalesRequestItems is empty with CompleteSalesRequestID: " + PayLoad.CompleteSalesRequestId);
                        return PullOrderLog;
                    }

                    GetEbayOrderTransaction(context);

                    if (ebayOrderTransactions.Count == 0)
                    {
                        SetFailLog("EbayOrderTransaction is empty with CompleteSalesRequestID: " + PayLoad.CompleteSalesRequestId);
                        return PullOrderLog;
                    }

                    ProcessItemAsync(context);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SetFailLog(ex.ToString());
            }

            return PullOrderLog;
        }

        private async Task ProcessItemAsync(mardevContext context)
        {
            foreach (CompleteSalesRequestItem item in completeSalesRequestItems)
            {
                //CompleteSalesRequestItem updatedItem = await RequestCompleteSalesAsync(item, context);

                //if (item.CompleteSalesRequestItemId == 0)
                //    context.CompleteSalesRequestItem.Add(updatedItem);
                //else
                //    context.CompleteSalesRequestItem.Update(updatedItem);
            }
        }

        private async Task<CompleteSalesRequestItem> RequestCompleteSalesAsync(CompleteSalesRequestItem item, mardevContext context)
        {
            if (!ebayOrderTransactions.ContainsKey(item.EbayOrderTransactionId))
            {
                item.Success = true;
                item.ErrorMessage = $"No eBay order transaction found";
            }

            EbayOrderTransaction ebayOrderTransaction = ebayOrderTransactions[item.EbayOrderTransactionId];

            if (!userPlatformTokens.ContainsKey(ebayOrderTransaction.ApiUserPlatformTokenId))
            {
                item.Success = false;
                item.ErrorMessage = $"Related token revoked";
            }

            ApiUserPlatformToken apiUserPlatformToken =
              userPlatformTokens[ebayOrderTransaction.ApiUserPlatformTokenId];
            CallCompleteSalesRequest request = new CallCompleteSalesRequest()
            {
                IsShipped = true,
                Courier = item.Courier,
                ShipDate = item.ShipDate,
                TrackingNumber = item.TrackingNumber,
                ItemId = ebayOrderTransaction.ItemId,
                ItemSite = ebayOrderTransaction.ItemSite,
                TransactionId = ebayOrderTransaction.TransactionId
            };
            using (CompleteSalesService service = new CompleteSalesService(apiUserPlatformToken, ebayOrderTransaction.TransactionSiteId))
            {
                service.EbayApilog = PullOrderLog;
                CallCompleteSalesResponse response = await service.CallCompleteSalesAsync(request);

                if (response.IsSuccess)
                {
                    ebayOrderTransaction.ShipmentCarrierUsed = item.Courier;
                    ebayOrderTransaction.ShipmentTrackingNumber = item.TrackingNumber;
                    ebayOrderTransaction.ShippedTime = item.ShipDate;

                    DateTime currentTime = DateTime.UtcNow;
                    if (ebayOrderTransaction.EbayOrderTransactionId == Guid.Empty)
                    {
                        ebayOrderTransaction.EbayOrderTransactionId = Guid.NewGuid();
                        ebayOrderTransaction.CreateOn = currentTime;
                        context.EbayOrderTransaction.Add(ebayOrderTransaction);
                    }
                    else
                    {
                        context.EbayOrderTransaction.Update(ebayOrderTransaction);
                    }
                    ebayOrderTransaction.ModifyOn = currentTime;
                }
                else
                {
                    item.ErrorMessage = response.Errors;
                }
            }

            return item;
        }


        #region GetRequestData
        private void GetEbayOrderTransaction(mardevContext context)
        {
            ebayOrderTransactions = new Dictionary<Guid, EbayOrderTransaction>();
            List<Guid> transactionIDs = completeSalesRequestItems.Select(x => x.EbayOrderTransactionId).ToList();
            List<EbayOrderTransaction> list = (context.EbayOrderTransaction.Where(
                x => x.ApiId == userKey.apiKey.apiId &&
                x.ApiUserId == userKey.apiUserId &&
                transactionIDs.Contains(x.EbayOrderTransactionId)
                )).ToList();

            foreach (EbayOrderTransaction ebayOrderTransaction in list)
            {
                ebayOrderTransactions.Add(ebayOrderTransaction.EbayOrderTransactionId, ebayOrderTransaction);
            }
        }

        private ApiUserKey GetUserKey(mardevContext context, Guid userID)
        {
            ApiUser apiUser = context.ApiUser.Where(x => !x.IsDeleted && x.ApiUserId == userID).FirstOrDefault();
            if (apiUser == null)
            {
                return null;
            }

            userKey = new ApiUserKey(apiUser.ApiUserId, apiUser.ApiId);
            return userKey;
        }

        private void GetUserPlatformTokens(mardevContext context)
        {
            userPlatformTokens = new Dictionary<long, ApiUserPlatformToken>();

            List<ApiUserPlatformToken> tokens = (
                from a in context.CompleteSalesRequest
                where a.CompleteSalesRequestId == PayLoad.CompleteSalesRequestId
                join b in context.ApiUserPlatformToken on new { a.ApiId, a.ApiUserId } equals new { b.ApiId, b.ApiUserId }
                select b
                ).ToList();

            if (tokens != null)
            {
                foreach (ApiUserPlatformToken apiUserPlatformToken in tokens)
                {
                    userPlatformTokens.Add(apiUserPlatformToken.ApiUserPlatformTokenId, apiUserPlatformToken);
                }
            }
        }

        private void GetRequest(mardevContext context)
        {
            completeSalesRequest = context.CompleteSalesRequest.Where(
                x => x.ApiId == userKey.apiKey.apiId &&
                x.ApiUserId == userKey.apiUserId &&
                x.CompleteSalesRequestId == PayLoad.CompleteSalesRequestId
                ).FirstOrDefault();

            completeSalesRequestItems = context.CompleteSalesRequestItem.Where(
               x => x.ApiId == userKey.apiKey.apiId &&
               x.ApiUserId == userKey.apiUserId &&
               x.CompleteSalesRequestId == PayLoad.CompleteSalesRequestId
               ).ToList();
        }
        #endregion
    }
}
