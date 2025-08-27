namespace TechFood.Common.DTO.Payment
{
    public class QrCodePaymentRequestDTO
    {
        public string PosId { get; set; }

        public string OrderId { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public List<PaymentItemDTO> Items { get; set; } = [];

        public QrCodePaymentRequestDTO(string posId, string orderId, string title, decimal amount, List<PaymentItemDTO> items)
        {
            PosId = posId;
            OrderId = orderId;
            Title = title;
            Amount = amount;
            Items = items;
        }

    }
}
