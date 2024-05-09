using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using TravelAgency.Presentation;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void ControllerNames_ShouldEndWithController()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .HaveNameEndingWith(NamingConvention.Controller)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}