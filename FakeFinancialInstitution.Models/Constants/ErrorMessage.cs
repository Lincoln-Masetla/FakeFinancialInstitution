using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models.Constants
{
	public static class ErrorMessage
	{
		public static string InvalidAccountType = "Invalid Account Type Entered!";
		public static string InvalidCustomerType = "Invalid Customer Type Entered!";
		public static string CustomerPhoneNumberExists = "Customer Phone Number Entered Exists!";
		public static string CustomerEmailExists = "Customer Email Entered Exists!";
		public static string CustomerIDNumberExists = "Customer ID Number Entered Exists!";
		public static string FromAccountNotFound = "FromAccount Not Found";
		public static string ToAccountNotFound = "ToAccount Not Found";
		public static string SameAccounttranfer = "You can not tranfer to the same account";
		public static string InsufficientFunds = "Insufficient Funds";
		public static string AccountDoesNotExit = "Account Does Not Exit";
		public static string AccountNumbeNotNull = "Account Number Can Not Be Null";
		public static string InvalidRequestState = "Invalid Request State";

	}
}
