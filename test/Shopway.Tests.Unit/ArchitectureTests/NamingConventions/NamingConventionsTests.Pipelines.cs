using MediatR;
using NetArchTest.Rules;
using TravelAgency.Application;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void PipelineNames_ShouldContainPipeline()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IPipelineBehavior<,>))
            .Should()
            .HaveNameMatching(Pipeline)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}