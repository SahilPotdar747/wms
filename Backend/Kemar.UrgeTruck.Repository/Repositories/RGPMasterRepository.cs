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
    public class RGPMasterRepository : IRGPMaster
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public RGPMasterRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public object CreatedBy { get; private set; }

        public async Task<List<RGPMasterResponse>> GetRGPRecord(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<RGPMaster> list = new List<RGPMaster>();
                if (id == 0 || id == null)
                {
                    list = await kUrgeTruckContext.RGPMaster.Include(x => x.GatePassMaster.PurchaseOrderMaster).Include(x => x.GatePassDetails).ToListAsync();
                }
                else
                {
                    list = await kUrgeTruckContext.RGPMaster.Include(x => x.GatePassMaster.PurchaseOrderMaster).Include(x => x.GatePassDetails).Where(x => x.GatePassId == id).ToListAsync();
                }
                return _mapper.Map<List<RGPMasterResponse>>(list);

            }
            catch (Exception ex)
            {
                throw;
            }

        }



        //       public async Task<ResultModel> SaveRGPRecords(List<RGPMasterRequest> request)
        //       {
        //           var msg = "RGP Generated";
        //           try
        //           {
        //               using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
        //               var detailsData = await kUrgeTruckContext.GatePassDetails.Include(x => x.GatePassMaster).Where(x => x.GatePassId == request.FirstOrDefault().GatePassId && x.RejectedQuantity > 0).ToListAsync();
        //
        //               var acceptedQty = await kUrgeTruckContext.GatePassDetails.Include(x => x.GatePassMaster).Where(x => x.GatePassId == request.FirstOrDefault().GatePassId).Select(x => x.AcceptedQuantity).ToListAsync();
        //
        //              foreach (var requests in request)
        //               {
        //                   if (detailsData != null)
        //                   {
        //                       var gatepassDetails = new GatePassDetails
        //                       {
        //                                 GatePassId = acceptedQty,
        //                       },
        //                          kUrgeTruckContext.Add(gatepassDetails);
        //                        }

        //               }
        //               await kUrgeTruckContext.SaveChangesAsync();
        //               return ResultModelFactory.CreateSucess(msg);
        //}
        //           catch (Exception ex)
        //           {
        //               throw;
        //           }
        //       }

        public async Task<ResultModel> SaveRGPRecords(List<RGPMasterRequest> request)
        {
            var msg = "RGP Generated";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                 var gatemasterdata = await kUrgeTruckContext.GatePassMaster.Include(x => x.GatePassDetails).FirstOrDefaultAsync(x => x.GatePassId == request.FirstOrDefault().GatePassId);
                foreach (var requests in request)
                {
                    var receivedqty = await kUrgeTruckContext.GatePassDetails.Where(x => x.GatePassId == requests.GatePassId).Select(x => x.AcceptedQuantity).FirstOrDefaultAsync();
                    var receivedqt = new GatePassDetails
                    {
                        AcceptedQuantity = receivedqty,
                    };

                    var rpgMaster = new RGPMaster
                    {
                        GatePassId = gatemasterdata.GatePassId,
                        CreatedBy = gatemasterdata.CreatedBy,
                    };
                    kUrgeTruckContext.RGPMaster.Add(rpgMaster);
                    msg = msg + UrgeTruckMessages.added;
                    
                }
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(msg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
