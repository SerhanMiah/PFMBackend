using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PFMBackend.Models
{

        public enum Currency
    {
        USD, // United States Dollar
        EUR, // Euro
        GBP, // British Pound Sterling
        JPY, // Japanese Yen
        AUD, // Australian Dollar
    }

    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(50)]
        public string? FirstName { get; set; }

        [Required, StringLength(50)]
        public string? LastName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public override string? Email { get; set; }

        [StringLength(250)]
        public string? ProfilePictureUrl { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalIncome { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }

        [StringLength(5)] 
        public Currency? CurrencyPreference { get; set; }

        public ICollection<TransactionModel>? Transactions { get; set; }

    }
}