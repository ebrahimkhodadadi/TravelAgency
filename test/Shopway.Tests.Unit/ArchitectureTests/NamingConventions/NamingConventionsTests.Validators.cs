using FluentValidation;
using NetArchTest.Rules;
using TravelAgency.Application;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public partial class NamingConventionsTests
{
    [Fact]
    public void ValidatorsNames_ShouldEndWithValidators()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreNotAbstract()
            .And()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith(Validator)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}