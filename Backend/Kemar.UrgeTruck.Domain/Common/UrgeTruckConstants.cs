using System.Collections.Generic;

namespace Kemar.UrgeTruck.Domain.Common
{
    public static class TagTypeConstants
    {
        public const string FastTage = "FastTag";
        public const string RFIDTag = "RFIDTag";
    }

    public static class LocationTypeConstants
    {
        public const string OSPP = "OSPP";
        public const string Plant_Gate = "Plant Gate";
        public const string TMT_Yard = "TMT Yard";
        public const string Coil_Yard = "Coil Yard";
        public const string Billet_Yard = "Billet Yard";
    }

    public static class MilestoneStatusConstants
    {
        public const string Open = "Open";
        public const string Pending = "Pending";
        public const string Completed = "Completed";
        public const string Failed = "Failed";
        public const string Cancelled = "Cancelled";
        public const string ReOpen = "ReOpen";
        public const string Invalid = "Invalid";
        public const string InProgress = "InProgress";
        public const string SystemByPass = "SystemByPass";
        public const string UserByPass = "UserByPass";
        
    }

    public static class TransactionTypeConstants
    {
        public const int Outbound = 1;
        public const int Inbound = 2;
        //public const int Internal = 3;
        public const int InPlant = 4;
        public const int Other = 5;
        public const string OutboundValue = "Outbound";
        public const string InboundValue = "Inbound";
        //public const string InternalValue = "Internal";
        public const string InPlantValue = "InPlant";
        public const string OtherValue = "Other";
    }
    public static class TransactionStatusConstants
    {
        public const string Pending = "Pending";
        public const string Cancelled = "Cancelled";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
        public const string YMSUpdatedETrax = "UpdatedETrax";
    }
    public static class TransactionsequenceConstants
    {
        public const int one = 1;

    }
    public static class TransactionMappingConstants
    {
        public static Dictionary<string, int> TransactionTypeLookup = new Dictionary<string, int>
            {
                { TransactionTypeConstants.OutboundValue , TransactionTypeConstants.Outbound },
                { TransactionTypeConstants.InboundValue , TransactionTypeConstants.Inbound },
                { TransactionTypeConstants.InPlantValue, TransactionTypeConstants.InPlant }
            };
    }
    public static class ResponseStatus
    {
        public const string Success = "Success";
        public const string Failed = "Failed";
        public const string Cancelled = "Cancelled";
        public const string Invalid = "Invalid";
        public const string PartialSuccess = "Succeeded Partially";
    }

    public static class MilestoneActionsConstants
    {
        public const string AX4_MilestoneUpdate = "AX4NOTIFY";
        public const string BoomBarrierUp = "BU";
        public const string DriverFaceRecognizer = "FR";
        public const string DriverBreathAnalyzer = "BA";
        public const string LEDDisplay = "LEDMSG";
        public const string TrafficLight = "TL";
        public const string TakeWeighment = "WEIGHMENT";
        public const string WeighbridgeAllocation = "WBALLOC";
        public const string LoadingBegin = "LOADBEGIN";
        public const string LoadingComplete = "LOADCOMPLETE";
        public const string UnloadingBegin = "UNLOADBEGIN";
        public const string UnloadingComplete = "UNLOADCOMPLETE";
        public const string InvoicingComplete = "INVCOMPLETE";
        public const string PhysicalCheckComplete = "PHYCHKCOMPLETE";
        public const string EntryInsepction = "EIMG";
        public const string YMSUpdate = "YMS";
    }

    public static class Ax4UserDesciption
    {
        public const string Defualt_AX4_CreatedBy_User = "AX4User";

    }

    public static class MilestoneConstants
    {
        public const string OSPP = "OSPP";
        public const string PLANTGATE = "PLANT GATE";
        public const string WEIGHMENT = "WEIGHMENT";
        public const string LOADINGYARD = "LOADING YARD";
        public const string STORES = "STORES";
        public const string RMHS = "RMHS";
        public const string INVOICING = "INVOICING";
        public const string PHYSICALCHECK = "PHYSICAL CHECK";
        public const string UNLOADING = "UNLOADING";
    }

    public static class MilestoneEventConstants
    {
        public const string IN = "IN";
        public const string OUT = "OUT";
        public const string TARE = "TARE";
        public const string GROSS = "GROSS";
        public const string BEGIN = "BEGIN";
        public const string COMPLETE = "COMPLETE";
    }

    public static class MilestoneCodeConstants
    {
        public const string OSPP_IN = "1001";
        public const string OSPP_OUT = "1002";
        public const string PLANT_GATE_IN = "1003";
        public const string PLANT_GATE_OUT = "1004";
        public const string WEIGHMENT_TARE = "1005";
        public const string WEIGHMENT_GROSS = "1006";
        public const string LOADING_YARD_BEGIN = "1007";
        public const string LOADING_YARD_COMPLETE = "1008";
        public const string STORES_BEGIN = "1009";
        public const string STORES_COMPLETE = "1010";
        public const string RMHS_COMPLETE = "1011";
        public const string INVOICING_COMPLETE = "1012";
        public const string PHYSICAL_CHECK_COMPLETE = "1013";
        public const string UNLOADING_YARD_BEGIN = "1014";
        public const string UNLOADING_YARD_COMPLETE = "1015";
        public const string GRN = "1016";
    }
    public static class ELVEventConstants
    {
        public const string ParkingIn = "InParking";
        public const string ParkingOut = "InTransit";
        public const string InPlant = "InPlant";
        public const string Completed = "Completed";
        public const string Failed = "Failed";
        public const string Cancelled = "Cancelled";
        public const string CalledIn = "CalledIn";
        public const string Pending = "Pending";
    }

