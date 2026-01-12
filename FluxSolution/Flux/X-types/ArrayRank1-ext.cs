namespace Flux
{
  public static partial class ArrayRank1Extensions
  {
    extension<T>(T[] source)
    {
      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan() => source;

      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan(int start) => new(source, start, source.Length - start);

      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan(int start, int length) => new(source, start, length);
    }
  }
}
