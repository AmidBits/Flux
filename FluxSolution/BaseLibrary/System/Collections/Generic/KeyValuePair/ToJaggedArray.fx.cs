namespace Flux
{
  public static partial class Fx
  {
    public static object?[][] ToJaggedArray<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, ConsoleFormatOptions? options = null)
      where TKey : notnull
      => source.Select(kvp => new object?[] { kvp.Key, kvp.Value }).ToArray();
  }
}
