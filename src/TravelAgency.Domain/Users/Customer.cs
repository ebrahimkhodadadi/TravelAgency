using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Users.Enumerations;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users;

public sealed class Customer : Entity<CustomerId>, IAuditable
{
    private Customer
    (
        CustomerId id,
        FirstName firstName,
        LastName lastName,
        Gender gender,
        DateOnly? dateOfBirth,
        PhoneNumber contactNumber,
        Address? address,
        User user,
        Rank rank,
        DebtLimit debtLimit
    )
        : base(id)
    {
        Rank = rank;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = contactNumber;
        Address = address;
        User = user;
        DebtLimit = debtLimit;
    }

    private Customer()
    {
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Gender Gender { get; private set; }
    public Rank Rank { get; private set; }
    public DebtLimit DebtLimit { get; private set; }

    public DateOnly? DateOfBirth { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Address? Address { get; private set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public UserId UserId { get; private set; }
    public User User { get; private set; }

    public static Customer Create
    (
        CustomerId id,
        FirstName firstName,
        LastName lastName,
        Email email,
        Gender gender,
        DateOnly? dateOfBirth,
        PhoneNumber contactNumber,
        Address? address,
        User user,
        Rank rank,
        DebtLimit debtLimit
    )
    {
        return new Customer
        (
            id,
            firstName,
            lastName,
            gender,
            dateOfBirth,
            contactNumber,
            address,
            User.Create(UserId.New(), Username.Create(email.Value).Value, email),
            rank,
            debtLimit
        );
    }
}