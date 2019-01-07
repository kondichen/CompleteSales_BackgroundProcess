using CompleteSales.Base.Models;
using DataBase;
using DataBase.Models;
using Microsoft.Extensions.Configuration;
using PullOrderTransaction.Base.Models;
using System;
using System.IO;

namespace PullOrderTransaction.Base
{
    public class CompleteSalesBase
    {
        #region Managing Log
        protected LogPullOrder PullOrderLog;
        protected LogPullOrder SetFailLog(string error)
        {
            PullOrderLog.ProcessStatus = (int)EnumLogStatus.Fail;
            PullOrderLog.ProcessEndTime = DateTime.UtcNow;
            PullOrderLog.ProcessMessage = error;
            return PullOrderLog;
        }
        protected void SaveLogToDB()
        {
            if (PullOrderLog.ProcessEndTime == null)
                PullOrderLog.ProcessEndTime = DateTime.UtcNow;

            using (mardevContext context = new mardevContext())
            {
                context.LogPullOrder.Add(PullOrderLog);
                context.SaveChanges();
            };
        }
        #endregion

        #region Appsettings
        protected EbayAPISettings EbayAPISettings;
        protected EbayAPISettings SetEbayAPIConfig()
        {
            //Setting AppSettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration.GetSection("EbayAPISettings").Get<EbayAPISettings>();
        }

        protected AppSettings AppSettings;
        protected AppSettings SetAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration.GetSection("AppSettings").Get<AppSettings>();
        }
        #endregion

    }


}
