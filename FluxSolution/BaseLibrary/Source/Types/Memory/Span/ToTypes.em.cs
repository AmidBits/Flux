namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new array from the source sequence, adding the number of specified pre-slots and post-slots.</summary>
    public static T[] ToArray<T>(this System.Span<T> source, int preCount, int postCount)
    {
      var array = new T[preCount + source.Length + postCount];
      source.CopyTo(new System.Span<T>(array).Slice(preCount, source.Length));
      return array;
    }

    /// <summary>Creates a new ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(this System.Span<T> source)
      => source.ToArray();

    /// <summary>Creates a string builder from the source.</summary>
    public static System.Text.StringBuilder ToStringBuilder(this System.Span<char> source)
      => new System.Text.StringBuilder().Append(source);
  }
}
