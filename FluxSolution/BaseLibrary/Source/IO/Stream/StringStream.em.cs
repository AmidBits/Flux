namespace Flux
{
  public sealed class StringStream
    : System.IO.MemoryStream
  {
    public StringStream(string value, System.Text.Encoding encoding)
      : base((encoding ?? throw new System.ArgumentNullException(nameof(encoding))).GetBytes(value ?? string.Empty))
    {
    }
    /// <summary>Initializes a memory stream using UTF8 encoding.</summary>
    /// <param name="value"></param>
    public StringStream(string value)
      : this(value, System.Text.Encoding.UTF8)
    {
    }
  }
}
