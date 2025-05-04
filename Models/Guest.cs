using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BridalBlueprint.Models
{
    public class Guest
    {
        public int GuestId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string RSVPStatus { get; set; }

        public string MealPreference { get; set; }

        [Required(ErrorMessage = "You must select a Wedding.")]
        public int WeddingId { get; set; }

        [ValidateNever]
        public Wedding Wedding { get; set; }
    }
}
