namespace PimIVBackend.Models.CreateDto
{
    public class RoomCreateDto
    {
#nullable enable
        public string? Code { get; set; }
        public string? Floor { get; set; }
#nullable disable
        public decimal Price { get; set; }
        public int Size { get; set; }
    }
}
