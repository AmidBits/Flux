using System.Linq;

namespace Flux.Media.Midi
{
  /// <summary>This note library enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public static class Note
  {
    public const double ReferenceFrequency = 440.0;
    public const byte ReferenceNote = 69;

    /// <summary>Determines the MIDI note from the specified frequency. An exception is thrown if the frequency is out of range.</summary>
    public static byte FromFrequency(double frequency) => (byte)((int)(ReferenceNote + (System.Math.Log(frequency / ReferenceFrequency, 2.0) * 12.0)) is var midiNote && midiNote >= 0 && midiNote <= 127 ? midiNote : throw new System.ArgumentOutOfRangeException(nameof(frequency), @"The frequency cannot be translated into a MIDI note number."));
    /// <summary>Determines the MIDI note from the specified frequency, using the try paradigm.</summary>
    public static bool TryFromFrequency(double frequency, out byte result)
    {
      try
      {
        result = FromFrequency(frequency);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }

    /// <summary>Determines the name of the specified MIDI note.</summary>
    public static string GetName(byte note) => Music.Note.GetNames().ElementAt(note % 12);
    /// <summary>Determines the octave of the specified MIDI note.</summary>
    public static int GetOctave(byte note) => ((note / 12) - 1);

    /// <summary>Parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static byte Parse(string scientificPitchNotation)
    {
      var m = System.Text.RegularExpressions.Regex.Match(scientificPitchNotation, @"^([^0-9\-]+)([\-0-9]+)$");

      if (m.Success && m.Groups is var gc && gc.Count >= 3 && gc[1].Success && gc[2].Success)
      {
        var octave = int.Parse(gc[2].Value, System.Globalization.CultureInfo.CurrentCulture);
        var offset = System.Array.FindIndex(Music.Note.GetNames().ToArray(), 0, n => n.StartsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase) || n.EndsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase));

        if (octave < -1 && octave > 9 && offset == -1)
        {
          throw new System.ArgumentException($"Invalid note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
        }

        return (byte)((octave + 1) * 12 + offset);
      }

      throw new System.ArgumentException($"Cannot parse note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
    }

    /// <summary>Determines the frequency of the specified MIDI note.</summary>
    public static double ToFrequency(byte note) => (ReferenceFrequency * System.Math.Pow(2, (double)(note - ReferenceNote) / 12.0));

    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static string ToString(byte note) => $"{GetName(note)}{GetOctave(note)}";

    /// <summary>Attempts to parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static bool TryParse(string scientificPitchNotation, out byte result)
    {
      try
      {
        result = Parse(scientificPitchNotation);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }
  }
}
