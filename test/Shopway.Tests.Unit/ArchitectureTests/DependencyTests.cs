﻿using NetArchTest.Rules;
using TravelAgency.Application;
using TravelAgency.Domain;
using TravelAgency.Persistence;
using TravelAgency.Presentation;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.ArchitectureTests;

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public sealed class DependencyTests
{
    [Fact]
    public void Domain_ShouldNotHaveDependencyOnOtherProjects()
    {
        //Arrange
        var assembly = Domain.AssemblyReference.Assembly;

        var otherAssemblies = new[]
        {
            Application.AssemblyReference.Assembly.GetName().Name,
            Infrastructure.AssemblyReference.Assembly.GetName().Name,
            Persistence.AssemblyReference.Assembly.GetName().Name,
            Presentation.AssemblyReference.Assembly.GetName().Name,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherAssemblies)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldNotHaveDependencyOnOtherProjectsThanDomain()
    {
        //Arrange
        var assembly = Application.AssemblyReference.Assembly;

        var otherAssemblies = new[]
        {
            Infrastructure.AssemblyReference.Assembly.GetName().Name,
            Persistence.AssemblyReference.Assembly.GetName().Name,
            Presentation.AssemblyReference.Assembly.GetName().Name,
            Application.AssemblyReference.Assembly.GetName().Name,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherAssemblies)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }


    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOnOtherProjectsThanApplicationAndDomain()
    {
        //Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;

        var otherAssemblies = new[]
        {
            Persistence.AssemblyReference.Assembly.GetName().Name,
            Presentation.AssemblyReference.Assembly.GetName().Name,
            Application.AssemblyReference.Assembly.GetName().Name,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherAssemblies)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_ShouldNotHaveDependencyOnOtherProjectsThanInfrastructureAndApplicationAndDomain()
    {
        //Arrange
        var assembly = Persistence.AssemblyReference.Assembly;

        var otherAssemblies = new[]
        {
            Presentation.AssemblyReference.Assembly.GetName().Name,
            Application.AssemblyReference.Assembly.GetName().Name,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherAssemblies)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_ShouldNotHaveDependencyOnOtherProjectsThanApplicationAndDomain()
    {
        //Arrange
        var assembly = Presentation.AssemblyReference.Assembly;

        var otherAssemblies = new[]
        {
            Infrastructure.AssemblyReference.Assembly.GetName().Name,
            Persistence.AssemblyReference.Assembly.GetName().Name,
            Application.AssemblyReference.Assembly.GetName().Name,
        };

        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherAssemblies)
            .GetResult();

        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
}