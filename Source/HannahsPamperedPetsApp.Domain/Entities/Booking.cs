// Domain/Entities/Booking.cs
namespace HannahsPamperedPetsApp.Domain.Entities;

public class Booking
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; } = string.Empty;
    public DateTime DropInTime { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed
    public string Notes { get; set; } = string.Empty;
}