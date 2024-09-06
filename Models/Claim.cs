using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Claim
    {
        public string ClaimNumber { get; set; }
        public DateTime DateOfClaim { get; set; }
        public string Status { get; set; }

        // Personal details
        [Required]
        public string LecturerName { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string LectureID { get; set; }   

        // Claim details
        public List<Module> Modules { get; set; } = new List<Module>();
        public IFormFile SupportingDocuments { get; set; }
    }

    public class Module
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string ClaimType { get; set; }
        public decimal RatePerHour { get; set; }
        public string Description { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime DateOfClaim { get; set; }
    }
}
