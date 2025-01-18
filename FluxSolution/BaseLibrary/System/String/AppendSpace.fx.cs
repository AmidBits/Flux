//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>Appends a space after the <paramref name="source"/> content.</summary>
//    public static string AppendSpace(this string source) => source + ' ';
//  }
//}
namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a string from source by repeating until the length is achieved.</summary>
    public static string MakeWidth(this string source, int width) => source.ToSpanMaker(width - source.Length).PadRight(width, source).ToString();
  }
}
