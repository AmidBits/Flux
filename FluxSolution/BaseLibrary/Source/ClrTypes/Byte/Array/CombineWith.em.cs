using System.Linq;

namespace Flux
{
  public static partial class XtendByteArray
  {
    /// <summary>Returs an array consisting of the original array appended with the specified arrays of bytes.</summary>
    public static byte[] CombineWith(this byte[] source, params byte[][] others)
    {
      var rv = new byte[source.Length + others.Sum(x => x.Length)];
      System.Buffer.BlockCopy(source, 0, rv, 0, source.Length);
      var offset = source.Length;
      foreach (var other in others)
      {
        System.Buffer.BlockCopy(other, 0, rv, offset, other.Length);
        offset += other.Length;
      }
      return rv;
    }
  }
}
