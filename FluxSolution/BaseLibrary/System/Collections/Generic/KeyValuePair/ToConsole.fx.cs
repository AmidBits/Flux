namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</summary>
    public static System.Text.StringBuilder ToConsole<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Func<TKey, string> keySelector, System.Func<TValue, string> valueSelector, ConsoleFormatOptions? options = null)
      where TKey : notnull
      => source.ToJaggedArray().JaggedToConsole(options ?? ConsoleFormatOptions.Default with { HorizontalSeparator = '=' });
  }
}
