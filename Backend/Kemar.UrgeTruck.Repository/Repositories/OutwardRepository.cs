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
    public class OutwardRepository : IOutward
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public OutwardRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<DeliveryChallanMasterResponse>> GetOutwardRecord(int dcmId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<DeliveryChallanMasterResponse> DeliveryMasterList = new List<DeliveryChallanMasterResponse>();
                var deliverydata = await kUrgeTruckContext.DeliveryChallanMaster.Include(x => x.GRN.ProductMaster.ProductCategory).Include(x => x.GRN.SupplierMaster).ToListAsync();


                if (dcmId == null || dcmId == 0)
                    DeliveryMasterList.AddRange(_mapper.Map<List<DeliveryChallanMasterResponse>>(deliverydata));
                else
                {
                    var TopDeliveryData = deliverydata.FirstOrDefault(x => x.DCMId == dcmId);
                    if (TopDeliveryData != null)
                    {
                        DeliveryMasterList.Add(_mapper.Map<DeliveryChallanMasterResponse>(TopDeliveryData));
                    }
                }

                return _mapper.Map<List<DeliveryChallanMasterResponse>>(deliverydata);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GRNDetailsResponse>> GetDCGeneratedRecord(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNDetailsResponse> GRNDetailsList = new List<GRNDetailsResponse>();
                var grnDetails = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).Where(x => x.Status == "DC-Generated").ToListAsync();
                if (id == null || id == 0)
                {
                    GRNDetailsList.AddRange(_mapper.Map<List<GRNDetailsResponse>>(grnDetails));
                }
                else
                {
                    var TopGRNData = grnDetails.FirstOrDefault(x => x.GRNDetailsId == id);
                    if (TopGRNData != null)
                    {
                        GRNDetailsList.Add(_mapper.Map<GRNDetailsResponse>(TopGRNData));
                    }
                }
                return GRNDetailsList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultModel> SaveOutwardRecord(List<DeliveryChallanDetailsRequest> request)
        {
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();

                foreach (var req in request)
                {
                    var outwardUpdates = await kUrgeTruckContext.DeliveryChallanDetails
                        .Include(x => x.GRNDetails)
                       .Where(x => x.GRNDetails.ProductSerialKey == req.ProductSerialKey && x.DCMId == req.DCMId && x.Status == DeliveryChallan.DCGenerated)
                        .ToListAsync();

                    if (outwardUpdates.Count == 0)
                    {
                        return ResultModelFactory.CreateFailure(ResultCode.Invalid, "Invalid data");
                    }

                    foreach (var outwardUpdate in outwardUpdates)
                    {
                        outwardUpdate.Status =Outward.Delivered;
                        kUrgeTruckContext.DeliveryChallanDetails.Update(outwardUpdate);
                        await kUrgeTruckContext.SaveChangesAsync();
                    }

                    var grnDetailsStatuses = await kUrgeTruckContext.GRNDetails
                        .Where(x => x.ProductSerialKey == req.ProductSerialKey && x.GRNDetailsId == req.GRNDetailsId && x.Status != "Delivered" && x.Status == "DC-Generated")
                        .ToListAsync();

                    foreach (var grnDetailsStatus in grnDetailsStatuses)
                    {
                        //grnDetailsStatus.Status = "Delivered";
                        grnDetailsStatus.Status = Outward.Delivered;
                    }

                    var outwardDCMList = await kUrgeTruckContext.DeliveryChallanMaster
                        .Where(x => x.DCMId == req.DCMId && x.DcStatus == DeliveryChallan.FullDelivery && x.Status =="InStock")
                        .ToListAsync();
                    var outwardDCDList = await kUrgeTruckContext.DeliveryChallanDetails.Where(x => x.DCMId == req.DCMId).CountAsync();
                    var outwardDCDCount = await kUrgeTruckContext.DeliveryChallanDetails.Where(x => x.DCMId == req.DCMId && x.Status== "Delivered").CountAsync();

                    if (outwardDCDList == outwardDCDCount)
                    {
                        foreach (var DC in outwardDCMList)
                        {
                            DC.Status = Outward.Delivered;
                            var grnData = await kUrgeTruckContext.GRN.ToListAsync();
                            foreach (var grn in grnData)
                            {
                                if (grn.GRNId == DC.GRNId && grn.Status == "InStock")
                                {
                                    grn.Status = "Delivered";
                                }
                            }
                        }
                    }
                  }
                    await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess("Delivered successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

     






