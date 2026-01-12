namespace Flux
{
  public interface IStringDecodable
  {
    /// <summary>Decode an coded string <paramref name="input"/> into a byte array.</summary>
    byte[] DecodeString(string input) => throw new System.NotImplementedException();

    bool TryDecodeString(string input, out byte[] output)
    {
      try
      {
        output = DecodeString(input);
        return true;
      }
      catch { }

      output = [];
      return false;
    }

    string DecodeString(string input, System.Text.Encoding? encoding = null)
    {
      encoding ??= System.Text.Encoding.UTF8;

      if (!TryDecodeString(input, out var output)) throw new System.InvalidOperationException();

      return encoding.GetString(output);
    }

    bool TryDecodeString(string input, out string output, System.Text.Encoding? encoding = null)
    {
      try
      {
        output = DecodeString(input, encoding);
        return true;
      }
      catch { }

      output = string.Empty;
      return false;
    }
  }
}
