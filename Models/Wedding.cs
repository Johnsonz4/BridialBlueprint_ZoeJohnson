using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BridalBlueprint.Models;

public class Wedding
{
    public int WeddingId { get; set; }

    [Required]
    public string Title { get; set; }

    public DateTime Date { get; set; }

    public string Location { get; set; }

  
    public virtual ICollection<WeddingUser> WeddingUsers { get; set; } = new List<WeddingUser>();
    public virtual ICollection<BridalBlueprint.Models.WeddingTask> Tasks { get; set; } = new List<BridalBlueprint.Models.WeddingTask>();
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
    public virtual ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();
}

