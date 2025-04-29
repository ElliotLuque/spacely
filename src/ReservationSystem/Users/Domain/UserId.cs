namespace ReservationSystem.Users.Domain;

using Shared.Domain.ValueObject;

public sealed class UserId(Guid value) : UuidValueObject(value);
