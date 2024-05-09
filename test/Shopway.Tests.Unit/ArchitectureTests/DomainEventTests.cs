using NetArchTest.Rules;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class DomainEventTests
{
    [Fact]
    public void DomainEvents_ShouldBeSealed()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types.InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}