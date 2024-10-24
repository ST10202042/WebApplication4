using Microsoft.EntityFrameworkCore;
using System;

using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
   
    public class Claim
    {
        // Claim details
        [Key]
        public string ClaimNumber { get; set; }




        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfClaim { get; set; }
        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        // Personal details
        [Required]
        [StringLength(100)]
        public string LectureID { get; set; }
        [Required]
        [StringLength(100)]
        public string LecturerName { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        [Required]
        [StringLength(10)]



        // Claim details
        // [Required]
        // [StringLength(100)]
        public List<Module> Modules { get; set; }
        public byte[] SupportingDocuments { get; set; }
    }

    public class Module
    {
        internal string ClaimNumber;

        //section for the fields of the module/s
        [Required]
        [StringLength (6)]
        [Key]
        public string ModuleCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; }
        [Required]
        [StringLength(100)]
        public string ClaimType { get; set; }
        [Required]
        [StringLength(100)]
        public decimal RatePerHour { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public decimal ClaimAmount { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfClaim { get; set; }

       
    }
}
