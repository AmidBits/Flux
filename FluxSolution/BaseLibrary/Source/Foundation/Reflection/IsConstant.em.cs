namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns whether the FieldInfo represents a constant. A constant is a field that is a literal and cannot be initialized.</summary>
    public static bool IsConstant(this System.Reflection.FieldInfo source)
      => source.IsLiteral && !source.IsInitOnly;
  }
}
