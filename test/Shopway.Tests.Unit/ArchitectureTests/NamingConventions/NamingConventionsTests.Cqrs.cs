using NetArchTest.Rules;
using TravelAgency.Application;
using TravelAgency.Application.Abstractions.CQRS;
using static TravelAgency.Tests.Unit.Constants.Constants;
using static TravelAgency.Tests.Unit.Constants.Constants.NamingConvention;

namespace TravelAgency.Tests.Unit.ArchitectureTests.NamingConventions;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed partial class NamingConventionsTests
{
    [Fact]
    public void CommandNames_ShouldEndWithCommand()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommand))
            .Should()
            .HaveNameEndingWith(Command)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GenericCommandNames_ShouldEndWithCommand()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith(Command)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlerNames_ShouldEndWithCommandHandler()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Should()
            .HaveNameEndingWith(CommandHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GenericCommandHandlerNames_ShouldEndWithCommandHandler()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith(CommandHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryNames_ShouldEndWithQuery()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith(Query)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlerNames_ShouldEndWithQueryHandler()
    {
        //Arrange
        var assembly = AssemblyReference.Assembly;

        //Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith(QueryHandler)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}