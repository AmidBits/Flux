namespace Flux
{
  public static partial class RandomExtensions
  {
    /// <summary>
    /// <para>Generates an array with the specified <paramref name="count"/> of random bytes.</para>
    /// </summary>
    public static byte[] GetRandomBytes(this System.Random source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var buffer = new byte[count];
      source.NextBytes(buffer);
      return buffer;
    }

    //public static byte[] GetRandomBytes(this System.Random source, int count)

  }
}
