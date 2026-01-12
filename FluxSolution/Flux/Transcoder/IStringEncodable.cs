namespace Flux
{
  public interface IStringEncodable
  {
    /// <summary>Encode a byte array <paramref name="input"/> into a coded string.</summary>
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

    string EncodeString(string input, System.Text.Encoding? encoding = null)
    {
      encoding ??= System.Text.Encoding.UTF8;

      if (!TryEncodeString(encoding.GetBytes(input), out var output)) throw new System.InvalidOperationException();

      return output;
    }

    bool TryEncodeString(string input, out string output, System.Text.Encoding? encoding = null)
    {
      try
      {
        output = EncodeString(input, encoding);
        return true;
      }
      catch { }

      output = string.Empty;
      return false;
    }
  }
}
