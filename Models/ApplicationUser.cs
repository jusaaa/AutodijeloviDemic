﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AutodijeloviDemic.Enums;

namespace AutodijeloviDemic.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public byte[]? ProfileImage { get; set; }


        public UserType UserType { get; set; } = UserType.Individual;
        public ICollection<Order> Orders { get; set; }
    }
}
