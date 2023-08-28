using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PFMBackend.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class TransactionModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        [Required]
        public TransactionType Type { get; set; } 
        public int AccountId { get; set; }
        public AccountModel? Account { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
        
    }
}