﻿namespace Forum_Web_API.ViewModels.UserViewModel
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}