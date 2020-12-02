namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
    public static bool IsWrapped(this System.Text.StringBuilder source, char left, char right)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return source.Length >= 2 && source[0] == left && source[source.Length - 1] == right;
    }

    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. SQL brackets, or parenthesis.</summary>
    public static System.Text.StringBuilder Unwrap(this System.Text.StringBuilder source, char left, char right)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return IsWrapped(source, left, right) ? source.Remove(0, 1).Remove(source.Length - 1, 1) : source;
    }

    /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
    public static System.Text.StringBuilder Wrap(this System.Text.StringBuilder source, char left, char right, bool force = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return force || !IsWrapped(source, left, right) ? source.Insert(0, left).Append(right) : source;
    }

    /// <summary>Indicates whether the source is already wrapped in the strings.</summary>
    public static bool IsWrapped(this System.Text.StringBuilder source, string left, string right)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (left is null) throw new System.ArgumentNullException(nameof(left));
      if (right is null) throw new System.ArgumentNullException(nameof(right));

      return source.Length >= (left.Length + right.Length) && StartsWith(source, left) && EndsWith(source, right);
    }

    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public static System.Text.StringBuilder Unwrap(this System.Text.StringBuilder source, string left, string right)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (left is null) throw new System.ArgumentNullException(nameof(left));
      if (right is null) throw new System.ArgumentNullException(nameof(right));

      return IsWrapped(source, left, right) ? source.Remove(0, left.Length).Remove(source.Length - right.Length, right.Length) : source;
    }

    /// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
    public static System.Text.StringBuilder Wrap(this System.Text.StringBuilder source, string left, string right, bool force = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return force || !IsWrapped(source, left, right) ? source.Insert(0, left).Append(right) : source;
    }
  }
}
