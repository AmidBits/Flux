namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ExtensionMethods
  {
    public static bool IsJaggedArray(this System.Array source)
      => source.GetType().GetElementType()?.IsArray ?? false;
  }
}
