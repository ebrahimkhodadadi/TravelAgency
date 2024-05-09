using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using TravelAgency.Persistence;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void ConfigurationNames_ShouldEndWithConfiguration()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotAbstract()
            .And()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should()
            .HaveNameEndingWith(Configuration)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}