    public static class LoadUnloadTypeConstants
    {
        public const int LoadTranType = 1;
        public const int UnloadTranType = 2;
    }

    public static class OperationTypeConstants
    {
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
    }

    public static class WeighmentTypeConstants
    {
        public const string TARE = "TARE";
        public const string GROSS = "GROSS";
        public const string OVERWEIGHT = "OVERWEIGHT";
        public const string UNDERWEIGHT = "UNDERWEIGHT";
        public const string CANCELLED = "Cancelled";
    }

    public static class Lists
    {
        public static List<string> LEDLanguages = new List<string> { "English" };
    }

    public static class ApplicationConfigConstant
    {
        public const string WEIGHMENT_TOLERANCE_KEY = "WEIGHMENT_TOLERANCE";
        public const string WEIGHMENT_TOLERANCE_KEY_TARE = "WEIGHMENT_TOLERANCE_TARE";
        public const string WEIGHMENT_TOLERANCE_KEY_GROSS = "WEIGHMENT_TOLERANCE_GROSS";
        public const string WEIGHMENT_TOLERANCE_KEY_CROSSCHECK = "WEIGHMENT_TOLERANCE_CROSSCHECK";
        public const string WEIGHMENT_CROSSCHECK_KEY = "CROSSCHECK";
        public const string NoOf_WB_Cross_Check_Each_Shift_Param = "NoOfWBCCEachShift";
        public const string AutoWBAllocation = "AUTO_WB_ALLOC";
        public const string AutoWBAllocationEnable = "AUTO_WB_ALLOC_ENABLED";
        public const string WBAllocationStrategy = "WB_ALLOC_STRATEGY"; // 0 = Auto Allocation, 1 = Bussiness Layer Application, 2 = Any
        public const string GateAllocationStrategy = "GATE_ALLOC_STRATEGY"; // 1 = Bussiness Layer Application, 2 = Any
        public const string ELVForRunningTransaction = "ELV_FOR_RUNNING_TRANSACTION"; // 0 - False, 1 - True
        public const string YMSService = "YMS_Reader_Service"; // TRUE, FALSE
        public const string YMSServiceRunningTime = "30";
        public const string Block_BlackListed = "BLOCK_BLACKLISTED"; // TRUE, FALSE
    }

    public static class AllocationStrategyConstant
    {
        public const string AutoAllocation = "0";
        public const string BusinessLayerApplicationAllocation = "1";
        public const string AnyAllocation = "2";
    }

    public static class MobileAppRequestConstants
    {
        public const string Success = "Success";
        public const string Fail = "Fail";
        public const string DuplicateRecord = "Duplicate";
        public const string RecordNotFound = "RecordNotFound";
        public const string InValid = "Invalid";
    }

    public static class DeviceTypeConstants
    {
        public const string BREATH_ANALYSER = "BA";
        public const string CCTV = "CCTV";
        public const string CONTROLLER = "Controller";
        public const string FACE_READER = "FR";
        public const string LED = "LED";
        public const string RFID = "RFID";
        public const string IPC = "IPC";
        public const string LOCATIONPC = "LOCATIONPC";
    }

    public static class DeviceInstalltionDirection
    {
        public const string IN = "In";
        public const string OUT = "Out";
    }

    public static class AX4EventConstant
    {
        public const string MILESTONE = "MILESTONE";
        public const string ELV = "ELV";
        public const string JOBALLOC = "JOBALLOC";
        public const string DRIVER = "DRIVER";
        public const string TRANSPORTER = "TRANSPORTER";
        public const string VEHICLE = "VEHICLE";
        public const string INPLANTMILESTONE = "INPLANTMILESTONE";
        public const string LOCATION = "LOCATION";
    }

    public static class WeighmentDecripancyConstant
    {
        public const string Accept = "Accept";
        public const string AcceptWithReWeighment = "AcceptWithReWeighment";
        public const string Reject = "Reject";
        public const string ReallocatedWb = "ReallocatedWb";
    }

    public static class RoleConstant
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
    }

    public static class RoleGroupConstant
    {
        public const string Admin= "Admin";
        public const string Operation = "Operation";
        public const string ControlTower = "ControlTower";
        public const string Security = "Security";
        public const string Management = "Management";
    }

    public static class VehicleTypeConstant
    {
        public const string Logistic = "LOGISTIC";
        public const string NonLogistic = "NONLOGISTIC";
        public const string InPlant = "INPLANT";
    }

    public static class InPlantWBFrequencyConstant
    {
        public const string Once = "Once";
        public const string OnceInShift = "OnceInShift";
        public const string Always = "Always";
    }

    public static class AccessRightsConst
    {
        public const string Read = "R";
        public const string Create = "C";
        public const string Update = "U";
        public const string Deactivate = "D";
    }

    public static class ScreenCodeConst
    {
        public const string UserManagement = "USERMGMT";
        public const string Rolemanagement = "ROLEMGMT";
        public const string UserRoleMapping = "URMAP";
    }

   
    public static class DeliveryChallan
    {
        public const string DCGenerated ="DC-Generated";
        public const string FullDelivery = "Full Delivery";
        public const string PartDelivery = "Part Delivery";
        
    }
    public static class Stocks
    {
        public const string InStock = "In-Stock";
    }
    public static class PurchaseOrder
    {
        public const string Closed = "Closed";
        public const string Draft = "Draft";
    }
    public static class Outward
    {
        public const string Delivered = "Delivered";
    }



}

