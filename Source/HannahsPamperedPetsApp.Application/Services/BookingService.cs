
// Application/Services/BookingService.cs
using HannahsPamperedPetsApp.Application.Interfaces;
using HannahsPamperedPetsApp.Domain.Entities;

namespace HannahsPamperedPetsApp.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task CreateDropInBookingAsync(string customerId, DateTime dropInTime, string notes)
    {
        if (dropInTime <= DateTime.UtcNow)
        {
            throw new ArgumentException("Booking time must be in the future.");
        }

        var booking = new Booking
        {
            CustomerId = customerId,
            DropInTime = dropInTime,
            Notes = notes,
            Status = "Pending"
        };

        await _bookingRepository.AddBookingAsync(booking);
        return booking;
    }
}