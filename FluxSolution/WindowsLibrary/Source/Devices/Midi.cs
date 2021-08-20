#if WINDOWS_UWP
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsChannelMode(this Windows.Devices.Midi.IMidiMessage source) => (source.RawData.GetByte(0) & 0xF0) == 0xB0 && source.RawData.GetByte(1) >= 120;
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsChannelVoice(this Windows.Devices.Midi.IMidiMessage source) => (source.RawData.GetByte(0) & 0xF0) is int b && b != 0xF0 && (b != 0xB0 || source.RawData.GetByte(1) <= 119);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsSystemCommon(this Windows.Devices.Midi.IMidiMessage source) => (source.RawData.GetByte(0) & 0xF8) == 0xF0;
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsSystemRealTime(this Windows.Devices.Midi.IMidiMessage source) => (source.RawData.GetByte(0) & 0xF8) == 0xF8;

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double ToFrequency(this Windows.Devices.Midi.MidiNoteOffMessage source) => Media.Midi.Note.ToFrequency(source.Note);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double ToFrequency(this Windows.Devices.Midi.MidiNoteOnMessage source) => Media.Midi.Note.ToFrequency(source.Note);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static string ToName(this Windows.Devices.Midi.MidiNoteOffMessage source) => Media.Midi.Note.GetName(source.Note);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static string ToName(this Windows.Devices.Midi.MidiNoteOnMessage source) => Media.Midi.Note.GetName(source.Note);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int ToOctave(this Windows.Devices.Midi.MidiNoteOffMessage source) => Media.Midi.Note.GetOctave(source.Note);
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int ToOctave(this Windows.Devices.Midi.MidiNoteOnMessage source) => Media.Midi.Note.GetOctave(source.Note);
  }
}

namespace Flux.Midi
{
  //public class DeviceWatcher
  //{
  //	private string _deviceSelector;
  //	private Windows.Devices.Enumeration.DeviceWatcher _deviceWatcher;
  //	private bool _enumerationCompleted;

  //	public Windows.Devices.Enumeration.DeviceInformationCollection DeviceInformationCollection { get; private set; }

  //	public DeviceWatcher(string deviceSelector)
  //	{
  //		_deviceSelector = deviceSelector;

  //		_deviceWatcher = Windows.Devices.Enumeration.DeviceInformation.CreateWatcher(deviceSelector);

  //		_deviceWatcher.Added += DeviceWatcher_Added;
  //		_deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
  //		_deviceWatcher.Removed += DeviceWatcher_Removed;
  //		_deviceWatcher.Stopped += DeviceWatcher_Stopped;
  //		_deviceWatcher.Updated += DeviceWatcher_Updated;
  //	}
  //	~DeviceWatcher()
  //	{
  //		_deviceWatcher.Added -= DeviceWatcher_Added;
  //		_deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
  //		_deviceWatcher.Removed -= DeviceWatcher_Removed;
  //		_deviceWatcher.Stopped += DeviceWatcher_Stopped;
  //		_deviceWatcher.Updated -= DeviceWatcher_Updated;
  //	}

  //	public void Start()
  //	{
  //		if (_deviceWatcher.Status != Windows.Devices.Enumeration.DeviceWatcherStatus.Started)
  //			_deviceWatcher.Start();
  //	}
  //	public void Stop()
  //	{
  //		if (_deviceWatcher.Status != Windows.Devices.Enumeration.DeviceWatcherStatus.Stopped)
  //			_deviceWatcher.Stop();
  //	}

  //	private async void UpdateDevices()
  //	{
  //		if (_enumerationCompleted)
  //			DeviceInformationCollection = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(_deviceSelector);
  //	}

  //	private void DeviceWatcher_Added(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformation args)
  //	{
  //		UpdateDevices();
  //	}
  //	private void DeviceWatcher_EnumerationCompleted(Windows.Devices.Enumeration.DeviceWatcher sender, object args)
  //	{
  //		_enumerationCompleted = true;

  //		UpdateDevices();
  //	}
  //	private void DeviceWatcher_Removed(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
  //	{
  //		UpdateDevices();
  //	}
  //	private void DeviceWatcher_Stopped(Windows.Devices.Enumeration.DeviceWatcher sender, object args)
  //	{
  //	}
  //	private void DeviceWatcher_Updated(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
  //	{
  //		UpdateDevices();
  //	}
  //}

  public class MidiPortsDeviceWatcher
  {
    public DeviceWatcher InPorts { get; private set; }
    public DeviceWatcher OutPorts { get; private set; }

    public MidiPortsDeviceWatcher()
    {
      InPorts = new DeviceWatcher(Windows.Devices.Midi.MidiInPort.GetDeviceSelector());
      OutPorts = new DeviceWatcher(Windows.Devices.Midi.MidiOutPort.GetDeviceSelector());

      InPorts.Start();
      OutPorts.Start();
    }
    ~MidiPortsDeviceWatcher()
    {
      InPorts.Stop();
      OutPorts.Stop();
    }
  }

  //public class MidiInPortWatcher : DeviceWatcher
  //{
  //	public MidiInPortWatcher()
  //		: base(Windows.Devices.Midi.MidiInPort.GetDeviceSelector())
  //	{
  //		base.Start();
  //	}
  //	~MidiInPortWatcher()
  //	{
  //		base.Stop();
  //	}
  //}

  //public class MidiOutPortWatcher : DeviceWatcher
  //{
  //	public MidiOutPortWatcher()
  //		: base(Windows.Devices.Midi.MidiOutPort.GetDeviceSelector())
  //	{
  //		base.Start();
  //	}
  //	~MidiOutPortWatcher()
  //	{
  //		base.Stop();
  //	}
  //}

  //public class MidiDeviceWatcher
  //{
  //	public MidiInPortWatcher MidiInPorts { get; private set; }
  //	public MidiOutPortWatcher MidiOutPorts { get; private set; }

  //public MidiDeviceWatcher()
  //	{
  //		MidiInPorts = new MidiInPortWatcher();
  //		MidiOutPorts = new MidiOutPortWatcher();
  //	}

  //	async void CreateAll()
  //	{
  //		var midiIn1 = await Windows.Devices.Midi.MidiInPort.FromIdAsync("1");
  //		midiIn1.MessageReceived += MidiIn1_MessageReceived;

  //	}

  //	private void MidiIn1_MessageReceived(Windows.Devices.Midi.MidiInPort sender, Windows.Devices.Midi.MidiMessageReceivedEventArgs args)
  //	{
  //		args.
  //	}
  //}
}
#endif
