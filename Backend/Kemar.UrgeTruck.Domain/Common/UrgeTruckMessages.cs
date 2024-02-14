using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.Common
{
    public static class UrgeTruckMessages
    {

        // Regex Patterns 
        // Accepts character AtoZ in both case Upper and lower, and also 0-9 and whiteSpace
        // Pattern1 length is 10 ,Pattern2 length is 30 ,Pattern3 length is 300 
        public const string regx_pattern1 = @"^\b[A-Za-z0-9\s]{0,10}\b+$"; 
        public const string regx_pattern2 = @"^\b[A-Za-z0-9\s]{0,30}\b+$";
        public const string regx_pattern3 = @"^[A-Za-z0-9\s\.\,\(\)]{0,300}$";

        // Global 
        public const string added = " Successfully.";
        public const string updated = " Successfully.";
        public const string added_successfully = " added successfully.";
        public const string no_record_found = "no record found.";
        public const string initiated_successfully = "initiated successfully.";
        public const string updated_successfully = " updated successfully.";
        public const string successfully_updated = "successfully updated";
        public const string ax4user = "ax4user";

        public const string NOT_A_VALID_MODEL = "Not a Valid Model";

        // RoleRegistrationRepository
     
        public const string Error_while_addupdate_role = "Error while add/update role";
        public const string role_Exist = "Role Already Exist";


        public const string User_name_or_password_is_incorrect = "User name or password is incorrect";
        public const string Duplicate_records_exists = "Duplicate records exists";
        public const string No_super_admin_role_exists_to_create_first_user = "No super admin role exists to create first user.";
        public const string User_registered_succesfully = "User registered succesfully.";
        public const string User_Name_is_invalid = "User Name is invalid";
        public const string ResetPasswordAsync_error = "ResetPasswordAsync - error:-";
        public const string Invalid_user_name_or_password = "Invalid user name or password.";
        public const string Invalid_password = "Invalid Old Password.";
        public const string Your_password_has_been_changed_successfully = "Your password has been changed successfully";
        public const string User_information_udpated_successfully = "User information udpated successfully";
        public const string UpdateUserDeatilAsync_error = "UpdateUserDeatilAsync- error:-";
        public const string Error_while_addupdate_User_details = "Error while add/update User details";
        public const string RegisterUserAsync_error = "RegisterUserAsync -error:-";
        public const string UserPhoneExist = "User Phone Number Already Exist";
        public const string UserNameExist = "User Name Already Exist";
        public const string AlreadyRegestered = "User Already Exist";
        public const string UserCantInactivate = "This User Hase Active Ticket Please Re-Assigne First ";

        // UserScreenRepository
        public const string User_screen = "User screen ";
        public const string Error_while_addupdate_user_screen = "Error while add/update user screen";

        // UserAccessManagerRepository
        public const string User_access_assinged_successfully = "User access assinged successfully";
        public const string Error_while_addupdate_user_access = "Error while add/update user accessw";


        public const string PO_Number = "PONumber";
        public const string Gate_Pass_Records = "Gate Pass Records";
        public const string Purchase_Order = "Purchase Order";
        public const string Purchase_Order_Details = "Purchase Order Details";
        public const string DeliVery_Challan = "DeliVery Challan";
        public const string GRN = "GRN";
        public const string GRN_Details = "GRN Details";
        public const string Suppliers_Data = "Suppliers Data";


    }
}
