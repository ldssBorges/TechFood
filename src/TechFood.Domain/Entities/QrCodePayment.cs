namespace TechFood.Domain.Entities
{
    public class QrCodePayment
    {
        public string Id { get; private set; }

        public string QrCodeData { get; private set; }

        public QrCodePayment(string id, string qrCodeData)
        {
            Id = id;
            QrCodeData = qrCodeData;
        }
    }
}
