namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the Euclidean modulus of a and b.</summary>
    public static System.Numerics.BigInteger Mod(System.Numerics.BigInteger a, System.Numerics.BigInteger b) 
      => b == 0 ? throw new System.ArithmeticException() : b == -1 ? 0 : a % b is var m && m < 0 ? (b < 0 ? m - b : m + b) : m;

    /// <summary>Returns the Euclidean modulus of a and b.</summary>
    public static int Mod(int a, int b)
      => b == 0 ? throw new System.ArithmeticException() : b == -1 ? 0 : a % b is var m && m < 0 ? (b < 0 ? m - b : m + b) : m;
    /// <summary>Returns the Euclidean modulus of a and b.</summary>
    public static long Mod(long a, long b)
      => b == 0 ? throw new System.ArithmeticException() : b == -1 ? 0 : a % b is var m && m < 0 ? (b < 0 ? m - b : m + b) : m;

    /// <summary>Returns the Euclidean modulus of a and b.</summary>
    [System.CLSCompliant(false)]
    public static uint Mod(uint a, uint b)
      => b == 0 ? throw new System.ArithmeticException() : a % b is var m && m < 0 ? (b < 0 ? m - b : m + b) : m;
    /// <summary>Returns the Euclidean modulus of a and b.</summary>
    [System.CLSCompliant(false)]
    public static ulong Mod(ulong a, ulong b)
      => b == 0 ? throw new System.ArithmeticException() : a % b is var m && m < 0 ? (b < 0 ? m - b : m + b) : m;
  }
}
