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

namespace Kemar.UrgeTruck.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrder
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public PurchaseOrderRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<PurchaseOrderResponse>> GetAllPurchaseOrderRecord(int poId)
       {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<PurchaseOrderResponse> PurchaseOrderList = new List<PurchaseOrderResponse>();
                var ll = await kUrgeTruckContext.PurchaseOrderMaster.Include(x => x.PurchaseOrderDetails).Include(x => x.SupplierMaster).ToListAsync();

                var list = await kUrgeTruckContext.PurchaseOrderMaster.Include(x => x.SupplierMaster).ToListAsync();
                if (poId == null || poId == 0)
                    PurchaseOrderList.AddRange(_mapper.Map<List<PurchaseOrderResponse>>(list));
                else
                {
                    var TopData = list.FirstOrDefault(x => x.POId == poId);
                    if (TopData != null)
                    {
                        PurchaseOrderList.Add(_mapper.Map<PurchaseOrderResponse>(TopData));
                    }
                }
                return PurchaseOrderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private async Task<string> GeneratePONumber()
        //{
        //    using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
        //    var latestPO = await kUrgeTruckContext.PurchaseOrderMaster
        //        .Where(x => x.DeliveryDate.Year == DateTime.Now.Year)
        //        .OrderByDescending(x => x.PONumber)
        //        .FirstOrDefaultAsync();

        //    int UpdatedPO = latestPO != null ? int.Parse(latestPO.PONumber.Split('-')[3]) : 0;
        //    int NextPO = UpdatedPO + 1;

        //    string PONumber = $"PO-{DateTime.Now.Year.ToString("D2")}-{DateTime.Now.Month.ToString("D2")}-{NextPO.ToString("D3")}";
        //    return PONumber;
        //}

        

        public async Task<ResultModel> SavePurchaseOrderRecords(PurchaseOrderRequest request)
        {
            var msg = UrgeTruckMessages.Purchase_Order;
            var resmsg = UrgeTruckMessages.PO_Number;
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();

                if (request.POId != 0)
                {
                    var orderList = await kUrgeTruckContext.PurchaseOrderMaster.Include(x => x.PurchaseOrderDetails).FirstOrDefaultAsync(x => x.PONumber == request.PONumber);
                    if (orderList.Status != PurchaseOrder.Closed)
                    {
                        orderList.DeliveryDate = DateTime.Now;
                        kUrgeTruckContext.PurchaseOrderMaster.Update(orderList);
                        await kUrgeTruckContext.SaveChangesAsync();
                        msg += UrgeTruckMessages.added_successfully;
                    }
                     else
                    {
                        resmsg += "Closed.";
                        return ResultModelFactory.CreateFailure(ResultCode.RecordNotFound, resmsg);
                    }

                }
                else
                {
                    var duplicateCheck = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.PONumber == request.PONumber);
                    if (duplicateCheck != null)
                    {
                        resmsg = "Duplicate PO Number.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resmsg);
                    }
                    request.DeliveryDate = DateTime.Now;
                    kUrgeTruckContext.PurchaseOrderMaster.Add(_mapper.Map<PurchaseOrderMaster>(request));
                    await kUrgeTruckContext.SaveChangesAsync();
                    msg += UrgeTruckMessages.added_successfully;
                }
                return ResultModelFactory.CreateSucess(msg);
             }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PurchaseOrderResponse>> GetDraftRecord(int poId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<PurchaseOrderResponse> purchaseList = new List<PurchaseOrderResponse>();
                var DraftRecord = await kUrgeTruckContext.PurchaseOrderMaster.Include(x => x.PurchaseOrderDetails).Where(x => x.Status == PurchaseOrder.Draft).ToListAsync();
                if (poId == null || poId == 0)
                {
                    purchaseList.AddRange(_mapper.Map<List<PurchaseOrderResponse>>(DraftRecord));
                }
                else
                {
                    var TopGRNData = DraftRecord.FirstOrDefault(x => x.POId == poId);
                    if (TopGRNData != null)
                    {
                        purchaseList.Add(_mapper.Map<PurchaseOrderResponse>(TopGRNData));
                    }
                }
                return purchaseList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




    }
}
