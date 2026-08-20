// Infrastructure/Data/FirestoreBookingRepository.cs
using Google.Cloud.Firestore;
using HannahsPamperedPetsApp.Application.Interfaces;
using HannahsPamperedPetsApp.Domain.Entities;

namespace HannahsPamperedPetsApp.Infrastructure.Data;

public class FirestoreBookingRepository : IBookingRepository
{
    private readonly FirestoreDb _firestoreDb;
    private const string CollectionName = "bookings";

    public FirestoreBookingRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task AddBookingAsync(Booking booking)
    {
        var collection = _firestoreDb.Collection(CollectionName);
        var document = collection.Document(booking.Id);
        await document.SetAsync(booking);
    }

    public async Task<Booking?> GetBookingAsync(string id)
    {
        var snapshot = await _firestoreDb.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ConvertTo<Booking>() : null;
    }
}