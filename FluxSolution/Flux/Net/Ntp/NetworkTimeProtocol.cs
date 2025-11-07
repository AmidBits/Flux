namespace Flux.Net.Ntp
{
  /// <summary>
  /// <para></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Network_Time_Protocol"/></para>
  /// <para><see href="https://datatracker.ietf.org/doc/html/rfc5905"/></para>
  /// </summary>
  public sealed class NetworkTimeProtocol
  {
    public static System.DateTime PrimeEpoch { get; } = new System.DateTime(1900, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

    /// <summary>Default DNS entries of NTP server hosts.</summary>
    public static System.Collections.Generic.IReadOnlyList<string> Hosts { get; } = [@"pool.ntp.org", @"time.nist.gov", @"time-a.nist.gov", @"time-b.nist.gov"];

    /// <summary>The 48 byte NTP data structure.</summary>
    private readonly byte[] m_data = new byte[48];

    private readonly System.DateTime m_clientTransmitTimestampUtc;
    private readonly System.DateTime m_clientReceiveTimestampUtc;

    private readonly string m_host;

    private NetworkTimeProtocol(byte[] data, System.DateTime clientTransmitTimestampUtc, System.DateTime clientReceiveTimestampUtc, string host)
    {
      m_data = data;

      m_clientTransmitTimestampUtc = clientTransmitTimestampUtc;
      m_clientReceiveTimestampUtc = clientReceiveTimestampUtc;

      m_host = host;
    }

    /// <summary>
    /// <para>The client transmission time (UTC).</para>
    /// </summary>
    public System.DateTime ClientTransmitTimestampUtc => m_clientTransmitTimestampUtc;

    /// <summary>
    /// <para>The client reception time (UTC).</para>
    /// </summary>
    public System.DateTime ClientReceiveTimestampUtc => m_clientReceiveTimestampUtc;

    /// <summary>
    /// <para>Time offset is positive or negative (client time > server time) difference in absolute time between the two clocks.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Network_Time_Protocol#Clock_synchronization_algorithm"/></para>
    /// </summary>
    public System.TimeSpan ComputedTimeOffset => ((ReceiveTimestampUtc - ClientTransmitTimestampUtc) + (TransmitTimestampUtc - ClientReceiveTimestampUtc)) / 2;

    /// <summary>
    /// <para>The round-trip delay, a.k.a. RTT or RTD.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Network_Time_Protocol#Clock_synchronization_algorithm"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Round-trip_delay"/></para>
    /// </summary>
    public System.TimeSpan ComputedRoundTripDelay => (ClientReceiveTimestampUtc - ClientTransmitTimestampUtc) - (TransmitTimestampUtc - ReceiveTimestampUtc);

    /// <summary>
    /// <para>The data received from the host server.</para>
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<byte> Data => m_data;

    /// <summary>
    /// <para>The host server that supplied the data.</para>
    /// </summary>
    public string Host => m_host;

    /// <summary>
    /// <para>LI Leap Indicator (leap): 2-bit integer warning of an impending leap second to be inserted or deleted in the last minute of the current month with values defined in Figure 9 (<see cref="NtpLeapIndicator"/>).</para>
    /// </summary>
    public NtpLeapIndicator LeapIndicator => (NtpLeapIndicator)(m_data[0] >> 6);

    /// <summary>
    /// <para>VN Version Number (version): 3-bit integer representing the NTP version number, currently 4.</para>
    /// </summary>
    public int VersionNumber => (m_data[0] >> 3) & 0b00000111;

    /// <summary>
    /// <para>Mode (mode): 3-bit integer representing the mode, with values defined in Figure 10 (<see cref="NtpMode"/>).</para>
    /// </summary>
    public NtpMode Mode => (NtpMode)(m_data[0] & 0b00000111);

    /// <summary>
    /// <para>Stratum (stratum): 8-bit integer representing the stratum, with values defined in Figure 11 (<see cref="NtpStratum"/>).</para>
    /// </summary>
    public NtpStratum Stratum => m_data[1] switch
    {
      0 => Net.Ntp.NtpStratum.UnspecifiedOrInvalid,
      1 => Net.Ntp.NtpStratum.PrimaryServer,
      >= 2 and <= 15 => Net.Ntp.NtpStratum.SecondaryServer,
      16 => Net.Ntp.NtpStratum.Unsynchronized,
      >= 17 => Net.Ntp.NtpStratum.Reserved,
    };

    /// <summary>
    /// <para>Poll: 8-bit signed integer representing the maximum interval between successive messages, in log2 seconds. Suggested default limits for minimum and maximum poll intervals are 6 and 10, respectively.</para>
    /// </summary>
    public byte Poll => m_data[2];

    /// <summary>
    /// <para>Precision: 8-bit signed integer representing the precision of the system clock, in log2 seconds.For instance, a value of -18 corresponds to a precision of about one microsecond. The precision can be determined when the service first starts up as the minimum time of several iterations to read the system clock.</para>
    /// </summary>
    public byte Precision => m_data[3];

    /// <summary>
    /// <para>Root Delay (rootdelay): Total round-trip delay to the reference clock, in NTP short format.</para>
    /// </summary>
    public System.TimeSpan RootDelay => ConvertNtpShortFormatToTimeSpan(RootDelaySeconds, RootDelayFraction);
    [System.CLSCompliant(false)] public ushort RootDelaySeconds => m_data.AsReadOnlySpan(4).ReadUInt16(Endianess.BigEndian);
    [System.CLSCompliant(false)] public ushort RootDelayFraction => m_data.AsReadOnlySpan(6).ReadUInt16(Endianess.BigEndian);

    /// <summary>
    /// <para>Root Dispersion (rootdisp): Total dispersion to the reference clock, in NTP short format.</para>
    /// </summary>
    public System.TimeSpan RootDispersion => ConvertNtpShortFormatToTimeSpan(RootDispersionSeconds, RootDispersionFraction);
    [System.CLSCompliant(false)] public ushort RootDispersionSeconds => m_data.AsReadOnlySpan(8).ReadUInt16(Endianess.BigEndian);
    [System.CLSCompliant(false)] public ushort RootDispersionFraction => m_data.AsReadOnlySpan(10).ReadUInt16(Endianess.BigEndian);

    /// <summary>
    /// <para>Reference ID (refid): 32-bit code identifying the particular server or reference clock. The interpretation depends on the value in the stratum field.</para>
    /// </summary>
    [System.CLSCompliant(false)] public uint ReferenceID => m_data.AsReadOnlySpan(12).ReadUInt32(Endianess.BigEndian);

    /// <summary>
    /// <para>For packet stratum 0 (unspecified or invalid), this is a four-character ASCII [RFC1345] string, called the "kiss code", used for debugging and monitoring purposes.</para>
    /// <para>For stratum 1 (reference clock), this is a four-octet, left-justified, zero-padded ASCII string assigned to the reference clock. The authoritative list of Reference Identifiers is maintained by IANA; however, any string beginning with the ASCII character "X" is reserved for unregistered experimentation and development.</para>
    /// <para>Above stratum 1 (secondary servers and clients): this is the reference identifier of the server and can be used to detect timing loops.</para>
    /// </summary>
    public string KissCode => System.Text.Encoding.ASCII.GetString(m_data.AsSpan(12, 4));

    /// <summary>
    /// <para>Reference Timestamp (UTC): Time when the system clock was last set or corrected, in NTP timestamp format.</para>
    /// </summary>
    public System.DateTime ReferenceTimestampUtc => ConvertNtpTimestampFormatToDateTime(ReferenceTimestampSeconds, ReferenceTimestampFraction);
    [System.CLSCompliant(false)] public uint ReferenceTimestampSeconds => m_data.AsReadOnlySpan(16).ReadUInt32(Endianess.BigEndian);
    [System.CLSCompliant(false)] public uint ReferenceTimestampFraction => m_data.AsReadOnlySpan(20).ReadUInt32(Endianess.BigEndian);

    /// <summary>
    /// <para>Origin Timestamp (UTC): Time at the client when the request departed for the server, in NTP timestamp format.</para>
    /// </summary>
    public System.DateTime OriginTimestampUtc => ConvertNtpTimestampFormatToDateTime(OriginTimestampSeconds, OriginTimestampFraction);
    [System.CLSCompliant(false)] public uint OriginTimestampSeconds => m_data.AsReadOnlySpan(24).ReadUInt32(Endianess.BigEndian);
    [System.CLSCompliant(false)] public uint OriginTimestampFraction => m_data.AsReadOnlySpan(28).ReadUInt32(Endianess.BigEndian);

    /// <summary>
    /// <para>Receive Timestamp (UTC): Time at the server when the request arrived from the client, in NTP timestamp format.</para>
    /// </summary>
    public System.DateTime ReceiveTimestampUtc => ConvertNtpTimestampFormatToDateTime(ReceiveTimestampSeconds, ReceiveTimestampFraction);
    [System.CLSCompliant(false)] public uint ReceiveTimestampSeconds => m_data.AsReadOnlySpan(32).ReadUInt32(Endianess.BigEndian);
    [System.CLSCompliant(false)] public uint ReceiveTimestampFraction => m_data.AsReadOnlySpan(36).ReadUInt32(Endianess.BigEndian);

    /// <summary>
    /// <para>Transmit Timestamp (UTC): Time at the server when the response left for the client, in NTP timestamp format.</para>
    /// </summary>
    public System.DateTime TransmitTimestampUtc => ConvertNtpTimestampFormatToDateTime(TransmitTimestampSeconds, TransmitTimestampFraction);
    [System.CLSCompliant(false)] public uint TransmitTimestampSeconds => m_data.AsReadOnlySpan(40).ReadUInt32(Endianess.BigEndian);
    [System.CLSCompliant(false)] public uint TransmitTimestampFraction => m_data.AsReadOnlySpan(44).ReadUInt32(Endianess.BigEndian);

    #region Static methods

    #region Conversion methods

    [System.CLSCompliant(false)]
    public static System.TimeSpan ConvertNtpShortFormatToTimeSpan(ushort seconds, ushort fraction)
      => System.TimeSpan.FromSeconds(seconds + fraction.ConvertDecimalToFraction(10));

    [System.CLSCompliant(false)]
    public static System.DateTime ConvertNtpTimestampFormatToDateTime(uint seconds, uint fraction)
      => NetworkTimeProtocol.PrimeEpoch.AddSeconds(seconds + fraction.ConvertDecimalToFraction(10));

    #endregion // Conversion methods

    public static byte[] GetBytes(string host, out System.DateTime clientTransmission, out System.DateTime clientReception)
    {
      var bytes = new byte[48];

      bytes[0] = 0x1B;

      using (var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp))
      {
        socket.Connect(host, 123);

        clientTransmission = System.DateTime.UtcNow;

        socket.Send(bytes);
        socket.Receive(bytes);

        clientReception = System.DateTime.UtcNow;
      };

      return bytes;
    }

    public static bool TryGet(string host, out byte[] bytes, out System.DateTime clientTransmission, out System.DateTime clientReception)
    {
      try
      {
        bytes = GetBytes(host, out clientTransmission, out clientReception);
        return true;
      }
      catch { }

      bytes = default!;
      clientTransmission = System.DateTime.MinValue;
      clientReception = System.DateTime.MaxValue;
      return false;
    }

    public static bool TryGet(out byte[] bytes, out System.DateTime clientTransmission, out System.DateTime clientReception, out string host)
    {
      (bytes, clientTransmission, clientReception, host) = Hosts.AsParallel().Select(static host => TryGet(host, out byte[] bytes, out System.DateTime clientTransmission, out System.DateTime clientReception) ? (bytes, clientTransmission, clientReception, host) : (bytes: null!, clientTransmission: System.DateTime.MinValue, clientReception: System.DateTime.MaxValue, string.Empty)).FirstOrDefault(data => data.bytes != null);

      return bytes != null;
    }

    //public static bool TryGetLocalDateTime(string host, out System.DateTime ldt)
    //{
    //  if (TryGetNetworkTimeProtocol(host, out var ntp))
    //  {
    //    ldt = ntp.TransmittedDateTimeLocal;
    //    return true;
    //  }
    //  else
    //  {
    //    ldt = default;
    //    return false;
    //  }
    //}

    //public static bool TryGetLocalDateTime(out System.DateTime ldt)
    //{
    //  if (TryGetNetworkTimeProtocol(out var ntp))
    //  {
    //    ldt = ntp.TransmittedDateTimeLocal;
    //    return true;
    //  }
    //  else
    //  {
    //    ldt = default;
    //    return false;
    //  }
    //}

    public static bool TryGetNetworkTimeProtocol(string host, out NetworkTimeProtocol ntp)
    {
      if (TryGet(host, out var bytes, out var clientTransmission, out var clientReception))
      {
        ntp = new(bytes, clientTransmission, clientReception, host);
        return true;
      }

      ntp = default!;
      return false;
    }

    public static bool TryGetNetworkTimeProtocol(out NetworkTimeProtocol ntp)
    {
      if (TryGet(out var bytes, out var clientTransmission, out var clientReception, out var host))
      {
        ntp = new(bytes, clientTransmission, clientReception, host);
        return true;
      }

      ntp = default!;
      return false;
    }

    #endregion // Static methods
  }
}
