namespace ApplicationCore.Entities.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public string Alias { get; set; }

        public string CardId { get; set; }

        public string Last4 { get; set; }
        
    }
}