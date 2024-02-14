----------09MAY2022-RENAMED WBAS to CurrentQueue----------------------------------------------------------------------

IF ( (SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WeighBridgeAvailabilityStatus') >  0 )
Begin
 drop table WeighBridgeAvailabilityStatus
End 
 
IF ( (SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CurrentQueue') =   0 )

Begin 

CREATE TABLE [dbo].[CurrentQueue](
	[CurrentQueueId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[TotalInProgressTransaction] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CurrentQueue] PRIMARY KEY CLUSTERED 
(
	[CurrentQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[CurrentQueue] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]

ALTER TABLE [dbo].[CurrentQueue]  WITH CHECK ADD  CONSTRAINT [FK_CurrentQueue_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ON DELETE CASCADE

ALTER TABLE [dbo].[CurrentQueue] CHECK CONSTRAINT [FK_CurrentQueue_Location_LocationId]

End 

GO 

If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'WeighBridgeMaster' 
and column_name = 'MaxQueueSize') > 0 
Begin
		alter table WeighBridgeMaster drop column MaxQueueSize 		
End 

GO 

If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'Location' 
and column_name = 'MaxQueueSize') < 1 
Begin
		alter table Location  add MaxQueueSize int 
End 

GO 
----------------------------------------------------------------------------------------------------------------------


----------10MAY2022---------------------------------------------------------------------------------------------------
--Added new column MinQueueSize in Location table
If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'Location' 
and column_name = 'MinQueueSize') < 1 
Begin
		alter table Location  add MinQueueSize int 
End 

GO 
----------------------------------------------------------------------------------------------------------------------


----------11MAY2022---------------------------------------------------------------------------------------------------
--Added new column Priority in ELV table
If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'ELV' 
and column_name = 'Priority') < 1 
Begin
		alter table ELV  add Priority int 
End 

GO

----------------------------------------------------------------------------------------------------------------------


----------12MAY2022---------------------------------------------------------------------------------------------------
--Added new table LocationCloseTime

IF (NOT EXISTS (SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LocationClosingTime'))

BEGIN
CREATE TABLE [dbo].[LocationClosingTime](
	[LocationClosingTimeId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[CloseStartTime] [datetime2](7) NOT NULL,
	[CloseEndTime] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LocationClosingTime] PRIMARY KEY CLUSTERED 
(
	[LocationClosingTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[LocationClosingTime] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]


ALTER TABLE [dbo].[LocationClosingTime]  WITH CHECK ADD  CONSTRAINT [FK_LocationClosingTime_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ON DELETE CASCADE


ALTER TABLE [dbo].[LocationClosingTime] CHECK CONSTRAINT [FK_LocationClosingTime_Location_LocationId]
END 

GO

----------------------------------------------------------------------------------------------------------------------
If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'MilestoneActionsMaster' 
and column_name = 'DeviceType') < 1 
Begin
		alter table MilestoneActionsMaster  add  DeviceType nvarchar(15) null 
End 

GO 

If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'MilestoneActionsTracking' 
and column_name = 'DeviceType') < 1 
Begin
		alter table MilestoneActionsTracking  add  DeviceType nvarchar(15) null 
End 

GO 
----07-06-2022-------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[ParkingAnnouncement](
	[ParkingAnnouncementId] [int] IDENTITY(1,1) NOT NULL,
	[VRN] [nvarchar](15) NOT NULL,
	[LocationName] [nvarchar](30) NOT NULL,
	[LEDIPAddress] [nvarchar](30) NOT NULL,
	[IPCIPAddress] [nvarchar](30) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AudioPlayedCount] [int] NOT NULL,
	[DisplayCount] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ParkingAnnouncement] PRIMARY KEY CLUSTERED 
(
	[ParkingAnnouncementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ParkingAnnouncement] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
----07-06-2022----------------------------------------------------------------------------------------------------


----13-06-2022----------------------------------------------------------------------------------------------------

----13-06-2022----------------------------------------------------------------------------------------------------


----14-06-2022----------------------------------------------------------------------------------------------------
--Changes for Control Tower

If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'JobMilestones' 
and column_name = 'SLAManagerTransactionId') < 1 
Begin
		Alter Table JobMilestones Add SLAManagerTransactionId int
End 

GO 


If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'JobMilestones' 
and column_name = 'AlertSLADateTime') < 1 
Begin
		Alter Table JobMilestones Add AlertSLADateTime datetime2
End 

GO 

If (select count(0)  from INFORMATION_SCHEMA.COLUMNS where table_name = 'JobMilestones' 
and column_name = 'ExceptionSLADateTime') < 1 
Begin
		Alter Table JobMilestones Add ExceptionSLADateTime datetime2
End 

GO 
----14-06-2022----------------------------------------------------------------------------------------------------



----18-06-2022----------------------------------------------------------------------------------------------------
Create Table  WeighingStatus 
(
WIC  int IDENTITY( 1,1) PRIMARY KEY NOT NULL , 
IPCIPAddress nvarchar(255) , 
Massages nvarchar(255), 
Status nvarchar(255),
DateTime datetime2 
)

Alter Table RoleMaster add RoleGroup nvarchar(30) NOT NULL
----18-06-2022----------------------------------------------------------------------------------------------------


----10-08-2022----------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[DeviceStatusHistory](
	[DeviceStatusHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[DeviceStatusId] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[DeviceIP] [nvarchar](max) NULL,
 CONSTRAINT [PK_DeviceStatusHistory] PRIMARY KEY CLUSTERED 
(
	[DeviceStatusHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DeviceStatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_DeviceStatusHistory_DeviceStatus_DeviceStatusId] FOREIGN KEY([DeviceStatusId])
REFERENCES [dbo].[DeviceStatus] ([DeviceStatusId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DeviceStatusHistory] CHECK CONSTRAINT [FK_DeviceStatusHistory_DeviceStatus_DeviceStatusId]
GO
----10-08-2022----------------------------------------------------------------------------------------------------

----20-09-2020----------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[WBTrigger](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[LocationIPAddress] [nvarchar](25) NOT NULL,
	[Token] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[VRN] [nvarchar](100) NULL,
 CONSTRAINT [PK_WBTrigger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
----20-09-2020----------------------------------------------------------------------------------------------------

----30-09-2020----------------------------------------------------------------------------------------------------
BEGIN TRANSACTION;
GO

ALTER TABLE [UserManager] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220923045326_AddColumnIsactiveUserManager', N'5.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [RFIDDetection] (
    [Id] int NOT NULL IDENTITY,
    [LocationId] int NOT NULL,
    [VehicleTransactionId] int NOT NULL,
    [DesktopId] nvarchar(50) NULL,
    [RfidIp] nvarchar(50) NULL,
    [ModifiedDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_RFIDDetection] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220923110716_CreatedRFIDDetectionTable', N'5.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BlackListingHistories]') AND [c].[name] = N'IncidentDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [BlackListingHistories] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [BlackListingHistories] ALTER COLUMN [IncidentDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220926044227_EditColumnIncidentDate', N'5.0.13');
GO

COMMIT;
GO


----30-09-2020----------------------------------------------------------------------------------------------------
