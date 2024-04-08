namespace SpeedyWheelsRentals.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation property for reservations
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }

}
