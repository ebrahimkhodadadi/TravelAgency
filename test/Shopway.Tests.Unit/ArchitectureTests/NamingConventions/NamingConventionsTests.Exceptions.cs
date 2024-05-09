using NetArchTest.Rules;
using TravelAgency.App;
using TravelAgency.Application;
using TravelAgency.Domain;
using TravelAgency.Infrastructure;
using TravelAgency.Persistence;
using TravelAgency.Presentation;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void ExceptionNames_ShouldEndWithException()
    {
        //Arrange
        var assemblies = new[]
        {
            TravelAgency.Domain.AssemblyReference.Assembly,
            TravelAgency.Application.AssemblyReference.Assembly,
            TravelAgency.Persistence.AssemblyReference.Assembly,
            TravelAgency.Infrastructure.AssemblyReference.Assembly,
            AssemblyReference.Assembly,
            TravelAgency.App.AssemblyReference.Assembly,
        };

        //Act
        var result = Types
            .InAssemblies(assemblies)
            .That()
            .AreNotInterfaces()
            .And()
            .Inherit(typeof(Exception))
            .Should()
            .HaveNameEndingWith(NamingConvention.Exception)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}