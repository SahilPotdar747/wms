IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211206053239_InitialCreateTable')
BEGIN
CREATE TABLE [ApplicationConfigMaster] (
    [AppConfigId] int NOT NULL IDENTITY,
    [Key] nvarchar(30) NOT NULL,
    [Parameter] nvarchar(50) NOT NULL,
    [Value] nvarchar(150) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_ApplicationConfigMaster] PRIMARY KEY ([AppConfigId])
);

CREATE TABLE [DriverMaster] (
    [DriverId] int NOT NULL IDENTITY,
    [DriverCode] nvarchar(30) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Category] nvarchar(30) NULL,
    [Address] nvarchar(200) NULL,
    [MobileNo] nvarchar(12) NULL,
    [Email] nvarchar(50) NULL,
    [AadharNo] nvarchar(20) NULL,
    [DLNo] nvarchar(50) NOT NULL,
    [DLValidaty] datetime2 NOT NULL,
    [AutoConsent] bit NULL DEFAULT CAST(1 AS bit),
    [IsBlackListed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_DriverMaster] PRIMARY KEY ([DriverId])
);

CREATE TABLE [LEDMessageMaster] (
    [LEDMessageId] int NOT NULL IDENTITY,
    [Category] nvarchar(max) NULL,
    [Message] nvarchar(200) NOT NULL,
    [langauage] nvarchar(50) NOT NULL,
    [PlayAudio] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_LEDMessageMaster] PRIMARY KEY ([LEDMessageId])
);

CREATE TABLE [LocationGroupMaster] (
    [LocationGroupId] int NOT NULL IDENTITY,
    [LocationGroupName] nvarchar(30) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_LocationGroupMaster] PRIMARY KEY ([LocationGroupId])
);

CREATE TABLE [LocationTypeMaster] (
    [LocationTypeId] int NOT NULL IDENTITY,
    [LocationTypeName] nvarchar(30) NOT NULL,
    [Description] nvarchar(200) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_LocationTypeMaster] PRIMARY KEY ([LocationTypeId])
);

CREATE TABLE [MilestoneActionsMaster] (
    [MilestoneActionId] int NOT NULL IDENTITY,
    [MilestoneAction] nvarchar(100) NOT NULL,
    [ActionCode] nvarchar(30) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_MilestoneActionsMaster] PRIMARY KEY ([MilestoneActionId])
);

CREATE TABLE [MilestoneMaster] (
    [MilestoneId] int NOT NULL IDENTITY,
    [MilestoneName] nvarchar(30) NOT NULL,
    [MilestoneEvent] nvarchar(30) NOT NULL,
    [MilestoneCode] nvarchar(10) NOT NULL,
    [Description] nvarchar(300) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_MilestoneMaster] PRIMARY KEY ([MilestoneId])
);

CREATE TABLE [NonLogisticVehicles] (
    [NonLogisticVehicleId] int NOT NULL IDENTITY,
    [VRN] nvarchar(30) NOT NULL,
    [PermitTill] datetime2 NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [IsBlackListed] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_NonLogisticVehicles] PRIMARY KEY ([NonLogisticVehicleId])
);

CREATE TABLE [ShiftMaster] (
    [ShiftId] int NOT NULL IDENTITY,
    [ShiftName] nvarchar(20) NOT NULL,
    [StartTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ShiftMaster] PRIMARY KEY ([ShiftId])
);

CREATE TABLE [TransporterMaster] (
    [TransporterId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [VendorCode] nvarchar(30) NOT NULL,
    [PlantCode] nvarchar(50) NULL,
    [Address] nvarchar(200) NULL,
    [MobileNo] nvarchar(15) NULL,
    [TelNo] nvarchar(20) NULL,
    [Email] nvarchar(50) NULL,
    [ContactPersonName] nvarchar(50) NULL,
    [ContactPersonNumber] nvarchar(20) NULL,
    [IsBlackListed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_TransporterMaster] PRIMARY KEY ([TransporterId])
);

CREATE TABLE [UserManager] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(20) NULL,
    [FirstName] nvarchar(20) NULL,
    [LastName] nvarchar(20) NULL,
    [Email] nvarchar(50) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [AcceptTerms] bit NOT NULL,
    [Role] int NOT NULL,
    [VerificationToken] nvarchar(max) NULL,
    [Verified] datetime2 NULL,
    [ResetToken] nvarchar(max) NULL,
    [ResetTokenExpires] datetime2 NULL,
    [PasswordReset] datetime2 NULL,
    [CreatedBy] nvarchar(30) NULL,
    [CreatedDate] date NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] date NULL,
    CONSTRAINT [PK_UserManager] PRIMARY KEY ([Id])
);

