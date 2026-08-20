// Domain/Entities/Pet.cs
namespace HannahsPamperedPetsApp.Domain.Entities;

public class Pet
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string CareInstructions { get; set; } = string.Empty;
}