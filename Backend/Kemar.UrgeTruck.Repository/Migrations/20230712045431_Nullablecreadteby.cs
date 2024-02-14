using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kemar.UrgeTruck.Repository.Migrations
{
    public partial class Nullablecreadteby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationConfigMaster",
                columns: table => new
                {
                    AppConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationConfigMaster", x => x.AppConfigId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMaster",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    State = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMaster", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Dispatch",
                columns: table => new
                {
                    DispatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatch", x => x.DispatchId);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfig",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    To = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CC = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    From = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfig", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "FailureLog",
                columns: table => new
                {
                    FailureLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FailureTime = table.Column<DateTime>(type: "datetime2", maxLength: 300, nullable: true),
                    RepairTime = table.Column<DateTime>(type: "datetime2", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailureLog", x => x.FailureLogId);
                });

            migrationBuilder.CreateTable(
                name: "LocationCategory",
                columns: table => new
                {
                    LocationCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCategory", x => x.LocationCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RetryObjectContainer",
                columns: table => new
                {
                    RetryObjectContainerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotifierName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleTransactionId = table.Column<int>(type: "int", nullable: false),
                    JobMilestoneId = table.Column<int>(type: "int", nullable: false),
                    MilestoneActionsTrackingId = table.Column<int>(type: "int", nullable: false),
                    TriggeredDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfRetry = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MaxRetry = table.Column<int>(type: "int", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RetryFailureReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetryObjectContainer", x => x.RetryObjectContainerId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleGroup = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftMaster",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftMaster", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierMaster",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContanctNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    State = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierMaster", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyServiceIntegrationTracking",
                columns: table => new
                {
                    IntegrationTrackingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitiatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APIType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyServiceIntegrationTracking", x => x.IntegrationTrackingId);
                });

            migrationBuilder.CreateTable(
                name: "TransGenerator",
                columns: table => new
                {
                    TransactionIdKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastTransactionNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransGenerator", x => x.TransactionIdKey);
                });

            migrationBuilder.CreateTable(
                name: "Uom",
                columns: table => new
                {
                    UomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uom", x => x.UomId);
                });

            migrationBuilder.CreateTable(
                name: "UserScreenMaster",
                columns: table => new
                {
                    UserScreenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ScreenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    RoutingURL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MenuIcon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScreenMaster", x => x.UserScreenId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentLocationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DIsplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_LocationCategory_LocationCategoryId",
                        column: x => x.LocationCategoryId,
                        principalTable: "LocationCategory",
                        principalColumn: "LocationCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaster",
                columns: table => new
                {
                    ProductMasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HSNCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Make = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaster", x => x.ProductMasterId);
                    table.ForeignKey(
                        name: "FK_ProductMaster_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptTerms = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordReset = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserManager_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderMaster",
                columns: table => new
                {
                    POId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PONumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderMaster", x => x.POId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderMaster_SupplierMaster_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierMaster",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    UomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Uom_UomId",
                        column: x => x.UomId,
                        principalTable: "Uom",
                        principalColumn: "UomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessManager",
                columns: table => new
                {
                    UserAccessManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserScreenId = table.Column<int>(type: "int", nullable: false),
                    CanCreate = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    CanDeactivate = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessManager", x => x.UserAccessManagerId);
                    table.ForeignKey(
                        name: "FK_UserAccessManager_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessManager_UserScreenMaster_UserScreenId",
                        column: x => x.UserScreenId,
                        principalTable: "UserScreenMaster",
                        principalColumn: "UserScreenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRN",
                columns: table => new
                {
                    GRNId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductMasterId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    PONumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    POProductQuantity = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceProductQuantity = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    POFile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceFile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRN", x => x.GRNId);
                    table.ForeignKey(
                        name: "FK_GRN_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRN_ProductMaster_ProductMasterId",
                        column: x => x.ProductMasterId,
                        principalTable: "ProductMaster",
                        principalColumn: "ProductMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRN_SupplierMaster_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierMaster",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_UserManager_UserManagerId",
                        column: x => x.UserManagerId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLocationAccess",
                columns: table => new
                {
                    UserLocationAccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocationAccess", x => x.UserLocationAccessId);
                    table.ForeignKey(
                        name: "FK_UserLocationAccess_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLocationAccess_UserManager_UserId",
                        column: x => x.UserId,
                        principalTable: "UserManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GatePassMaster",
                columns: table => new
                {
                    GatePassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePassNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POId = table.Column<int>(type: "int", nullable: false),
                    DeliveryMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RGPGenerated = table.Column<bool>(type: "bit", nullable: false),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GatePassDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SupplierMasterSupplierId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatePassMaster", x => x.GatePassId);
                    table.ForeignKey(
                        name: "FK_GatePassMaster_PurchaseOrderMaster_POId",
                        column: x => x.POId,
                        principalTable: "PurchaseOrderMaster",
                        principalColumn: "POId");
                    table.ForeignKey(
                        name: "FK_GatePassMaster_SupplierMaster_SupplierMasterSupplierId",
                        column: x => x.SupplierMasterSupplierId,
                        principalTable: "SupplierMaster",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    PODId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POId = table.Column<int>(type: "int", nullable: false),
                    ProductMasterId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.PODId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_ProductMaster_ProductMasterId",
                        column: x => x.ProductMasterId,
                        principalTable: "ProductMaster",
                        principalColumn: "ProductMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrderMaster_POId",
                        column: x => x.POId,
                        principalTable: "PurchaseOrderMaster",
                        principalColumn: "POId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LocationCategoryId = table.Column<int>(type: "int", nullable: false),
                    TimeUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeUnitValue = table.Column<int>(type: "int", nullable: false),
                    RateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rate_LocationCategory_LocationCategoryId",
                        column: x => x.LocationCategoryId,
                        principalTable: "LocationCategory",
                        principalColumn: "LocationCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rate_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StockEntryDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    StockExitDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    ItemBarcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stock_Location_StockId",
                        column: x => x.StockId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stock_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanMaster",
                columns: table => new
                {
                    DCMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNId = table.Column<long>(type: "bigint", nullable: false),
                    DcStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanMaster", x => x.DCMId);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanMaster_GRN_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GRN",
                        principalColumn: "GRNId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRNDetails",
                columns: table => new
                {
                    GRNDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNId = table.Column<long>(type: "bigint", nullable: false),
                    DropLoc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RackNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SubRack = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductSerialKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNDetails", x => x.GRNDetailsId);
                    table.ForeignKey(
                        name: "FK_GRNDetails_GRN_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GRN",
                        principalColumn: "GRNId");
                });

            migrationBuilder.CreateTable(
                name: "GatePassDetails",
                columns: table => new
                {
                    GPDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePassId = table.Column<int>(type: "int", nullable: false),
                    PartCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedQuantity = table.Column<int>(type: "int", nullable: false),
                    RejectedQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatePassDetails", x => x.GPDId);
                    table.ForeignKey(
                        name: "FK_GatePassDetails_GatePassMaster_GatePassId",
                        column: x => x.GatePassId,
                        principalTable: "GatePassMaster",
                        principalColumn: "GatePassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispatchDetail",
                columns: table => new
                {
                    DispatchDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DispatchId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    PartyDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemBarcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchDetail", x => x.DispatchDetailId);
                    table.ForeignKey(
                        name: "FK_DispatchDetail_Dispatch_DispatchId",
                        column: x => x.DispatchId,
                        principalTable: "Dispatch",
                        principalColumn: "DispatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DispatchDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DispatchDetail_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DamageDetail",
                columns: table => new
                {
                    DamageDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNDetailsId = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageDetail", x => x.DamageDetailId);
                    table.ForeignKey(
                        name: "FK_DamageDetail_GRNDetails_GRNDetailsId",
                        column: x => x.GRNDetailsId,
                        principalTable: "GRNDetails",
                        principalColumn: "GRNDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChallanDetails",
                columns: table => new
                {
                    DCDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DCMId = table.Column<int>(type: "int", nullable: false),
                    GRNDetailsId = table.Column<long>(type: "bigint", nullable: false),
                    ChallanNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChallanDetails", x => x.DCDId);
                    table.ForeignKey(
                        name: "FK_DeliveryChallanDetails_DeliveryChallanMaster_DCMId",
                        column: x => x.DCMId,
                        principalTable: "DeliveryChallanMaster",
                        principalColumn: "DCMId");
                    table.ForeignKey(
                        name: "FK_DeliveryChallanDetails_GRNDetails_GRNDetailsId",
                        column: x => x.GRNDetailsId,
                        principalTable: "GRNDetails",
                        principalColumn: "GRNDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RGPMaster",
                columns: table => new
                {
                    RGPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatePassId = table.Column<int>(type: "int", nullable: false),
                    TotalNoOfRGPItems = table.Column<int>(type: "int", nullable: false),
                    PartCode = table.Column<int>(type: "int", nullable: false),
                    ProductSerialKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RGPDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GatePassDetailsGPDId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RGPMaster", x => x.RGPId);
                    table.ForeignKey(
                        name: "FK_RGPMaster_GatePassDetails_GatePassDetailsGPDId",
                        column: x => x.GatePassDetailsGPDId,
                        principalTable: "GatePassDetails",
                        principalColumn: "GPDId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RGPMaster_GatePassMaster_GatePassId",
                        column: x => x.GatePassId,
                        principalTable: "GatePassMaster",
                        principalColumn: "GatePassId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamageDetail_GRNDetailsId",
                table: "DamageDetail",
                column: "GRNDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanDetails_DCMId",
                table: "DeliveryChallanDetails",
                column: "DCMId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanDetails_GRNDetailsId",
                table: "DeliveryChallanDetails",
                column: "GRNDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChallanMaster_GRNId",
                table: "DeliveryChallanMaster",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchDetail_DispatchId",
                table: "DispatchDetail",
                column: "DispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchDetail_ProductId",
                table: "DispatchDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchDetail_StockId",
                table: "DispatchDetail",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassDetails_GatePassId",
                table: "GatePassDetails",
                column: "GatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassMaster_POId",
                table: "GatePassMaster",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_GatePassMaster_SupplierMasterSupplierId",
                table: "GatePassMaster",
                column: "SupplierMasterSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_GRN_LocationId",
                table: "GRN",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GRN_ProductMasterId",
                table: "GRN",
                column: "ProductMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_GRN_SupplierId",
                table: "GRN",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetails_GRNId",
                table: "GRNDetails",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationCategoryId",
                table: "Location",
                column: "LocationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UomId",
                table: "Product",
                column: "UomId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_ProductCategoryId",
                table: "ProductMaster",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_POId",
                table: "PurchaseOrderDetails",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductMasterId",
                table: "PurchaseOrderDetails",
                column: "ProductMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderMaster_SupplierId",
                table: "PurchaseOrderMaster",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_LocationCategoryId",
                table: "Rate",
                column: "LocationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_ProductId",
                table: "Rate",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserManagerId",
                table: "RefreshToken",
                column: "UserManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RGPMaster_GatePassDetailsGPDId",
                table: "RGPMaster",
                column: "GatePassDetailsGPDId");

            migrationBuilder.CreateIndex(
                name: "IX_RGPMaster_GatePassId",
                table: "RGPMaster",
                column: "GatePassId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessManager_RoleId",
                table: "UserAccessManager",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessManager_UserScreenId",
                table: "UserAccessManager",
                column: "UserScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocationAccess_LocationId",
                table: "UserLocationAccess",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocationAccess_UserId",
                table: "UserLocationAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserManager_RoleId",
                table: "UserManager",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationConfigMaster");

            migrationBuilder.DropTable(
                name: "CustomerMaster");

            migrationBuilder.DropTable(
                name: "DamageDetail");

            migrationBuilder.DropTable(
                name: "DeliveryChallanDetails");

            migrationBuilder.DropTable(
                name: "DispatchDetail");

            migrationBuilder.DropTable(
                name: "EmailConfig");

            migrationBuilder.DropTable(
                name: "FailureLog");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RetryObjectContainer");

            migrationBuilder.DropTable(
                name: "RGPMaster");

            migrationBuilder.DropTable(
                name: "ShiftMaster");

            migrationBuilder.DropTable(
                name: "ThirdPartyServiceIntegrationTracking");

            migrationBuilder.DropTable(
                name: "TransGenerator");

            migrationBuilder.DropTable(
                name: "UserAccessManager");

            migrationBuilder.DropTable(
                name: "UserLocationAccess");

            migrationBuilder.DropTable(
                name: "DeliveryChallanMaster");

            migrationBuilder.DropTable(
                name: "GRNDetails");

            migrationBuilder.DropTable(
                name: "Dispatch");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "GatePassDetails");

            migrationBuilder.DropTable(
                name: "UserScreenMaster");

            migrationBuilder.DropTable(
                name: "UserManager");

            migrationBuilder.DropTable(
                name: "GRN");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "GatePassMaster");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "ProductMaster");

            migrationBuilder.DropTable(
                name: "Uom");

            migrationBuilder.DropTable(
                name: "PurchaseOrderMaster");

            migrationBuilder.DropTable(
                name: "LocationCategory");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "SupplierMaster");
        }
    }
}
