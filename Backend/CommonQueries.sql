B/****Query to get Device List with their IPs and Locations *****************/
select 
	DLM.DeviceType,
	LocationName + ' [' + DLM.TransactionType  + ']' Location, 
	DeviceLocationMappingId, 
	DLM.DeviceIP
from  LocationMaster LM
	join LocationGroupMaster LGM on LM.LocationGroupId = LGM.LocationGroupId
	join LocationTypeMaster LTM on LM.LocationTypeId = LTM.LocationTypeId
	join DeviceLocationMapping DLM on DLM.LocationId = LM.LocationId
--where 
--	DLM.DeviceType = 'RFID'
order by 
	DLM.DeviceType, LM.LocationId
/****************************************************************************/


/****Milestone Actions Master Query *****************************************/
select MilestoneName , MilestoneEvent , MilestoneCode , MilestoneAction , mactm.ActionCode from MilestoneMaster MM 
join MilestoneActionMapping MAP on MM.MilestoneId = map.MilestoneId
join MilestoneActionsMaster MACTM on map.MilestoneActionId = MACTM.MilestoneActionId
order by map.MilestoneActionMappingId , mm.MilestoneId,  mactm.MilestoneActionId
/****************************************************************************/

/****Query to delete all transaction data************************************/
delete VehicleTransaction
delete JobMilestoneDetails
delete JobMilestones
delete WeighBridgeTransaction
delete elv 
delete JobAllocationDetails
delete lednotification
delete LEDNotificationDetails
/****************************************************************************/

/****LED Queries*************************************************************/
select * from LEDNotification 
select * from LEDNotificationDetails 
/****************************************************************************/

/* Queries to check Transactions*/
select * from VehicleTransaction
select * from JobMilestones
select * from MilestoneActionsTracking  order by JobMilestoneId , Sequence
select * from WeighBridgeTransaction
/****************************************************************************/

/****Queries required to complete milestone actions manually*****************/
update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1853
update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1669
update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1667
/****************************************************************************/

/****Update IPs of all LEDs to 192.168.1.11 for testing in office************/
update DeviceLocationMapping set DeviceIP = '192.168.1.11' where DeviceType = 'LED'
/****************************************************************************/

/****Update Driver Code to EC1109 for testing in office**********************/
update DriverMaster set DriverCode = 'EC1109' , Name = 'Rajesh Chandras' where driverCode = 'DR310' 
/****************************************************************************/

/****Uodate DLM for actual RFID IP address and antennna number in office*****/
update DeviceLocationMapping set deviceip = '192.168.1.164' , Antenna = 1 where DeviceName = 'Jetty Parking Out RFID Reader'
update DeviceLocationMapping set deviceip = '192.168.1.164' , Antenna = 2 where Devicename = 'Alibag Gate In RFID Reader'
/*****************************************************************************/

/****To shorten the name of location group name*******************************/
update LocationGroupMaster set LocationGroupName = REPLACE(LocationGroupName , 'Loading' , 'Load') 
where LocationGroupName like 'coil load%' or LocationGroupName like 'TMT load%'
/*****************************************************************************/



/*****************************************************************************/
--QUERY TO DROP SELECTED URGE-TRUCK TABLES CREATED MISTAKENLY IN OTHER DATABASE
/*****************************************************************************/
declare @tablelist table 
(
	tablename nvarchar(300), 
	object_id int
)

insert @tablelist 
select name, object_id from sys.tables where name in (
'__EFMigrationsHistory',
'ApplicationConfigMaster',
'CrossCheckDailySchedule',
'CrossCheckEventMaster',
'CrossCheckEventSchedule',
'CrossCheckWGBroup',
'DashBoardDataSPdto',
'DeviceLocationMapping',
'DriverMaster',
'ELV',
'FailureLog',
'ImageTransaction',
'JobAllocationDetails',
'JobAllocationProductDetails',
'JobMilestoneDetails',
'JobMilestones',
'LEDMessageMaster',
'LEDNotification',
'LEDNotificationDetails',
'LoadUnloadTransaction',
'LocationGroupMaster',
'LocationMaster',
'LocationTypeMaster',
'MilestoneActionMapping',
'MilestoneActionsMaster',
'MilestoneActionsTracking',
'MilestoneMaster',
'MobileRequest',
'NonLogisticVehicles',
'PhysicalCheckTransaction',
'ProductMaster',
'RefreshToken',
'RetryObjectContainer',
'ShiftMaster',
'TransporterMaster',
'UserManager',
'VehicleMaster',
'VehicleTransaction',
'WeighbridgeAllocationPerferences',
'WeighBridgeAvailabilityStatus',
'WeighBridgeMaster',
'WeighBridgeTransaction'
)
/*  DROP ALL CONTRAINSTS */
DECLARE @sql NVARCHAR(MAX) = N'';

--SELECT @sql += N'
--ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
--    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
--    ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
--FROM sys.foreign_keys where parent_object_id in ( select object_id from @tablelist) ;

--PRINT @sql;



/* DROP ALL FOREIGN KEY*/
SELECT @sql += N'
ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + 
    ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
FROM sys.foreign_keys where parent_object_id in ( select object_id from @tablelist) ;

PRINT @sql;
EXEC sp_executesql @sql;

set @sql  = ''

/* DROP ALL TABLES */
SELECT @sql += N'
DROP TABLE ' + tablename  + ';'
FROM @tablelist

PRINT @sql;
EXEC sp_executesql @sql;

/*****************************************************************************/