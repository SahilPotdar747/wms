using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public CustomerRepository(IKUrgeTruckContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetCustomerRecords()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var customerList = await kUrgeTruckContext.CustomerMaster.ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customerList);
        }
        public async Task<ResultModel> AddOrUpdateCustomerAsync(CustomerRequest request)
        {
            var resMessage = "New customer ";
            var updateMessage = "Customer ";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var customerList = await kUrgeTruckContext.CustomerMaster.ToListAsync();
                if (request.CustomerId == null || request.CustomerId == 0)
                {
                    if (!customerList.Any(x => x.CustomerName == request.CustomerName))
                    {
                        //request.CreatedDate = DateTime.Now;
                        kUrgeTruckContext.Add(_mapper.Map<CustomerMaster>(request));
                        resMessage += "added successfully!";
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(resMessage);
                    }
                    else
                    {
                        updateMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!customerList.Any(x => (x.CustomerName == request.CustomerName) && x.CustomerId != request.CustomerId))
                    {
                        var customer = await kUrgeTruckContext.CustomerMaster.Where(x => x.CustomerId == request.CustomerId).FirstOrDefaultAsync();
                        //kUrgeTruckContext.Update(_mapper.Map<Location>(request));
                        customer.CustomerId = request.CustomerId;
                        customer.CustomerName = request.CustomerName;
                        customer.ContactNo = request.ContactNo;
                        customer.EmailId = request.EmailId;
                        customer.Address = request.Address;
                        customer.City = request.City;
                        customer.State = request.State;
                        customer.PinCode = request.PinCode;
                        customer.Country = request.Country;
                        customer.Remark = request.Remark;
                        customer.IsActive = request.IsActive;
                        customer.ModifiedDate = DateTime.Now;
                        customer.ModifiedBy = request.CreatedBy;
                        updateMessage += " details updated successfully!";
                        kUrgeTruckContext.Update(customer);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resMessage);
                    }
                    else
                    {
                        updateMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //throw new NotImplementedException();
        }
    }
}
