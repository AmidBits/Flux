namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region GetRandomElement

      /// <summary>
      /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"/>
      public T GetRandomElement(System.Random? rng = null)
        => source[(rng ?? System.Random.Shared).Next(source.Length)];

      /// <summary>
      /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="result"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public bool TryGetRandomElement(out T result, System.Random? rng = null)
      {
        try
        {
          result = GetRandomElement(source, rng);
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      #endregion
    }
  }
}
