namespace PimIVBackend.Models.Dto
{
    public class ClosedFolioDto
    {
        public ClosedFolioDto(decimal itemQuantity, decimal amount, EntityGuest mainGuest, int folioId)
        {
            ItemQuantity = itemQuantity;
            Amount = amount;
            MainGuest = mainGuest;
            MainGuestId = MainGuest.Id;
            FolioId = folioId;
        }
        public int FolioId { get; private set; }
        public int MainGuestId { get; private set; }
        public EntityGuest MainGuest { get; private set; }
        public decimal ItemQuantity { get; private set; }
        public decimal Amount { get; private set; }
    }
}
