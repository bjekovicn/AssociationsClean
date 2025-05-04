namespace AssociationsClean.Application.Shared.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);
