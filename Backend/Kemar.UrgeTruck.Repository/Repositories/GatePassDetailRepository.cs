using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using log4net.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class GatePassDetailRepository : IGatePassDetails
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;
        public GatePassDetailRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<List<GatePassDetailResponse>> GetGatePassDetailData()
        {
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var data = await kUrgeTruckContext.GatePassDetails.Include(x => x.GatePassMaster)
                    .ThenInclude(x => x.PurchaseOrderMaster).ThenInclude(x => x.PurchaseOrderDetails).ThenInclude(x => x.ProductMaster).ToListAsync();
                 return _mapper.Map<List<GatePassDetailResponse>>(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultModel> SaveGatePassDetails(List<GatePassDetailsRequest> detailsRequest)
        {
            var resg = "GatePass Details";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var poMaster = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.POId ==detailsRequest.FirstOrDefault().POId);
                var gatelist = await kUrgeTruckContext.GatePassMaster.Include(x=>x.PurchaseOrderMaster).FirstOrDefaultAsync(x => x.POId == detailsRequest.FirstOrDefault().POId);
                var gateDetailsList = await kUrgeTruckContext.GatePassMaster.Include(x => x.GatePassDetails).Where(x => x.GatePassId == detailsRequest.FirstOrDefault().GatePassId).ToListAsync();
                if (gatelist != null && poMaster.Status != PurchaseOrder.Closed)
                {
                    var msg1 = "Can't Create Gate Pass";
                    return ResultModelFactory.CreateFailure(ResultCode.NotAllowed, msg1);
                }
                foreach (var request in detailsRequest)
                {
                    if (request.GPDId != 0)
                    {
                        // Update GatePassDetails
                        var gateDetails = await kUrgeTruckContext.GatePassDetails.FirstOrDefaultAsync(x => x.GPDId == request.GPDId);
                        if (gateDetails != null)
                        {
                            gateDetails.RejectedQuantity = request.RejectedQuantity;
                            kUrgeTruckContext.Update(gateDetails);
                            resg = "Updated " + resg;
                        }
                    }
                    else
                    {
                        request.GatePassId = gatelist.GatePassId;
                        kUrgeTruckContext.GatePassDetails.Add(_mapper.Map<GatePassDetails>(request));
                        await kUrgeTruckContext.SaveChangesAsync();

                    }
                }
                resg = resg + " added Successfully";
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(resg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        public async Task<GatePassDetailResponse> GetSumOfProductQuantity(string productid, int poid)
        {
            try
            {
                using (KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext())
                {
                    int productMasterId = Convert.ToInt32(productid);
                    var result = await kUrgeTruckContext.PurchaseOrderDetails.Include(x=>x.PurchaseOrderMaster)
                        .Where(x => x.ProductMasterId == productMasterId && x.POId == poid)
                        .GroupBy(x => new { x.ProductMasterId, x.POId, x.ProductMaster.PartCode })
                        .Select(g => new
                         GatePassDetailResponse
                        {
                             ProductMasterId = g.Key.ProductMasterId,
                             POId = g.Key.POId,
                             PartCode = g.Key.PartCode,
                             SumQuantity = g.Sum(x => x.Quantity)
                        }).FirstOrDefaultAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}

