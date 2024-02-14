
/****** Object:  StoredProcedure [dbo].[sp_GetDashboardCardData]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


      
CREATE         proc [dbo].[sp_GetDashboardCardData]      
as      
begin      
      
declare @VehicleInParking int = 0      
declare @VehicleUnallocateCount int = 0      
declare @VehicleAllocateCount int = 0      
declare @VehicleCalledInCount int = 0      
      
      
declare @VehicleParkingOutTodays int = 0      
declare @VehiclePlantOutTodays int = 0      
declare @VehicleInPlantTodays int = 0      
declare @VehicleInParkingtodays int = 0      
      
      
declare @VehicleInPlant int = 0      
declare @VehicleInboundCount int = 0      
declare @VehicleOutboundCount int = 0      
      
declare @VehicleInternalCount int = 0      
declare @VehicleInboundTran int = 0      
declare @VehicleOutboundTran int = 0      
      
declare @VehicleMonthlyTat int = 0      
declare @VehicleMonthlyMinTat int = 0      
declare @VehicleMonthlyMaxTat int = 0      
declare @VehicleMonthlyMinTatStr nvarchar(50)  =  ''       
declare @VehicleMonthlyMaxTatStr nvarchar(50)  =  ''       
declare @VehicleMonthlyTatStr nvarchar(50)  =  ''       
      
declare @VehicleOutboundMonthlyTran int = 0      
declare @VehicleInboundMonthlyTran int = 0      
declare @VehicleInternalMonthlyTran int =0     
    
declare @VehicleMonthlyTran int = 0      
declare @VehicleInPlantPercent int = 0       
declare @VehicleInParkingPercent int = 0  
      


declare @today datetime = (SELECT DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)) 
declare @currMonth datetime = (SELECT DATEADD(m, DATEDIFF(m, 0, GETDATE()), 0))    
declare @nextMonth datetime = (SELECT dateadd(second , -1, DATEADD(month, DATEDIFF(month, 0, getdate())+1, 0) ) AS EndOfMonth)  
declare @todate datetime = getDate();
set @todate =  dateadd(ms, -3, (dateadd(day, +1, convert(varchar, @todate, 101))))   
--select @VehicleInParking = count(VehicleId) from ELV where ExitDateTime is null and EntryDateTime is not null      
--select @VehicleUnallocateCount = count(VehicleId) from ELV where EntryDateTime is not null and ExitDateTime is null and VehicleTransactionId < 1      
--select @VehicleAllocateCount = count(VehicleId) from ELV where EntryDateTime is not null and ExitDateTime is null and VehicleTransactionId > 0      
--select @VehicleCalledInCount = count(VehicleId) from ELV where Status = 'CalledIn' and ExitDateTime is null      
      
select @VehicleInParking = count(VehicleId) from ELV where Status = 'CalledIn' or Status = 'InParking'      
select @VehicleUnallocateCount = count(VehicleId) from ELV where  Status = 'InParking' and VehicleTransactionId = 0      
select @VehicleAllocateCount = count(VehicleId) from ELV where  Status = 'InParking' and VehicleTransactionId > 0      
select @VehicleCalledInCount = count(VehicleId) from ELV where Status = 'CalledIn' and ExitDateTime is null      
      
      
select @VehicleInPlant = count(VRN) from VehicleTransaction where TransactionStartTime is not null and TransactionEndTime is null      
select @VehicleInboundCount = count(VRN) from VehicleTransaction where TranType = '2' and  TransactionStartTime is not null  and TransactionEndTime is null      
select @VehicleOutboundCount = count(VRN) from VehicleTransaction where TranType = '1' and  TransactionStartTime is not null  and TransactionEndTime is null      
select @VehicleInternalCount = count(VRN) from VehicleTransaction where TranType = '3' and  TransactionStartTime is not null  and TransactionEndTime is null      
      
 select @VehicleInParkingtodays = count(VRN) from ELV where EntryDateTime between dateadd(DAY, Datediff(DAY, 0, Getdate()) , 0)      
 and DATEADD(second, -1, Dateadd(DAY, Datediff(DAY, 0, Getdate() + 1) , 0)) ;      
      
 select @VehicleParkingOutTodays = count(VRN) from ELV where ExitDateTime between dateadd(DAY, Datediff(DAY, 0, Getdate()) , 0)      
 and DATEADD(second, -1, Dateadd(DAY, Datediff(DAY, 0, Getdate() + 1) , 0)) ;      
      
  select @VehicleInPlantTodays = count(VRN) from VehicleTransaction where TransactionStartTime between dateadd(DAY, Datediff(DAY, 0, Getdate()) , 0)      
 and DATEADD(second, -1, Dateadd(DAY, Datediff(DAY, 0, Getdate() + 1) , 0)) ;      
      
  select @VehiclePlantOutTodays = count(VRN) from VehicleTransaction where TransactionEndTime between dateadd(DAY, Datediff(DAY, 0, Getdate()) , 0)      
 and DATEADD(second, -1, Dateadd(DAY, Datediff(DAY, 0, Getdate() + 1) , 0)) ;      
      
select @VehicleUnallocateCount = count(VehicleId) from ELV where  Status = 'InParking' and VehicleTransactionId = 0      
select @VehicleAllocateCount = count(VehicleId) from ELV where  Status = 'InParking' and VehicleTransactionId > 0      
select @VehicleCalledInCount = count(VehicleId) from ELV where Status = 'CalledIn' and ExitDateTime is null      
      
      
--select @VehicleOutExceptionCount = count(VehicleId) from TBGATE_ENTRY where IsException = 1      
--select @VehicleOutEmployeeCount = count(PersonTagNo) from TBGATE_ENTRY where PersonTagNo is not null      
select @VehicleMonthlyMaxTat = max(dateDiff(MINUTE,TransactionStartTime,TransactionEndTime))  from VehicleTransaction where      
(TransactionStartTime is not null and   TransactionStartTime >= @currMonth)       
and ( TransactionEndTime is not null and  TransactionEndTime <= @nextMonth)       
      
if @VehicleMonthlyMaxTat is null set @VehicleMonthlyMaxTat = 0       
      
      
      
       
select @VehicleMonthlyMinTat = min(dateDiff(MINUTE,TransactionStartTime,TransactionEndTime)) from VehicleTransaction where      
(TransactionStartTime is not null and  TransactionStartTime >= @currMonth)      
and (TransactionEndTime is not null and TransactionEndTime <= @nextMonth)      
if @VehicleMonthlyMinTat is null set @VehicleMonthlyMinTat = 0       
      
select @VehicleMonthlyTat = ((@VehicleMonthlyMaxTat+ @VehicleMonthlyMinTat)/2) from VehicleTransaction      
      
select  @VehicleMonthlyMaxTatStr = dbo.fn_ConvertMinutesToString(@VehicleMonthlyMaxTat)      
select  @VehicleMonthlyMinTatStr = dbo.fn_ConvertMinutesToString(@VehicleMonthlyMinTat)      
select  @VehicleMonthlyTatStr = dbo.fn_ConvertMinutesToString(@VehicleMonthlyTat)      
      
select @VehicleInboundTran = count(VehicleId) from ELV where TranType = 1      
select @VehicleOutboundTran = count(VehicleId) from ELV where TranType = 1      
--select @VehicleTodayEntry = count(VehicleTagNo) from TBGATE_ENTRY where Gate_OutTime is null and Gate_InTime > @today      
--select @VehiclePrevious = count(VehicleTagNo) from TBGATE_ENTRY where Gate_OutTime is null and Gate_InTime < @today    

--########################################################## Fetching Montly Transaction data ###########################################--
--select @VehicleMonthlyTran = count(VRN) from VehicleTransaction where TransactionEndTime < @nextMonth and TransactionStartTime > @currMonth      
--select @VehicleOutboundMonthlyTran = count(VRN) from VehicleTransaction where TranType = 1 and TransactionEndTime < @nextMonth and TransactionStartTime > @currMonth      
--select @VehicleInboundMonthlyTran = count(VRN) from VehicleTransaction where TranType = 2 and TransactionEndTime < @nextMonth and TransactionStartTime > @currMonth      
--select @VehicleInternalMonthlyTran = count(VRN) from VehicleTransaction where TranType = 4 and TransactionEndTime < @nextMonth --and TransactionStartTime > @currMonth     
--########################################################## Fetching Montly Transaction data ###########################################--

--############################################### Fetching Daily Transaction Completed Date ###############################################-- 
select @VehicleMonthlyTran = count(VRN) from VehicleTransaction where TransactionEndTime >=  @today and TransactionStartTime <= @todate    
select @VehicleOutboundMonthlyTran = count(VRN) from VehicleTransaction where TranType = 1 and TransactionEndTime >=  @today and TransactionStartTime <= @todate      
select @VehicleInboundMonthlyTran = count(VRN) from VehicleTransaction where TranType = 2 and TransactionEndTime >=  @today and TransactionStartTime <= @todate     
select @VehicleInternalMonthlyTran = count(VRN) from VehicleTransaction where TranType = 4 and TransactionEndTime >=  @today and TransactionStartTime <= @todate 
--############################################### Fetching Daily Transaction Completed Date ###############################################-- 



--select @VehicleException = count(VehicleId) from TBGATE_ENTRY where IsException = 1      
--select @VehicleEntry = count(VehicleTagNo) from TBGATE_ENTRY where Gate_OutTime is null      
--select @VehicleExit = count(VehicleTagNo) from TBGATE_ENTRY where Gate_OutTime is null      
Declare @ParkingCapacity int = (select sum(MaxQueueSize) from Location where LocationType = 'OSPP')      
Declare @PlantCapacity int = (select sum(MaxQueueSize) from Location where       
(LocationType != 'OSPP' and  LocationType is not null and  MaxQueueSize is not null) )      
      
set @VehicleInParkingPercent = cast ((@VehicleInParking / cast (@ParkingCapacity as decimal )) * 100 as int )      
set @VehicleInPlantPercent =  cast ((@VehicleInPlant / cast (@PlantCapacity as decimal )) * 100  as int )      
      
select      
@VehicleInParking VehicleInParking,      
@VehicleUnallocateCount VehicleUnallocateCount,      
@VehicleAllocateCount VehicleAllocateCount,      
@VehicleCalledInCount VehicleCalledInCount,      
--select    
@VehicleInPlant VehicleInPlant,      
@VehicleInboundCount VehicleInboundCount,      
@VehicleOutboundCount VehicleOutboundCount,      
@VehicleInternalCount VehicleInternalCount,      
@VehicleMonthlyTran VehicleMonthlyTran,      
@VehicleOutboundTran VehicleOutboundTran,      
@VehicleInboundTran VehicleInboundTran,      
@VehicleMonthlyTat VehicleMonthlyTat,      
@VehicleMonthlyMaxTat VehicleMonthlyMaxTat,      
@VehicleMonthlyMinTat VehicleMonthlyMinTat,      
@VehicleMonthlyTatStr VehicleMonthlyTatStr,      
@VehicleMonthlyMaxTatStr VehicleMonthlyMaxTatStr,      
@VehicleMonthlyMinTatStr VehicleMonthlyMinTatStr,      
@VehicleInboundMonthlyTran VehicleInboundMonthlyTran,      
@VehicleOutboundMonthlyTran VehicleOutboundMonthlyTran,      
@VehicleInternalMonthlyTran  VehicleInternalMonthlyTran,    
@VehicleParkingOutTodays VehicleParkingOutTodays,      
@VehicleInParkingtodays VehicleInParkingtodays,      
@VehiclePlantOutTodays VehiclePlantOutTodays,      
@VehicleInPlantTodays VehicleInPlantTodays,       
@VehicleInParkingPercent VehicleInParkingPercent,       
@VehicleInPlantPercent VehicleInPlantPercent      
      
END  --SP ENDS 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDashBoardData]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDashBoardData]    Script Date: 30-Apr-22 6:08:54 PM ******/

