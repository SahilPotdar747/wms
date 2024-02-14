﻿using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface ICustomer
    {
        Task<List<CustomerResponse>> GetCustomerRecords();
        Task<ResultModel> AddOrUpdateCustomerAsync(CustomerRequest request);
    }
}
