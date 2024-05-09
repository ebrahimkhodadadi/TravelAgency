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
    public void EntityKeyNames_ShouldEndWithKey()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotInterfaces()
            .And()
            .ImplementInterface(typeof(IUniqueKey))
            .Should()
            .HaveNameEndingWith(Key)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}