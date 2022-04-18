namespace Flux.Music.Midi.Protocol
{
  public enum ChannelVoiceStatus
  {
    NoteOff = 0x80,
    NoteOn = 0x90,
    PolyphonicKeyPressure = 0xA0,
    ControlChange = 0xB0,
    ProgramChange = 0xC0,
    ChannelPressure = 0xD0,
    PitchBend = 0xE0,
  }

  public sealed class ChannelVoice
  {
    public static byte CreateStatusByte(ChannelVoiceStatus status, int channel)
      => (byte)((int)status | Utility.Ensure4BitByte(channel, nameof(channel)));

    public static byte[] NoteOff(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOff, channel), Utility.Ensure7BitByte(note), Utility.Ensure7BitByte(velocity) };
    public static byte[] NoteOn(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOn, channel), Utility.Ensure7BitByte(note), Utility.Ensure7BitByte(velocity) };
    public static byte[] PolyphonicKeyPressure(int channel, int note, int notePressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.PolyphonicKeyPressure, channel), Utility.Ensure7BitByte(note), Utility.Ensure7BitByte(notePressure) };
    public static byte[] ControlChange(int channel, int controllerType, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Utility.Ensure7BitByte(controllerType), Utility.Ensure7BitByte(value) };
    public static byte[] ControlChange(int channel, ControlChangeController controllerType, int value)
      => ControlChange(channel, (int)controllerType, value);
    public static byte[] ControlChange14(int channel, int controllerMsb, int controllerLsb, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Utility.Ensure7BitByte(controllerMsb), Utility.Ensure14BitHiByte(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Utility.Ensure7BitByte(controllerLsb), Utility.Ensure14BitLoByte(value) };
    public static byte[] ControlChange14(int channel, ControlChangeController controllerMsb, ControlChangeController controllerLsb, int value)
      => ControlChange14(channel, (int)controllerMsb, (int)controllerLsb, value);
    public static byte[] ControlChangeNrpn(int channel, int parameter, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControlChangeController.NrpnMsb, Utility.Ensure14BitHiByte(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControlChangeController.NrpnLsb, Utility.Ensure14BitLoByte(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControlChangeController.DataEntryMsb, Utility.Ensure14BitHiByte(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControlChangeController.DataEntryLsb, Utility.Ensure14BitLoByte(value) };
    public static byte[] ChannelPressure(int channel, int channelPressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Utility.Ensure7BitByte(channelPressure) };
    public static byte[] PitchBend(int channel, int pitchBend)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Utility.Ensure14BitLoByte(pitchBend), Utility.Ensure14BitHiByte(pitchBend) };
  }
}
