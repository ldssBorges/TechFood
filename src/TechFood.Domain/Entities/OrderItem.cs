using TechFood.Domain.Validations;

namespace TechFood.Domain.Entities;

public class OrderItem : Entity
{
    private OrderItem() { }

    public OrderItem(
        Guid productId,
        decimal unitPrice,
        int quantity,
        Guid? id = null)
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;

        Validate();
    }

    public Guid ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        CommonValidations.ThrowIsGreaterThanZero(quantity, Common.Resources.Exceptions.OrderItem_ThrowQuantityGreaterThanZero);

        Quantity = quantity;
    }

    private void Validate()
    {
        CommonValidations.ThrowValidGuid(ProductId, Common.Resources.Exceptions.OrderItem_ThrowProductIdIsInvalid);
        CommonValidations.ThrowIsGreaterThanZero(UnitPrice, Common.Resources.Exceptions.OrderItem_ThrowUnitPriceGreaterThanZero);
        CommonValidations.ThrowIsGreaterThanZero(Quantity, Common.Resources.Exceptions.OrderItem_ThrowQuantityGreaterThanZero);
    }
}
