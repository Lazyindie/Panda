namespace Panda.Domain.Common;

public class Auditable : HasId, IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
