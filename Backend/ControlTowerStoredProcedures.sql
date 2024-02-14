-- ================================================
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[USP_ControlTowerActionDashboard] 

@AssignedUserId AS INTEGER = 0
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  --------------------------------- THE BELOW QUERY RETURNS LIST OF ACTIVE ALERTS ------------------------------------- 
	SELECT  SLAMT.TransactionId, SLAMT.IncidentNo IncidentNo, TC.CategoryName Category, TSC.SubCategoryName SubCategory, SLAMT.LocationName, SLAMT.Subject,
			SLAMT.Description ,SLAMT.TransactionStartTime, SLAMT.Status, SLAMT.SLAStatus, SLAMT.JsonData SLAMT.DriverName, SLAMT.DriverNo, SLAMT.VRN  from SLAManagerTransaction SLAMT
			LEFT OUTER JOIN TicketCategory TC
			on SLAMT.CategoryId = TC.CategoryId
			LEFT OUTER JOIN TicketSubCategory TSC
			ON TC.CategoryId = TSC.SubCategoryId
			WHERE SLAMT.IsActive = 1

------------------------------------ END ALERT QUERY----------------------------------------------------------------------

------------------------------------ THE BELOW QUERY RETURNS LIST OF ACTIVE TICKETS ---------------------------------------
		SELECT TT.TicketId, TT.TicketNo TicketNo, TT.IsSystemGenerated,TT.AssignedUserId, UM.UserName AssignedUser, UM1.UserName RaisedUser, TT.Status, 
			   TT.LocationName,TT.TicketSubject, TT.ticketDescription,TT.Remarks, TT.ServiceSLATime,TT.Priority,
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

----------------------------------- THE BELOW QUERY RETURNS LIST OF TICKET ASSIGNED TO A USER ---------------------------------------
		SELECT TT.TicketId, TT.TicketNo TicketNo, TT.IsSystemGenerated,TT.AssignedUserId, UM.UserName AssignedUser, UM1.UserName RaisedUser, TT.Status, 
			   TT.LocationName,TT.TicketSubject, TT.ticketDescription,TT.Remarks, TT.ServiceSLATime,TT.Priority,
			   TT.RaisedAt, TT.IsEscalated, TT.CompletedAt, TT.JsonData,TT.DriverName, TT.DriverNo, TT.VRN,
			   TC.CategoryName Category, TT.CategoryId, TSC.SubCategoryName SubCategory, TT.SubCategoryId, TT.IsActive  from TicketTransaction TT
			   LEFT OUTER JOIN TicketCategory TC
			   on TT.CategoryId = TC.CategoryId
			   LEFT OUTER JOIN TicketSubCategory TSC
			   ON TT.SubCategoryId = TSC.SubCategoryId
			   LEFT OUTER JOIN UserManager UM
			   ON TT.AssignedUserId = UM.Id
			   LEFT OUTER JOIN UserManager UM1
			   ON TT.RaisedUserId = UM1.Id
		       WHERE TT.AssignedUserId = @AssignedUserId

------------------------------------ END TICKET QUERY----------------------------------------------------------------------

------------------------------------ THE BELOW QUERY RETURNS COUNT OF ALERTS AND TICKETS ---------------------------------------
		SELECT 
			SUM(case when SLAStatus = 'Alert' then 1 else 0 end) AS AlertCount,
			SUM(case when SLAStatus = 'Exception' then 1 else 0 end) AS TicketCount
		FROM SLAManagerTransaction
		WHERE IsActive =1
------------------------------------ END OF QUERY----------------------------------------------------------------------	
END


Go

-- --------------- dahsbaord SP ------------ 

ALTER PROCEDURE [dbo].[USP_ControlTowerDashboard]  
	
AS
BEGIN
	DECLARE @AlertCount INT = 0; 
	DECLARE @TicketCount INT = 0; 


	SET NOCOUNT ON;
--  THE BELOW QUERY RETURNS ACTIVE ALERT DETAILS FOR CONTROL TOWER. ---------------------------
	SELECT SLAMT.TransactionId, SLAMT.LocationCode, SLAMT.LocationName, SLAMT.Subject, SLAMT.Description, SLAMT.Status, SLAMT.SLAStatus,
		   TC.CategoryName, TSC.SubCategoryName, SLAMT.TransactionStartTime
	FROM SLAManagerTransaction SLAMT
	LEFT OUTER JOIN TicketCategory TC
	on SLAMT.CategoryId = TC.CategoryId
	LEFT OUTER JOIN TicketSubCategory TSC
	ON SLAMT.SubCategoryId = TSC.SubCategoryId
	WHERE SLAMT.IsActive = 1
	ORDER BY SLAMT.TransactionStartTime desc

