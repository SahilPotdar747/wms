
/*
--delete all tables including masters
delete UserManager
delete ApplicationConfigMaster
delete ExistCheckListMaster
delete MobileRequest
delete LEDConfigurationMaster 
delete TicketCategory
delete TicketSubCategory
delete TicketUrgency
delete TransactionSLAMaster
delete CommonMasterData

--use the above statements if you are using insert data from other server

delete VehicleMaster
delete VehicleTransaction
delete DriverMaster
delete JobMilestoneDetails
delete JobMilestones
delete WeighBridgeMaster
delete WeighBridgeTransaction
delete WeighbridgeAllocationPerferences
delete CurrentQueue
delete Location
delete MilestoneMaster
delete MilestoneActionsMaster
delete milestoneactionmapping
delete elv 
delete JobAllocationDetails
delete JobMilestones
delete JobMilestoneDetails
delete DeviceLocationMapping
truncate table ApplicationConfigMaster
delete ProductMaster 
delete RoleMaster 
delete UserAccessManager 
delete UserScreenMaster  
delete UserScreenMaster
delete InPlantTransaction
delete InPlantTransactionTracking 
delete InPlantWBRules
delete TransGenerator
delete JobScheduleMaster

--delete only transactions 
delete VehicleTransaction
delete JobMilestoneDetails
delete JobMilestones
delete WeighBridgeTransaction
delete elv 
delete JobAllocationDetails
delete JobAllocationProductDetails
delete JobAllocationProductItems
delete LEDNotification
delete CurrentQueue
delete WeighBridgeTransaction
delete CrossCheckDailySchedule
delete MilestoneActionsTracking
--delete CrossCheckEventMaster
--delete CrossCheckWGBroup
--delete CrossCheckEventSchedule
--delete TicketTransaction
--delete TicketTransactionHistory
--delete SLAManagerTransaction


--DBCC CHECKIDENT ('UserScreenMaster', RESEED, 0)
*/


GO 
	IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RoleMaster'))
	BEGIN
		Delete Rolemaster 
		DBCC CHECKIDENT ('Rolemaster', RESEED, 0)
		set identity_insert Rolemaster ON 
		
		insert RoleMaster (RoleId , RoleName,RoleGroup)
		values 
		(1,'SuperAdmin','Admin'),
		(2, 'Admin','Admin'),
		(3, 'CT Manager','ControlTower'),
		(4, 'CT Supervisor','ControlTower'),
		(5, 'CT Operator','ControlTower'),
		(6, 'Security','Security'),
		(7, 'Operation Manager','Operation')
			
		set identity_insert RoleMaster OFF 
END

GO 
	IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserScreenMaster'))
	BEGIN	
	    Delete UserScreenMaster 
		DBCC CHECKIDENT ('UserScreenMaster', RESEED, 0)
		set identity_insert UserScreenMaster ON

INSERT [dbo].[UserScreenMaster] ([UserScreenId], [MenuName], [ScreenName], [ScreenCode], [ParentId], [RoutingURL], [MenuIcon], [IsActive]) 

VALUES (1, N'Admin', N'', N'', 0, N'', N'pi pi-user', 1)
 , (2, N'Master', N'', N'', 0, N'', N'fa fa-cogs', 1)
 , (3, N'Transaction', N'', N'', 0, N'', N'fa fa-cogs', 1)
 , (4, N'User Management', N'User Management', N'USERMGMT', 1, N'/user-registration', N'', 1)
 , (5, N'Role Management', N'Role Management', N'ROLEMGMT', 1, N'/role', N'', 1)
 , (6, N'User Role Mapping', N'User Role Mapping', N'URMAP', 1, N'/userRoleMapping', N'', 1)
 , (8, N'Location', N'Location', N'LOCATION', 2, N'/location', N'', 1)
 , (9, N'Supplier', N'Supplier', N'SUPPLIER', 2, N'/supplier', N'', 1)
 , (10, N'Product', N'Product', N'PRODUCT', 2, N'/product', N'', 1)
 , (12, N'GRN', N'GRN', N'GRN', 3, N'/GRN', N'', 1)
 , (13, N'GRN Detail', N'GRN Detail', N'GRN DETAIL', 3, N'/grn-details/0', N'', 1)
 , (14, N'Storage', N'Storage', N'STORAGE', 3, N'/storage', N'', 1)
 , (15, N'Storage Detail', N'Storage Detail', N'Storage Detail', 3, N'/storage-details', N'', 1)
 , (15, N'Outward', N'Outward', N'OUTWARD', 3, N'/outward', N'', 1)
 , (15, N'Delevery Challan', N'DeleveryChallan', N'DELEVERYC', 3, N'/deliveryChallan', N'', 1)
  set identity_insert UserScreenMaster OFF

	END  
GO 
	IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserAccessManager'))
	BEGIN
		insert UserAccessManager (RoleId,UserScreenId,IsActive, CanUpdate, CanCreate, CanDeactivate)
		values 
		(1,(select UserScreenId from UserScreenMaster where ScreenCode = 'TATREPORT'),1, 1, 0, 0),
		(1,(select UserScreenId from UserScreenMaster where ScreenCode = 'GATEREPORT'),1, 1, 0, 0)
	END 
