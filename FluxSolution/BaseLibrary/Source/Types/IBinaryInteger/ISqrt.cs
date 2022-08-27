#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the (floor) root of the <paramref name="square"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static TSelf ISqrt<TSelf>(this TSelf square)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (square <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(square));

      var l = TSelf.Zero;
      var r = square + TSelf.One;

      var two = TSelf.One + TSelf.One;

      while (l != r - TSelf.One)
        checked
        {
          var m = (l + r) / two;

          if (m * m <= square)
            l = m;
          else
            r = m;
        }

      return l;
    }
  }
}
#endif
