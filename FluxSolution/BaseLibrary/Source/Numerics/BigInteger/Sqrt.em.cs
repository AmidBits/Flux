using System.Linq;

namespace Flux
{
  public static partial class XtendNumerics
  {
    /// <summary>Indicates whether the specified number is a square of the specified root.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool IsSqrt(this System.Numerics.BigInteger source, System.Numerics.BigInteger root)
      => (root * root is var lowerBound) && (lowerBound + root + root + 1 is var upperBound) ? (source >= lowerBound && source < upperBound) : throw new System.Exception();

    /// <summary>Returns the square root of the System.Numerics.BigInteger.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static System.Numerics.BigInteger Sqrt(this System.Numerics.BigInteger source)
    {
      if (source == 0)
      {
        return 0;
      }

      if (source > 0)
      {
        var bitLength = System.Convert.ToInt32(System.Math.Ceiling(System.Numerics.BigInteger.Log(source, 2)));

        var root = System.Numerics.BigInteger.One << (bitLength >> 1);

        while (!source.IsSqrt(root))
        {
          root += (source / root);

          root >>= 1;
        }

        return root;
      }

      throw new System.ArithmeticException();
    }
    /// <summary>Returns the square root of a specified number, using the exponential identity method. This is an approximation with lesser accuracy the higher the number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Methods_of_computing_square_roots"/>
    public static double SqrtByExponentialIdentity(this System.Numerics.BigInteger source)
      => System.Math.Exp(System.Numerics.BigInteger.Log(source) / 2.0);
    /// <summary>Returns the square root of a specified number, using an implementation from Java.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static System.Numerics.BigInteger SqrtByJavaAdoption(this System.Numerics.BigInteger source)
    {
      if (source == 0)
      {
        return 0;
      }

      if (source > System.Numerics.BigInteger.Zero)
      {
        var bitLength = System.Convert.ToInt32(System.Math.Ceiling(System.Numerics.BigInteger.Log(source, 2)));

        var root = System.Numerics.BigInteger.One << (bitLength / 2);

        while (!source.IsSqrt(root))
        {
          root += source / root;

          root /= 2;
        }

        return root;
      }

      throw new System.ArithmeticException();
    }
  }
}
