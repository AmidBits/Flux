namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, T left, T right)
      where T : System.IEquatable<T>
      => source.Length >= 2 && left.Equals(source[0]) && right.Equals(source[^1]);
    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. SQL brackets, or parenthesis.</summary>
    public static System.ReadOnlySpan<T> Unwrap<T>(this System.ReadOnlySpan<T> source)
      where T : System.IEquatable<T>
      => source[1..^1];
    /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
    public static T[] Wrap<T>(this System.ReadOnlySpan<T> source, T left, T right)
      where T : System.IEquatable<T>
    {
      var array = source.ToNewArray(1, 1);
      array[0] = left;
      array[^1] = right;
      return array;
    }

    /// <summary>Indicates whether the source is already wrapped in the strings.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
      where T : System.IEquatable<T>
      => source.Length >= 2 && source.StartsWith(left) && source.EndsWith(right);
    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public static System.ReadOnlySpan<T> Unwrap<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
      where T : System.IEquatable<T>
      => source.Slice(left.Length, source.Length - (left.Length + right.Length));
    /// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
    public static T[] Wrap<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
      where T : System.IEquatable<T>
    {
      var array = source.ToNewArray(left.Length, right.Length);
      left.CopyTo(new System.Span<T>(array).Slice(0, left.Length));
      right.CopyTo(new System.Span<T>(array).Slice(left.Length + source.Length, right.Length));
      return array;
    }
  }
}
