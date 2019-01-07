using DataBase;
using DataBase.Models;
using PullOrderTransaction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PullOrderTransaction
{
    public class PullOrderTransactionStart : CompleteSalesBase
    {
        private List<Task> TaskList;
        private int MaxThreadCount;
        public PullOrderTransactionStart()
        {
            AppSettings = SetAppSettings();
        }

        public void Start()
        {
            TaskList = new List<Task>();
            MaxThreadCount = AppSettings.MaxThreadCount;
            while (true)
            {
                try
                {
                    CheckTaskIsCompleted();

                    if (MaxThreadCount == 0)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    QueueCompleteSales QueueCompleteSales = new QueueCompleteSales();
                    using (mardevContext context = new mardevContext())
                    {
                        QueueCompleteSales = context.QueueCompleteSales.FirstOrDefault();

                        if (QueueCompleteSales != null)
                        {
                            context.QueueCompleteSales.Remove(QueueCompleteSales);
                            context.SaveChanges();
                        }
                    };

                    if (QueueCompleteSales == null)
                    {
                        Thread.Sleep(10000);
                        continue;
                    }

                    CompleteSalesProcess DoPcc = new CompleteSalesProcess() { };
                    var NewTask = Task.Run(() => { DoPcc.Process(QueueCompleteSales); });
                    //統計Task列表
                    TaskList.Add(NewTask);
                    MaxThreadCount--;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void CheckTaskIsCompleted()
        {
            if (TaskList.Count != 0)
            {
                //Task 在Running狀態無法跑foreach
                var GetNotRunList = TaskList.Where(x => x.Status != TaskStatus.Running).ToList();

                foreach (Task t in GetNotRunList)
                {
                    if (t.IsCompleted)
                    {
                        TaskList.Remove(t);
                        MaxThreadCount++;
                    }
                }
            }
        }
    }
}