GO 
	IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ExistCheckListMaster'))
	BEGIN
		insert ExistCheckListMaster values 
		('Properly Loaded' , 1, 1, 'admin' , getdate(), null, null), 
		('Product Packing Ok' , 1, 1, 'admin' , getdate(), null, null), 
		('Tyre Pressure OK' , 1, 1, 'admin' , getdate(), null, null), 
		('Vehicle Body OK' , 1, 1, 'admin' , getdate(), null, null), 
		('Documents OK' , 1, 1, 'admin' , getdate(), null, null), 
		('Driver Not Drunk' , 1, 1, 'admin' , getdate(), null, null), 
		('Fuel Available' , 1, 1, 'admin' , getdate(), null, null), 
		('Vehicle Maintained' , 1, 1, 'admin' , getdate(), null, null)
	END 

	GO
IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserAccessManager'))
	BEGIN
	    Delete UserAccessManager 
		DBCC CHECKIDENT ('UserAccessManager', RESEED, 0)
		
		INSERT INTO [dbo].[UserAccessManager]
           ([RoleId]
           ,[UserScreenId]
           ,[CanCreate]
           ,[CanUpdate]
           ,[CanDeactivate]
           ,[IsActive])
     VALUES
			( 1, 1, 1,1,1,1) , 
			( 1, 2, 1,1,1,1) , 
			( 1, 3, 1,1,1,1) , 
			( 1, 4, 1,1,1,1) , 
			( 1, 5, 1,1,1,1) , 
			( 1, 6, 1,1,1,1) ,
			( 1, 7, 1,1,1,1) 
   set identity_insert UserScreenMaster OFF
END

 
GO

IF(EXISTS(SELECT count(0) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserManager'))
	BEGIN
		SET IDENTITY_INSERT [dbo].[UserManager] ON 
		INSERT [dbo].[UserManager] ([Id], [UserName], [FirstName], [LastName], [Email], [MobileNumber], [PasswordHash], [AcceptTerms], [RoleId], [VerificationToken], [Verified], [ResetToken], [ResetTokenExpires], [PasswordReset], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'superadmin', N'Super', N'Admin', N'girish.eng@gmail.com', NULL, N'$2a$11$SXmfW5fWxsi2QCG5M1XfROsAeCTeKS4imcAPdx4RUSH/7.g.E0el.', 1, 1, N'D5F3D8B2EE7693E82879BA5423AB9C71F6F7E3F1B2C58B23F884BF185864186736E3009BD10C0393', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-02' AS Date), NULL, CAST(N'0001-01-01' AS Date))
		INSERT [dbo].[UserManager] ([Id], [UserName], [FirstName], [LastName], [Email], [MobileNumber], [PasswordHash], [AcceptTerms], [RoleId], [VerificationToken], [Verified], [ResetToken], [ResetTokenExpires], [PasswordReset], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'ctmanager', N'CT', N'Manager', N'girish.eng@gmail.com', NULL, N'$2a$11$SXmfW5fWxsi2QCG5M1XfROsAeCTeKS4imcAPdx4RUSH/7.g.E0el.', 1, 3, N'D5F3D8B2EE7693E82879BA5423AB9C71F6F7E3F1B2C58B23F884BF185864186736E3009BD10C0393', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-02' AS Date), NULL, CAST(N'0001-01-01' AS Date))
		INSERT [dbo].[UserManager] ([Id], [UserName], [FirstName], [LastName], [Email], [MobileNumber], [PasswordHash], [AcceptTerms], [RoleId], [VerificationToken], [Verified], [ResetToken], [ResetTokenExpires], [PasswordReset], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'ctsupervisor', N'CT', N'Supervisor', N'girish.eng@gmail.com', NULL, N'$2a$11$SXmfW5fWxsi2QCG5M1XfROsAeCTeKS4imcAPdx4RUSH/7.g.E0el.', 1, 4, N'D5F3D8B2EE7693E82879BA5423AB9C71F6F7E3F1B2C58B23F884BF185864186736E3009BD10C0393', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-02' AS Date), NULL, CAST(N'0001-01-01' AS Date))
		INSERT [dbo].[UserManager] ([Id], [UserName], [FirstName], [LastName], [Email], [MobileNumber], [PasswordHash], [AcceptTerms], [RoleId], [VerificationToken], [Verified], [ResetToken], [ResetTokenExpires], [PasswordReset], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'ctoperator', N'CT', N'Operator', N'girish.eng@gmail.com', NULL, N'$2a$11$SXmfW5fWxsi2QCG5M1XfROsAeCTeKS4imcAPdx4RUSH/7.g.E0el.', 1, 5, N'D5F3D8B2EE7693E82879BA5423AB9C71F6F7E3F1B2C58B23F884BF185864186736E3009BD10C0393', NULL, NULL, NULL, NULL, NULL, CAST(N'2022-05-02' AS Date), NULL, CAST(N'0001-01-01' AS Date))
		SET IDENTITY_INSERT [dbo].[UserManager] OFF
	END 








