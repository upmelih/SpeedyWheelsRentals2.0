namespace SpeedyWheelsRentals.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? RegistrationNumber { get; set; }
        public VehicleStatus Status { get; set; }

        // Navigation property for reservations
        public virtual ICollection<Reservation>? Reservations { get; set; }

        // Method to update the vehicle status based on reservation statuses

       
        public void UpdateStatusBasedOnReservations()
        {
            if (Reservations == null || Reservations.All(r => r.Status == ReservationStatus.Completed || r.Status == ReservationStatus.Cancelled))
            {
                Status = VehicleStatus.Available;
            }
            else if (Reservations.Any(r => r.Status == ReservationStatus.Ongoing))
            {
                Status = VehicleStatus.Rented;
            }
            // For Upcoming reservations, you may decide based on your business logic whether that affects availability
        }
    }


    public enum VehicleStatus
    {
        Available,
        Rented
    }

}
