using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Flux.Media.Midi
{
  #region Helpers
  /// <summary>
  /// Manufacturer of MIDI device driver.
  /// </summary>
  public enum Manufacturer : int
  {
    /// <summary>
    /// Unknown manufacturer.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Antex Electronics Corporation.
    /// </summary>
    Antex = 31,

    /// <summary>
    /// APPS Software.
    /// </summary>
    Apps = 42,

    /// <summary>
    /// Audio Processing Technology.
    /// </summary>
    Apt = 56,

    /// <summary>
    /// Artisoft, Inc.
    /// </summary>
    Artisoft = 20,

    /// <summary>
    /// AST Research, Inc.
    /// </summary>
    Ast = 64,

    /// <summary>
    /// ATI Technologies, Inc.
    /// </summary>
    Ati = 27,

    /// <summary>
    /// Audio, Inc.
    /// </summary>
    Audiofile = 47,

    /// <summary>
    /// Audio Processing Technology.
    /// </summary>
    Audiopt = 74,

    /// <summary>
    /// Auravision Corporation.
    /// </summary>
    Auravision = 80,

    /// <summary>
    /// Aztech Labs, Inc.
    /// </summary>
    Aztech = 52,

    /// <summary>
    /// Canopus, Co., Ltd.
    /// </summary>
    Canopus = 49,

    /// <summary>
    /// Computer Aided Technology, Inc.
    /// </summary>
    Cat = 41,

    /// <summary>
    /// Compusic.
    /// </summary>
    Compusic = 89,

    /// <summary>
    /// Computer Friends, Inc.
    /// </summary>
    ComputerFriends = 45,

    /// <summary>
    /// Control Resources Corporation.
    /// </summary>
    Controlres = 84,

    /// <summary>
    /// Creative Labs, Inc.
    /// </summary>
    Creative = 2,

    /// <summary>
    /// Dialogic Corporation.
    /// </summary>
    Dialogic = 93,

    /// <summary>
    /// Dolby Laboratories, Inc.
    /// </summary>
    Dolby = 78,

    /// <summary>
    /// DSP Group, Inc.
    /// </summary>
    DspGroup = 43,

    /// <summary>
    /// DSP Solutions, Inc.
    /// </summary>
    DspSolutions = 25,

    /// <summary>
    /// Echo Speech Corporation.
    /// </summary>
    Echo = 39,

    /// <summary>
    /// Seiko Epson Corporation, Inc.
    /// </summary>
    Epson = 50,

    /// <summary>
    /// ESS Technology, Inc.
    /// </summary>
    Ess = 46,

    /// <summary>
    /// Everex Systems, Inc.
    /// </summary>
    Everex = 38,

    /// <summary>
    /// EXAN, Ltd.
    /// </summary>
    Exan = 63,

    /// <summary>
    /// Fujitsu, Ltd.
    /// </summary>
    Fujitsu = 4,

    /// <summary>
    /// Advanced Gravis Computer Technology, Ltd.
    /// </summary>
    Gravis = 34,

    /// <summary>
    /// International Business Machines.
    /// </summary>
    Ibm = 22,

    /// <summary>
    /// ICL Personal Systems.
    /// </summary>
    IclPs = 32,

    /// <summary>
    /// Integrated Circuit Systems, Inc.
    /// </summary>
    Ics = 57,

    /// <summary>
    /// Intel Corporation.
    /// </summary>
    Intel = 33,

    /// <summary>
    /// InterActive, Inc.
    /// </summary>
    Interactive = 36,

    /// <summary>
    /// I/O Magic Corporation.
    /// </summary>
    Iomagic = 82,

    /// <summary>
    /// Iterated Systems, Inc.
    /// </summary>
    Iteratedsys = 58,

    /// <summary>
    /// Toshihiko Okuhura, Korg, Inc.
    /// </summary>
    Korg = 55,

    /// <summary>
    /// Logitech, Inc.
    /// </summary>
    Logitech = 60,

    /// <summary>
    /// Lyrrus, Inc.
    /// </summary>
    Lyrrus = 88,

    /// <summary>
    /// Matsushita Electric Corporation of America.
    /// </summary>
    Matsushita = 83,

    /// <summary>
    /// Media Vision, Inc.
    /// </summary>
    Mediavision = 3,

    /// <summary>
    /// microEngineering Labs.
    /// </summary>
    Melabs = 44,

    /// <summary>
    /// Metheus Corporation.
    /// </summary>
    Metheus = 59,

    /// <summary>
    /// Microsoft Corporation.
    /// </summary>
    Microsoft = 1,

    /// <summary>
    /// MOSCOM Corporation.
    /// </summary>
    Moscom = 68,

    /// <summary>
    /// Motorola, Inc.
    /// </summary>
    Motorola = 48,

    /// <summary>
    /// NCR Corporation.
    /// </summary>
    Ncr = 62,

    /// <summary>
    /// NEC Corporation.
    /// </summary>
    Nec = 26,

    /// <summary>
    /// New Media Corporation.
    /// </summary>
    Newmedia = 86,

    /// <summary>
    /// Natural MicroSystems Corporation.
    /// </summary>
    Nms = 87,

    /// <summary>
    /// OKI.
    /// </summary>
    Oki = 79,

    /// <summary>
    /// Ing. C. Olivetti &amp; C., S.p.A.
    /// </summary>
    Olivetti = 81,

    /// <summary>
    /// OPTi, Inc.
    /// </summary>
    Opti = 90,

    /// <summary>
    /// Roland Corporation.
    /// </summary>
    Roland = 24,

    /// <summary>
    /// SCALACS.
    /// </summary>
    Scalacs = 54,

    /// <summary>
    /// Sierra Semiconductor Corporation.
    /// </summary>
    Sierra = 40,

    /// <summary>
    /// Silicon Software, Inc.
    /// </summary>
    Siliconsoft = 69,

    /// <summary>
    /// Sonic Foundry.
    /// </summary>
    Sonicfoundry = 66,

    /// <summary>
    /// Speech Compression.
    /// </summary>
    Speechcomp = 76,

    /// <summary>
    /// Supermac Technology, Inc.
    /// </summary>
    Supermac = 73,

    /// <summary>
    /// Tandy Corporation.
    /// </summary>
    Tandy = 29,

    /// <summary>
    /// Truevision, Inc.
    /// </summary>
    Truevision = 51,

    /// <summary>
    /// Turtle Beach Systems.
    /// </summary>
    TurtleBeach = 21,

    /// <summary>
    /// Video Associates Labs, Inc.
    /// </summary>
    Val = 35,

    /// <summary>
    /// VideoLogic, Inc.
    /// </summary>
    Videologic = 53,

    /// <summary>
    /// Visual Information Technologies, Inc.
    /// </summary>
    Vitec = 67,

    /// <summary>
    /// VocalTec, Inc.
    /// </summary>
    Vocaltec = 23,

    /// <summary>
    /// Voyetra Technologies.
    /// </summary>
    Voyetra = 30,

    /// <summary>
    /// Wang Laboratories.
    /// </summary>
    Wanglabs = 28,

    /// <summary>
    /// Willow Pond Corporation.
    /// </summary>
    Willowpond = 65,

    /// <summary>
    /// Winnov, LP.
    /// </summary>
    Winnov = 61,

    /// <summary>
    /// Yamaha Corporation of America.
    /// </summary>
    Yamaha = 37,

    /// <summary>
    /// Xebec Multimedia Solutions Limited.
    /// </summary>
    Xebec = 85
  }

  //public static byte[] UnpackSysExBytes(System.IntPtr headerPointer)
  //{
  //  var header = (MIDIHDR)System.Runtime.InteropServices.Marshal.PtrToStructure(headerPointer, typeof(MIDIHDR));
  //  var data = new byte[header.dwBytesRecorded - 1];
  //  System.Runtime.InteropServices.Marshal.Copy(System.IntPtr.Add(header.lpData, 1), data, 0, data.Length);

  //  return data;
  //}
  #endregion Helpers

  //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  //internal struct Header
  //{
  //  public System.IntPtr lpData;
  //  public int dwBufferLength;
  //  public int dwBytesRecorded;
  //  public System.IntPtr dwUser;
  //  public int dwFlags;
  //  public System.IntPtr lpNext;
  //  public System.IntPtr reserved;
  //  public int dwOffset;
  //  [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4)]
  //  public int[] dwReserved;
  //}

  //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  //public struct MidiInCapabilities
  //  : System.IEquatable<MidiInCapabilities>
  //{
  //  private ushort wMid;
  //  private ushort wPid;
  //  private uint vDriverVersion;
  //  [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
  //  private string szPname;
  //  private uint dwSupport;

  //  public System.Version DriverVersion => new System.Version((int)(vDriverVersion >> 8), (int)(vDriverVersion & 0xFF));
  //  public Manufacturer Manufacturer => System.Enum.IsDefined(typeof(Manufacturer), wMid) ? (Manufacturer)wMid : Manufacturer.Unknown;
  //  public string Name => szPname;
  //  public int ProductIdentifier => wPid;
  //  public long Support => dwSupport;

  //  // Operators
  //  public static bool operator ==(MidiInCapabilities a, MidiInCapabilities b)
  //    => a.Equals(b);
  //  public static bool operator !=(MidiInCapabilities a, MidiInCapabilities b)
  //    => !a.Equals(b);
  //  // IEquatable
  //  public bool Equals([AllowNull] MidiInCapabilities other)
  //    => DriverVersion == other.DriverVersion && Manufacturer == other.Manufacturer && Name == other.Name && ProductIdentifier == other.ProductIdentifier && Support == other.Support;
  //  // Object overrides
  //  public override bool Equals(object? obj)
  //    => obj is MidiInCapabilities mic && Equals(mic);
  //  public override int GetHashCode()
  //    => Flux.HashCode.CombineCore(DriverVersion, Manufacturer, Name, ProductIdentifier, Support);
  //  public override string ToString()
  //    => $"<{Manufacturer}, \"{Name}\", v{DriverVersion}>";
  //}

  /// <summary>Manage MIDI In Ports</summary>
  public class MidiIn
    : Disposable
  {
    public const int MmSysErrNoError = 0;

    public const int CallbackFunction = 0x00030000;

    private readonly Winmm.MidiInProc m_midiInProc;

    private System.IntPtr m_id;

    public int Index { get; }

    public Winmm.MidiInCaps Capabilities { get; }

    private MidiIn(int index, Winmm.MidiInCaps capabilities)
    {
      m_midiInProc = new Winmm.MidiInProc(MidiProc);

      Index = index;
      Capabilities = capabilities;

      ErrorHandled(Winmm.NativeMethods.midiInOpen(out m_id, (uint)index, m_midiInProc, System.IntPtr.Zero, 0));
    }

    private static bool ErrorHandled(uint mmsyserr)
      => mmsyserr == MmSysErrNoError ? true : throw new System.InvalidOperationException();

    public static int Count
      => unchecked((int)Winmm.NativeMethods.midiInGetNumDevs());

    public bool Start()
      => ErrorHandled(Winmm.NativeMethods.midiInStart(m_id));

    public bool Stop()
      => ErrorHandled(Winmm.NativeMethods.midiInStop(m_id));

    private void MidiProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2)
    {
      // Receive messages here
    }

    protected override void DisposeManaged()
    {
      ErrorHandled(Winmm.NativeMethods.midiInClose(m_id));

      m_id = System.IntPtr.Zero;
    }
  }

  //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  //public struct MidiOutCapabilities
  //  : System.IEquatable<MidiOutCapabilities>
  //{
  //  private readonly ushort wMid;
  //  private readonly ushort wPid;
  //  private readonly uint vDriverVersion;
  //  [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
  //  private readonly string szPname;
  //  private readonly ushort wTechnology;
  //  private readonly ushort wVoices;
  //  private readonly ushort wNotes;
  //  private readonly ushort wChannelMask;
  //  private readonly uint dwSupport;

  //  public int ChannelMask => wChannelMask;
  //  public System.Version DriverVersion => new System.Version((int)(vDriverVersion >> 8), (int)(vDriverVersion & 0xFF));
  //  public Manufacturer Manufacturer => System.Enum.IsDefined(typeof(Manufacturer), wMid) ? (Manufacturer)wMid : Manufacturer.Unknown;
  //  public string Name => szPname;
  //  public int Notes => wNotes;
  //  public int ProductIdentifier => wPid;
  //  public long Support => dwSupport;
  //  public int Technology => wTechnology;
  //  public int Voices => wVoices;

  //  // Operators
  //  public static bool operator ==(MidiOutCapabilities a, MidiOutCapabilities b)
  //    => a.Equals(b);
  //  public static bool operator !=(MidiOutCapabilities a, MidiOutCapabilities b)
  //    => !a.Equals(b);
  //  // IEquatable
  //  public bool Equals([AllowNull] MidiOutCapabilities other)
  //    => ChannelMask == other.ChannelMask && DriverVersion == other.DriverVersion && Manufacturer == other.Manufacturer && Name == other.Name && Notes == other.Notes && ProductIdentifier == other.ProductIdentifier && Support == other.Support && Technology == other.Technology && Voices == other.Voices;
  //  // Object overrides
  //  public override bool Equals(object? obj)
  //    => obj is MidiOutCapabilities moc && Equals(moc);
  //  public override int GetHashCode()
  //    => Flux.HashCode.CombineCore(ChannelMask, DriverVersion, Manufacturer, Name, Notes, ProductIdentifier, Support, Technology, Voices);
  //  public override string ToString()
  //    => $"<{Manufacturer}, \"{Name}\", v{DriverVersion}>";
  //};

  /// <summary>Manage MIDI Out Ports</summary>
  public class MidiOut
    : Disposable
  {
    public const int MmSysErrNoError = 0;

    private System.IntPtr m_id;

    public int Index { get; }

    public Winmm.MidiOutCaps Capabilities { get; }

    //private void MidiProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2)
    //{
    //}

    private MidiOut(int index, Winmm.MidiOutCaps capabilities)
    {
      Index = index;
      Capabilities = capabilities;

      ErrorHandled(Winmm.NativeMethods.midiOutOpen(out m_id, (uint)index, null, System.IntPtr.Zero, 0));
    }

    public static MidiOut Create(int index) => GetMidiOuts().Where(mo => mo.m_id.ToInt32() == index).Single();
    public static MidiOut Create(string name) => GetMidiOuts().Where(mo => mo.Capabilities.Name == name).Single();

    private static bool ErrorHandled(uint mmsyserr)
      => mmsyserr == MmSysErrNoError ? true : throw new System.InvalidOperationException();

    public void TrySendMore(byte[] message)
    {
      var mhdr = new Winmm.Header();
      mhdr.dwBufferLength = mhdr.dwBytesRecorded = message?.Length ?? throw new System.ArgumentNullException(nameof(message));
      mhdr.lpData = System.Runtime.InteropServices.Marshal.AllocHGlobal(mhdr.dwBufferLength);
      System.Runtime.InteropServices.Marshal.Copy(message, 0, mhdr.lpData, mhdr.dwBufferLength);
      var sizeOfHeader = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Winmm.Header));
      var nhdr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeOfHeader);
      System.Runtime.InteropServices.Marshal.StructureToPtr(mhdr, nhdr, false);
      ErrorHandled(Winmm.NativeMethods.midiOutPrepareHeader(m_id, nhdr, sizeOfHeader));
      ErrorHandled(Winmm.NativeMethods.midiOutLongMsg(m_id, nhdr, sizeOfHeader));
      ErrorHandled(Winmm.NativeMethods.midiOutUnprepareHeader(m_id, nhdr, sizeOfHeader));
    }
    //=> message.Length <= 3 ? midiOutShortMsg(Id, BitConverter.BigEndian.ToUInt32(message, 0)) : throw new System.ArgumentOutOfRangeException(nameof(message));
    public bool TrySend(byte status, byte data1, byte data2)
      => Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)(data2 << 16 | data1 << 8 | status)) == MmSysErrNoError;
    public bool TrySend(int message)
      => Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)(message)) == MmSysErrNoError;

    public void NoteOff(int channel, int note, int velocity)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((velocity << 16) | (note << 8) | (0x80 | (channel & 0xF)))));
    public void NoteOn(int channel, int note, int velocity)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((velocity << 16) | (note << 8) | (0x90 | (channel & 0xF)))));
    public void AfterTouch(int channel, int note, int pressure)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 16) | (note << 8) | (0xA0 | (channel & 0xF)))));
    public void Controller(int channel, int cc, int value)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((value << 16) | (cc << 8) | (0xB0 | (channel & 0xF)))));
    public void ProgramChange(int channel, int pressure)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 8) | (0xC0 | (channel & 0xF)))));
    public void ChannelPressure(int channel, int pressure)
      => ErrorHandled(Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 8) | (0xD0 | (channel & 0xF)))));

    private static System.Collections.Generic.IEnumerable<MidiOut> GetMidiOuts()
    {
      var mo = new MidiOut[Winmm.NativeMethods.midiOutGetNumDevs()];

      for (var index = 0; index < mo.Length; index++)
      {
        var moc = default(Winmm.MidiOutCaps);

        ErrorHandled(Winmm.NativeMethods.midiOutGetDevCaps(new System.IntPtr(index), out moc, (uint)System.Runtime.InteropServices.Marshal.SizeOf(moc)));

        yield return new MidiOut(index, moc);
      }
    }

