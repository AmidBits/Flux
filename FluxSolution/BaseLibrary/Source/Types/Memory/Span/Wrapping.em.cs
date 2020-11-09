namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
    public static bool IsWrapped<T>(this System.Span<T> source, T left, T right)
      => source.Length >= 2 && left.Equals(source[0]) && right.Equals(source[source.Length - 1]);
    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. SQL brackets, or parenthesis.</summary>
    public static System.Span<T> Unwrap<T>(this System.Span<T> source, T left, T right)
      => source.IsWrapped(left, right) ? source.Slice(1, source.Length - 2) : source;
    /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
    public static System.Span<T> Wrap<T>(this System.Span<T> source, T left, T right, bool force = false)
    { if (force || !source.IsWrapped(left, right))
      {
        source.ToArray()
        var wrapped = new T[1 + source.Length + 1];
        left.ToString() + source + right.ToString()
          return wrapped;
}
      else
        return source;
    }

    ///// <summary>Indicates whether the source is already wrapped in the strings.</summary>
    //public static bool IsWrapped(this string source, string left, string right)
    //  => (source ?? throw new System.ArgumentNullException(nameof(source))).Length >= ((left ?? throw new System.ArgumentNullException(nameof(left))).Length + (right ?? throw new System.ArgumentNullException(nameof(right))).Length) && source.StartsWith(left, System.StringComparison.Ordinal) && source.EndsWith(right, System.StringComparison.Ordinal);
    ///// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    //public static string Unwrap(this string source, string left, string right)
    //  => source.IsWrapped(left, right) ? source.Substring(left.Length, source.Length - (left.Length + right.Length)) : source;
    ///// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
    //public static string Wrap(this string source, string left, string right, bool force = false)
    //  => force || !source.IsWrapped(left, right) ? left + source + right : source;
  }
}
