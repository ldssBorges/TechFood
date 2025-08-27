using TechFood.Domain.Entities;
using TechFood.Domain.Shared.Exceptions;
using TechFood.Doman.Tests.Fixtures;

namespace TechFood.Doman.Tests
{
    public class OrderTests :
        IClassFixture<CustomerFixture>,
        IClassFixture<OrderFixture>
    {
        private readonly OrderFixture _orderFixture;
        private readonly CustomerFixture _customerFixture;

        public OrderTests(
            OrderFixture orderFixture,
            CustomerFixture customerFixture)
        {
            _orderFixture = orderFixture;
            _customerFixture = customerFixture;
        }

        [Fact(DisplayName = "Cannot add item to an order that is not in the Created status.")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenAddingItemToOrderThatIsNotCreatedStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            var item = new OrderItem(
                productId: Guid.NewGuid(),
                quantity: 1,
                unitPrice: 10);

            // Act
            var result = Assert.Throws<DomainException>(() => order.AddItem(item));

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotAddItemToNonCreatedStatus, result.Message);
        }

        [Fact(DisplayName = "Cannot remove item to an order that is not in the Created status.")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenRemovingItemToOrderThatIsNotCreatedStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            var item = new OrderItem(
                productId: Guid.NewGuid(),
                quantity: 1,
                unitPrice: 10);

            order.AddItem(item);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(() => order.RemoveItem(item.Id));

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotRemoveItemToNonCreatedStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Payment Creation when Order is Created")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenCreatingPaymentToOrderThatIsNotCreatedStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(order.CreatePayment);

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotCreatePaymentToNonCreatedStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Discount Application when Order is Created")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenApplyingDiscountToOrderThatIsNotCreatedStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(() => order.ApplyDiscount(10));

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotApplyDiscountToNonCreatedStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Payment when Order is Wainting Payment")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenPayingPaymentToOrderThatIsNotWaintingPaymentStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(order.ConfirmPayment);

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotPayToNonWaitingPaymentStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Payment Refusal when Order is Wainting Payment")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenRefusingPaymentToOrderThatIsNotCreatedStatus()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(order.RefusedPayment);

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotRefuseToNonWaitingPaymentStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Done when Order is not in Preparation")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenDoneToOrderThatIsNotInPreparation()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(order.FinishPreparation);

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotFinishToNonInPreparationStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Prepare when Order is not in Paid")]
        [Trait("Order", "Order Status")]
        public void ShoudThrowException_WhenPrepareToOrderThatIsNotInPaid()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            order.Finish();

            // Act
            var result = Assert.Throws<DomainException>(order.StartPreparation);

            // Assert
            Assert.Equal(Domain.Resources.Exceptions.Order_CannotPrepareToNonPaidStatus, result.Message);
        }

        [Fact(DisplayName = "Validate Amount Calculation in Order")]
        [Trait("Order", "Calculation")]
        public void ShoudThrowException_WhenCalculatingAmountIsNotCorrect()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            var item = new OrderItem(
                productId: Guid.NewGuid(),
                quantity: 7,
                unitPrice: 10.42m);

            order.AddItem(item);
            order.AddItem(item);
            order.AddItem(item);
            order.AddItem(item);

            // Act

            // Assert
            Assert.Equal(7 * 10.42m * 4, order.Amount);
        }

        [Fact(DisplayName = "Validate Discount Calculation in Order")]
        [Trait("Order", "Calculation")]
        public void ShoudThrowException_WhenCalculatingDiscountIsNotCorrect()
        {
            // Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var order = _orderFixture.CreateValidOrder(customer.Id);

            var item = new OrderItem(
                productId: Guid.NewGuid(),
                quantity: 7,
                unitPrice: 10.42m);

            order.AddItem(item);
            order.ApplyDiscount(9.76m);

            // Act

            // Assert
            Assert.Equal((7 * 10.42m) - 9.76m, order.Amount);
        }
    }
}
