using System;

namespace PimIVBackend.Models.CreateDto
{
    public class ReservationCreateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestId { get; set; }
        public int? CompanyId { get; set; }
        public int RoomId { get; set; }
    }
}
