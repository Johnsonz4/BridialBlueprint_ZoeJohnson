using BridalBlueprint.Models;

public class WeddingUser
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int WeddingId { get; set; }
    public Wedding Wedding { get; set; }
}

