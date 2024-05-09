using NetArchTest.Rules;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Tests.Unit.ArchitectureTests.Utilities;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class EntityTests
{
    [Fact]
    public void Entities_ShouldDefineCreateMethod()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(Entity<>))
            .And()
            .AreNotAbstract()
            .Should()
            .DefineMethod("Create")
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_ShouldHavePrivateParameterlessConstructor()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        var result = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(Entity<>))
            .And()
            .AreNotAbstract()
            .Should()
            .HavePrivateParameterlessConstructor()
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}