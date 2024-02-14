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
    public class GRNMasterRepository : IGRNMaster
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;
        public GRNMasterRepository(IKUrgeTruckContextFactory contextFactory,IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<List<GRNMasterResponse>> GetGRNMaster(long grnId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNMasterResponse> GRNMasterList = new List<GRNMasterResponse>();
                var AllGRNListdata = await kUrgeTruckContext.GRN.Include(x => x.ProductMaster.ProductCategory).Include(x => x.SupplierMaster).Include(x => x.Location.LocationCategory).Include(x=>x.GRNDetails).ToListAsync();
                if (grnId == null || grnId == 0)
                    GRNMasterList.AddRange(_mapper.Map<List<GRNMasterResponse>>(AllGRNListdata));
                else
                {
                   
                    var TopGRNData = AllGRNListdata.FirstOrDefault(x => x.GRNId == grnId );
                    if (TopGRNData != null)
                    {
                        GRNMasterList.Add(_mapper.Map<GRNMasterResponse>(TopGRNData));
                    }
                }
                return GRNMasterList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region AutoGenerate GRN
        /// <summary>
        /// Pradyumn 
        /// </summary>
        /// the latest GRN number of the current year, 
        /// extracts the numeric part, increments it by 1 to generate the next GRN number, 
        /// and formats it as per the required pattern
        /// <returns></returns>
        private async Task<string> GenerateUniqueGRNNumberAsync()
        {
            using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var latestGRN = await kUrgeTruckContext.GRN
                .Where(x => x.EntryDate.Year == DateTime.Now.Year)
                .OrderByDescending(x => x.GRNNumber)
                .FirstOrDefaultAsync();

            int latestGRNNumber = latestGRN != null ? int.Parse(latestGRN.GRNNumber.Split('-')[3]) : 0;
            int nextGRNNumber = latestGRNNumber + 1;

            string grnNumber = $"GRN-{DateTime.Now.Year.ToString("D4")}-{DateTime.Now.Month.ToString("D2")}-{nextGRNNumber.ToString("D4")}";
            return grnNumber;
        }
        #endregion 

        public async Task<ResultModel> AddORUpdateGRNMasterAsync(GRNMasterRequest requestModel)
        {
            var resMessage = UrgeTruckMessages.GRN;
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var grnList = await kUrgeTruckContext.GRN.ToListAsync();
                if (requestModel.GRNId == null || requestModel.GRNId == 0)
                {
                    if (!grnList.Any(x => x.PONumber == requestModel.PONumber && x.InvoiceNumber == requestModel.InvoiceNumber))
                    {
                        requestModel.GRNNumber = await GenerateUniqueGRNNumberAsync();
                        requestModel.EntryDate = DateTime.Now;
                        //requestModel.Status = "InStock";
                        requestModel.Status = Stocks.InStock;
                        kUrgeTruckContext.Add(_mapper.Map<GRN>(requestModel));
                        resMessage +=UrgeTruckMessages.added_successfully;
                        await kUrgeTruckContext.SaveChangesAsync();

                        // Create a new DeliveryChallanMaster entity
                        var grndata = await kUrgeTruckContext.GRN.OrderBy(x => x.GRNId).Select(x => (int)x.GRNId).LastOrDefaultAsync();
                        var deliveryChallanMaster = new DeliveryChallanMaster
                        {
                            GRNId = grndata, // Set the GRNId as the foreign key
                            //Status = "InStock",
                            Status  =Stocks.InStock,
                            CreatedBy = requestModel.CreatedBy,
                            CreatedDate = requestModel.CreatedDate
                        };

                        // Add the DeliveryChallanMaster entity to the DeliveryChallanMaster table
                        kUrgeTruckContext.Add(deliveryChallanMaster);
                        await kUrgeTruckContext.SaveChangesAsync();

                        return ResultModelFactory.CreateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += " already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!grnList.Any(x => (x.PONumber == requestModel.PONumber || x.GRNNumber == requestModel.GRNNumber) && x.GRNId != requestModel.GRNId))
                    {
                        var grndata = await kUrgeTruckContext.GRN.Where(x => x.GRNId == requestModel.GRNId).FirstOrDefaultAsync();
                        grndata.PONumber = requestModel.PONumber;
                        grndata.POProductQuantity = requestModel.POProductQuantity;
                        grndata.POFile = requestModel.POFile;
                        grndata.InvoiceNumber = requestModel.InvoiceNumber;
                        grndata.InvoiceProductQuantity = requestModel.InvoiceProductQuantity;
                        grndata.InvoiceFile = requestModel.InvoiceFile;
                        grndata.Remark = requestModel.Remark;
                        grndata.EntryDate = DateTime.Now;
                        grndata.IsActive = requestModel.IsActive;
                        grndata.ModifiedDate = DateTime.Now;
                        grndata.ModifiedBy = requestModel.CreatedBy;
                        resMessage += UrgeTruckMessages.updated_successfully;
                        kUrgeTruckContext.Update(grndata);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += " already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}