CREATE TABLE [VehicleMaster] (
    [VehicleId] int NOT NULL IDENTITY,
    [VRN] nvarchar(15) NOT NULL,
    [RFIDTagNumber] nvarchar(200) NULL,
    [TagType] nvarchar(12) NULL,
    [VRDate] datetime2 NOT NULL,
    [PermitTill] datetime2 NOT NULL,
    [NumberOfWheels] int NULL,
    [EngineCapacityCC] real NULL,
    [CapacityInTons] real NULL,
    [TareweightKg] real NULL,
    [IsBlackListed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_VehicleMaster] PRIMARY KEY ([VehicleId])
);

CREATE TABLE [VehicleTransaction] (
    [VehicleTransactionId] int NOT NULL IDENTITY,
    [VehicleTransactionCode] nvarchar(30) NOT NULL,
    [VRN] nvarchar(50) NOT NULL,
    [DriverId] int NOT NULL,
    [RFIDTagNumber] nvarchar(200) NULL,
    [TranType] int NOT NULL,
    [ShipmentNo] nvarchar(50) NOT NULL,
    [GateEntryNo] nvarchar(50) NOT NULL,
    [TransactionDate] datetime2 NULL,
    [TransactionStartTime] datetime2 NULL,
    [TransactionEndTime] datetime2 NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [TranStatus] nvarchar(30) NOT NULL,
    [Remarks] nvarchar(max) NULL,
    CONSTRAINT [PK_VehicleTransaction] PRIMARY KEY ([VehicleTransactionId]),
    CONSTRAINT [FK_VehicleTransaction_DriverMaster_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [DriverMaster] ([DriverId]) ON DELETE CASCADE
);

CREATE TABLE [LocationMaster] (
    [LocationId] int NOT NULL IDENTITY,
    [LocationName] nvarchar(30) NOT NULL,
    [LocationCode] nvarchar(25) NOT NULL,
    [LocationGroupId] int NOT NULL,
    [LocationTypeId] int NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_LocationMaster] PRIMARY KEY ([LocationId]),
    CONSTRAINT [FK_LocationMaster_LocationGroupMaster_LocationGroupId] FOREIGN KEY ([LocationGroupId]) REFERENCES [LocationGroupMaster] ([LocationGroupId]) ON DELETE CASCADE,
    CONSTRAINT [FK_LocationMaster_LocationTypeMaster_LocationTypeId] FOREIGN KEY ([LocationTypeId]) REFERENCES [LocationTypeMaster] ([LocationTypeId]) ON DELETE CASCADE
);

CREATE TABLE [MilestoneActionMapping] (
    [MilestoneActionMappingId] int NOT NULL IDENTITY,
    [MilestoneId] int NOT NULL,
    [MilestoneActionId] int NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [IsDependentOnPreviuos] bit NOT NULL DEFAULT CAST(1 AS bit),
    [SequenceNumber] int NOT NULL,
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_MilestoneActionMapping] PRIMARY KEY ([MilestoneActionMappingId]),
    CONSTRAINT [FK_MilestoneActionMapping_MilestoneActionsMaster_MilestoneActionId] FOREIGN KEY ([MilestoneActionId]) REFERENCES [MilestoneActionsMaster] ([MilestoneActionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MilestoneActionMapping_MilestoneMaster_MilestoneId] FOREIGN KEY ([MilestoneId]) REFERENCES [MilestoneMaster] ([MilestoneId]) ON DELETE CASCADE
);

