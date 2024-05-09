using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Persistence;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void RepositoryNames_ShouldEndWithRepository()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var repositories = assembly
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(x => x.Name.EndsWith(Repository)));

        //Assert
        repositories
            .Should()
            .OnlyContain(x => x.Name.EndsWith(Repository));
    }
}