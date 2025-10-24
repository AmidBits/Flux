namespace Flux
{
  public static partial class XtensionInt128
  {
    extension(System.Int128)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static System.Int128 MaxPrimeNumber => new(0x7FFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFFFFul);
    }
  }
}
