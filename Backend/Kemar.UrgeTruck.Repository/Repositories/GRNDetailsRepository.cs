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
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class GRNDetailsRepository : IGRNDetails
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;


        public GRNDetailsRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        //public async Task<List<GRNDetailsResponse>> GetGRNDetailsAsync(long id)
        //{
        //    try
        //    {
        //        using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
        //        List<GRNDetailsResponse> GRNDetailsList = new List<GRNDetailsResponse>();
        //        var grnDetails = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).ToListAsync();
        //        if (id == null || id == 0)
        //        {
        //            GRNDetailsList.AddRange(_mapper.Map<List<GRNDetailsResponse>>(grnDetails));
        //        }
        //        else
        //        {
        //            var TopGRNData = grnDetails.Where(x => x.GRNDetailsId == id).ToList();
        //            if (TopGRNData != null)
        //            {

        //                return _mapper.Map<List<GRNDetailsResponse>>(TopGRNData);
        //            }
        //        }
        //        return GRNDetailsList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        public async Task<List<GRNDetailsResponse>> GetGRNDetailsAsync(long id)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNDetails> gRNDetails = new List<GRNDetails>();
                if (id == null || id == 0)
                {
                    gRNDetails = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).ToListAsync();
                }
                else
                {
                    gRNDetails = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).Where(x => x.GRNDetailsId == id).ToListAsync();
                }
                return _mapper.Map<List<GRNDetailsResponse>>(gRNDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GRNDetailsResponse>> GetAllGrnDetails()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<GRNDetails> gRNDetail= new List<GRNDetails>();
               gRNDetail = await kUrgeTruckContext.GRNDetails.Include(x => x.GRN).ToListAsync();
                return _mapper.Map<List<GRNDetailsResponse>>(gRNDetail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// add and update both operations in one method
        public async Task<ResultModel> addGRNDetails(List<GRNDetailsRequest> requestModels)
        {
            var resMessage = UrgeTruckMessages.GRN_Details;
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();

                // Get the latest GRN ID from GRNMaster table
                var latestGRNId = await kUrgeTruckContext.GRN.OrderBy(x => x.GRNId).Select(x => x.GRNId).LastOrDefaultAsync();
                var grnList = await kUrgeTruckContext.GRNDetails.ToListAsync();
                var grnId = latestGRNId; // Initialize the GRNID variable

                foreach (var requestModel in requestModels)
                {
                    if (requestModel.GRNDetailsId == 0 || requestModel.GRNDetailsId == null)
                    {
                        if (!grnList.Any(x => x.ProductSerialKey == requestModel.ProductSerialKey))
                        {
                            requestModel.GRNId = grnId;
                            //requestModel.Status = "InStock";
                            requestModel.Status = Stocks.InStock;
                            // Add the GRNDetails entity to the GRNDetails table
                            kUrgeTruckContext.Add(_mapper.Map<GRNDetails>(requestModel));
                            await kUrgeTruckContext.SaveChangesAsync();
}
                        else
                        {
                            var proserialkey = "ProductSerialKey already exists ";
                            return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, proserialkey);
                        }
                        
                    }
                    else
                    {
                        var grnData = await kUrgeTruckContext.GRNDetails.Where(x => x.GRNDetailsId == requestModel.GRNDetailsId).FirstOrDefaultAsync();
                        if (grnData.ProductSerialKey == requestModel.ProductSerialKey && grnData.GRNDetailsId == requestModel.GRNDetailsId)
                        {

                            requestModel.GRNId = await kUrgeTruckContext.GRN.OrderBy(x => x.GRNId).Select(x => (int)x.GRNId).LastOrDefaultAsync();
                            grnData.DropLoc = requestModel.DropLoc;
                            grnData.RackNo = requestModel.RackNo;
                            grnData.SubRack = requestModel.SubRack;
                            grnData.IsActive = requestModel.IsActive;
                            kUrgeTruckContext.GRNDetails.Update(grnData);
                            resMessage = UrgeTruckMessages.updated_successfully;
                        }
                        else
                        {
                            var msg = "Product key does not match";
                            return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, msg);
                        }
                    }
                }
                resMessage += UrgeTruckMessages.added_successfully;
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(resMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }








    }
}