CREATE      procedure [dbo].[sp_GetDashBoardData]
@LocationCode varchar(50)
as
begin

Declare @OutputTable table
(
VehicleTransactionId int,
MilestoneSequenceId int
)

insert
@OutputTable(VehicleTransactionId , MilestoneSequenceId)
Select
JM.VehicleTransactionId , max(JM.MilestoneSequence) MilestoneSequenceId From JobMilestones JM
where
((JM.Status = 'Open' and JM.MilestoneBeginTime is not null) or (jm.Status = 'Completed' ))
group by
JM.VehicleTransactionId

IF (@LocationCode ='All')

begin
select top 5
VT.VRN, VT.TransactionDate , VT.TranType , VT.VehicleTransactionCode,
JM.Milestone , JM.MilestioneEvent, JM.LocationCode, LM.LocationName, VT.VehicleTransactionId,
JM.Status, JM.MilestoneBeginTime , JM.MilestoneCompletionTime
from @OutputTable OT
join VehicleTransaction VT on OT.VehicleTransactionId = VT.VehicleTransactionId
join JobMilestones JM on JM.VehicleTransactionId = OT.VehicleTransactionId and JM.MilestoneSequence = OT.MilestoneSequenceId
join Location LM on JM.LocationCode = LM.LocationCode
order by
case when MilestoneCompletionTime is null then MilestoneBeginTime else MilestoneCompletionTime end desc
end
else
begin
select top 5
VT.VRN, VT.TransactionDate , VT.TranType , VT.VehicleTransactionCode,
JM.Milestone , JM.MilestioneEvent, JM.LocationCode, LM.LocationName, VT.VehicleTransactionId,
JM.Status, JM.MilestoneBeginTime , JM.MilestoneCompletionTime
from @OutputTable OT
join VehicleTransaction VT on OT.VehicleTransactionId = VT.VehicleTransactionId
join JobMilestones JM on JM.VehicleTransactionId = OT.VehicleTransactionId and JM.MilestoneSequence = OT.MilestoneSequenceId
join location LM on JM.LocationCode = LM.LocationCode
where
jm.LocationCode = @LocationCode
order by
case when MilestoneCompletionTime is null then MilestoneBeginTime else MilestoneCompletionTime end desc
end

