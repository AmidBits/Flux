namespace Flux.Random
{
  public class Cryptographic
    : System.Random
  {
    /// <summary>Returns a non-negative random integer.</summary>
    /// <returns>A non-negative random integer.</returns>
    public override int Next() => System.Security.Cryptography.RandomNumberGenerator.GetInt32(int.MaxValue);
    /// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
    /// <returns>A non-negative random integer that is less than the specified maximum.</returns>
    public override int Next(int maxValue) => System.Security.Cryptography.RandomNumberGenerator.GetInt32(maxValue);
    /// <summary>Returns a random integer that is within a specified range.</summary>
    /// <returns>A random integer that is within a specified range, greater than or equal to specified minValue, and less than the specified maxValue.</returns>
    public override int Next(int minValue, int maxValue) => System.Security.Cryptography.RandomNumberGenerator.GetInt32(minValue, maxValue);
    /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
    public override void NextBytes(byte[] buffer) => System.Security.Cryptography.RandomNumberGenerator.Fill(buffer);
    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    public override double NextDouble() => (System.BitConverter.ToUInt64(this.GetRandomBytes(8), 0) >> 11) / (1UL << 53);
    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    protected override double Sample() => (System.BitConverter.ToUInt64(this.GetRandomBytes(8), 0) >> 11) / (1UL << 53);
  }
}
