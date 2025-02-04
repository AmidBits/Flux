namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the source is wrapped in the specified characters. E.g. brackets, or parenthesis.</summary>
    public static bool IsWrapped(this System.Text.StringBuilder source, char left, char right)
      => source is not null && source.Length >= 2 && source[0] == left && source[^1] == right;

    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. brackets, or parenthesis.</summary>
    public static System.Text.StringBuilder Unwrap(this System.Text.StringBuilder source, char left, char right)
      => source is not null && IsWrapped(source, left, right) ? source.Remove(0, 1).Remove(source.Length - 1, 1) : (source ?? throw new System.ArgumentNullException(nameof(source)));

    /// <summary>Add the specified wrap characters to the source. E.g. brackets, or parenthesis.</summary>
    public static System.Text.StringBuilder Wrap(this System.Text.StringBuilder source, char left, char right)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, left).Append(right);

    /// <summary>Indicates whether the source is wrapped in the specified left and right strings. If either the strings are null, a false is returned.</summary>
    public static bool IsWrapped(this System.Text.StringBuilder source, string left, string right)
      => source is not null && left is not null && right is not null && source.Length >= (left.Length + right.Length) && source.IsCommonPrefix(0, left) && source.IsCommonSuffix(0, right);

    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public static System.Text.StringBuilder Unwrap(this System.Text.StringBuilder source, string left, string right)
      => source is not null && !string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right) && IsWrapped(source, left, right) ? source.Remove(0, left.Length).Remove(source.Length - right.Length, right.Length) : (source ?? throw new System.ArgumentNullException(nameof(source)));

    /// <summary>Add the specified wrap strings to the source.</summary>
    public static System.Text.StringBuilder Wrap(this System.Text.StringBuilder source, string left, string right)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, left).Append(right);
  }
}
