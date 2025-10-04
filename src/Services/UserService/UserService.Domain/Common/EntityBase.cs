namespace UserService.Domain.Common;

public abstract class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
}