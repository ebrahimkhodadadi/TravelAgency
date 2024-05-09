using static TravelAgency.Domain.Common.Utilities.IEnumerableUtilities;
using static TravelAgency.Tests.Unit.Constants.Constants;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Tests.Unit.UtilityTests;

[Trait(nameof(UnitTest), UnitTest.Utility)]
public sealed class EnumerableUtilityTests
{
    [Fact]
    public void Distinct_ShouldReturnDistinctPeopleByGivenPredicate()
    {
        //Arrange
        TestPerson[] testPeople = [new TestPerson("Mike", "Arnak", 22), new TestPerson("Ammy", "Collins", 21), new TestPerson("Andy", "Collins", 16)];

        //Act
        var result = testPeople.Distinct((first, second) => first!.LastName == second!.LastName);

        //Assert
        result.Should().HaveCount(2);
    }

    private record class TestPerson(string FirstName, string LastName, int Age);
}