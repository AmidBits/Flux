//namespace Flux.Media
//{
//  /// <summary>MIDI</summary>
//  /// <see cref="https://docs.microsoft.com/en-us/windows/win32/multimedia/midi-reference"/>
//  //[System.CLSCompliant(false)]
//  public class WinMm
//  {
//    public const int MMSYSERR_NOERROR = 0;

//    public const int CALLBACK_FUNCTION = 0x00030000;

//    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//    internal struct MIDIHDR
//    {
//      public System.IntPtr lpData;
//      public int dwBufferLength;
//      public int dwBytesRecorded;
//      public System.IntPtr dwUser;
//      public int dwFlags;
//      public System.IntPtr lpNext;
//      public System.IntPtr reserved;
//      public int dwOffset;
//      [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4)]
//      public int[] dwReserved;
//    }

//    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//    public struct MIDIINCAPS
//    {
//      public ushort wMid;
//      public ushort wPid;
//      public uint vDriverVersion;
//      [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
//      public string szPname;
//      public uint dwSupport;
//    }

//    public delegate void MidiInProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2);

//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInClose(System.IntPtr hMidiIn);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInGetDevCaps(System.IntPtr uDeviceID, out MIDIINCAPS pmic, uint cbmic);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInGetErrorText(uint wError, System.Text.StringBuilder lpText, uint cchText);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInGetNumDevs();
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInOpen(out System.IntPtr lphMidiIn, int uDeviceID, MidiInProc dwCallback, System.IntPtr dwCallbackInstance, uint dwFlags);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInMessage(System.IntPtr hmi, uint uMsg, out uint dw1, out uint dw2);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInStart(System.IntPtr hMidiIn);
//    [System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiInStop(System.IntPtr hMidiIn);

//    //[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//    //public struct MIDIOUTCAPS
//    //{
//    //  private ushort wMid;
//    //  private ushort wPid;
//    //  private uint vDriverVersion;
//    //  [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
//    //  private string szPname;
//    //  private ushort wTechnology;
//    //  private ushort wVoices;
//    //  private ushort wNotes;
//    //  private ushort wChannelMask;
//    //  private uint dwSupport;

//    //  public int ChannelMask => wChannelMask;
//    //  public System.Version DriverVersion => new System.Version((int)(vDriverVersion >> 8), (int)(vDriverVersion & 0xFF));
//    //  public Manufacturer Manufacturer => System.Enum.IsDefined(typeof(Manufacturer), wMid) ? (Manufacturer)wMid : Manufacturer.Unknown;
//    //  public string Name => szPname;
//    //  public int Notes => wNotes;
//    //  public int ProductIdentifier => wPid;
//    //  public long Support => dwSupport;
//    //  public int Technology => wTechnology;
//    //  public int Voices => wVoices;
//    //};

//    //public delegate void MidiOutProc(System.IntPtr hMidiIn, int wMsg, System.IntPtr dwInstance, int dwParam1, int dwParam2);

//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutClose(System.IntPtr hMidiIn);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutGetDevCaps(System.IntPtr uDeviceID, out MIDIOUTCAPS lpMidiOutCaps, uint cbMidiOutCaps);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutGetErrorText(uint mmrError, System.Text.StringBuilder pszText, uint cchText);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutGetNumDevs();
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutLongMsg(System.IntPtr hmo, System.IntPtr lpMidiOutHdr, int cbMidiOutHdr);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutMessage(System.IntPtr hmi, uint uMsg, in uint dw1, in uint dw2);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutOpen(out System.IntPtr lphMidiIn, uint uDeviceID, MidiOutProc dwCallback, System.IntPtr dwCallbackInstance, uint dwFlags);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutPrepareHeader(System.IntPtr hmo, System.IntPtr lpMidiOutHdr, int cbMidiOutHdr);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutShortMsg(uint hMidiOut, uint dwMsg);
//    //[System.Runtime.InteropServices.DllImport(@"winmm.dll")] [System.CLSCompliant(false)] public static extern uint midiOutUnprepareHeader(System.IntPtr hmo, System.IntPtr lpMidiOutHdr, int cbMidiOutHdr);

//    #region Helpers
//    /// <summary>
//    /// Manufacturer of MIDI device driver.
//    /// </summary>
//    public enum Manufacturer : ushort
//    {
//      /// <summary>
//      /// Unknown manufacturer.
//      /// </summary>
//      Unknown = 0,

