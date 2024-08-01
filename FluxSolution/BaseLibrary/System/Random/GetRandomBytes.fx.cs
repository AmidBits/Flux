//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class Fx
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    /// <summary>Generates an array with the specified <paramref name="count"/> of random bytes.</summary>
    public static byte[] GetRandomBytes(this System.Random source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var buffer = new byte[count];
      source.NextBytes(buffer);
      return buffer;
    }
  }
}
