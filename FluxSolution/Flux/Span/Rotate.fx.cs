
namespace Flux
{
  public static partial class Spans
  {
    public static void RotateLeft1<T>(this System.Span<T> source)
    {
      var tmp = source[0];
      source[1..].CopyTo(source[..^1]);
      source[^1] = tmp;
    }

    public static void RotateRight1<T>(this System.Span<T> source)
    {
      var tmp = source[^1];
      source[0..^1].CopyTo(source[1..]);
      source[0] = tmp;
    }

    public static void RotateLeft<T>(this System.Span<T> source, int count)
    {
      count %= source.Length;

      source.Reverse();
      source[^count..].Reverse();
      source[..^count].Reverse();
    }

    public static void RotateRight<T>(this System.Span<T> source, int count)
    {
      count %= source.Length;

      source.Reverse();
      source[..count].Reverse();
      source[count..].Reverse();
    }
  }
}
