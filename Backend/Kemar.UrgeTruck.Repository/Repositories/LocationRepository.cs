using AutoMapper;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.UserManagement;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kemar.UrgeTruck.Domain.Common;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class LocationRepository : ILocation
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;



        public LocationRepository(IKUrgeTruckContextFactory contextFactory,
        IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<LocationListResponse>> GetLocationAsync(string locCode)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<LocationListResponse> LocationList = new List<LocationListResponse>();
                var AllLocation = await kUrgeTruckContext.Location.Include(x => x.LocationCategory).ToListAsync();
                if (string.IsNullOrWhiteSpace(locCode) || locCode == "undefined")
                    LocationList.AddRange(_mapper.Map<List<LocationListResponse>>(AllLocation));
                else
                {
                    var TopLocation = AllLocation.FirstOrDefault(x => x.LocationCode == locCode);
                    if (TopLocation != null)
                    {
                        LocationList.Add(_mapper.Map<LocationListResponse>(TopLocation));
                        if (AllLocation.Exists(x => x.ParentLocationCode == TopLocation.LocationCode))
                        {
                            var Check = AllLocation.Where(x => x.ParentLocationCode == TopLocation.LocationCode).Select(x => x.LocationCode).ToList();
                            if (!LocationList.Exists(x => x.LocationCode == Check[0]) || Check.Count > 1)
                            {
                                await LocRecursion(Check, LocationList, AllLocation);
                            }
                        }
                    }
                }
                return LocationList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #region LocRecursion
        /// <summary>
        /// Moiz khot 29-4-2023
        /// </summary>
        /// For Getting List of Parent Child Location By Searching
        /// <returns></returns>

        public async Task LocRecursion(List<string> locCode, List<LocationListResponse> LocationList, List<Location> AllLocation)
        {
            try
            {
                List<string> locid = new();
                foreach (var item in locCode)
                {
                    var adLoc = AllLocation.Where(x => x.ParentLocationCode == item).ToList();
                    if (!adLoc.Any() || adLoc.Any(x => x.ParentLocationCode == item)) { adLoc = AllLocation.Where(x => x.LocationCode == item).ToList(); }
                    LocationList.AddRange(_mapper.Map<List<LocationListResponse>>(adLoc));
                    locid.AddRange(from ad in adLoc
                                   join al in AllLocation on ad.LocationCode equals al.ParentLocationCode
                                   select al.LocationCode);
                }
                if (locid.Any())
                {
                    await LocRecursion(locid, LocationList, AllLocation);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public async Task<List<LocationListResponse>> GetLocationsdropdownAsync()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<LocationListResponse> LocationList = new List<LocationListResponse>();
                var AllLocation = await kUrgeTruckContext.Location.Where(x => x.IsActive == true).ToListAsync();
                LocationList.AddRange(_mapper.Map<List<LocationListResponse>>(AllLocation));
                return LocationList;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        //public async Task<ResultModel> AddLocationsAsync(LocationRequest request)

        //{
        //    var resMessage = "Location data  ";
        //    try
        //    {
        //        using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
        //        var locationdata = await kUrgeTruckContext.Location.FirstOrDefaultAsync(x => x.LocationId == request.LocationId);
        //        if (locationdata == null)
        //        {
        //            var dublicate = await kUrgeTruckContext.Location.FirstOrDefaultAsync(x => x.LocationCode == request.LocationCode && x.Name == request.Name);

        //            if (dublicate == null)
        //            {
        //                kUrgeTruckContext.Add(_mapper.Map<Location>(request));
        //                resMessage = resMessage + " " + "added successfully.";
        //                await kUrgeTruckContext.SaveChangesAsync();
        //                return ResultModelFactory.CreateSucess(resMessage);
        //            }
        //            else
        //                return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage + "Already Exists");
        //        }
        //        else
        //        {
        //            locationdata.Name = request.Name;
        //            locationdata.LocationCode = request.LocationCode;
        //            locationdata.LocationCategoryId = request.LocationCategoryId;
        //            locationdata.LocationType = request.LocationType;
        //            locationdata.Longitude = request.Longitude;
        //            locationdata.ParentLocationCode = request.ParentLocationCode;
        //            locationdata.IsActive = request.IsActive;
        //            resMessage = resMessage + " " + "Updated Successfully.";

        //            kUrgeTruckContext.Location.Update(locationdata);
        //            await kUrgeTruckContext.SaveChangesAsync();
        //            return ResultModelFactory.UpdateSucess(resMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        public async Task<ResultModel> AddOrUpdateLocationsAsync(LocationRequest request)
        {
            var resMessage = "Location data ";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var locList = await kUrgeTruckContext.Location.ToListAsync();
                if (request.LocationId == null || request.LocationId == 0)
                {
                    if (!locList.Any(x =>
                    x.LocationCode == request.LocationCode || x.Name == request.Name))
                    {
                        //request.CreatedDate = DateTime.Now;
                        kUrgeTruckContext.Add(_mapper.Map<Location>(request));
                        resMessage += UrgeTruckMessages.added_successfully;
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!locList.Any(x => (x.Name == request.Name || x.LocationCode == request.LocationCode) && x.LocationId != request.LocationId))
                    {
                        var location = await kUrgeTruckContext.Location.Where(x => x.LocationId == request.LocationId).FirstOrDefaultAsync();
                        //kUrgeTruckContext.Update(_mapper.Map<Location>(request));
                        location.LocationCategoryId = request.LocationCategoryId;
                        location.LocationCode = request.LocationCode;
                        location.Name = request.Name;
                        location.DIsplayName = request.DIsplayName;
                        location.LocationType = request.LocationType;
                        location.ParentLocationCode = request.ParentLocationCode;
                        location.IsActive = request.IsActive;
                        location.ModifiedDate = DateTime.Now;
                        location.ModifiedBy = request.CreatedBy;
                        resMessage += UrgeTruckMessages.updated_successfully;
                        kUrgeTruckContext.Update(location);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<LocationCategoryResponse>> GetLocationCategoryAsync()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var categoryList = await kUrgeTruckContext.LocationCategory.ToListAsync();
                return _mapper.Map<List<LocationCategoryResponse>>(categoryList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<ResultModel> SaveLocationCategory(LocationCategoryRequest request)
        {
            var resMessage = "Location Category ";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var locList = await kUrgeTruckContext.LocationCategory.ToListAsync();
                if (request.LocationCategoryId == null || request.LocationCategoryId == 0)
                {
                    if (!locList.Any(x => x.LocationCategoryName == request.LocationCategoryName))
                    {
                        kUrgeTruckContext.Add(_mapper.Map<LocationCategory>(request));
                        resMessage += "added successfully.";
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!locList.Any(x => (x.LocationCategoryName == request.LocationCategoryName) && x.LocationCategoryId != request.LocationCategoryId))
                    {
                        var location = await kUrgeTruckContext.LocationCategory.Where(x => x.LocationCategoryId == request.LocationCategoryId).FirstOrDefaultAsync();

                        location.LocationCategoryName = request.LocationCategoryName;
                        resMessage += "updated successfully.";
                        kUrgeTruckContext.Update(location);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<LocationListResponse>> GetGRNDropDownAsync(int userId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var locationIds = kUrgeTruckContext.UserLocationAccess
                    .Where(ula => ula.UserId == userId)
                    .Select(ula => ula.LocationId);

                var locationCodes = kUrgeTruckContext.Location
                    .Where(l => locationIds.Contains(l.LocationId))
                    .Select(l => l.LocationCode)
                    .ToList();

                var locations = kUrgeTruckContext.Location
                    .Where(l => locationCodes.Contains(l.ParentLocationCode));
                return _mapper.Map<List<LocationListResponse>>(locations);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<LocationListResponse>> GerRackSubrackDropDownAsync(string locCode)
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var rackList = await kUrgeTruckContext.Location.Where(x => x.ParentLocationCode == locCode).ToListAsync();
            return _mapper.Map<List<LocationListResponse>>(rackList);
        }

    }
}


