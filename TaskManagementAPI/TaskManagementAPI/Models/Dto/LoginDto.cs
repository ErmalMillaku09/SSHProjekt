﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.Dto
{
    public class LoginDto
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
