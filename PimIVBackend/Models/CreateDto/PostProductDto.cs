namespace PimIVBackend.Models.CreateDto
{
    public class PostProductDto
    {
        public int FolioId { get; set; }
        public int ProductId { get; set; }
        public int GuestId { get; set; }
        public decimal Quantity { get; set; }

    }
}
