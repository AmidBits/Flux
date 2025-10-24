namespace Flux
{
  public interface IStringEncodable
  {
    /// <summary>Encode a byte array <paramref name="input"/> into a string.</summary>
    string EncodeString(byte[] input) => throw new System.NotImplementedException();

    bool TryEncodeString(byte[] input, out string output)
    {
      try
      {
        output = EncodeString(input);
        return true;
      }
      catch { }

      output = string.Empty;
      return false;
    }
  }
}