------------------------------ END ------------------------------

--  THE BELOW QUERY RETURNS ACTIVE EXCEPTION/TICKET DETAILS FOR CONTROL TOWER. ---------------------------

	SELECT TT.TicketId, TT.TicketNo, TT.IsSystemGenerated,TT.AssignedUserId,  UM.UserName as RaisedBy, UM1.UserName as AssignedTo, TT.TicketSubject, TT.TicketDescirption, TT.Status, TT.Priority,
		   TC.CategoryName,TT.CategoryId, TSC.SubCategoryName, TT.SubCategoryId, TT.RaisedAt
	FROM TicketTransaction TT
	LEFT OUTER JOIN TicketCategory TC
	on TT.CategoryId = TC.CategoryId
	LEFT OUTER JOIN TicketSubCategory TSC
	ON TT.SubCategoryId = TSC.SubCategoryId
	LEFT OUTER JOIN UserManager UM
	ON TT.RaisedUserId = UM.Id
	LEFT OUTER JOIN UserManager UM1
	ON TT.AssignedUserId = UM1.Id
	WHERE TT.IsActive = 1
	ORDER BY TT.RaisedAt DESC

------------------------------ END ------------------------------

-- -------------------------- THE BELOW QUERY RETURNS TOP 10 RECENT CLOSED TICKETS ----------------------

SELECT TOP 10 TT.TicketId, TT.TicketNo, TT.IsSystemGenerated, UM.UserName as RaisedBy, UM1.UserName as AssignedTo, TT.TicketSubject, TT.TicketDescirption, TT.Status, TT.Priority,
		   TC.CategoryName, TSC.SubCategoryName, TT.CompletedAt
	FROM TicketTransaction TT
	LEFT OUTER JOIN TicketCategory TC
	on TT.CategoryId = TC.CategoryId
	LEFT OUTER JOIN TicketSubCategory TSC
	ON TT.SubCategoryId = TSC.SubCategoryId
	LEFT OUTER JOIN UserManager UM
	ON TT.RaisedUserId = UM.Id
	LEFT OUTER JOIN UserManager UM1
	ON TT.AssignedUserId = UM1.Id
	WHERE TT.IsActive = 0
	ORDER BY TT.CompletedAt ASC
------------------------------ END ------------------------------

--------------------- THE BELOW QUERY BLOCK RETURNS COUNT OF DIFFERENT STATS --------------------

-- TOTAL ALERT OF THE DAY
Select @AlertCount =  COUNT(*) FROM SLAManagerTransaction  WHERE SLAStatus ='Alert' AND cast(TransactionStartTime as date) = cast(GETDATE() as date)
Select @TicketCount =  COUNT(*) FROM TicketTransaction WHERE cast(RaisedAt as date) = cast(GETDATE() as date)

SELECT @AlertCount AlertCount, @TicketCount TicketCount
END

  --------------------------------- THE BELOW QUERY RETURNS Control Tower Dashboard -------------------------------------  

ALTER PROCEDURE [dbo].[USP_ControlTowerDashboardData]  
   
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
(SELECT count(*)  FROM TicketTransaction TT4 where TT4.IsSystemGenerated = 0 and TT4.IsActive = 1 and TT4.Status not in ('Completed','Cloesed', 'Auto Closed')) AS UserQueries,
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



  ALTER PROCEDURE [dbo].[USP_ControlTowerUserDetails]  
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


--------------------------------- THE BELOW QUERY RETURNS Total Alert Data -------------------------------------  
ALTER PROCEDURE [dbo].[USP_ControlTowerActionDashboardAlert]   
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

------------------------------------ END CTotal Alert Data----------------------------------------------------------------------


--------------------------------- THE BELOW QUERY RETURNS Total Ticket Data -------------------------------------  
ALTER PROCEDURE [dbo].[USP_ControlTowerActionDashboardTicket]   
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

------------------------------------ END CTotal Tickets Data----------------------------------------------------------------------