//    #region WinMM MIDI Output functions
//    public delegate void MidiOutProc(System.IntPtr hmo, int wMsg, in System.IntPtr dwInstance, in int dwParam1, in int dwParam2);
//#pragma warning disable IDE1006 // Naming Styles
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutClose(System.IntPtr hmo);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutGetDevCaps(System.IntPtr uDeviceID, out MidiOutCapabilities pmoc, uint cbmoc);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)] private static extern uint midiOutGetErrorText(uint mmrError, System.Text.StringBuilder pszText, uint cchText);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutGetNumDevs();
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutLongMsg(System.IntPtr hmo, System.IntPtr pmh, int cbmh);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutMessage(System.IntPtr hmo, uint uMsg, in uint dw1, in uint dw2);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutOpen(out System.IntPtr phmo, uint uDeviceID, MidiOutProc? dwCallback, System.IntPtr dwInstance, uint fdwOpen);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutPrepareHeader(System.IntPtr hmo, System.IntPtr pmh, int cbmh);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutReset(System.IntPtr hmo);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutShortMsg(System.IntPtr hmo, uint dwMsg);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] private static extern uint midiOutUnprepareHeader(System.IntPtr hmo, System.IntPtr pmh, int cbmh);
//#pragma warning restore IDE1006 // Naming Styles
//    #endregion

    protected override void DisposeManaged()
    {
      ErrorHandled(Winmm.NativeMethods.midiOutClose(m_id));

      m_id = System.IntPtr.Zero;
    }
  }
}
