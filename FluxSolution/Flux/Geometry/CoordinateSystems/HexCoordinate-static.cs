namespace Flux.Geometry.CoordinateSystems
{
  public static partial class HexCoordinate
  {
    public static HexCoordinate<TTarget> Convert<TSource, TTarget>(TSource sq, TSource sr, TSource ss, out TTarget tq, out TTarget tr, out TTarget ts)
      where TSource : System.Numerics.IBinaryInteger<TSource>
      where TTarget : System.Numerics.INumber<TTarget>
    {
      tq = TTarget.CreateChecked(sq);
      tr = TTarget.CreateChecked(sr);
      ts = TTarget.CreateChecked(ss);

      return new(
        tq,
        tr,
        ts
      );
    }

    public static HexCoordinate<TTarget> Round<TSource, TTarget>(TSource sq, TSource sr, TSource ss, UniversalRounding mode, out TTarget tq, out TTarget tr, out TTarget ts)
      where TSource : System.Numerics.IFloatingPoint<TSource>
      where TTarget : System.Numerics.INumber<TTarget>
    {
      var rQ = sq.RoundUniversal(mode);
      var rR = sr.RoundUniversal(mode);
      var rS = ss.RoundUniversal(mode);

      var aQ = TSource.Abs(rQ - sq);
      var aR = TSource.Abs(rR - sr);
      var aS = TSource.Abs(rS - ss);

      if (aQ > aR && aQ > aS)
        rQ = -rR - rS;
      else if (aR > aS)
        rR = -rQ - rS;
      else
        rS = -rQ - rR;

      tq = TTarget.CreateChecked(rQ);
      tr = TTarget.CreateChecked(rR);
      ts = TTarget.CreateChecked(rS);

      return new(
        tq,
        tr,
        ts
      );
    }
  }
}
