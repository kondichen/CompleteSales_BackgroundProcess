using DataBase.Models;
using PullOrderTransaction.Base;
using PullOrderTransaction.Base.Models;
using System;
using System.Threading.Tasks;

namespace PullOrderTransaction
{
    public class CompleteSalesProcess : CompleteSalesBase
    {
        public void Process(QueueCompleteSales payload)
        {
            PullOrderLog = new LogPullOrder()
            {
                ProcessStartTime = DateTime.UtcNow,
                ProcessStatus = (int)EnumLogStatus.Pending,
            };
            try
            {
                CompleteSalesWorking PullOrdersWorker = new CompleteSalesWorking(payload)
                { workinglog = PullOrderLog };

                PullOrderLog = PullOrdersWorker.Working();
            }
            catch (Exception ex)
            {
                this.SetFailLog(ex.ToString());
            }

            this.SaveLogToDB();
        }
    }
}
