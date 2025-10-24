namespace Flux
{
  public static partial class XtensionUInt128
  {
    extension(System.UInt128)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static System.UInt128 LargestPrimeNumber => new(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFF53ul);
    }
  }
}
