namespace ReservationSystem.Spaces.Domain;

using Shared.Domain.ValueObject;

public sealed class SpaceId(Guid value) : UuidValueObject(value);
