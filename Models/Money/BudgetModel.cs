using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PFMBackend.Models
{
    public class BudgetModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<CategoryModel> Categories { get; set; }
    }
}
