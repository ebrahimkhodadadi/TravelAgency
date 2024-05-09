using TravelAgency.Domain.Common.Errors;
using TravelAgency.Tests.Unit.Abstractions;
using TravelAgency.Domain.Users.ValueObjects;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using static TravelAgency.Tests.Unit.Constants.Constants;

namespace TravelAgency.Tests.Unit.LayerTests.Domain.ValueObjects;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class EmailTests : TestBase
{
    private sealed class InvalidEmailTestData : TheoryData<string, Error[]>
    {
        public InvalidEmailTestData()
        {
            var tooLongEmail = TestString(1000);
            Add(tooLongEmail, [EmailError.TooLong, EmailError.Invalid]);

            string emptyEmail = string.Empty;
            Add(emptyEmail, [EmailError.Empty, EmailError.Invalid]);

            string whitespaceEmail = "    ";
            Add(whitespaceEmail, [EmailError.Empty, EmailError.Invalid]);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidEmailTestData))]
    public void Email_ShouldNotCreate_WhenInvalidInput(string invalidEmail, Error[] exceptedError)
    {
        //Act
        var emailResult = Email.Create(invalidEmail);

        //Assert
        emailResult.IsFailure.Should().BeTrue();
        emailResult.Error.Should().Be(Error.ValidationError);
        emailResult.ValidationErrors.Should().HaveCount(2);
        emailResult.ValidationErrors.Should().Contain(exceptedError);
    }
}