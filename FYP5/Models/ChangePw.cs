﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FYP5.Models
{
	public class ChangePw
	{
		[Required(ErrorMessage = "Current Password Required")]
		[DataType(DataType.Password)]
		[Remote("VerifyCurrentPassword", "Account",
				ErrorMessage = "Current Password Incorrect")]
		public string CurrentPwd { get; set; } = null!;

		[Required(ErrorMessage = "New Password Required")]
		[DataType(DataType.Password)]
		[Remote("VerifyNewPassword", "Account",
			  ErrorMessage = "New Password Invalid")]
		public string NewPwd { get; set; } = null!;

		[Required(ErrorMessage = "Confirm Password Required")]
		[DataType(DataType.Password)]
		[Compare("NewPwd", ErrorMessage = "Passwords Unmatched")]
		public string ConfirmPwd { get; set; } = null!;
	}
}

