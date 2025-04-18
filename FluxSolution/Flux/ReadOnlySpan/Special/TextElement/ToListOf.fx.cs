namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>Creates a new list of <see cref="System.Char"/> from the source.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> ToListOfChar(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsReadOnlyListOfChar);

      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> ToListOfRune(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsReadOnlySpan().ToListOfRune());

      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Char"/> from the source.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> ToListOfChar(this System.ReadOnlySpan<string> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index]);

      return list;
    }

    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> ToListOfRune(this System.ReadOnlySpan<string> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsSpan().ToListOfRune());

      return list;
    }
  }
}
