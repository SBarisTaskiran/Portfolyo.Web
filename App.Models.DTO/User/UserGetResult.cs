﻿using System.Text.Json.Serialization;

namespace App.Models.DTO.User
{
    public class UserGetResult
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool Enabled { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;

    }
}