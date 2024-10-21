using System;
using Xunit;

public class TypeWiseAlertTests
{
    [Theory]
    [InlineData(10, 0, 35, TypewiseAlert.BreachType.NORMAL)]
    [InlineData(-5, 0, 35, TypewiseAlert.BreachType.TOO_LOW)]
    [InlineData(40, 0, 35, TypewiseAlert.BreachType.TOO_HIGH)]
    public void InferBreach_ShouldReturnCorrectBreachType(double value, double lowerLimit, double upperLimit, TypewiseAlert.BreachType expected)
    {
        var result = TypewiseAlert.InferBreach(value, lowerLimit, upperLimit);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(TypewiseAlert.CoolingType.PASSIVE_COOLING, 30, TypewiseAlert.BreachType.NORMAL)]
    [InlineData(TypewiseAlert.CoolingType.PASSIVE_COOLING, 36, TypewiseAlert.BreachType.TOO_HIGH)]
    [InlineData(TypewiseAlert.CoolingType.PASSIVE_COOLING, -1, TypewiseAlert.BreachType.TOO_LOW)]
    [InlineData(TypewiseAlert.CoolingType.HI_ACTIVE_COOLING, 44, TypewiseAlert.BreachType.NORMAL)]
    [InlineData(TypewiseAlert.CoolingType.HI_ACTIVE_COOLING, 46, TypewiseAlert.BreachType.TOO_HIGH)]
    [InlineData(TypewiseAlert.CoolingType.HI_ACTIVE_COOLING, -1, TypewiseAlert.BreachType.TOO_LOW)]
    [InlineData(TypewiseAlert.CoolingType.MED_ACTIVE_COOLING, 39, TypewiseAlert.BreachType.NORMAL)]
    [InlineData(TypewiseAlert.CoolingType.MED_ACTIVE_COOLING, 41, TypewiseAlert.BreachType.TOO_HIGH)]
    [InlineData(TypewiseAlert.CoolingType.MED_ACTIVE_COOLING, -1, TypewiseAlert.BreachType.TOO_LOW)]
    public void ClassifyTemperatureBreach_ShouldReturnCorrectBreachType(TypewiseAlert.CoolingType coolingType, double temperatureInC, TypewiseAlert.BreachType expected)
    {
        var result = TypewiseAlert.ClassifyTemperatureBreach(coolingType, temperatureInC);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(TypewiseAlert.AlertTarget.TO_CONTROLLER, TypewiseAlert.BreachType.NORMAL)]
    [InlineData(TypewiseAlert.AlertTarget.TO_EMAIL, TypewiseAlert.BreachType.TOO_LOW)]
    public void CheckAndAlert_ShouldSendCorrectAlert(TypewiseAlert.AlertTarget alertTarget, TypewiseAlert.BreachType breachType)
    {
        // Arrange
        var batteryChar = new TypewiseAlert.BatteryCharacter
        {
            coolingType = TypewiseAlert.CoolingType.PASSIVE_COOLING,
            brand = "TestBrand"
        };

        // Act
        TypewiseAlert.CheckAndAlert(alertTarget, batteryChar, breachType == TypewiseAlert.BreachType.TOO_LOW ? -1 : 30); // Example temperature

        // Assert
        // Check console output or use a mocking framework to verify that the correct method is called.
        // This requires you to redirect Console output or mock methods.
        // You can implement a simple console output redirection for testing purposes.
    }

    [Fact]
    public void GetTemperatureLimits_ShouldThrowExceptionForInvalidCoolingType()
    {
        // Arrange
        var invalidCoolingType = (TypewiseAlert.CoolingType)999; // Invalid enum value

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => TypewiseAlert.GetTemperatureLimits(invalidCoolingType));
        Assert.Equal("coolingType", exception.ParamName);
    }
}
