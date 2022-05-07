namespace Flux
{
  /// <summary>The functionality here relates to the major part of unicode categories.</summary>
  public static partial class Unicode
  {
    public static UnicodeCategoryMajorCode ParseCategoryMajor(char majorCode)
      => (UnicodeCategoryMajorCode)System.Enum.Parse(typeof(UnicodeCategoryMajorCode), majorCode.ToString());
    public static bool TryParseCategoryMajor(char majorCode, out UnicodeCategoryMajorCode result)
    {
      try
      {
        result = ParseCategoryMajor(majorCode);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
  }
}
