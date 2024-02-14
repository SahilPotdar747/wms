/* Milestone Actions Master Query */
select MilestoneName , MilestoneEvent , MilestoneCode , MilestoneAction , mactm.ActionCode from MilestoneMaster MM 
join MilestoneActionMapping MAP on MM.MilestoneId = map.MilestoneId
join MilestoneActionsMaster MACTM on map.MilestoneActionId = MACTM.MilestoneActionId
order by map.MilestoneActionMappingId , mm.MilestoneId,  mactm.MilestoneActionId


/* Query to delete all transaction data */
delete VehicleTransaction
delete JobMilestoneDetails
delete JobMilestones
delete WeighBridgeTransaction
delete elv 
delete JobAllocationDetails
delete lednotification
delete LEDNotificationDetails

/* LED Queries */
select * from LEDNotification 
select * from LEDNotificationDetails 


/* Queries to check Transactions*/
select * from JobMilestones
select * from MilestoneActionsTracking  order by JobMilestoneId , Sequence
select * from WeighBridgeTransaction

update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1703
update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1669
update MilestoneActionsTracking set IsActive = 0 , Status = 'Completed' where  MilestoneActionsTrackingId = 1667

