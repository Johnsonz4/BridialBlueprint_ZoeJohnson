using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BridalBlueprint.Models
{
    public class WeddingTask
    {
        public int WeddingTaskId { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [Required(ErrorMessage = "You must select a Wedding.")]
        public int WeddingId { get; set; }

        [ValidateNever]
        public Wedding Wedding { get; set; }
    }
}
