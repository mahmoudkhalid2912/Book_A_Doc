namespace Book_A_Doc.Domain.Models;

public abstract class BaseEntity
{
    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}

