namespace Domain.Entities;
public class EntityBase
{
    public DateTime? LastModifiedDateUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? CreatedDateUtc { get; set; }
    public string? CreatedBy { get; set; }
}
