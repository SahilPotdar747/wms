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
using System.Threading;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class PurchaseOrderDetailsRepository : IPurchaseOrderDetails
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public PurchaseOrderDetailsRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<PurchaseOrderDetailsResponse>> GetPurchaseOrderDetails(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<PurchaseOrderDetails> purchaseDetails = new List<PurchaseOrderDetails>();
                if (id == null || id == 0)
                {
                    purchaseDetails = await kUrgeTruckContext.PurchaseOrderDetails.Include(x => x.PurchaseOrderMaster).Include(x => x.ProductMaster).ToListAsync();
                }
                else
                {
                    purchaseDetails = await kUrgeTruckContext.PurchaseOrderDetails.Include(x => x.PurchaseOrderMaster).Include(x => x.ProductMaster).Where(x => x.POId == id).ToListAsync();
                }
                return _mapper.Map<List<PurchaseOrderDetailsResponse>>(purchaseDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<string> GeneratePONumber()
        {
            using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var latestPO = await kUrgeTruckContext.PurchaseOrderMaster
                .Where(x => x.DeliveryDate.Year == DateTime.Now.Year)
                .OrderByDescending(x => x.PONumber)
                .FirstOrDefaultAsync();

            int UpdatedPO = latestPO != null ? int.Parse(latestPO.PONumber.Split('-')[3]) : 0;
            int NextPO = UpdatedPO + 1;

            string PONumber = $"PO-{DateTime.Now.Year.ToString("D2")}-{DateTime.Now.Month.ToString("D2")}-{NextPO.ToString("D3")}";
            return PONumber;
        }


        public async Task<ResultModel> SavePODetails(List<PurchaseOrdeDetailsrRequest> purchadeOrders)
        {
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var resg = UrgeTruckMessages.Purchase_Order_Details;
                var msg = UrgeTruckMessages.Purchase_Order;

                var poMaster = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.PONumber == purchadeOrders.FirstOrDefault().PONumber);
                if (poMaster != null && poMaster.Status == PurchaseOrder.Closed)
                {
                    resg = "PO Closed.";
                    return ResultModelFactory.CreateFailure(ResultCode.RecordNotFound, resg);
                }
                foreach (var purchaseOrder in purchadeOrders)
                {
                    if (purchaseOrder.PODId != 0)
                    {
                        var poDetail = await kUrgeTruckContext.PurchaseOrderDetails.FirstOrDefaultAsync(x => x.PODId == purchaseOrder.PODId);
                        if (poDetail != null)
                        {
                            poDetail.ProductMasterId = purchaseOrder.ProductMasterId;
                            poDetail.Status = purchaseOrder.Status;
                            poDetail.Quantity = purchaseOrder.Quantity;
                            poDetail.Amount = purchaseOrder.Amount;
                            poDetail.ModifiedBy = purchaseOrder.ModifiedBy;
                            poDetail.ModifiedDate = purchaseOrder.ModifiedDate;
                            kUrgeTruckContext.PurchaseOrderDetails.Update(poDetail);
                            resg += UrgeTruckMessages.added_successfully;
                        }
                    }
                    else
                    {
                         purchaseOrder.POId = poMaster.POId;
                           kUrgeTruckContext.PurchaseOrderDetails.Add(_mapper.Map<PurchaseOrderDetails>(purchaseOrder));
                        //resg = "Added " + resg;
                        resg += UrgeTruckMessages.added_successfully;
                        await kUrgeTruckContext.SaveChangesAsync();
                       }
                        
                    }
                var poMasterStatus = purchadeOrders.Where(x => x.Status == "Draft").ToList();
                if (poMasterStatus.Count == 0)
                {
                    var poMasterDtls = await kUrgeTruckContext.PurchaseOrderMaster.FirstOrDefaultAsync(x => x.POId == purchadeOrders[0].POId);
                    if (poMasterDtls != null)
                    {
                        poMasterDtls.Status = PurchaseOrder.Closed;
                        kUrgeTruckContext.PurchaseOrderMaster.Update(poMasterDtls);
                        msg += UrgeTruckMessages.updated_successfully;
                     }
                }
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(resg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      }
}















