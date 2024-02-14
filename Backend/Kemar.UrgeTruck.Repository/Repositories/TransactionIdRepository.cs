using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Interface;
using System;
using System.Linq;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class TransactionIdRepository : ITransactionId
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;

        public TransactionIdRepository(IKUrgeTruckContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public string GetNextTransactionId(string TransactionType)
        {
            string generatedTransactionNumber = String.Empty;
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var transRec = kUrgeTruckContext.TransGenerator.Where(x => x.TransactionType == TransactionType).FirstOrDefault();
            int currYear = DateTime.Now.Year % 2000; //Take only last two digits of current year
            if (transRec == null || transRec.Year == 0 || transRec.Year != currYear)
            {
                int StartTransactionNumer = 1;
                kUrgeTruckContext.TransGenerator.Add(new Entities.TransGenerator
                {
                    Year = currYear,
                    TransactionType = TransactionType,
                    LastTransactionNumber = StartTransactionNumer
                });
                generatedTransactionNumber = TransactionType + currYear.ToString().Trim() + string.Format("{0:D4}", StartTransactionNumer);
            }
            else
            {
                transRec.LastTransactionNumber = transRec.LastTransactionNumber + 1;
                kUrgeTruckContext.Update(transRec);
                /* string formatting required only up to 999, from 1000 onwards simply concat the number as it is */
                generatedTransactionNumber = TransactionType + currYear.ToString().Trim() +
                (transRec.LastTransactionNumber <= 999 ? string.Format("{0:D4}", transRec.LastTransactionNumber) : transRec.LastTransactionNumber.ToString());
            }

            kUrgeTruckContext.SaveChanges();
            return generatedTransactionNumber;
        }

        public string GetNextTransactionIdExcludingPrefix(string TransactionType)
        {
            try
            {
                string generatedTransactionNumber = String.Empty;
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var transRec = kUrgeTruckContext.TransGenerator.Where(x => x.TransactionType == TransactionType).FirstOrDefault();

                if (transRec == null)
                {
                    int StartTransactionNumer = 1000;
                    kUrgeTruckContext.TransGenerator.Add(new Entities.TransGenerator
                    {
                        Year = 0,
                        TransactionType = TransactionType,
                        LastTransactionNumber = StartTransactionNumer
                    });
                    generatedTransactionNumber = string.Format("{0:D4}", StartTransactionNumer);
                }
                else
                {
                    transRec.LastTransactionNumber = transRec.LastTransactionNumber + 1;
                    kUrgeTruckContext.Update(transRec);
                    /* string formatting required only up to 999, from 1000 onwards simply concat the number as it is */
                    generatedTransactionNumber =
                        (transRec.LastTransactionNumber <= 999 ? string.Format("{0:D4}", transRec.LastTransactionNumber) : transRec.LastTransactionNumber.ToString());
                }

                kUrgeTruckContext.SaveChanges();
                return generatedTransactionNumber;
            }
            catch (Exception ex)
            {
                Logger.Error("Error while get next transaction Id Excluding Prefix " + ex);
                throw;
            }
        }
    }
}
