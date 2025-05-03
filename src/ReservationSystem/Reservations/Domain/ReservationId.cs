namespace ReservationSystem.Reservations.Domain;

using SharedKernel.Domain.ValueObject;

public sealed class ReservationId(Guid value) : UuidValueObject(value);
