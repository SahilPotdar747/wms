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
    public class SupplierMasterRepository: ISupplierMaster
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public SupplierMasterRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<SupplierMasterResponse>> GetSuppliersAsync()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var suppliers = await kUrgeTruckContext.SupplierMaster.ToListAsync();
                return _mapper.Map<List<SupplierMasterResponse>>(suppliers);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
          
        public async Task<ResultModel> SaveSuppliersDetailsAsync(SupplierMasterRequest request)
        {
            var resmsg = UrgeTruckMessages.Suppliers_Data;
            request.CreatedDate = DateTime.Now;
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var list = await kUrgeTruckContext.SupplierMaster.ToListAsync();
                if(request.SupplierId == null || request.SupplierId ==0)
                {
                    if(!list.Any(x=>x.SupplierName == request.SupplierName))
                    {
                        kUrgeTruckContext.Add(_mapper.Map<SupplierMaster>(request));
                        resmsg += UrgeTruckMessages.added_successfully;
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(resmsg);
                    }
                    else
                    {
                        resmsg += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resmsg);
                    }
                }
                else
                {
                    if (!list.Any(x => (x.SupplierName == request.SupplierName) && x.SupplierId != request.SupplierId))
                    {
                        var supplierdata = await kUrgeTruckContext.SupplierMaster.Where(x => x.SupplierId == request.SupplierId).FirstOrDefaultAsync();

                        supplierdata.SupplierName = request.SupplierName;
                        supplierdata.ContanctNo = request.ContanctNo;
                        supplierdata.EmailId = request.EmailId;
                        supplierdata.IsActive = request.IsActive;
                        supplierdata.Remark = request.Remark;
                        resmsg += UrgeTruckMessages.updated_successfully;
                        kUrgeTruckContext.Update(supplierdata);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resmsg);
                    }
                    else
                    {
                        resmsg += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resmsg);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }




       

    }
}
