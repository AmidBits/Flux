using Microsoft.VisualBasic.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace Flux.Media.Midi
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
  /// <see cref="https://www.midi.org/specifications/item/table-1-summary-of-midi-message"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public static class Protocol
  {
    public enum ControllerChange
    {
      BankSelectMsb = 0,
      ModulationWheelMsb = 1,
      BreathControllerMsb = 2,
      FootControllerMsb = 4,
      PortamentoTimeMsb = 5,
      DataEntryMsb = 6,
      ChannelVolumneMsb = 7,
      BalanceMsb = 8,
      PanMsb = 10,
      ExpressionControllerMsb = 11,
      EffectControl1Msb = 12,
      EffectControl2Msb = 13,
      GeneralPurposeController1Msb = 16,
      GeneralPurposeController2Msb = 17,
      GeneralPurposeController3Msb = 18,
      GeneralPurposeController4Msb = 19,

      BankSelectLsb = 32,
      ModulationWheelLsb = 33,
      BreathControllerLsb = 34,
      FootControllerLsb = 36,
      PortamentoTimeLsb = 37,
      DataEntryLsb = 38,
      ChannelVolumneLsb = 39,
      BalanceLsb = 40,
      PanLsb = 42,
      ExpressionControllerLsb = 43,
      EffectControl1Lsb = 44,
      EffectControl2Lsb = 45,
      GeneralPurposeController1Lsb = 48,
      GeneralPurposeController2Lsb = 49,
      GeneralPurposeController3Lsb = 50,
      GeneralPurposeController4Lsb = 51,

      DamperPedalSwitch = 64,
      PortamentoSwitch = 65,
      SostenutoSwitch = 66,
      SoftPedalSwitch = 67,
      LegatoSwitch = 68,
      Hold2Switch = 69,

      GeneralPurposeController5Lsb = 80,
      GeneralPurposeController6Lsb = 81,
      GeneralPurposeController7Lsb = 82,
      GeneralPurposeController8Lsb = 83,

      Effects1Depth = 91,
      Effects2Depth = 92,
      Effects3Depth = 93,
      Effects4Depth = 94,
      Effects5Depth = 95,

      NrpnLsb = 98,
      NrpnMsb = 99,
      RpnLsb = 100,
      RpnMsb = 101,

      AllSoundOff = 120,
      ResetAllControllers = 121,
      LocalControl = 122,
      AllNotesOff = 123,
      OmniModeOff = 124,
      OmniModeOn = 125,
      MonoModeOn = 126,
      PolyModeOn = 127
    }

    public enum ChannelVoiceMessage
    {
      NoteOff = 0x80,
      NoteOn = 0x90,
      PolyphonicKeyPressure = 0xA0,
      ControlChange = 0xB0,
      ProgramChange = 0xC0,
      ChannelPressure = 0xD0,
      PitchBend = 0xE0,
    }

    #region Channel Voice messages
    public static byte CreateStatusByte(ChannelVoiceMessage status, int channel)
      => (byte)((int)status | (channel >= 0 && channel <= 0xF ? channel : throw new System.ArgumentOutOfRangeException(nameof(channel))));

    public static byte[] NoteOff(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.NoteOff, channel), Ensure7BitValue(note), Ensure7BitValue(velocity) };
    public static byte[] NoteOn(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.NoteOn, channel), Ensure7BitValue(note), Ensure7BitValue(velocity) };
    public static byte[] PolyphonicKeyPressure(int channel, int note, int notePressure)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.PolyphonicKeyPressure, channel), Ensure7BitValue(note), Ensure7BitValue(notePressure) };
    public static byte[] ControlChange(int channel, int controllerType, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), Ensure7BitValue(controllerType), Ensure7BitValue(value) };
    public static byte[] ControlChange(int channel, ControllerChange controllerType, int value)
      => ControlChange(channel, (int)controllerType, value);
    public static byte[] ControlChange14(int channel, int controllerMsb, int controllerLsb, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), Ensure7BitValue(controllerMsb), Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), Ensure7BitValue(controllerLsb), Ensure14BitValueLow(value) };
    public static byte[] ControlChange14(int channel, ControllerChange controllerMsb, ControllerChange controllerLsb, int value)
      => ControlChange14(channel, (int)controllerMsb, (int)controllerLsb, value);
    public static byte[] ControlChangeNrpn(int channel, int parameter, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), (int)ControllerChange.NrpnMsb, Ensure14BitValueHigh(parameter), CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), (int)ControllerChange.NrpnLsb, Ensure14BitValueLow(parameter), CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), (int)ControllerChange.DataEntryMsb, Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceMessage.ControlChange, channel), (int)ControllerChange.DataEntryLsb, Ensure14BitValueLow(value) };
    public static byte[] ChannelPressure(int channel, int channelPressure)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.ChannelPressure, channel), Ensure7BitValue(channelPressure) };
    public static byte[] PitchBend(int channel, int pitchBend)
      => new byte[] { CreateStatusByte(ChannelVoiceMessage.ChannelPressure, channel), Ensure14BitValueLow(pitchBend), Ensure14BitValueHigh(pitchBend) };
    #endregion Channel Voice messages

    #region Channel Mode messages
    public static byte[] AllSoundOff(int channel)
      => ControlChange(channel, ControllerChange.AllNotesOff, 0);
    public static byte[] ResetAllControllers(int channel)
      => ControlChange(channel, ControllerChange.ResetAllControllers, 0);
    public static byte[] LocalControlOff(int channel)
      => ControlChange(channel, ControllerChange.LocalControl, 0);
    public static byte[] LocalControlOn(int channel)
      => ControlChange(channel, ControllerChange.LocalControl, 127);
    public static byte[] AllNotesOff(int channel)
      => ControlChange(channel, ControllerChange.AllNotesOff, 0);
    public static byte[] OmniModeOff(int channel)
      => ControlChange(channel, ControllerChange.OmniModeOff, 0);
    public static byte[] OmniModeOn(int channel)
      => ControlChange(channel, ControllerChange.OmniModeOn, 0);
    public static byte[] MonoModeOn(int channel, int numberOfChannels)
      => ControlChange(channel, ControllerChange.MonoModeOn, numberOfChannels);
    public static byte[] PolyModeOn(int channel)
      => ControlChange(channel, ControllerChange.PolyModeOn, 0);
    #endregion Channel Mode messages

    #region Common messages
    public enum MidiTimeCodeRate
    {
      TwentyFour = 24,
      TwentyFive = 25,
      TwentyNineNinetySeven = 29,
      Thirty = 30
    }
    public struct MidiTimeCode
    {
      private MidiTimeCodeRate m_rate;
      public MidiTimeCodeRate Rate
      {
        get => m_rate;
        set => m_rate = value;
      }

      private int m_hour;
      public int Hour
      {
        get => m_hour;
        set => m_hour = value >= 0 && value <= 23 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
      }

      private int m_minute;
      public int Minute
      {
        get => m_minute;
        set => m_minute = value >= 0 && value <= 59 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
      }

      private int m_second;
      public int Second
      {
        get => m_second;
        set => m_second = value >= 0 && value <= 59 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
      }

      private int m_frame;
      public int Frame
      {
        get => m_frame;
        set => m_frame = value >= 0 && value < (int)m_rate ? value : throw new System.ArgumentOutOfRangeException(nameof(value));
      }

      public MidiTimeCode(MidiTimeCodeRate rate, int hour, int minute, int second, int frame)
      {
        m_rate = rate;
        m_hour = hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
        m_minute = minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
        m_second = second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second));
        m_frame = frame >= 0 && frame < (int)rate ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame));
      }

      public MidiTimeCode AddHours(int value)
      {
        var quotient = System.Math.DivRem(m_hour + value, 24, out var reminder);

        if (quotient > 0) throw new System.ArithmeticException($"The maximum value for the hour part is 23.");

        return new MidiTimeCode(m_rate, reminder, m_minute, m_second, m_frame);
      }
      public MidiTimeCode AddMinutes(int value)
      {
        var quotient = System.Math.DivRem(m_minute + value, 60, out var reminder);

        return new MidiTimeCode(m_rate, m_hour + quotient, reminder, m_second, m_frame);
      }
      public MidiTimeCode AddSeconds(int value)
      {
        var quotient = System.Math.DivRem(m_second + value, 60, out var reminder);

        return new MidiTimeCode(m_rate, m_hour, m_minute + quotient, reminder, m_frame);
      }
      public MidiTimeCode AddFrames(int value)
      {
        var quotient = System.Math.DivRem(m_frame + value, (int)m_rate, out var reminder);

        return new MidiTimeCode(m_rate, m_hour, m_minute, m_second + quotient, reminder);
      }

      public byte[] ToFullFrameBytes()
        => MtcFullFrame((int)m_rate, m_hour, m_minute, m_second, m_frame);
      public byte[] ToQuarterFrameBytes(int piece)
        => piece switch
        {
          0 => MtcQuarterFrame(0x0, m_frame & 0xF),
          1 => MtcQuarterFrame(0x1, m_frame >> 4),
          2 => MtcQuarterFrame(0x2, m_second & 0xF),
          3 => MtcQuarterFrame(0x3, m_second >> 4),
          4 => MtcQuarterFrame(0x4, m_minute & 0xF),
          5 => MtcQuarterFrame(0x5, m_minute >> 4),
          6 => MtcQuarterFrame(0x6, m_hour & 0xF),
          7 => MtcQuarterFrame(0x7, (int)m_rate | m_hour >> 4),
          _ => throw new System.ArgumentOutOfRangeException(nameof(piece)),
        };
    }

    public enum SystemCommonMessage
    {
      /// <summary>This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</summary>
      StartOfExclusive = 0xF0,
      /// <summary></summary>
      MidiTimeCodeQuarterFrame = 0xF1,
      /// <summary></summary>
      SongPositionPointer = 0xF2,
      /// <summary></summary>
      SongSelect = 0xF3,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0xF4,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0xF5,
      /// <summary>Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</summary>
      TuneRequest = 0xF6,
      /// <summary>Used to terminate a System Exclusive dump (see above).</summary>
      EndOfExclusive = 0xF7
    }

    public static byte[] MtcFullFrame(int rate, int hour, int minute, int second, int frame)
      => new byte[] { (int)SystemCommonMessage.StartOfExclusive, 0x7F, 0x7F, 0x01, 0x01, (byte)((rate >= 0 && rate <= 3 ? rate << 5 : throw new System.ArgumentOutOfRangeException(nameof(rate))) | (hour >= 0 && hour <= 23 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour)))), (byte)(minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute))), (byte)(second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second))), (byte)(frame >= 0 && frame <= 29 ? frame : throw new System.ArgumentOutOfRangeException(nameof(frame))), (byte)SystemCommonMessage.EndOfExclusive };
    public static byte[] MtcQuarterFrame(int type, int value)
      => new byte[] { (byte)SystemCommonMessage.MidiTimeCodeQuarterFrame, (byte)((Ensure3BitValue(type) << 4) | Ensure4BitValue(value)) };
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
       => new byte[] { (byte)SystemCommonMessage.SongPositionPointer, Ensure14BitValueLow(songPositionInMidiBeats), Ensure14BitValueHigh(songPositionInMidiBeats) };
    public static byte[] SongSelect(int songNumber)
      => new byte[] { (byte)SystemCommonMessage.SongSelect, Ensure7BitValue(songNumber) };
    public static byte[] TuneRequest()
      => new byte[] { (byte)SystemCommonMessage.TuneRequest };
    #endregion Common messages

    #region Realtime messages
    public enum SystemRealtimeMessage
    {
      /// <summary>Sent 24 times per quarter note when synchronization is required.</summary>
      TimingClock = 0xF8,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0xF9,
      /// <summary>Start the current sequence playing. (This message will be followed with Timing Clocks).</summary>
      Start = 0xFA,
      /// <summary>Continue at the point the sequence was Stopped.</summary>
      Continue = 0xFB,
      /// <summary>Stop the current sequence.</summary>
      Stop = 0xFC,
      /// <summary>Undefined (reserved).</summary>
      //Undefined = 0xFD,
      /// <summary>This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off allChannels and return to normal (non- active sensing) operation.</summary>
      ActiveSensing = 0xFE,
      /// <summary>Reset all receivers in the system to power-up status. This should be used sparingly, preferably under manual control. In particular, it should not be sent on power-up.</summary>
      Reset = 0xFF
    }

    public static byte[] TimingClock()
      => new byte[] { (byte)SystemRealtimeMessage.TimingClock };
    public static byte[] Start()
      => new byte[] { (byte)SystemRealtimeMessage.Start };
    public static byte[] Continue()
      => new byte[] { (byte)SystemRealtimeMessage.Continue };
    public static byte[] Stop()
      => new byte[] { (byte)SystemRealtimeMessage.Stop };
    public static byte[] ActiveSensing()
      => new byte[] { (byte)SystemRealtimeMessage.ActiveSensing };
    public static byte[] Reset()
      => new byte[] { (byte)SystemRealtimeMessage.Reset };
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
