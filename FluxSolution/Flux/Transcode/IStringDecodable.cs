namespace Flux
{
  public interface IStringDecodable
  {
    /// <summary>Decode a string <paramref name="input"/> into a byte array.</summary>
    byte[] DecodeString(string input) => throw new System.NotImplementedException();

    bool TryDecodeString(string input, byte[] output)
    {
      try
      {
        DecodeString(input).CopyTo(output);
        return true;
      }
      catch { }

      return false;
    }
  }
}
