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
    public class DeliveryChallanRepository : IDeliveryChallan
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public DeliveryChallanRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<List<DeliveryChallanMasterResponse>> GetDeliveryChallanRecord(int deliveryChallanId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<DeliveryChallanMasterResponse> DeliveryMasterList = new List<DeliveryChallanMasterResponse>();
                var deliverydata = await kUrgeTruckContext.DeliveryChallanMaster.Include(x => x.GRN.ProductMaster.ProductCategory).Include(x => x.GRN.SupplierMaster).ToListAsync();


                if (deliveryChallanId == null || deliveryChallanId == 0)
                    DeliveryMasterList.AddRange(_mapper.Map<List<DeliveryChallanMasterResponse>>(deliverydata));
                else
                {
                    var TopDeliveryData = deliverydata.FirstOrDefault(x => x.DCMId == deliveryChallanId);
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

        #region AutoGenerate ChallanNumber
        /// <summary>
        /// Pradyumn 
        /// </summary>
        /// the latest Challan number of the current year, 
        /// extracts the numeric part, increments it by 1 to generate the next Challan number, 
        /// and formats it as per the required pattern
        /// <returns></returns>
        private async Task<string> GenerateUniqueChallanNumberAsync()
        {
            using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var latestChallan = await kUrgeTruckContext.DeliveryChallanDetails
             .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
             .OrderByDescending(x => x.ChallanNumber)
             .FirstOrDefaultAsync();


            string yearPrefix = (DateTime.Now.Year % 100).ToString("D2");
            int latestChallanNumber = latestChallan != null ? int.Parse(latestChallan.ChallanNumber.Split('-')[1]) : 0;
            int nextChallanNumber = latestChallanNumber + 1;

            string challanNumber = $"{yearPrefix}-{nextChallanNumber.ToString("D4")}";
            return challanNumber;
        }
        #endregion


        public async Task<ResultModel> SaveDeliveryChallanDetails(List<DeliveryChallanDetailsRequest> requestModels)
        {
            var resMessage = "DeliveryChallan";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();

                var dcMList = await kUrgeTruckContext.DeliveryChallanMaster
                    .Include(x => x.DeliveryChallanDetails)
                    .ToListAsync();

                var dcDList = await kUrgeTruckContext.DeliveryChallanDetails.ToListAsync();

                var newChallanNumber = await GenerateUniqueChallanNumberAsync();

                foreach (var requestModel in requestModels)
                {
                    if (requestModel.DCDId == 0 || requestModel.DCDId == null)
                    {
                        if (!dcDList.Any(x => x.GRNDetailsId == requestModel.GRNDetailsId))
                        {
                            var existingDCMaster = dcMList.FirstOrDefault(x => x.DCMId == requestModel.DCMId);
                            if (existingDCMaster != null)
                            {
                                requestModel.ChallanNumber = newChallanNumber;
                                requestModel.Status = DeliveryChallan.DCGenerated;
                                kUrgeTruckContext.Add(_mapper.Map<DeliveryChallanDetails>(requestModel));
                                await kUrgeTruckContext.SaveChangesAsync();

                                var grndeatilsdatas = await kUrgeTruckContext.GRNDetails.ToListAsync();
                                foreach (var grndeatilsdata in grndeatilsdatas)
                                {
                                    if (grndeatilsdata.GRNDetailsId == requestModel.GRNDetailsId)
                                    {
                                        grndeatilsdata.Status = DeliveryChallan.DCGenerated;
                                    }
                                }

                                // Update DcStatus column of DeliveryChallanMaster table
                                var fullDcMIds = await kUrgeTruckContext.DeliveryChallanMaster
                                    .Where(x => x.DeliveryChallanDetails.Count == x.GRN.GRNDetails.Count)
                                    .Select(x => x.DCMId)
                                    .ToListAsync();

                                var partDcMIds = await kUrgeTruckContext.DeliveryChallanMaster
                                    .Where(x => x.DeliveryChallanDetails.Count < x.GRN.GRNDetails.Count)
                                    .Select(x => x.DCMId)
                                    .ToListAsync();

                                var fullDcMs = await kUrgeTruckContext.DeliveryChallanMaster
                                    .Where(x => fullDcMIds.Contains(x.DCMId))
                                    .ToListAsync();

                                var partDcMs = await kUrgeTruckContext.DeliveryChallanMaster
                                    .Where(x => partDcMIds.Contains(x.DCMId))
                                    .ToListAsync();
                                var deliveryChallanList = await kUrgeTruckContext.DeliveryChallanMaster.ToListAsync();
                                foreach (var fullDcM in fullDcMs)
                                {
                                    if (fullDcM.DCMId == requestModel.DCMId)
                                    {
                                        fullDcM.DcStatus = DeliveryChallan.FullDelivery;
                                        fullDcM.DeliveryDate = DateTime.Now;
                                    }
                                }
                                foreach (var partDcM in partDcMs)
                                {
                                    if(partDcM.DCMId==requestModel.DCMId)
                                    {
                                        partDcM.DcStatus = DeliveryChallan.PartDelivery;
                                        partDcM.DeliveryDate = DateTime.Now;
                                    }
                                 }
                                
                            }
                            else
                            {
                                var proserialkey = "ProductSerialKey already exists ";
                                return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, proserialkey);
                            }
                        }
                        else
                        {
                            var proserialkey = " already exists ";
                            return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, proserialkey);
                        }
                    }
                    else
                    {
                        var msg = "Product key does not match";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, msg);
                    }
                }

                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(resMessage + " added successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<GRNDetailsResponse>> GetInStockGRNDetails(int id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNDetailsResponse> GRNDetailsList = new List<GRNDetailsResponse>();
                var grnDetails = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).Where(x => x.Status ==Stocks.InStock).ToListAsync();
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






    }
}





    
    
