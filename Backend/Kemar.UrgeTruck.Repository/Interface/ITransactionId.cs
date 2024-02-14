using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface ITransactionId
    {
        public string GetNextTransactionId(string TransactionType);
        public string GetNextTransactionIdExcludingPrefix(string TransactionType);
    }
}