end
GO
/****** Object:  StoredProcedure [dbo].[SpApiTransactionReport]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[SpApiTransactionReport]
@FromDate varchar(20) = null ,
@Todate varchar(20) = null 
as
begin
declare @FromDateConvrt DateTime = convert(datetime,@FromDate)
declare @ToDateConvrt DateTime = convert(datetime,@ToDate)
select ts.RequestJson, ts.RequestId, ts.InitiatedTime, ts.TransactionStatus, ts.CompletedTime, ts.ResponseMessage from ThirdPartyServiceIntegrationTracking ts
where  (@FromDateConvrt is null or (ts.InitiatedTime is not null and ts.InitiatedTime >= @FromDate))
and (@ToDateConvrt is null or (ts.InitiatedTime is not null and ts.InitiatedTime <= @ToDate))
end
GO
/****** Object:  StoredProcedure [dbo].[SpGateReport]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[SpGateReport]  Script Date: 30-Apr-22 6:08:54 PM ******/

CREATE     Procedure [dbo].[SpGateReport]
@FromDate varchar(20) = null ,
@Todate varchar(20) = null ,
@deafualt varchar(20) = null
As
Begin
declare @FromDateConvrt DateTime = convert(datetime,@FromDate)
declare @ToDateConvrt DateTime = convert(datetime,@ToDate)

