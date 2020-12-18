namespace Flux
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Network_Time_Protocol"/>
  public class NetworkTimeProtocol
  {
    public enum LeapIndicatorEnum
    {
      NoWarning = 0b00,
      LastMinuteHas61Seconds = 0b01,
      LastMinuteHas59Seconds = 0b10,
      ClockNotSynchronized = 0x11,
    }
    public enum ModeEnum
    {
      Reserved = 0b000,
      SymmetricActive = 0b001,
      SymmetricPassive = 0b010,
      Client = 0b011,
      Server = 0b100,
      Broadcast = 0b101,
      ReservedForNtpControlMessage = 0b110,
      ReservedForPrivateUse = 0b111,
    }

    // private const ulong m_ntpTimestampDelta = 2208988800UL;

    /// <summary>Default DNS entries of NTP server hosts.</summary>
    public static System.Collections.Generic.IReadOnlyList<string> DefaultNtpHosts
      => new System.Collections.Generic.List<string> { @"pool.ntp.org", @"time.nist.gov", @"time-a.nist.gov", @"time-b.nist.gov" };

    /// <summary>The 48 byte NTP data structure.</summary>
    private byte[] m_data = new byte[48];

    public LeapIndicatorEnum LeapIndicator
      => (LeapIndicatorEnum)(m_data[0] >> 6);
    public int VersionNumber
      => (m_data[0] >> 3) & 0b00000111;
    public ModeEnum Mode
      => (ModeEnum)(m_data[0] & 0b00000111);
    public byte StratumLevel
      => m_data[1];
    public byte MaximumPollInterval
      => m_data[2];
    public byte Precision
      => m_data[3];
    [System.CLSCompliant(false)]
    public uint RootDelay
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 4);
    //=> (uint)m_data[4] << 24 | (uint)m_data[5] << 16 | (uint)m_data[6] << 8 | m_data[7];
    [System.CLSCompliant(false)]
    public uint RootDispersion
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 8);
    //=> (uint)m_data[8] << 24 | (uint)m_data[9] << 16 | (uint)m_data[10] << 8 | m_data[11];
    [System.CLSCompliant(false)]
    public uint ReferenceClockIdentifier
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 12);
    //=> (uint)m_data[12] << 24 | (uint)m_data[13] << 16 | (uint)m_data[14] << 8 | m_data[15];
    [System.CLSCompliant(false)]
    public uint ReferenceTimestampSeconds
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 16);
    //=> (uint)m_data[16] << 24 | (uint)m_data[17] << 16 | (uint)m_data[18] << 8 | m_data[19];
    [System.CLSCompliant(false)]
    public uint ReferenceTimestampFraction
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 20);
    //=> (uint)m_data[20] << 24 | (uint)m_data[21] << 16 | (uint)m_data[22] << 8 | m_data[23];
    [System.CLSCompliant(false)]
    public uint OriginateTimestampSeconds
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 24);
    //=> (uint)m_data[24] << 24 | (uint)m_data[25] << 16 | (uint)m_data[26] << 8 | m_data[27];
    [System.CLSCompliant(false)]
    public uint OriginateTimestampFraction
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 28);
    //=> (uint)m_data[28] << 24 | (uint)m_data[29] << 16 | (uint)m_data[30] << 8 | m_data[31];
    [System.CLSCompliant(false)]
    public uint ReceiveTimestampSeconds
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 32);
    //=> (uint)m_data[32] << 24 | (uint)m_data[33] << 16 | (uint)m_data[34] << 8 | m_data[35];
    [System.CLSCompliant(false)]
    public uint ReceiveTimestampFraction
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 36);
    //=> (uint)m_data[36] << 24 | (uint)m_data[37] << 16 | (uint)m_data[38] << 8 | m_data[39];
    [System.CLSCompliant(false)]
    public uint TransmitTimestampSeconds
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 40);
    //=> (uint)m_data[40] << 24 | (uint)m_data[41] << 16 | (uint)m_data[42] << 8 | m_data[43];
    [System.CLSCompliant(false)]
    public uint TransmitTimestampFraction
      => Flux.BitConverter.BigEndian.ToUInt32(m_data, 44);
    //=> (uint)m_data[44] << 24 | (uint)m_data[45] << 16 | (uint)m_data[46] << 8 | m_data[47];

    //public System.TimeSpan ReferenceTimestamp => new System.TimeSpan(0, 0, 0, (int)ReferenceTimestampSeconds, (int)ReferenceTimestampFraction);
    //public System.TimeSpan OriginateTimestamp => new System.TimeSpan(0, 0, 0, (int)OriginateTimestampSeconds, (int)OriginateTimestampFraction);
    //public System.TimeSpan ReceiveTimestamp => new System.TimeSpan(0, 0, 0, (int)ReceiveTimestampSeconds, (int)ReceiveTimestampFraction);
    //public System.TimeSpan TransmitTimestamp => new System.TimeSpan(0, 0, 0, (int)TransmitTimestampSeconds, (int)TransmitTimestampFraction);

    //public System.TimeSpan GetOffset()
    //  => ((OriginateTimestamp - ReferenceTimestamp) + (ReceiveTimestamp - TransmitTimestamp)) / 2;
    //public System.TimeSpan GetDelay()
    //  => (TransmitTimestamp - ReferenceTimestamp) - (ReceiveTimestamp - OriginateTimestamp);

    public System.DateTime TransmittedDateTime
      => new System.DateTime(1900, 1, 1).AddSeconds(TransmitTimestampSeconds).AddMilliseconds(TransmitTimestampFraction * 1000UL / 0x100000000UL).AddHours(-7);

    public System.DateTime Request(string ntpHost)
    {
      Reset();

      using (var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp))
      {
        socket.Connect(ntpHost, 123);
        socket.Send(m_data);
        socket.Receive(m_data);
      }

      return TransmittedDateTime;
    }
    public System.DateTime Request()
    {
      foreach (string ntpHost in DefaultNtpHosts)
      {
        try { return Request(ntpHost); }
#pragma warning disable CA1031 // Do not catch general exception types
        catch { }
#pragma warning restore CA1031 // Do not catch general exception types
      }

      throw new System.Exception($"Unable to reach any (tried {DefaultNtpHosts.Count}) NTP servers.");
    }

    private void Reset()
    {
      m_data = new byte[48];
      m_data[0] = 0x1B;
    }

    public bool TryRequest(string ntpHost, out System.DateTime result)
    {
      try
      {
        result = Request(ntpHost);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      {
        result = new System.DateTime();
        return false;
      }
    }
    public bool TryRequest(out System.DateTime result)
    {
      try
      {
        result = Request();
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      {
        result = new System.DateTime();
        return false;
      }
    }
  }
}
