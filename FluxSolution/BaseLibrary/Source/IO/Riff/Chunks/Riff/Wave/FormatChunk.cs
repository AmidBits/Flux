namespace Flux.Riff
{
  public sealed class FormatChunk
    : SubChunk
  {
    public const string ID = @"fmt ";

    #region 16 bytes of data.

    [System.CLSCompliant(false)] public ushort Format { get => System.BitConverter.ToUInt16(m_buffer, 8); set { System.BitConverter.GetBytes((ushort)value).CopyTo(m_buffer, 8); } }
    [System.CLSCompliant(false)] public ushort SampleChannels { get => System.BitConverter.ToUInt16(m_buffer, 10); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 10); UpdateBlockAlign(); } }
    [System.CLSCompliant(false)] public uint SampleRate { get => System.BitConverter.ToUInt32(m_buffer, 12); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 12); UpdateAvgBytesPerSec(); } }
    [System.CLSCompliant(false)] public uint AvgBytesPerSec { get => System.BitConverter.ToUInt32(m_buffer, 16); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 16); } }
    [System.CLSCompliant(false)] public ushort BlockAlign { get => System.BitConverter.ToUInt16(m_buffer, 20); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 20); UpdateAvgBytesPerSec(); } }
    [System.CLSCompliant(false)] public ushort SampleBitDepth { get => System.BitConverter.ToUInt16(m_buffer, 22); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 22); UpdateBlockAlign(); } }

    #endregion // 16 bytes of data.

    public int BytesPerSample => (SampleBitDepth / 8) * SampleChannels; // (Computed, not stored.) Bytes per sample is.

    #region 22 bytes of extended data.

    [System.CLSCompliant(false)] public ushort ExtensionSize { get => System.BitConverter.ToUInt16(m_buffer, 24); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 24); } }
    [System.CLSCompliant(false)] public ushort ValidBitsPerSample { get => System.BitConverter.ToUInt16(m_buffer, 26); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 26); } }
    [System.CLSCompliant(false)] public uint ChannelMask { get => System.BitConverter.ToUInt32(m_buffer, 28); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 28); } }
    public System.Guid SubFormat { get { var bytes = new byte[16]; m_buffer.AsReadOnlySpan().Slice(32, 16).CopyTo(bytes); return new System.Guid(bytes); } set => value.ToByteArray().CopyTo(m_buffer, 32); }

    #endregion // 22 bytes of extended data.

    public bool IsExtendedFormat => m_buffer.Length > 24 && ExtensionSize == 22; // (Computed, not stored.) The buffer is greater than 8 bytes header chunk + 16 bytes format chunk, and the extension-size equals exactly 22.

    public FormatChunk() : base(ID, 8 + 16) => Format = 1; // PCM

    public FormatChunk(int sampleChannels, int sampleRate, int sampleBitDepth)
      : this()
    {
      SampleBitDepth = (ushort)sampleBitDepth;
      SampleChannels = (ushort)sampleChannels;
      SampleRate = (uint)sampleRate;
    }

    public FormatChunk(byte[] buffer) : base(buffer) { }

    //[System.CLSCompliant(false)]
    private ushort UpdateBlockAlign()
      => BlockAlign = (ushort)(SampleChannels * (SampleBitDepth / 8));
    //[System.CLSCompliant(false)]
    private uint UpdateAvgBytesPerSec()
      => AvgBytesPerSec = (SampleRate * BlockAlign);

    public static string GetFormatName(int format)
      => format switch
      {
        1 => @"PCM",
        _ => throw new System.NotImplementedException()
      };

    public override string ToString()
    {
      var sm = new SpanMaker<char>(base.ToString());

      if (IsExtendedFormat)
        sm = sm.Insert(sm.AsReadOnlySpan().IndexOf(" bytes"), "+22");

      sm = sm.Insert(sm.AsReadOnlySpan().IndexOf(')') + 1, $" {GetFormatName(Format)} {SampleChannels} ch. {SampleBitDepth}-bit {SampleRate} Hz");

      return sm.ToString();
      //return base.ToString().Replace(">", $", {GetFormatName((int)Format)}, {SampleChannels} ch., {SampleRate} Hz, {SampleBitDepth}-bit>", System.StringComparison.Ordinal);
    }
  }
}
