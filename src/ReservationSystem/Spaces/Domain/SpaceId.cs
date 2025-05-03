namespace ReservationSystem.Spaces.Domain;

using SharedKernel.Domain.ValueObject;

public sealed class SpaceId(Guid value) : UuidValueObject(value);
