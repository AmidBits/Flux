﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Flux.Midi
{
  #region Helpers
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

    private readonly Win32.Winmm.MidiInProc m_midiInProc;

    private System.IntPtr m_id;

    public int Index { get; }

    public Win32.Winmm.MidiInCapabilities Capabilities { get; }

    private MidiIn(int index, Win32.Winmm.MidiInCapabilities capabilities)
    {
      m_midiInProc = new Win32.Winmm.MidiInProc(MidiProc);

      Index = index;
      Capabilities = capabilities;

      ErrorHandled(Win32.Winmm.NativeMethods.midiInOpen(out m_id, (uint)index, m_midiInProc, System.IntPtr.Zero, 0));
    }

    private static bool ErrorHandled(uint mmsyserr)
      => mmsyserr == MmSysErrNoError ? true : throw new System.InvalidOperationException();

    public static int Count
      => unchecked((int)Win32.Winmm.NativeMethods.midiInGetNumDevs());

    public bool Start()
      => ErrorHandled(Win32.Winmm.NativeMethods.midiInStart(m_id));

    public bool Stop()
      => ErrorHandled(Win32.Winmm.NativeMethods.midiInStop(m_id));

    private void MidiProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2)
    {
      // Receive messages here
    }

    protected override void DisposeManaged()
    {
      ErrorHandled(Win32.Winmm.NativeMethods.midiInClose(m_id));

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

    public Win32.Winmm.MidiOutCapabilities Capabilities { get; }

    //private void MidiProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2)
    //{
    //}

    private MidiOut(int index, Win32.Winmm.MidiOutCapabilities capabilities)
    {
      Index = index;
      Capabilities = capabilities;

      ErrorHandled(Win32.Winmm.NativeMethods.midiOutOpen(out m_id, (uint)index, null, System.IntPtr.Zero, 0));
    }

    public static MidiOut Create(int index) => GetMidiOuts().Where(mo => mo.m_id.ToInt32() == index).Single();
    public static MidiOut Create(string name) => GetMidiOuts().Where(mo => mo.Capabilities.Name == name).Single();

    private static bool ErrorHandled(uint mmsyserr)
      => mmsyserr == MmSysErrNoError ? true : throw new System.InvalidOperationException();

    public void TrySendMore(byte[] message)
    {
      var mhdr = new Win32.Winmm.Header();
      mhdr.dwBufferLength = mhdr.dwBytesRecorded = message?.Length ?? throw new System.ArgumentNullException(nameof(message));
      mhdr.lpData = System.Runtime.InteropServices.Marshal.AllocHGlobal(mhdr.dwBufferLength);
      System.Runtime.InteropServices.Marshal.Copy(message, 0, mhdr.lpData, mhdr.dwBufferLength);
      var sizeOfHeader = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Win32.Winmm.Header));
      var nhdr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeOfHeader);
      System.Runtime.InteropServices.Marshal.StructureToPtr(mhdr, nhdr, false);
      ErrorHandled(Win32.Winmm.NativeMethods.midiOutPrepareHeader(m_id, nhdr, sizeOfHeader));
      ErrorHandled(Win32.Winmm.NativeMethods.midiOutLongMsg(m_id, nhdr, sizeOfHeader));
      ErrorHandled(Win32.Winmm.NativeMethods.midiOutUnprepareHeader(m_id, nhdr, sizeOfHeader));
    }
    //=> message.Length <= 3 ? midiOutShortMsg(Id, BitConverter.BigEndian.ToUInt32(message, 0)) : throw new System.ArgumentOutOfRangeException(nameof(message));
    public bool TrySend(byte status, byte data1, byte data2)
      => Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)(data2 << 16 | data1 << 8 | status)) == MmSysErrNoError;
    public bool TrySend(int message)
      => Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)(message)) == MmSysErrNoError;

    public void NoteOff(int channel, int note, int velocity)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((velocity << 16) | (note << 8) | (0x80 | (channel & 0xF)))));
    public void NoteOn(int channel, int note, int velocity)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((velocity << 16) | (note << 8) | (0x90 | (channel & 0xF)))));
    public void AfterTouch(int channel, int note, int pressure)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 16) | (note << 8) | (0xA0 | (channel & 0xF)))));
    public void Controller(int channel, int cc, int value)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((value << 16) | (cc << 8) | (0xB0 | (channel & 0xF)))));
    public void ProgramChange(int channel, int pressure)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 8) | (0xC0 | (channel & 0xF)))));
    public void ChannelPressure(int channel, int pressure)
      => ErrorHandled(Win32.Winmm.NativeMethods.midiOutShortMsg(m_id, (uint)((pressure << 8) | (0xD0 | (channel & 0xF)))));

    private static System.Collections.Generic.IEnumerable<MidiOut> GetMidiOuts()
    {
      var mo = new MidiOut[Win32.Winmm.NativeMethods.midiOutGetNumDevs()];

      for (var index = 0; index < mo.Length; index++)
      {
        Win32.Winmm.MidiOutCapabilities moc = new();
        ErrorHandled(Win32.Winmm.NativeMethods.midiOutGetDevCaps(new System.IntPtr(index), out moc, (uint)System.Runtime.InteropServices.Marshal.SizeOf(moc)));

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
      ErrorHandled(Win32.Winmm.NativeMethods.midiOutClose(m_id));

      m_id = System.IntPtr.Zero;
    }
  }
}
