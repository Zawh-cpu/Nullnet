// using Optional;
//
// namespace Database.Application.Extensions;
//
// public static class OptionExtensions
// {
//     public static Option<T> ToOption<T>(this T? value)
//         where T : struct
//         => value.HasValue ? Option.Some(value.Value) : Option.None<T>();
//
//     public static Option<T> ToOption<T>(this T? value)
//         where T : class
//         => value != null ? Option.Some(value) : Option.None<T>();
// }