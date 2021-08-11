namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static class UnicodeCategoryMajor
  {
    public static UnicodeCategoryMajorCode Parse(char majorCode)
      => (UnicodeCategoryMajorCode)System.Enum.Parse(typeof(UnicodeCategoryMajorCode), majorCode.ToString());
    public static bool TryParse(char majorCode, out UnicodeCategoryMajorCode result)
    {
      try
      {
        result = Parse(majorCode);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
  }
}
