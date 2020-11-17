

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Numerics.BigInteger MaxX(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Numerics.BigInteger MaxX(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Decimal MaxX(System.Decimal a, System.Decimal b, System.Decimal c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Decimal MaxX(System.Decimal a, System.Decimal b, System.Decimal c, System.Decimal d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Double MaxX(System.Double a, System.Double b, System.Double c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Double MaxX(System.Double a, System.Double b, System.Double c, System.Double d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Single MaxX(System.Single a, System.Single b, System.Single c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Single MaxX(System.Single a, System.Single b, System.Single c, System.Single d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Int32 MaxX(System.Int32 a, System.Int32 b, System.Int32 c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Int32 MaxX(System.Int32 a, System.Int32 b, System.Int32 c, System.Int32 d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
    /// <summary>The maximum of the three specified values.</summary>
    public static System.Int64 MaxX(System.Int64 a, System.Int64 b, System.Int64 c)
      => (a > b) ? (a > c ? a : c) : (b > c ? b : c);

    /// <summary>The maximum of the four specified values.</summary>
    public static System.Int64 MaxX(System.Int64 a, System.Int64 b, System.Int64 c, System.Int64 d)
      => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    
  }
}
