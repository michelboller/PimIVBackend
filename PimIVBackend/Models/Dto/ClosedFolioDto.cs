namespace PimIVBackend.Models.Dto
{
    public class ClosedFolioDto
    {
        public ClosedFolioDto(decimal amount)
        {
            Amount = amount;
        }
        public decimal Amount { get; private set; }
    }
}
