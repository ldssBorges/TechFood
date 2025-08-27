using TechFood.Domain.Validations;

namespace TechFood.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    private Product() { }

    public Product(
        Guid? id,
        string name,
        string description,
        Guid categoryId,
        string imageFileName,
        bool outOfStock,
        decimal price
        )
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }

        Name = name;
        Description = description;
        CategoryId = categoryId;
        ImageFileName = imageFileName;
        Price = price;

        Validate();
    }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public Guid CategoryId { get; private set; }

    public bool OutOfStock { get; private set; }

    public string ImageFileName { get; private set; } = null!;

    public decimal Price { get; private set; }

    public void SetCategory(Guid categoryId)
        => CategoryId = categoryId;

    public void SetOutOfStock(bool outOfStock)
       => OutOfStock = outOfStock;

    public void SetImageFileName(string imageFileName)
       => ImageFileName = imageFileName;

    public void Update(
        string name,
        string description,
        string imageFileName,
        decimal price,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ImageFileName = imageFileName;
        Price = price;

        Validate();
    }

    private void Validate()
    {
        CommonValidations.ThrowIfEmpty(Name, Common.Resources.Exceptions.Product_ThrowNameIsEmpty);
        CommonValidations.ThrowIfEmpty(Description, Common.Resources.Exceptions.Product_ThrowDescriptionIsEmpty);
        CommonValidations.ThrowValidGuid(CategoryId, Common.Resources.Exceptions.Product_ThrowCategoryIdInvalid);
        CommonValidations.ThrowIfEmpty(ImageFileName, Common.Resources.Exceptions.Product_ThrowCategoryImageFileIsEmpty);
        CommonValidations.ThrowIsGreaterThanZero(Price, Common.Resources.Exceptions.Product_ThrowPriceIsGreaterThanZero);
    }
}
