using NetArchTest.Rules;
using TravelAgency.Application;
using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void DomainEventNames_ShouldEndWithDomainEvent()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith(DomainEvent)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEventHandlersNames_ShouldEndWithDomainEventHandler()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IDomainEventHandler<>))
            .Should()
            .HaveNameEndingWith(DomainEventHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}