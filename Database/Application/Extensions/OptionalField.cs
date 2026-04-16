namespace Database.Application.Extensions;

public readonly struct OptionalField<T>
{
    public bool HasValue { get; }
    public T? Value { get; }

    private OptionalField(T? value, bool hasValue)
    {
        HasValue = hasValue;
        Value = value;
    }

    public static OptionalField<T> Some(T? value)
        => new OptionalField<T>(value, true);

    public static OptionalField<T> None()
        => default;

    public static implicit operator OptionalField<T>(T? value)
    {
        if (value is null)
            return default;

        return new OptionalField<T>(value, true);
    }
}