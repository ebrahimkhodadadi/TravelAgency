using NetArchTest.Rules;
using Quartz;
using TravelAgency.Persistence;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void JobNames_ShouldEndWithJob()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IJob))
            .Should()
            .HaveNameEndingWith(Job)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}