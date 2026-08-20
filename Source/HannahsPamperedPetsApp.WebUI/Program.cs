using Google.Cloud.Firestore;
using HannahsPamperedPetsApp.Application.Interfaces;
using HannahsPamperedPetsApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Pull the project ID from your Secret Manager / User Secrets
var projectId = builder.Configuration["Firestore:ProjectId"];

// Register Firestore Database
builder.Services.AddSingleton(provider => 
    FirestoreDb.Create(projectId)
);

// Register the Repository (Whenever IBookingRepository is requested, provide FirestoreBookingRepository)
builder.Services.AddScoped();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/bookings", async (IBookingService bookingService, BookingRequest request) =>
{
    try
    {
        var booking = await bookingService.CreateDropInBookingAsync(
            request.CustomerId, 
            request.DropInTime, 
            request.Notes);
            
        return Results.Ok(booking);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// Create a simple DTO (Data Transfer Object) to hold the incoming JSON payload
public record BookingRequest(string CustomerId, DateTime DropInTime, string Notes);

app.Run();
