namespace Flux.Checksum
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Adler-32"/>
  public record struct Adler32
    : IChecksumGenerator32
  {
    public static readonly Adler32 Empty;

    private uint m_hash;// = 1;

    public int Checksum32 { get => unchecked((int)m_hash); set => m_hash = unchecked((uint)value); }

    public Adler32(int hash = 1) => m_hash = unchecked((uint)hash);

    public int GenerateChecksum32(byte[] bytes, int offset, int count)
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      unchecked
      {
        var a = m_hash & 0xffff;
        var b = (m_hash >> 16) & 0xffff;

        for (var maxCount = (count < 5552) ? count : 5552; count > 0; count -= maxCount, maxCount = count < 5552 ? count : 5552)
        {
          for (int counter = 0; counter < maxCount; counter++)
          {
            a += bytes[offset++];
            b += a;
          }

          a %= 65521;
          b %= 65521;
        }

        m_hash = (b << 16) | a;
      }

      return Checksum32;
    }

    #region Object overrides.
    public override string ToString() => $"{nameof(Adler32)} {{ CheckSum = {m_hash} }}";
    #endregion Object overrides.
  }
}