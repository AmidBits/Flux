namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Asserts the number is a valid root (throws an exception if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertRoot<TSelf>(TSelf root, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRoot(root) ? root : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(root), "Must be an integer, greater than or equal to 2.");

    /// <summary>Returns whether the number is a valid root.</summary>
    public static bool IsRoot<TSelf>(TSelf root)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so any type can be validated used if needed.
      => root > TSelf.One && TSelf.IsInteger(root);
  }
}
