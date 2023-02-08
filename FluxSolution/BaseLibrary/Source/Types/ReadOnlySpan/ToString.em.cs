namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static string ToString<T>(this System.ReadOnlySpan<T> source)
    {
      var sb = new System.Text.StringBuilder();
      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index]?.ToString() ?? string.Empty);
      return sb.ToString();
    }
  }
}
