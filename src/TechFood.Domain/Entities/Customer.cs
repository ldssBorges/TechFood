using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.Entities;

public class Customer : Entity, IAggregateRoot
{
    private Customer() { }

    public Customer(Name name, Email email, Document document, Phone? phone, Guid? id = null)
    {
        if (id is not null)
        {
            SetId(id.Value);
        }
        Name = name;
        Email = email;
        Document = document;
        Phone = phone;
    }

    public Name Name { get; private set; } = null!;

    public Email Email { get; private set; } = null!;

    public Document Document { get; private set; } = null!;

    public Phone? Phone { get; private set; } = null!;
}
