using System;

namespace PimIVBackend.Models.UpdateDto
{
    public class ReservationUpdateDto
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
