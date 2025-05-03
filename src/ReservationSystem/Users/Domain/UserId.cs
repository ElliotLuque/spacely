namespace ReservationSystem.Users.Domain;

using SharedKernel.Domain.ValueObject;

public sealed class UserId(Guid value) : UuidValueObject(value);
