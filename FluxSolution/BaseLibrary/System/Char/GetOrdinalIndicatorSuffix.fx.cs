namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Gets the ordinal indicator suffix ("th", "st", "nd", "rd") for the specified <paramref name="onesDigit"/> and <paramref name="tensDigit"/>.</para>
    /// </summary>
    /// <param name="onesDigit"></param>
    /// <param name="tensDigit"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string GetOrdinalIndicatorSuffix(this char onesDigit, char tensDigit = '0')
      => !System.Char.IsDigit(onesDigit)
      ? throw new System.ArgumentOutOfRangeException(nameof(onesDigit))
      : !System.Char.IsDigit(tensDigit)
      ? throw new System.ArgumentOutOfRangeException(nameof(tensDigit))
      : ((tensDigit - '0') * 10 + (onesDigit - '0')).GetOrdinalIndicatorSuffix();
  }
}
