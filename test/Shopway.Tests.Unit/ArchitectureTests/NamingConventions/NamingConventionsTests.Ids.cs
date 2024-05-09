using NetArchTest.Rules;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void EntityIdNames_ShouldEndWithId()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotInterfaces()
            .And()
            .ImplementInterface(typeof(IEntityId))
            .Should()
            .HaveNameEndingWith(Id)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}