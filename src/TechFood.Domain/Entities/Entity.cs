namespace TechFood.Domain.Entities;

public class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; }
    public void SetId(Guid id)
    {
        Id = id;
    }
}
