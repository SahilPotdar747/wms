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
    public class StorageRepository:IStorage
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public StorageRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<List<GRNDetailsResponse>>GetStorageDetails(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNDetailsResponse> StorageList = new List<GRNDetailsResponse>();
                var storage = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN.ProductMaster).Include(x => x.GRN.SupplierMaster).Include(x => x.GRN.Location).ToListAsync();
                if (id == null || id == 0)
                {
                    StorageList.AddRange(_mapper.Map<List<GRNDetailsResponse>>(storage));
                }
                else
                {
                    var TopStorageData = storage.FirstOrDefault(x => x.GRNDetailsId == id);
                    if (TopStorageData != null)
                    {
                        StorageList.Add(_mapper.Map<GRNDetailsResponse>(TopStorageData));
                    }
                }
                return StorageList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultModel> UpdateStorage(GRNDetailsRequest requestModel)
        {
            var resMessage = "Storage";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var grnData = await kUrgeTruckContext.GRNDetails.Where(x => x.GRNId == requestModel.GRNId).FirstOrDefaultAsync();
                if (grnData.ProductSerialKey == requestModel.ProductSerialKey && grnData.GRNDetailsId == requestModel.GRNDetailsId)
                {
                    grnData.DropLoc = requestModel.DropLoc;
                    grnData.RackNo = requestModel.RackNo;
                    grnData.SubRack = requestModel.SubRack;
                    grnData.IsActive = requestModel.IsActive;

                    resMessage = resMessage + " " + "Updated Successfully.";
                    kUrgeTruckContext.GRNDetails.Update(grnData);
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
                else
                {
                    var msg = "Product key does not match";
                    return ResultModelFactory.CreateFailure(ResultCode.RecordNotFound, msg);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
