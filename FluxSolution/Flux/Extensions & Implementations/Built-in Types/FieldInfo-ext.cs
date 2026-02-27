namespace Flux
{
  public static class FieldInfoExtensions
  {
    extension(System.Reflection.FieldInfo source)
    {
      /// <summary>
      /// <para>Returns whether the FieldInfo represents a constant. A constant is a field that is a literal and cannot be initialized.</para>
      /// </summary>
      public bool IsConstant()
        => source.IsLiteral && !source.IsInitOnly;
    }
  }
}
