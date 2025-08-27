using TechFood.Domain.Validations;

namespace TechFood.Domain.Entities;

public class Category : Entity, IAggregateRoot
{
    public Category() { }

    public Category(string name, string imageFileName, int sortOrder, bool isDeleted = false, Guid? id = null)
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }
        IsDeleted = isDeleted;
        Name = name;
        ImageFileName = imageFileName;
        SortOrder = sortOrder;

        Validate();
    }

    public string Name { get; private set; } = null!;

    public string ImageFileName { get; private set; } = null!;

    public int SortOrder { get; private set; }

    public void UpdateAsync(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;

        Validate();
    }

    private void Validate()
    {
        CommonValidations.ThrowIfEmpty(Name, Common.Resources.Exceptions.Category_ThrowNameIsEmpty);
        CommonValidations.ThrowIfEmpty(ImageFileName, Common.Resources.Exceptions.Category_ThrowFileImageIsEmpty);
        CommonValidations.ThrowIfLessThan(SortOrder, 0, Common.Resources.Exceptions.Category_ThrowIndexIsLessThanZero);
    }
}
