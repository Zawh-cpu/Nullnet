namespace Database.Application.Extensions;

public readonly struct OptionalField<T>
{
    public bool HasValue { get; }
    public T? Value { get; }

    public OptionalField(T? value)
    {
        HasValue = true;
        Value = value;
    }

    public static implicit operator OptionalField<T>(T? value)
        => new(value);
}