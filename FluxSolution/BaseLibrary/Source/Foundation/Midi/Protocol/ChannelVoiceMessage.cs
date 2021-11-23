namespace Flux.Midi.Protocol
{
  public sealed class ChannelVoiceMessage
  {
    public static byte CreateStatusByte(ChannelVoiceStatus status, int channel)
      => (byte)((int)status | (channel >= 0 && channel <= 0xF ? channel : throw new System.ArgumentOutOfRangeException(nameof(channel))));

    public static byte[] NoteOff(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOff, channel), Helper.Ensure7BitValue(note), Helper.Ensure7BitValue(velocity) };
    public static byte[] NoteOn(int channel, int note, int velocity)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.NoteOn, channel), Helper.Ensure7BitValue(note), Helper.Ensure7BitValue(velocity) };
    public static byte[] PolyphonicKeyPressure(int channel, int note, int notePressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.PolyphonicKeyPressure, channel), Helper.Ensure7BitValue(note), Helper.Ensure7BitValue(notePressure) };
    public static byte[] ControlChange(int channel, int controllerType, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Helper.Ensure7BitValue(controllerType), Helper.Ensure7BitValue(value) };
    public static byte[] ControlChange(int channel, ControllerChangeNumber controllerType, int value)
      => ControlChange(channel, (int)controllerType, value);
    public static byte[] ControlChange14(int channel, int controllerMsb, int controllerLsb, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Helper.Ensure7BitValue(controllerMsb), Helper.Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), Helper.Ensure7BitValue(controllerLsb), Helper.Ensure14BitValueLow(value) };
    public static byte[] ControlChange14(int channel, ControllerChangeNumber controllerMsb, ControllerChangeNumber controllerLsb, int value)
      => ControlChange14(channel, (int)controllerMsb, (int)controllerLsb, value);
    public static byte[] ControlChangeNrpn(int channel, int parameter, int value)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.NrpnMsb, Helper.Ensure14BitValueHigh(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.NrpnLsb, Helper.Ensure14BitValueLow(parameter), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.DataEntryMsb, Helper.Ensure14BitValueHigh(value), CreateStatusByte(ChannelVoiceStatus.ControlChange, channel), (int)ControllerChangeNumber.DataEntryLsb, Helper.Ensure14BitValueLow(value) };
    public static byte[] ChannelPressure(int channel, int channelPressure)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Helper.Ensure7BitValue(channelPressure) };
    public static byte[] PitchBend(int channel, int pitchBend)
      => new byte[] { CreateStatusByte(ChannelVoiceStatus.ChannelPressure, channel), Helper.Ensure14BitValueLow(pitchBend), Helper.Ensure14BitValueHigh(pitchBend) };
  }
}
