using System.Linq;

namespace Flux.Media.Midi
{
  /// <summary>A MIDI note is an integer value in the range [1, 127]. It enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public struct MidiNote
  {
    public const double ReferenceFrequency = 440;
    public const byte ReferenceNote = 69;

    private int m_number;
    public int Number => m_number;

    public MidiNote(byte noteNumber)
      => m_number = noteNumber >= 0 && noteNumber <= 127 ? noteNumber : throw new System.ArgumentOutOfRangeException(nameof(noteNumber));

    public Frequency ToFrequency()
      => new Frequency(ConvertToFrequency(m_number));

    #region Statics
    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public static double ConvertToFrequency(int midiNote)
      => midiNote >= 0 && midiNote <= 127 ? ReferenceFrequency * System.Math.Pow(2, (midiNote - ReferenceNote) / 12.0) : throw new System.ArgumentOutOfRangeException(nameof(midiNote));

    /// <summary>Determines the MIDI note from the specified frequency. An exception is thrown if the frequency is out of range.</summary>
    public static MidiNote FromFrequency(double frequency)
      => (int)(ReferenceNote + (System.Math.Log(frequency / ReferenceFrequency, 2.0) * 12.0)) is var midiNote && midiNote >= 0 && midiNote <= 127 ? new MidiNote((byte)midiNote) : throw new System.ArgumentOutOfRangeException(nameof(frequency), @"The frequency cannot be translated into a MIDI note number.");
    /// <summary>Determines the MIDI note from the specified frequency, using the try paradigm.</summary>
    public static bool TryFromFrequency(double frequency, out MidiNote result)
    {
      try
      {
        result = FromFrequency(frequency);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Determines the name of the specified MIDI note.</summary>
    public static string GetName(int midiNote)
      => Music.Note.Names.ElementAt(midiNote % 12);
    /// <summary>Determines the octave of the specified MIDI note.</summary>
    public static int GetOctave(int midiNote)
      => (midiNote / 12) - 1;

    /// <summary>Parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static MidiNote Parse(string scientificPitchNotation)
    {
      var m = System.Text.RegularExpressions.Regex.Match(scientificPitchNotation, @"^([^0-9\-]+)([\-0-9]+)$");

      if (m.Success && m.Groups is var gc && gc.Count >= 3 && gc[1].Success && gc[2].Success)
      {
        var octave = int.Parse(gc[2].Value, System.Globalization.CultureInfo.CurrentCulture);
        var offset = System.Array.FindIndex(Music.Note.Names.ToArray(), 0, n => n.StartsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase) || n.EndsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase));

        if (octave < -1 && octave > 9 && offset == -1)
          throw new System.ArgumentException($"Invalid note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));

        return new MidiNote((byte)((octave + 1) * 12 + offset));
      }

      throw new System.ArgumentException($"Cannot parse note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
    }
    /// <summary>Attempts to parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static bool TryParse(string scientificPitchNotation, out MidiNote result)
    {
      try
      {
        result = Parse(scientificPitchNotation);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
    #endregion Statics

    // IEquatable
    public bool Equals(MidiNote other)
      => m_number == other.m_number;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new Formatters.AngleFormatter(), format ?? $"<{nameof(MidiNote)}: {{0:D3}}>", this);
    // Overrides
    public override bool Equals(object? obj)
      => obj is MidiNote o && Equals(o);
    public override int GetHashCode()
      => m_number.GetHashCode();
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public override string ToString()
      => $"{GetName(m_number)}{GetOctave(m_number)}";
  }
}
