namespace ReservationSystem.Reservations.Domain;

using Shared.Domain.ValueObject;

public sealed class ReservationId(Guid value) : UuidValueObject(value);
