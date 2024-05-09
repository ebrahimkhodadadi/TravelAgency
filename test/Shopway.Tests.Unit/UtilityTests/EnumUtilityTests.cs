using TravelAgency.Domain.Users.Enumerations;
using static TravelAgency.Domain.Common.Utilities.EnumUtilities;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.UtilityTests;

[Trait(nameof(UnitTest), UnitTest.Utility)]
public sealed class EnumUtilityTests
{
    [Fact]
    public void LongestOf_ShouldReturnRankStandardLength()
    {
        //Act
        var result = LongestOf<Rank>();

        //Assert
        result.Should().Be(Rank.Standard.ToString().Length);
    }
}