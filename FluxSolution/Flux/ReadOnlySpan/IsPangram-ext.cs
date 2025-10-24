namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region IsPangram

      public bool IsPangram(System.ReadOnlySpan<T> alphabet, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        foreach (var letter in alphabet)
          if (source.IndexOf(letter, equalityComparer) < 0)
            return false;

        return true;
      }

      #endregion
    }
  }
}
