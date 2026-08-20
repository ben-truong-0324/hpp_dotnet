using HannahsPamperedPetsApp.Domain.Entities;

namespace HannahsPamperedPetsApp.Tests;

public class CustomerTests
{
    [Fact]
    public void Customer_Should_Generate_Id_On_Creation()
    {
        // Arrange & Act
        var customer = new Customer();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(customer.Id));
    }
}