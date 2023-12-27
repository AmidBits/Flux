namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
    public static bool IsWrapped(this string source, char left, char right)
      => source is not null && source.Length >= 2 && source[0] == left && source[^1] == right;
    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. SQL brackets, or parenthesis.</summary>
    public static string Unwrap(this string source, char left, char right)
      => IsWrapped(source, left, right) ? source[1..^1] : source;
    /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
    public static string Wrap(this string source, char left, char right, bool force = false)
      => force || !IsWrapped(source, left, right) ? left.ToString() + source + right.ToString() : source;

    /// <summary>Indicates whether the source is already wrapped in the strings.</summary>
    public static bool IsWrapped(this string source, string left, string right)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Length >= ((left ?? throw new System.ArgumentNullException(nameof(left))).Length + (right ?? throw new System.ArgumentNullException(nameof(right))).Length) && source.StartsWith(left, System.StringComparison.Ordinal) && source.EndsWith(right, System.StringComparison.Ordinal);
    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public static string Unwrap(this string source, string left, string right)
      => IsWrapped(source, left, right) ? source.Substring(left.Length, source.Length - (left.Length + right.Length)) : source;
    /// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
    public static string Wrap(this string source, string left, string right, bool force = false)
      => force || !IsWrapped(source, left, right) ? left + source + right : source;
  }
}
