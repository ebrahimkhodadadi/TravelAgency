using NetArchTest.Rules;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Tests.Unit.ArchitectureTests.Utilities;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class ValueObjectTests
{
    [Fact]
    public void ValueObjects_ShouldBeImmutable()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .BeImmutable()
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [InlineData("Validate")]
    [InlineData("Create")]
    public void ValueObjects_ShouldDefineMethod(string methodName)
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .DefineMethod(methodName)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}