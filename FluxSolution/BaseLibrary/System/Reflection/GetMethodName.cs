namespace Flux
{
  public static partial class FxReflection
  {
    /// <summary>Get the current method name without using reflection.</summary>
    /// <remarks>Using reflection System.Reflection.MethodInfo.GetCurrentMethod() also works.</remarks>
    public static string GetMethodName([System.Runtime.CompilerServices.CallerMemberName] string caller = null!)
      => caller;
  }
}
