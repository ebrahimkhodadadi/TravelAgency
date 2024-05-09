namespace TravelAgency.Application.Exceptions;

public sealed class BadRequestException(string message) : Exception(message);