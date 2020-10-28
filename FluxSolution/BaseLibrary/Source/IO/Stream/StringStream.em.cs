namespace Flux
{
  public static partial class Xtensions
  {
    public static System.IO.Stream ToStream(this string value, System.Text.Encoding encoding)
      => new System.IO.MemoryStream((encoding ?? throw new System.ArgumentNullException(nameof(encoding))).GetBytes(value ?? string.Empty));
    public static System.IO.Stream ToStream(this string value)
      => ToStream(value, System.Text.Encoding.UTF8);
  }
}

namespace Flux
{
  internal sealed class StringStream
    : System.IO.MemoryStream
  {
    private readonly string str;

    private int position;

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite => false;

    public override long Length
    {
      get
      {
        if (position < 0) throw new System.ObjectDisposedException(@"");
        return str.Length << 1;
      }
    }

    public override long Position
    {
      get
      {
        if (position < 0) throw new System.ObjectDisposedException(@"");

        return position;
      }
      set
      {
        if (position < 0) throw new System.ObjectDisposedException(@"");
        if (value < 0L || value > Length) throw new System.ArgumentOutOfRangeException(nameof(value));

        position = (int)value;
      }
    }

    public override int Capacity
    {
      get
      {
        if (position < 0) throw new System.ObjectDisposedException(@"");

        return str.Length << 1;
      }
      set => base.Capacity = value;
    }

    private byte this[int i] => (i & 1) == 0 ? (byte)(str[i >> 1] & 0xFF) : (byte)(str[i >> 1] >> 8);

    public StringStream(string s)
      : base(System.Array.Empty<byte>(), false)
    {
      if (s == null) throw new System.ArgumentNullException(s);

      str = s;
    }

    public override long Seek(long offset, System.IO.SeekOrigin origin)
    {
      if (position < 0) throw new System.ObjectDisposedException(@"");

      switch (origin)
      {
        case System.IO.SeekOrigin.Begin:
          Position = offset;
          break;
        case System.IO.SeekOrigin.Current:
          Position += offset;
          break;
        case System.IO.SeekOrigin.End:
          Position = Length - offset;
          break;
      }

      return position;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (position < 0) throw new System.ObjectDisposedException(@"");
      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));
      if (offset < 0) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));
      if (buffer.Length - offset < count) throw new System.ArgumentException(nameof(offset));

      int len = System.Math.Min(count, (str.Length << 1) - position);

      for (int i = 0; i < len; i++)
      {
        buffer[offset] = this[position];
        offset += 1;
        position += 1;
      }

      return len;
    }

    public override int ReadByte()
    {
      if (position < 0) throw new System.ObjectDisposedException(@"");

      return position == str.Length << 1 ? -1 : this[position++];
    }

    public override byte[] ToArray()
    {
      var result = new byte[str.Length << 1];
      for (int i = 0; i < result.Length; i++)
        result[i] = this[i];
      return result;
    }

    public override void WriteTo(System.IO.Stream stream)
    {
      if (position < 0) throw new System.ObjectDisposedException(@"");
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      using (var ss = new StringStream(str))
        ss.CopyTo(stream);
    }

    public override void SetLength(long value) => throw new System.NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new System.NotSupportedException();
    public override void WriteByte(byte value) => throw new System.NotSupportedException();

    public override string ToString() => str;

    protected override void Dispose(bool disposing)
    {
      if (position < 0)
        return;

      position = -1;

      base.Dispose(disposing);
    }
  }
}
