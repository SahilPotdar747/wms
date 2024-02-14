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
    public class GatePassMasterRepository : IGatePassMaster
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public GatePassMasterRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<GatePassMasterResponse>> GetGatePassMasterData(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GatePassMasterResponse> list = new List<GatePassMasterResponse>();
                var data = await kUrgeTruckContext.GatePassMaster.Include(x => x.SupplierMaster).Include(x => x.PurchaseOrderMaster).ThenInclude(x=>x.PurchaseOrderDetails).ToListAsync();
                if (id == null || id == 0)
                {
                    list.AddRange(_mapper.Map<List<GatePassMasterResponse>>(data));
                }
                else
                {
                    var firstpass = data.FirstOrDefault(x => x.GatePassId == id);
                    if (firstpass != null)
                    {
                        list.Add(_mapper.Map<GatePassMasterResponse>(firstpass));
                    }
                }
                return _mapper.Map<List<GatePassMasterResponse>>(data);

            }
            catch (Exception ex)
            {
                throw;
            }




        }

        private async Task<string> GenerateGatePassNo()
        {
            using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var latestPass = await kUrgeTruckContext.GatePassMaster
                .Where(x => x.GatePassDate.Year == DateTime.Now.Year)
                .OrderByDescending(x => x.GatePassNo)
                .FirstOrDefaultAsync();

            int updatedPassNo = latestPass != null ? int.Parse(latestPass.GatePassNo.Split('-')[3]) : 0;
            int nextPassNo = updatedPassNo + 1;
            string GatePassNo = $"GP-{DateTime.Now.Year.ToString("D2")}-{DateTime.Now.Month.ToString("D2")}-{nextPassNo.ToString("D3")}";

            return GatePassNo;
        }

        



        public async Task<ResultModel> SaveGatePassRecord(GatePassMasterRequest request)
        {
            var msg = UrgeTruckMessages.Gate_Pass_Records;

            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var poMaster = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.POId == request.POId);
                if (poMaster != null && poMaster.Status != PurchaseOrder.Closed)
                {
                    var msg1 = "Can't Create Gate Pass";
                    return ResultModelFactory.CreateFailure(ResultCode.NotAllowed, msg1);
                }
                var gatemaster = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster)
                        .ThenInclude(x => x.SupplierMaster).ToListAsync();
                if (request.GatePassId == 0)
                {
                    ///var list = await kUrgeTruckContext.GatePassMaster.Include(x => x.GatePassDetails).FirstOrDefaultAsync(x => x.GatePassNo == request.GatePassNo);
                    var duplicate = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster).FirstOrDefaultAsync(x => x.GatePassNo == request.GatePassNo && x.POId == request.POId);
                    if (duplicate == null)
                    {
                        request.GatePassNo = await GenerateGatePassNo();
                        request.GatePassDate = DateTime.Now;
                        request.Status = "Gate Pass Generated";
                        kUrgeTruckContext.GatePassMaster.Add(_mapper.Map<GatePassMaster>(request));
                        //await kUrgeTruckContext.SaveChangesAsync();
                        msg += UrgeTruckMessages.added_successfully;
                    }
                    else
                    {
                        var resmsg = "Duplicate GatePassNumber.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resmsg);
                    }
                }
                else
                {
                    var updatedata = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster).
                        Include(x => x.SupplierMaster).FirstOrDefaultAsync(x => x.GatePassId == request.GatePassId);
                    if (updatedata != null)
                    {
                        
                        updatedata.POId = request.POId;
                        updatedata.DeliveryMode = request.DeliveryMode;
                        updatedata.RGPGenerated = request.RGPGenerated;
                        updatedata.Status = request.Status;
                        updatedata.InvoiceNo = request.InvoiceNo;
                        updatedata.ModifiedBy = request.ModifiedBy;
                        updatedata.ModifiedDate = request.ModifiedDate;
                        kUrgeTruckContext.GatePassMaster.Update(updatedata);
                        msg += UrgeTruckMessages.updated_successfully;

                    }
                }
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(msg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<ResultModel> SaveGatePassRecords(List<GatePassMasterRequest> requests)
        //{
        //    var msg = UrgeTruckMessages.Gate_Pass_Records;
        //    try
        //    {
        //        using var kUrgeTruckContext = _contextFactory.CreateKGASContext();

        //        foreach (var request in requests)
        //        {
        //            var poMaster = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.POId == request.POId);
        //            if (poMaster != null && poMaster.Status != PurchaseOrder.Closed)
        //            {
        //                var msg1 = "Can't Create Gate Pass";
        //                return ResultModelFactory.CreateFailure(ResultCode.NotAllowed, msg1);
        //            }

        //            var gatemaster = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster)
        //                .ThenInclude(x => x.SupplierMaster).ToListAsync();

        //            if (request.GatePassId == 0)
        //            {
        //                var duplicate = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster)
        //                    .FirstOrDefaultAsync(x => x.GatePassNo == request.GatePassNo && x.POId == request.POId);

        //                if (duplicate == null)
        //                {
        //                    request.GatePassNo = await GenerateGatePassNo();
        //                    request.GatePassDate = DateTime.Now;
        //                    request.Status = "Gate Pass Generated";
        //                    kUrgeTruckContext.GatePassMaster.Add(_mapper.Map<GatePassMaster>(request));
        //                    await kUrgeTruckContext.SaveChangesAsync();
        //                    msg += UrgeTruckMessages.added_successfully;
        //                }
        //                else
        //                {
        //                    var resmsg = "Duplicate GatePassNumber.";
        //                    return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resmsg);
        //                }
        //            }
        //            else
        //            {
        //                var updatedata = await kUrgeTruckContext.GatePassMaster.Include(x => x.PurchaseOrderMaster)
        //                    .Include(x => x.SupplierMaster).FirstOrDefaultAsync(x => x.GatePassId == request.GatePassId);

        //                if (updatedata != null)
        //                {
        //                    updatedata.SupplierId = request.SupplierId;
        //                    updatedata.POId = request.POId;
        //                    updatedata.DeliveryMode = request.DeliveryMode;
        //                    updatedata.RGPGenerated = request.RGPGenerated;
        //                    updatedata.Status = request.Status;
        //                    updatedata.InvoiceNo = request.InvoiceNo;
        //                    updatedata.ModifiedBy = request.ModifiedBy;
        //                    updatedata.ModifiedDate = request.ModifiedDate;
        //                    kUrgeTruckContext.GatePassMaster.Update(updatedata);
        //                    msg += UrgeTruckMessages.updated_successfully;
        //                }
        //            }
        //        }

        //        await kUrgeTruckContext.SaveChangesAsync();
        //        return ResultModelFactory.CreateSucess(msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}




    }
}
