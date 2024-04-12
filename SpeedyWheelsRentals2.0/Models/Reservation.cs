namespace SpeedyWheelsRentals.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus Status { get; set; }

        // Navigation properties
        public virtual Customer? Customer { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        public double ReservationCost
        {
            get
            {
                if (Vehicle != null && StartDate < EndDate)
                {
                    var totalDays = (EndDate - StartDate).TotalDays;
                    return totalDays * Vehicle.DailyRentalPrice;
                }
                return 0;
            }
        }
    }

    public enum ReservationStatus
    {
        Upcoming,
        Ongoing,
        Completed,
        Cancelled
    }

}