select

VehicleTransactionCode as transCode ,
GateEntryNo as gateEntry,
VRN as VRN,
TranType as TranType,
TransactionEndTime as TransactionEndTime ,
TransactionStartTime as TransactionStartTime

from VehicleTransaction
where (@FromDateConvrt is null or (TransactionStartTime is not null and TransactionStartTime >= @FromDate))
and (@ToDateConvrt is null or (TransactionEndTime is not null and TransactionEndTime <= @ToDate))
end
GO
/****** Object:  StoredProcedure [dbo].[SpTatReport]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SpTatReport]  Script Date: 30-Apr-22 6:08:54 PM ******/

CREATE     Procedure [dbo].[SpTatReport]
@FromDate varchar(20) = null ,
@Todate varchar(20) = null ,
@deafualt varchar(20) = null
as
begin
declare @FromDateConvrt DateTime = convert(datetime,@FromDate)
declare @ToDateConvrt DateTime = convert(datetime,@ToDate)
select

VehicleTransactionCode as VehicleTransactionCode ,
GateEntryNo as GateEntryNo,
VRN as VRN,
DriverId = dm.Name,
TranType as TranType,
TransactionStartTime as TransactionStartTime,
TransactionEndTime as TransactionEndTime,
(select
right(REPLICATE('0' , 10) + CAST(FLOOR(datediff(second ,TransactionStartTime , TransactionEndTime) / 86400) AS VARCHAR(10)) , 2)+':' +
CONVERT(VARCHAR(5), DATEADD(SECOND, datediff(second ,TransactionStartTime , TransactionEndTime), '19000101'), 8)
) Duration

from VehicleTransaction vt
join DriverMaster dm on vt.DriverId = dm.DriverId
where TransactionStartTime is not null and TransactionEndTime is not null
and (@FromDateConvrt is null or (TransactionStartTime is not null and TransactionStartTime >= @FromDate))
and (@ToDateConvrt is null or (TransactionEndTime is not null and TransactionEndTime <= @ToDate))