//      /// <summary>
//      /// Antex Electronics Corporation.
//      /// </summary>
//      Antex = 31,

//      /// <summary>
//      /// APPS Software.
//      /// </summary>
//      Apps = 42,

//      /// <summary>
//      /// Audio Processing Technology.
//      /// </summary>
//      Apt = 56,

//      /// <summary>
//      /// Artisoft, Inc.
//      /// </summary>
//      Artisoft = 20,

//      /// <summary>
//      /// AST Research, Inc.
//      /// </summary>
//      Ast = 64,

//      /// <summary>
//      /// ATI Technologies, Inc.
//      /// </summary>
//      Ati = 27,

//      /// <summary>
//      /// Audio, Inc.
//      /// </summary>
//      Audiofile = 47,

//      /// <summary>
//      /// Audio Processing Technology.
//      /// </summary>
//      Audiopt = 74,

//      /// <summary>
//      /// Auravision Corporation.
//      /// </summary>
//      Auravision = 80,

//      /// <summary>
//      /// Aztech Labs, Inc.
//      /// </summary>
//      Aztech = 52,

//      /// <summary>
//      /// Canopus, Co., Ltd.
//      /// </summary>
//      Canopus = 49,

//      /// <summary>
//      /// Computer Aided Technology, Inc.
//      /// </summary>
//      Cat = 41,

//      /// <summary>
//      /// Compusic.
//      /// </summary>
//      Compusic = 89,

//      /// <summary>
//      /// Computer Friends, Inc.
//      /// </summary>
//      ComputerFriends = 45,

//      /// <summary>
//      /// Control Resources Corporation.
//      /// </summary>
//      Controlres = 84,

//      /// <summary>
//      /// Creative Labs, Inc.
//      /// </summary>
//      Creative = 2,

//      /// <summary>
//      /// Dialogic Corporation.
//      /// </summary>
//      Dialogic = 93,

//      /// <summary>
//      /// Dolby Laboratories, Inc.
//      /// </summary>
//      Dolby = 78,

//      /// <summary>
//      /// DSP Group, Inc.
//      /// </summary>
//      DspGroup = 43,

//      /// <summary>
//      /// DSP Solutions, Inc.
//      /// </summary>
//      DspSolutions = 25,

//      /// <summary>
//      /// Echo Speech Corporation.
//      /// </summary>
//      Echo = 39,

//      /// <summary>
//      /// Seiko Epson Corporation, Inc.
//      /// </summary>
//      Epson = 50,

//      /// <summary>
//      /// ESS Technology, Inc.
//      /// </summary>
//      Ess = 46,

//      /// <summary>
//      /// Everex Systems, Inc.
//      /// </summary>
//      Everex = 38,

//      /// <summary>
//      /// EXAN, Ltd.
//      /// </summary>
//      Exan = 63,

//      /// <summary>
//      /// Fujitsu, Ltd.
//      /// </summary>
//      Fujitsu = 4,

//      /// <summary>
//      /// Advanced Gravis Computer Technology, Ltd.
//      /// </summary>
//      Gravis = 34,

//      /// <summary>
//      /// International Business Machines.
//      /// </summary>
//      Ibm = 22,

//      /// <summary>
//      /// ICL Personal Systems.
//      /// </summary>
//      IclPs = 32,

//      /// <summary>
//      /// Integrated Circuit Systems, Inc.
//      /// </summary>
//      Ics = 57,

//      /// <summary>
//      /// Intel Corporation.
//      /// </summary>
//      Intel = 33,

//      /// <summary>
//      /// InterActive, Inc.
//      /// </summary>
//      Interactive = 36,

//      /// <summary>
//      /// I/O Magic Corporation.
//      /// </summary>
//      Iomagic = 82,

//      /// <summary>
//      /// Iterated Systems, Inc.
//      /// </summary>
//      Iteratedsys = 58,

//      /// <summary>
//      /// Toshihiko Okuhura, Korg, Inc.
//      /// </summary>
//      Korg = 55,

//      /// <summary>
//      /// Logitech, Inc.
//      /// </summary>
//      Logitech = 60,

//      /// <summary>
//      /// Lyrrus, Inc.
//      /// </summary>
//      Lyrrus = 88,