CREATE TABLE [RefreshToken] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(max) NULL,
    [Expires] datetime2 NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedByIp] nvarchar(max) NULL,
    [Revoked] datetime2 NULL,
    [RevokedByIp] nvarchar(max) NULL,
    [ReplacedByToken] nvarchar(max) NULL,
    [UserManagerId] int NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshToken_UserManager_UserManagerId] FOREIGN KEY ([UserManagerId]) REFERENCES [UserManager] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ELV] (
    [ELVId] int NOT NULL IDENTITY,
    [ELVCode] nvarchar(30) NOT NULL,
    [VehicleId] int NOT NULL,
    [DriverId] int NOT NULL,
    [VRN] nvarchar(30) NOT NULL,
    [TranType] int NOT NULL,
    [DocNumber] nvarchar(50) NULL,
    [ShipmentNo] nvarchar(50) NULL,
    [GateEntryNo] nvarchar(50) NULL,
    [CustomerVendorName] nvarchar(50) NULL,
    [ValidTill] datetime2 NULL,
    [TransactionDate] datetime2 NULL,
    [EntryDateTime] datetime2 NULL,
    [ExitDateTime] datetime2 NULL,
    [FRToBeDone] bit NULL,
    [BAToBeDone] bit NULL,
    [IsActive] bit NULL DEFAULT CAST(1 AS bit),
    [Status] nvarchar(30) NULL,
    [Remarks] nvarchar(200) NULL,
    CONSTRAINT [PK_ELV] PRIMARY KEY ([ELVId]),
    CONSTRAINT [FK_ELV_DriverMaster_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [DriverMaster] ([DriverId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ELV_VehicleMaster_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [VehicleMaster] ([VehicleId]) ON DELETE CASCADE
);

CREATE TABLE [JobMilestones] (
    [JobMilestoneId] int NOT NULL IDENTITY,
    [ELVId] int NOT NULL,
    [VehicleTransactionId] int NOT NULL,
    [MilestoneTransactionCode] nvarchar(20) NOT NULL,
    [Milestone] nvarchar(50) NOT NULL,
    [MilestoneCode] nvarchar(30) NOT NULL,
    [MilestoneDescription] nvarchar(150) NULL,
    [MilestioneEvent] nvarchar(30) NULL,
    [LocationCode] nvarchar(30) NOT NULL,
    [MilestoneSequence] int NULL,
    [IsRequiredMilestone] bit NOT NULL DEFAULT CAST(1 AS bit),
    [IsActiveMilestone] bit NOT NULL DEFAULT CAST(0 AS bit),
    [Status] nvarchar(30) NOT NULL DEFAULT N'Pending',
    [Remarks] nvarchar(200) NULL,
    [MilestoneBeginTime] datetime2 NULL,
    [MilestoneCompletionTime] datetime2 NULL,
    [IsAX4Updated] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_JobMilestones] PRIMARY KEY ([JobMilestoneId]),
    CONSTRAINT [FK_JobMilestones_VehicleTransaction_VehicleTransactionId] FOREIGN KEY ([VehicleTransactionId]) REFERENCES [VehicleTransaction] ([VehicleTransactionId]) ON DELETE CASCADE
);

CREATE TABLE [DeviceLocationMapping] (
    [DeviceLocationMappingId] int NOT NULL IDENTITY,
    [LocationId] int NOT NULL,
    [DeviceType] nvarchar(50) NOT NULL,
    [DeviceName] nvarchar(50) NOT NULL,
    [DeviceIP] nvarchar(30) NOT NULL,
    [Antenna] int NULL,
    [TransactionType] nvarchar(30) NOT NULL,
    [Remark] nvarchar(150) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_DeviceLocationMapping] PRIMARY KEY ([DeviceLocationMappingId]),
    CONSTRAINT [FK_DeviceLocationMapping_LocationMaster_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [LocationMaster] ([LocationId]) ON DELETE CASCADE
);

CREATE TABLE [WeighbridgeAllocationPerferences] (
    [WeighBridgePerferencesId] int NOT NULL IDENTITY,
    [LocationGroupId] int NOT NULL,
    [NearbyLocationId] int NOT NULL,
    [TransactionType] nvarchar(20) NOT NULL,
    [WeighmentType] nvarchar(20) NOT NULL,
    [Priority] int NOT NULL,
    [IsActive] int NOT NULL DEFAULT 1,
    [LocationMasterLocationId] int NULL,
    CONSTRAINT [PK_WeighbridgeAllocationPerferences] PRIMARY KEY ([WeighBridgePerferencesId]),
    CONSTRAINT [FK_WeighbridgeAllocationPerferences_LocationGroupMaster_LocationGroupId] FOREIGN KEY ([LocationGroupId]) REFERENCES [LocationGroupMaster] ([LocationGroupId]) ON DELETE CASCADE,
    CONSTRAINT [FK_WeighbridgeAllocationPerferences_LocationMaster_LocationMasterLocationId] FOREIGN KEY ([LocationMasterLocationId]) REFERENCES [LocationMaster] ([LocationId]) ON DELETE NO ACTION
);

CREATE TABLE [WeighBridgeMaster] (
    [WeighBridgeId] int NOT NULL IDENTITY,
    [WeighBridgeName] nvarchar(20) NOT NULL,
    [LocationId] int NOT NULL,
    [Capacity] int NOT NULL,
    [MaxQueueSize] int NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [DeactivationReason] nvarchar(150) NULL,
    [CreatedBy] nvarchar(30) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(30) NULL,
    [ModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_WeighBridgeMaster] PRIMARY KEY ([WeighBridgeId]),
    CONSTRAINT [FK_WeighBridgeMaster_LocationMaster_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [LocationMaster] ([LocationId]) ON DELETE CASCADE
);

CREATE TABLE [JobAllocationDetails] (
    [JobId] int NOT NULL IDENTITY,
    [VehicleTransactionId] int NOT NULL,
    [ELVId] int NOT NULL,
    [MaterialType] nvarchar(30) NOT NULL,
    [ProductName] nvarchar(50) NOT NULL,
    [BatchNo] nvarchar(50) NOT NULL,
    [Weight] real NULL,
    [JobAlocatedDate] datetime2 NULL,
    CONSTRAINT [PK_JobAllocationDetails] PRIMARY KEY ([JobId]),
    CONSTRAINT [FK_JobAllocationDetails_ELV_ELVId] FOREIGN KEY ([ELVId]) REFERENCES [ELV] ([ELVId]) ON DELETE CASCADE
);

CREATE TABLE [JobMilestoneDetails] (
    [JobMilestoneDetailId] int NOT NULL IDENTITY,
    [JobMilestoneId] int NOT NULL,
    [ReprocessReason] nvarchar(200) NOT NULL,
    [Status] nvarchar(30) NOT NULL DEFAULT N'Pending',
    [IsRequiredMilestone] bit NOT NULL DEFAULT CAST(1 AS bit),
    [IsActiveMilestone] bit NOT NULL DEFAULT CAST(0 AS bit),
    [MilestoneBeginTime] datetime2 NULL,
    [MilestoneCompletionTime] datetime2 NULL,
    CONSTRAINT [PK_JobMilestoneDetails] PRIMARY KEY ([JobMilestoneDetailId]),
    CONSTRAINT [FK_JobMilestoneDetails_JobMilestones_JobMilestoneId] FOREIGN KEY ([JobMilestoneId]) REFERENCES [JobMilestones] ([JobMilestoneId]) ON DELETE CASCADE
);

CREATE TABLE [MilestoneActionsTracking] (
    [MilestoneActionsTrackingId] int NOT NULL IDENTITY,
    [JobMilestoneId] int NOT NULL,
    [MilestoneAction] nvarchar(30) NOT NULL,
    [ActionCode] nvarchar(30) NOT NULL,
    [Status] nvarchar(30) NOT NULL DEFAULT N'Pending',
    [IsRequired] bit NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit),
    [IsDependent] bit NOT NULL,
    [DependentActionId] bit NOT NULL,
    [IsDependentOnAllPrevious] bit NOT NULL,
    [Sequence] int NOT NULL,
    [DeActivatedBy] nvarchar(30) NULL,
    [Remarks] nvarchar(150) NULL,
    CONSTRAINT [PK_MilestoneActionsTracking] PRIMARY KEY ([MilestoneActionsTrackingId]),
    CONSTRAINT [FK_MilestoneActionsTracking_JobMilestones_JobMilestoneId] FOREIGN KEY ([JobMilestoneId]) REFERENCES [JobMilestones] ([JobMilestoneId]) ON DELETE CASCADE
);

CREATE TABLE [WeighBridgeAvailabilityStatus] (
    [WeighBridgeAvailabilityId] int NOT NULL IDENTITY,
    [WeighBridgeId] int NOT NULL,
    [TotalInProgressTransaction] int NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_WeighBridgeAvailabilityStatus] PRIMARY KEY ([WeighBridgeAvailabilityId]),
    CONSTRAINT [FK_WeighBridgeAvailabilityStatus_WeighBridgeMaster_WeighBridgeId] FOREIGN KEY ([WeighBridgeId]) REFERENCES [WeighBridgeMaster] ([WeighBridgeId]) ON DELETE CASCADE
);

GO

CREATE TABLE [WeighBridgeTransaction] (
    [WeighBridgeTransactionId] int NOT NULL IDENTITY,
    [JobMilestoneId] int NOT NULL,
    [WeighBridgeId] int NOT NULL,
    [ActualTareweight] real NULL,
    [ActualGrossWeight] real NULL,
    [TransactionDateTime] datetime2 NULL,
    [IsImageCaptured] bit NOT NULL DEFAULT CAST(0 AS bit),
    [WeighBridgeImagePath] nvarchar(200) NULL,
    [Status] nvarchar(30) NULL,
    CONSTRAINT [PK_WeighBridgeTransaction] PRIMARY KEY ([WeighBridgeTransactionId]),
    CONSTRAINT [FK_WeighBridgeTransaction_JobMilestones_JobMilestoneId] FOREIGN KEY ([JobMilestoneId]) REFERENCES [JobMilestones] ([JobMilestoneId]) ON DELETE CASCADE,
    CONSTRAINT [FK_WeighBridgeTransaction_WeighBridgeMaster_WeighBridgeId] FOREIGN KEY ([WeighBridgeId]) REFERENCES [WeighBridgeMaster] ([WeighBridgeId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DeviceLocationMapping_LocationId] ON [DeviceLocationMapping] ([LocationId]);

GO

CREATE INDEX [IX_ELV_DriverId] ON [ELV] ([DriverId]);

GO

CREATE INDEX [IX_ELV_VehicleId] ON [ELV] ([VehicleId]);

GO

CREATE INDEX [IX_JobAllocationDetails_ELVId] ON [JobAllocationDetails] ([ELVId]);

GO

CREATE INDEX [IX_JobMilestoneDetails_JobMilestoneId] ON [JobMilestoneDetails] ([JobMilestoneId]);

GO

CREATE INDEX [IX_JobMilestones_VehicleTransactionId] ON [JobMilestones] ([VehicleTransactionId]);

GO

CREATE INDEX [IX_LocationMaster_LocationGroupId] ON [LocationMaster] ([LocationGroupId]);

GO

CREATE INDEX [IX_LocationMaster_LocationTypeId] ON [LocationMaster] ([LocationTypeId]);

GO

CREATE INDEX [IX_MilestoneActionMapping_MilestoneActionId] ON [MilestoneActionMapping] ([MilestoneActionId]);

GO

CREATE INDEX [IX_MilestoneActionMapping_MilestoneId] ON [MilestoneActionMapping] ([MilestoneId]);

GO

CREATE INDEX [IX_MilestoneActionsTracking_JobMilestoneId] ON [MilestoneActionsTracking] ([JobMilestoneId]);

GO

CREATE INDEX [IX_RefreshToken_UserManagerId] ON [RefreshToken] ([UserManagerId]);

GO

CREATE INDEX [IX_VehicleTransaction_DriverId] ON [VehicleTransaction] ([DriverId]);

GO

CREATE INDEX [IX_WeighbridgeAllocationPerferences_LocationGroupId] ON [WeighbridgeAllocationPerferences] ([LocationGroupId]);

GO

CREATE INDEX [IX_WeighbridgeAllocationPerferences_LocationMasterLocationId] ON [WeighbridgeAllocationPerferences] ([LocationMasterLocationId]);

GO

CREATE INDEX [IX_WeighBridgeAvailabilityStatus_WeighBridgeId] ON [WeighBridgeAvailabilityStatus] ([WeighBridgeId]);

GO

CREATE INDEX [IX_WeighBridgeMaster_LocationId] ON [WeighBridgeMaster] ([LocationId]);

GO

CREATE INDEX [IX_WeighBridgeTransaction_JobMilestoneId] ON [WeighBridgeTransaction] ([JobMilestoneId]);

GO

CREATE INDEX [IX_WeighBridgeTransaction_WeighBridgeId] ON [WeighBridgeTransaction] ([WeighBridgeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211216171543_InitialCreateTable', N'3.1.5');

END;

GO

USE [URGE_TRUCK]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDashBoardData]    Script Date: 25-01-2022 12:30:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER  PROCEDURE [dbo].[sp_GetDashBoardData] 
    @LocationCode varchar(50),
	@defualt varchar(5)
AS
BEGIN
	Declare @SQLQuery AS NVarchar(4000)  

	IF (@defualt ='All')
    BEGIN
	Set @SQLQuery = N'SELECT  TOP 5 VRN,TransactionDate,TranType,VehicleTransactionCode,JB.Milestone,JB.LocationCode,LO.LocationName,JB.VehicleTransactionId,JB.Status,jb.MilestioneEvent
	FROM VehicleTransaction AS VT  INNER JOIN   JobMilestones as JB ON VT.VehicleTransactionId =JB.VehicleTransactionId
	INNER JOIN LocationMaster AS LO ON LO.LocationCode=JB.LocationCode WHERE  JB.Status  = ''Open'''
	END
	ELSE
	BEGIN
	Set @SQLQuery = N'SELECT  TOP 5 VRN,TransactionDate,TranType,VehicleTransactionCode,JB.Milestone,JB.LocationCode,LO.LocationName,JB.VehicleTransactionId,JB.Status,jb.MilestioneEvent
	FROM VehicleTransaction AS VT  INNER JOIN   JobMilestones as JB ON VT.VehicleTransactionId =JB.VehicleTransactionId
	INNER JOIN LocationMaster AS LO ON LO.LocationCode=JB.LocationCode WHERE JB.LocationCode= '+ @LocationCode+'AND (JB.Status  = ''Open'' OR JB.Status=''Completed'')'
	END
	EXECUTE sp_executesql @SQLQuery

	print @SQLQuery

END