end
GO
/****** Object:  StoredProcedure [dbo].[USP_ControlTowerActionDashboard]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ControlTowerActionDashboard]   
 @AssignedUserId AS INTEGER = 0  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
  --------------------------------- THE BELOW QUERY RETURNS LIST OF ACTIVE ALERTS -------------------------------------  
 SELECT  SLAMT.TransactionId, SLAMT.IncidentNo IncidentNo, TC.CategoryName Category, TSC.SubCategoryName SubCategory, SLAMT.LocationName, SLAMT.Subject,  
   SLAMT.Description ,SLAMT.TransactionStartTime, SLAMT.Status, SLAMT.SLAStatus, SLAMT.JsonData, SLAMT.DriverName, SLAMT.DriverNo, SLAMT.VRN  from SLAManagerTransaction SLAMT  
   LEFT OUTER JOIN TicketCategory TC  
   on SLAMT.CategoryId = TC.CategoryId  
   LEFT OUTER JOIN TicketSubCategory TSC  
   ON TC.CategoryId = TSC.SubCategoryId  
   WHERE SLAMT.IsActive = 1  
  
------------------------------------ END ALERT QUERY----------------------------------------------------------------------  
  
------------------------------------ THE BELOW QUERY RETURNS LIST OF ACTIVE TICKETS ---------------------------------------  
  SELECT TT.TicketId, TT.TicketNo TicketNo, TT.IsSystemGenerated,TT.AssignedUserId, UM.UserName AssignedUser, UM1.UserName RaisedUser, TT.Status,   
      TT.LocationName,TT.TicketSubject, TT.TicketDescription,TT.Remarks, TT.ServiceSLATime,TT.Priority,  
      TT.RaisedAt, TT.IsEscalated, TT.CompletedAt, TT.JsonData, TT.DriverName, TT.DriverNo, TT.VRN,
      TC.CategoryName Category, TT.CategoryId, TSC.SubCategoryName SubCategory, TT.SubCategoryId, TT.IsActive  from TicketTransaction TT  
      LEFT OUTER JOIN TicketCategory TC  
      on TT.CategoryId = TC.CategoryId  
      LEFT OUTER JOIN TicketSubCategory TSC  
      ON TT.SubCategoryId = TSC.SubCategoryId  
      LEFT OUTER JOIN UserManager UM  
      ON TT.AssignedUserId = UM.Id  
      LEFT OUTER JOIN UserManager UM1  
      ON TT.RaisedUserId = UM1.Id  
         WHERE TT.IsActive = 1  
  
------------------------------------ END TICKET QUERY----------------------------------------------------------------------  
  
------------------------------------ THE BELOW QUERY RETURNS COUNT OF ALERTS AND TICKETS ---------------------------------------  
  SELECT   
   SUM(case when SLAStatus = 'Alert' then 1 else 0 end) AS AlertCount,  
   SUM(case when SLAStatus = 'Exception' then 1 else 0 end) AS TicketCount  
  FROM SLAManagerTransaction  
  WHERE IsActive =1  
------------------------------------ END OF QUERY----------------------------------------------------------------------   
END
GO
/****** Object:  StoredProcedure [dbo].[USP_ControlTowerActionDashboardAlert]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ControlTowerActionDashboardAlert]   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
  --------------------------------- THE BELOW QUERY RETURNS LIST OF ACTIVE ALERTS -------------------------------------  
 SELECT  SLAMT.TransactionId, SLAMT.IncidentNo IncidentNo, TC.CategoryName Category, TSC.SubCategoryName SubCategory, SLAMT.LocationName, SLAMT.Subject,  
   SLAMT.Description ,SLAMT.TransactionStartTime, SLAMT.Status, SLAMT.SLAStatus, SLAMT.JsonData, SLAMT.DriverName, SLAMT.DriverNo, SLAMT.VRN  from SLAManagerTransaction SLAMT  
   LEFT OUTER JOIN TicketCategory TC  
   on SLAMT.CategoryId = TC.CategoryId  
   LEFT OUTER JOIN TicketSubCategory TSC  
   ON TC.CategoryId = TSC.SubCategoryId  
   WHERE SLAMT.IsActive = 1  
  
------------------------------------ END ALERT QUERY----------------------------------------------------------------------  
  
------------------------------------ THE BELOW QUERY RETURNS COUNT OF ALERTS AND TICKETS ---------------------------------------  
  SELECT   
   SUM(case when SLAStatus = 'Alert' then 1 else 0 end) AS AlertCount,  
   SUM(case when SLAStatus = 'Exception' then 1 else 0 end) AS TicketCount  
  FROM SLAManagerTransaction  
  WHERE IsActive =1  
------------------------------------ END OF QUERY----------------------------------------------------------------------   
END
GO
/****** Object:  StoredProcedure [dbo].[USP_ControlTowerActionDashboardTicket]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ControlTowerActionDashboardTicket]   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
    
------------------------------------ THE BELOW QUERY RETURNS LIST OF ACTIVE TICKETS ---------------------------------------  
  SELECT TT.TicketId, TT.TicketNo TicketNo, TT.IsSystemGenerated,TT.AssignedUserId, UM.UserName AssignedUser, UM1.UserName RaisedUser, TT.Status,   
      TT.LocationName,TT.TicketSubject, TT.TicketDescription,TT.Remarks, TT.ServiceSLATime,TT.Priority,  
      TT.RaisedAt, TT.IsEscalated, TT.CompletedAt, TT.JsonData, TT.DriverName, TT.DriverNo, TT.VRN,
      TC.CategoryName Category, TT.CategoryId, TSC.SubCategoryName SubCategory, TT.SubCategoryId, TT.IsActive  from TicketTransaction TT  
      LEFT OUTER JOIN TicketCategory TC  
      on TT.CategoryId = TC.CategoryId  
      LEFT OUTER JOIN TicketSubCategory TSC  
      ON TT.SubCategoryId = TSC.SubCategoryId  
      LEFT OUTER JOIN UserManager UM  
      ON TT.AssignedUserId = UM.Id  
      LEFT OUTER JOIN UserManager UM1  
      ON TT.RaisedUserId = UM1.Id  
         WHERE TT.IsActive = 1  
  
------------------------------------ END TICKET QUERY----------------------------------------------------------------------     
END
GO
/****** Object:  StoredProcedure [dbo].[USP_ControlTowerDashboardData]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_ControlTowerDashboardData]   
   
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
  --------------------------------- THE BELOW QUERY RETURNS Total Ticket Data -------------------------------------  
  select top 1
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId where TC.CategoryName = 'Transaction Time' and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsActive=1) AS TransactionTime,
( Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId where TC.CategoryName = 'Time Lapse' and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsActive=1) as TimeLapse,
(SELECT SUM(case when TT1.Status = 'Assigned' then 1 else 0 end)  FROM TicketTransaction TT1 where TT1.IsActive = 1 ) AS AssignedTicket,
(SELECT SUM(case when TT2.Status = 'Un Assigned' then 1 else 0 end)  FROM TicketTransaction TT2 where TT2.IsActive = 1 ) AS UnAssignedTicket,
( Select count(*) as DeviceError from DeviceStatus where Status = 'Not Available') AS DeviceError,
(SELECT count(*)  FROM TicketTransaction TT4 where TT4.IsSystemGenerated = 0 and TT4.IsActive = 1 and TT4.Status not in ('Completed','Cloesed')) AS UserQueries,
(select count(*) from TicketTransaction TT5 where TT5.Status IN ('Completed','Auto Closed','Closed') and TT5.CompletedAt >= CONVERT(Varchar, GetDate(), 106) ) AS TodayClosedTicket,
(select count(*) from TicketTransaction TT6 where TT6.Status IN ('Completed','Auto Closed','Closed') and TT6.CompletedAt >= CONVERT(Varchar, GetDate()-7, 106) ) AS WeeklyClosedTicket ,
(select count(*) from TicketTransaction TT7 where TT7.Status IN ('Completed','Auto Closed','Closed') and TT7.CompletedAt >= CONVERT(varchar,dateadd(d,-(day(getdate()-1)),getdate()),106)) AS MonthlyClosedTicket,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName where LT.LocationType = 'Plant Gate' and TT.Status not IN ('Completed','Auto Closed','Closed')) AS PlantGateTicketTickets,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName where LT.LocationType = 'Weighbridge' and TT.Status not IN ('Completed','Auto Closed','Closed')) AS WeighbridgeTickets,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName where LT.LocationType IN ('Coil Yard','TMT Yard') and TT.Status not IN ('Completed','Auto Closed','Closed')) AS LoadingStationTickets,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName where LT.LocationType IN ('Stores', 'RHMS') and TT.Status not IN ('Completed','Auto Closed','Closed')) AS UnloadingStationTickets,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse' and (substring(SMT.EventCode ,charindex('-', SMT.EventCode) + 1 , len(SMT.EventCode))) = '1003') AS TimeLapseAtGateIn,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse' and (substring(SMT.EventCode ,charindex('-', SMT.EventCode) + 1 , len(SMT.EventCode))) = '1004') AS TimeLapseAtGateOut,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse' and (substring(SMT.EventCode ,charindex('-', SMT.EventCode) + 1 , len(SMT.EventCode))) IN ('1005','1006')) AS TimeLapseAtWeighment,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse' and (substring(SMT.EventCode ,charindex('-', SMT.EventCode) + 1 , len(SMT.EventCode))) IN ('1007','1008')) AS TimeLapseAtLoading,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse' and (substring(SMT.EventCode ,charindex('-', SMT.EventCode) + 1 , len(SMT.EventCode))) IN ('1009','1010','1011','1014','1015')) AS TimeLapseAtUnloading,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time' and SMT.EventCode = '1003') AS TransactionAtGateIn,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time' and SMT.EventCode = '1004') AS TransactionAtGateOut,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time' and SMT.EventCode IN ('1005','1006')) AS TransactionAtWeighment,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time' and SMT.EventCode IN ('1007','1008')) AS TransactionAtLoading,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join SLAManagerTransaction SMT on TT.TicketNo = SMT.TicketNo 
where TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time' and SMT.EventCode IN ('1009','1010','1011','1014','1015')) AS TransactionAtUnloading,
(SELECT count(*)  FROM TicketTransaction TT left join Location LL on LL.LocationName = TT.LocationName 
where TT.IsSystemGenerated = 0 and TT.IsActive = 1 and TT.Status not IN ('Completed','Auto Closed','Closed') and LL.LocationType  = 'Plant Gate') As UserQueriesAtOnPlantGate,
(SELECT count(*)  FROM TicketTransaction TT left join Location LL on LL.LocationName = TT.LocationName
where TT.IsSystemGenerated = 0 and TT.IsActive = 1 and TT.Status not IN ('Completed','Auto Closed','Closed') and LL.LocationType  = 'Weighbridge') As UserQueriesAtWeighment,
(SELECT count(*)  FROM TicketTransaction TT left join Location LL on LL.LocationName = TT.LocationName
where TT.IsSystemGenerated = 0 and TT.IsActive = 1 and TT.Status not IN ('Completed','Auto Closed','Closed') and LL.LocationType  IN ('Coil Yard','TMT Yard')) AS UserQueriesAtLoading,
(SELECT count(*)  FROM TicketTransaction TT left join Location LL on LL.LocationName = TT.LocationName
where TT.IsSystemGenerated = 0 and TT.IsActive = 1 and TT.Status not IN ('Completed','Auto Closed','Closed') and LL.LocationType  IN ('Stores', 'RHMS')) AS UserQueriesAtUnloading,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType = 'Plant Gate' and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse') AS PlantGateOfTimeLapse,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType = 'Plant Gate' and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time') AS PlantGateOfTransaction,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName 
where LT.LocationType = 'Plant Gate' and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsSystemGenerated = 0) AS PlantGateOfUserQuesries,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType = 'Weighbridge' and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse') AS WeighbridgeOfTimeLapse,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType = 'Weighbridge' and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time') AS WeighbridgeOfTransaction,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName 
where LT.LocationType = 'Weighbridge' and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsSystemGenerated = 0) AS WeighbridgeOfUserQuesries,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType IN ('Coil Yard','TMT Yard') and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse') AS LoadingOfTimeLapse,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType IN ('Coil Yard','TMT Yard') and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time') AS LoadingOfTransaction,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName 
where LT.LocationType IN ('Coil Yard','TMT Yard') and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsSystemGenerated = 0) AS LoadingOfUserQuesries,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType IN ('Stores', 'RHMS') and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Time Lapse') AS UnloadingOfTimeLapse,
(Select count(*) from TicketTransaction TT left join TicketCategory TC on TC.CategoryId = TT.CategoryId left join Location LT on TT.LocationName = LT.LocationName
where LT.LocationType IN ('Stores', 'RHMS') and TT.Status not IN ('Completed','Auto Closed','Closed') and TC.CategoryName = 'Transaction Time') AS UnloadingOfTransaction,
(Select count(*) from TicketTransaction TT left join Location LT on TT.LocationName = LT.LocationName 
where LT.LocationType IN ('Stores', 'RHMS') and TT.Status not IN ('Completed','Auto Closed','Closed') and TT.IsSystemGenerated = 0) AS UnloadingOfUserQuesries,
( Select count(*) as DeviceError from DeviceStatus DS left join DeviceLocationMapping DM on DM.DeviceLocationMappingId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and DM.DeviceType = 'CCTV') AS CCTVDeviceNotAvilable,
 (Select count(*) as DeviceError from DeviceStatus DS  left join DeviceLocationMapping DM on DM.DeviceLocationMappingId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and DM.DeviceType = 'RFID') AS RFIDDeviceNotAvilable,
 (Select count(*) as DeviceError from DeviceStatus DS left join DeviceLocationMapping DM on DM.DeviceLocationMappingId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and DM.DeviceType = 'LED') AS LEDDeviceNotAvilable,
 (Select count(*) as DeviceError from DeviceStatus DS left join DeviceLocationMapping DM on DM.DeviceLocationMappingId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and DM.DeviceType not in ('CCTV','LED','RFID')) AS OthersDeviceNotAvilable,
 (Select count(*) as DeviceError from DeviceStatus DS left join Location LC on Lc.LocationId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and LC.LocationType ='Plant Gate') AS DeviceErrorOnPlantgate,
 (Select count(*) as DeviceError from DeviceStatus DS left join Location LC on Lc.LocationId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and LC.LocationType ='Weighbridge') AS DeviceErrorOnWeighbridge,
 (Select count(*) as DeviceError from DeviceStatus DS left join Location LC on Lc.LocationId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and LC.LocationType IN ('Coil Yard', 'TMT Yard')) AS DeviceErrorOnLoading,
 (Select count(*) as DeviceError from DeviceStatus DS left join Location LC on Lc.LocationId = DS.DeviceLocationMappingId
 where Status = 'Not Available' and LC.LocationType IN ('Stores', 'RHMS')) AS DeviceErrorOnUnLoading
from SLAManagerTransaction 
  
------------------------------------ END ALERT QUERY----------------------------------------------------------------------  
  
------------------------------------ THE BELOW QUERY RETURNS for Bar Graph Data ---------------------------------------  
  Select top 7 * from  (Select CONVERT(VARCHAR(10),ISNULL(ServiceSLATime,RaisedAt) ,105) as Date, Status from TicketTransaction  ) t 
	PIVOT(
	count(Status)
	FOR Status in (
	[Completed],[Closed], [Auto Closed],
	[Un Assigned] ,[Assigned], [Promoted])
	) AS Data1
	order by Date ASC 
  
------------------------------------ END TICKET QUERY----------------------------------------------------------------------

--------------------------------- THE BELOW QUERY RETURNS LIST OF ACTIVE ALERTS -------------------------------------  
 SELECT top 5  SLAMT.TransactionId, SLAMT.IncidentNo as IncidentNo, TC.CategoryName as Category, TSC.SubCategoryName as SubCategory,  
    SLAMT.SLAStatus  from SLAManagerTransaction SLAMT  
   LEFT OUTER JOIN TicketCategory TC  
   on SLAMT.CategoryId = TC.CategoryId  
   LEFT OUTER JOIN TicketSubCategory TSC  
   ON TC.CategoryId = TSC.SubCategoryId  
   WHERE SLAMT.IsActive = 1  
  
------------------------------------ END ALERT QUERY---------------------------------------------------------------------- 

------------------------------------ THE BELOW QUERY RETURNS LIST OF ACTIVE TICKETS ---------------------------------------  
  SELECT top 5 TT.TicketId, TT.TicketNo as TicketNo, TT.Status,   
      TT.TicketSubject, TT.Priority, TC.CategoryName as Category, TSC.SubCategoryName as SubCategory from TicketTransaction TT  
      LEFT OUTER JOIN TicketCategory TC  
	  on TT.CategoryId = TC.CategoryId  
      LEFT OUTER JOIN TicketSubCategory TSC  
      ON TT.SubCategoryId = TSC.SubCategoryId  
         WHERE TT.IsActive = 1  
  
------------------------------------ END TICKET QUERY----------------------------------------------------------------------

-------------------------------------- High Priority Tickets --------------------------------------------------------------

Select top 10 TicketId,  TicketNo, LocationName, Status from TicketTransaction where Priority = 1 and IsActive = 1 and Status not in ('Auto Closed','Closed','Completed')

-------------------------- End Below Query Retuns List of High Priority Tickets -------------------------------------------
  END
GO
/****** Object:  StoredProcedure [dbo].[USP_ControlTowerUserDetails]    Script Date: 09-30-2022 12:30:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_ControlTowerUserDetails]  
AS  
BEGIN  
  
------------------------------------ THE BELOW QUERY RETURNS LIST OF Control Tower Users Details ---------------------------------------  
 Select * from ( Select TT.AssignedUserId, UM.FirstName, UM.LastName, TT.Status, (Select (Case when Max(RT.Expires) > GetDate() then 1 else 0 end) AS IsLogIn from CTUserAvailabilityStatus CTA
left join RefreshToken RT on RT.UserManagerId = CTA.UserId where CTA.UserId = UA.UserId group by CTA.UserId) AS IsLogIn,
(Select (convert(Varchar(10),Max(RT.Created),105)+' '+convert(Varchar(10),Max(RT.Created),8)) AS LogInTime from CTUserAvailabilityStatus CTA
left join RefreshToken RT on RT.UserManagerId = CTA.UserId where CTA.UserId = UA.UserId group by CTA.UserId ) AS LogInTime,
( Select Count(TT1.Status) from TicketTransaction TT1 where TT1.AssignedUserId = UA.UserId and TT1.Status In ('Completed', 'Closed') and TT1.CompletedAt > CAST(GETDATE() AS Date)) as CompletedTicket
from CTUserAvailabilityStatus UA
left join UserManager UM on UA.UserId = UM.Id left join TicketTransaction TT on TT.AssignedUserId = UA.UserId
where ISNULL(TT.AssignedUserId,'') != '' and TT.Status In ('Assigned', 'Promoted', 'InProgress', 'OnHold')
) t PIVOT(	count(Status) FOR Status in ([Assigned], [Promoted], [InProgress],[OnHold]) ) AS UserData
  
------------------------------------ END Control Tower User Details----------------------------------------------------------------------  
  
END
GO



create   or alter    FUNCTION [dbo].[fn_ConvertMinutesToString] 
(
	@Minutes int
)
RETURNS nvarchar(50)
AS
BEGIN
	Declare @retstring nvarchar(50) = '' 
	If ( @Minutes < 60) 
	Begin 
		set @retstring = '00:' + case 
	when @Minutes < 10 then RIGHT('00'+ISNULL(ltrim(rtrim(str(@Minutes))),''),2) 
	else ltrim(rtrim(str(@Minutes)))  end 

	End 
	Else
	Begin 
	declare @hours int   = @Minutes /60
	declare @mins int = @Minutes %60 

	set @retstring = 
	case 
	when @hours < 10 then RIGHT('00'+ISNULL(ltrim(rtrim(str(@hours))),''),2) 
	else ltrim(rtrim(str(@hours))) 
	End 
	 + ':' 
	 + case 
	when @mins < 10 then RIGHT('00'+ISNULL(ltrim(rtrim(str(@mins))),''),2) 
	else ltrim(rtrim(str(@mins))) End
	End 

	return @retstring 
END
GO
