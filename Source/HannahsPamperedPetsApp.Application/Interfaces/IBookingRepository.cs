// Application/Interfaces/IBookingRepository.cs
using HannahsPamperedPetsApp.Domain.Entities;

namespace HannahsPamperedPetsApp.Application.Interfaces;

public interface IBookingRepository
{
    Task AddBookingAsync(Booking booking);
    Task<Booking?> GetBookingAsync(string id);
}