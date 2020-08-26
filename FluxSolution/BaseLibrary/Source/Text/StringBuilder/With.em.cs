namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Indicates whether the source ends with value. Uses the specified comparer.</summary>
    public static bool EndsWith(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (int sourceIndex = source.Length - 1, valueIndex = value.Length - 1; sourceIndex >= 0 && valueIndex >= 0; sourceIndex--, valueIndex--)
        if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the source ends with value. Uses the default comparer.</summary>
    public static bool EndsWith(this System.Text.StringBuilder source, string value)
      => EndsWith(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Indicates whether the source starts with value. Uses the specified comparer.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (var index = 0; index < value.Length; index++)
        if (!comparer.Equals(source[index], value[index]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the source starts with value. Uses the default comparer.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, string value)
       => EndsWith(source, value, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