//      /// <summary>
//      /// Matsushita Electric Corporation of America.
//      /// </summary>
//      Matsushita = 83,

//      /// <summary>
//      /// Media Vision, Inc.
//      /// </summary>
//      Mediavision = 3,

//      /// <summary>
//      /// microEngineering Labs.
//      /// </summary>
//      Melabs = 44,

//      /// <summary>
//      /// Metheus Corporation.
//      /// </summary>
//      Metheus = 59,

//      /// <summary>
//      /// Microsoft Corporation.
//      /// </summary>
//      Microsoft = 1,

//      /// <summary>
//      /// MOSCOM Corporation.
//      /// </summary>
//      Moscom = 68,

//      /// <summary>
//      /// Motorola, Inc.
//      /// </summary>
//      Motorola = 48,

//      /// <summary>
//      /// NCR Corporation.
//      /// </summary>
//      Ncr = 62,

//      /// <summary>
//      /// NEC Corporation.
//      /// </summary>
//      Nec = 26,

//      /// <summary>
//      /// New Media Corporation.
//      /// </summary>
//      Newmedia = 86,

//      /// <summary>
//      /// Natural MicroSystems Corporation.
//      /// </summary>
//      Nms = 87,

//      /// <summary>
//      /// OKI.
//      /// </summary>
//      Oki = 79,

//      /// <summary>
//      /// Ing. C. Olivetti &amp; C., S.p.A.
//      /// </summary>
//      Olivetti = 81,

//      /// <summary>
//      /// OPTi, Inc.
//      /// </summary>
//      Opti = 90,

//      /// <summary>
//      /// Roland Corporation.
//      /// </summary>
//      Roland = 24,

//      /// <summary>
//      /// SCALACS.
//      /// </summary>
//      Scalacs = 54,

//      /// <summary>
//      /// Sierra Semiconductor Corporation.
//      /// </summary>
//      Sierra = 40,

//      /// <summary>
//      /// Silicon Software, Inc.
//      /// </summary>
//      Siliconsoft = 69,

//      /// <summary>
//      /// Sonic Foundry.
//      /// </summary>
//      Sonicfoundry = 66,

//      /// <summary>
//      /// Speech Compression.
//      /// </summary>
//      Speechcomp = 76,

//      /// <summary>
//      /// Supermac Technology, Inc.
//      /// </summary>
//      Supermac = 73,

//      /// <summary>
//      /// Tandy Corporation.
//      /// </summary>
//      Tandy = 29,

//      /// <summary>
//      /// Truevision, Inc.
//      /// </summary>
//      Truevision = 51,

//      /// <summary>
//      /// Turtle Beach Systems.
//      /// </summary>
//      TurtleBeach = 21,

//      /// <summary>
//      /// Video Associates Labs, Inc.
//      /// </summary>
//      Val = 35,

//      /// <summary>
//      /// VideoLogic, Inc.
//      /// </summary>
//      Videologic = 53,

//      /// <summary>
//      /// Visual Information Technologies, Inc.
//      /// </summary>
//      Vitec = 67,

//      /// <summary>
//      /// VocalTec, Inc.
//      /// </summary>
//      Vocaltec = 23,

//      /// <summary>
//      /// Voyetra Technologies.
//      /// </summary>
//      Voyetra = 30,

//      /// <summary>
//      /// Wang Laboratories.
//      /// </summary>
//      Wanglabs = 28,

//      /// <summary>
//      /// Willow Pond Corporation.
//      /// </summary>
//      Willowpond = 65,

//      /// <summary>
//      /// Winnov, LP.
//      /// </summary>
//      Winnov = 61,

//      /// <summary>
//      /// Yamaha Corporation of America.
//      /// </summary>
//      Yamaha = 37,

//      /// <summary>
//      /// Xebec Multimedia Solutions Limited.
//      /// </summary>
//      Xebec = 85
//    }

//    public static byte[] UnpackSysExBytes(System.IntPtr headerPointer)
//    {
//      var header = (MIDIHDR)System.Runtime.InteropServices.Marshal.PtrToStructure(headerPointer, typeof(MIDIHDR));
//      var data = new byte[header.dwBytesRecorded - 1];
//      System.Runtime.InteropServices.Marshal.Copy(System.IntPtr.Add(header.lpData, 1), data, 0, data.Length);

//      return data;
//    }
//    #endregion Helpers
//  }
//}
