namespace Flux.Midi
{
  public class NoteOffMidiMessage
  {
    private int m_channel;
    public int Channel { get => m_channel; set => m_channel = value >= 0 && value <= 15 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_note;
    public int Note { get => m_note; set => m_note = value >= 0 && value <= 127 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    private int m_velocity;
    public int Velocity { get => m_velocity; set => m_velocity = value >= 0 && value <= 127 ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

    public NoteOffMidiMessage(int channel, int note, int velocity)
    {
      Channel = channel;
      Note = note;
      Velocity = velocity;
    }
  }

  /// <summary>This note library enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://www.midi.org/specifications/item/table-1-summary-of-midi-message"/>
  /// <seealso cref="https://tttapa.github.io/Arduino/MIDI/Chap01-MIDI-Protocol.html"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public static class Protocol
  {
    #region Channel Voice messages
    public static byte CreateStatusByte(ChannelVoiceStatus status, int channel)
      => (byte)((int)status | (channel >= 0 && channel <= 0xF ? channel : throw new System.ArgumentOutOfRangeException(nameof(channel))));

    public static byte[] NoteOff(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOff, channel), Ensure7BitValue(note), Ensure7BitValue(velocity) };
    public static byte[] NoteOn(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOn, channel), Ensure7BitValue(note), Ensure7BitValue(velocity) };
    public static byte[] PolyphonicKeyPressure(int channel, int note, int notePressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.PolyphonicKeyPressure, channel), Ensure7BitValue(note), Ensure7BitValue(notePressure) };
    public static byte[] ControlChange(int channel, int controllerType, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Ensure7BitValue(controllerType), Ensure7BitValue(value) };
    public static byte[] ControlChange(int channel, ControllerChangeNumber controllerType, int value)
      => ControlChange(channel, (int)controllerType, value);
    public static byte[] ControlChange14(int channel, int controllerMsb, int controllerLsb, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Ensure7BitValue(controllerMsb), Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Ensure7BitValue(controllerLsb), Ensure14BitValueLow(value) };
    public static byte[] ControlChange14(int channel, ControllerChangeNumber controllerMsb, ControllerChangeNumber controllerLsb, int value)
      => ControlChange14(channel, (int)controllerMsb, (int)controllerLsb, value);
    public static byte[] ControlChangeNrpn(int channel, int parameter, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.NrpnMsb, Ensure14BitValueHigh(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.NrpnLsb, Ensure14BitValueLow(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.DataEntryMsb, Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.DataEntryLsb, Ensure14BitValueLow(value) };
    public static byte[] ChannelPressure(int channel, int channelPressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Ensure7BitValue(channelPressure) };
    public static byte[] PitchBend(int channel, int pitchBend)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Ensure14BitValueLow(pitchBend), Ensure14BitValueHigh(pitchBend) };
    #endregion Channel Voice messages

    #region Channel Mode messages
    public static byte[] AllSoundOff(int channel)
      => ControlChange(channel, ControllerChangeNumber.AllNotesOff, 0);
    public static byte[] ResetAllControllers(int channel)
      => ControlChange(channel, ControllerChangeNumber.ResetAllControllers, 0);
    public static byte[] LocalControlOff(int channel)
      => ControlChange(channel, ControllerChangeNumber.LocalControl, 0);
    public static byte[] LocalControlOn(int channel)
      => ControlChange(channel, ControllerChangeNumber.LocalControl, 127);
    public static byte[] AllNotesOff(int channel)
      => ControlChange(channel, ControllerChangeNumber.AllNotesOff, 0);
    public static byte[] OmniModeOff(int channel)
      => ControlChange(channel, ControllerChangeNumber.OmniModeOff, 0);
    public static byte[] OmniModeOn(int channel)
      => ControlChange(channel, ControllerChangeNumber.OmniModeOn, 0);
    public static byte[] MonoModeOn(int channel, int numberOfChannels)
      => ControlChange(channel, ControllerChangeNumber.MonoModeOn, numberOfChannels);
    public static byte[] PolyModeOn(int channel)
      => ControlChange(channel, ControllerChangeNumber.PolyModeOn, 0);
    #endregion Channel Mode messages

    #region Common messages
    public static byte[] MtcFullFrame(int rate, int hour, int minute, int second, int frame)
      => new byte[] { (int)SystemCommonStatus.StartOfExclusive, 0x7F, 0x7F, 0x01, 0x01, (byte)((rate >= 0 && rate <= 3 ? rate << 5 : throw new System.ArgumentOutOfRangeException(nameof(rate))) | (hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour)))), (byte)(minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute))), (byte)(second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second))), (byte)(frame >= 0 && frame <= 29 ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame))), (byte)SystemCommonStatus.EndOfExclusive };
    public static byte[] MtcQuarterFrame(int type, int value)
      => new byte[] { (byte)SystemCommonStatus.MidiTimeCodeQuarterFrame, (byte)((Ensure3BitValue(type) << 4) | Ensure4BitValue(value)) };
    public static byte[] MtcQuarterFrame0(int frameNumber)
      => MtcQuarterFrame(0x0, Ensure5BitValue(frameNumber) & 0xF);
    public static byte[] MtcQuarterFrame1(int frameNumber)
      => MtcQuarterFrame(0x1, Ensure5BitValue(frameNumber) >> 4);
    public static byte[] MtcQuarterFrame2(int second)
      => MtcQuarterFrame(0x2, Ensure6BitValue(second) & 0xF);
    public static byte[] MtcQuarterFrame3(int second)
      => MtcQuarterFrame(0x3, Ensure6BitValue(second) >> 4);
    public static byte[] MtcQuarterFrame4(int minute)
      => MtcQuarterFrame(0x4, Ensure6BitValue(minute) & 0xF);
    public static byte[] MtcQuarterFrame5(int minute)
      => MtcQuarterFrame(0x5, Ensure6BitValue(minute) >> 4);
    public static byte[] MtcQuarterFrame6(int hour)
      => MtcQuarterFrame(0x6, Ensure5BitValue(hour) & 0xF);
    public static byte[] MtcQuarterFrame7(int hour, int rate)
      => MtcQuarterFrame(0x7, Ensure2BitValue(rate) | Ensure5BitValue(hour) >> 4);
    public static byte[] SongPositionPointer(int songPositionInMidiBeats)
       => new byte[] { (byte)SystemCommonStatus.SongPositionPointer, Ensure14BitValueLow(songPositionInMidiBeats), Ensure14BitValueHigh(songPositionInMidiBeats) };
    public static byte[] SongSelect(int songNumber)
      => new byte[] { (byte)SystemCommonStatus.SongSelect, Ensure7BitValue(songNumber) };
    public static byte[] TuneRequest
      => new byte[] { (byte)SystemCommonStatus.TuneRequest };
    #endregion Common messages

    #region Realtime messages
    public static byte[] TimingClock
      => new byte[] { (byte)SystemRealtimeStatus.TimingClock };
    public static byte[] Start
      => new byte[] { (byte)SystemRealtimeStatus.Start };
    public static byte[] Continue
      => new byte[] { (byte)SystemRealtimeStatus.Continue };
    public static byte[] Stop
      => new byte[] { (byte)SystemRealtimeStatus.Stop };
    public static byte[] ActiveSensing
      => new byte[] { (byte)SystemRealtimeStatus.ActiveSensing };
    public static byte[] Reset
      => new byte[] { (byte)SystemRealtimeStatus.Reset };
    #endregion Realtime messages

    public static byte Ensure14BitValueHigh(int value)
      => value < 0 || value > 0x3FFF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)((value >> 7) & 0x7F);
    public static byte Ensure14BitValueLow(int value)
      => value < 0 || value > 0x3FFF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)(value & 0x7F);
    public static byte Ensure7BitValue(int value)
      => value < 0 || value > 0x7F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure6BitValue(int value)
      => value < 0 || value > 0x3F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure5BitValue(int value)
      => value < 0 || value > 0x1F ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure4BitValue(int value)
      => value < 0 || value > 0xF ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure3BitValue(int value)
      => value < 0 || value > 0x7 ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
    public static byte Ensure2BitValue(int value)
      => value < 0 || value > 0x3 ? throw new System.ArgumentOutOfRangeException(nameof(value)) : (byte)value;
  }
}
