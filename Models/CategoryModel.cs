using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PFMBackend.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<TransactionModel>? Transactions { get; set; }
        
    }
}