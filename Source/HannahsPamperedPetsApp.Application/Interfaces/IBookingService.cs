// Application/Interfaces/IBookingService.cs
using HannahsPamperedPetsApp.Domain.Entities;

namespace HannahsPamperedPetsApp.Application.Interfaces;

public interface IBookingService
{
    Task CreateDropInBookingAsync(string customerId, DateTime dropInTime, string notes);
}