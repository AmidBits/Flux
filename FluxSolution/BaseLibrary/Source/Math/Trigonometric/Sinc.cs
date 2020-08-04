namespace Flux
{
  public static partial class Math
  {
    /// <summary>Returns the normalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static double Sincn(double value)
      => Sincu(System.Math.PI * value);
    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static double Sincu(double value)
      => value != 0 ? System.Math.Sin(value) / value : 1;
  }
}
