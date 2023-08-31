using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PFMBackend.Models;

namespace PFMBackend.Models
{
    public enum Currency
    {
        USD,
        EUR,
        GBP,
        JPY,
        AUD
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

        public Currency? CurrencyPreference { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalBalance { get; set; }

        public virtual ICollection<FinanceGoal> FinancialGoals { get; set; }

        public virtual ICollection<AccountModel> Accounts { get; set; }

        public virtual ICollection<TransactionModel> Transactions { get; set; }

        public virtual ICollection<CategoryModel> TransactionCategories { get; set; }
    }
}
