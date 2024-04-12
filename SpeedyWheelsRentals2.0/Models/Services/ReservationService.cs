using SpeedyWheelsRentals.Models;
using SpeedyWheelsRentals2._0.Data;
using Microsoft.EntityFrameworkCore;


namespace SpeedyWheelsRentals2._0.Models.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrUpdateReservation(Reservation reservation)
        {
            // Assuming _dbContext.Reservations.AddOrUpdate() is an EF method or similar to add or update a reservation.
            // This is pseudo-code for illustration purposes.
            _dbContext.Reservation?.Update(reservation);

            // Save changes to persist the added/updated reservation
            _dbContext.SaveChanges();

            // Now, find the related vehicle and update its status
            var vehicle = _dbContext.Vehicle?.Include(v => v.Reservations)
                                             .FirstOrDefault(v => v.VehicleId == reservation.VehicleId);

            vehicle?.UpdateStatusBasedOnReservations();

            // Save changes again to update the vehicle status
            _dbContext.SaveChanges();
        }
    }

}
