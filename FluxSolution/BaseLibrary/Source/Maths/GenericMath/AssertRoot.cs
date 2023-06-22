namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Asserts the number is a valid nth root (throws an exception if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertRoot<TSelf>(TSelf root, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRoot(root) ? root : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(root), "Must be an integer, greater than or equal to 2.");

    /// <summary>Returns whether the number is a valid nth root.</summary>
    public static bool IsRoot<TSelf>(TSelf root)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so any type can be validated used if needed.
      => root > TSelf.One && TSelf.IsInteger(root);

#else

    public static System.Numerics.BigInteger AssertRoot(this System.Numerics.BigInteger root)
      => IsRoot(root) ? root : throw new System.ArgumentOutOfRangeException(nameof(root));

    /// <summary>Returns whether the number is a valid nth root.</summary>
    public static bool IsRoot(this System.Numerics.BigInteger root)
      => root > 1;

#endif
  }
}
