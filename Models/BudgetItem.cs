using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BridalBlueprint.Models
{
    public class BudgetItem
    {
        public int BudgetItemId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountAllocated { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountSpent { get; set; }

        [Required(ErrorMessage = "You must select a Wedding.")]
        public int WeddingId { get; set; }

        [ValidateNever]
        public Wedding Wedding { get; set; }
    }
}

