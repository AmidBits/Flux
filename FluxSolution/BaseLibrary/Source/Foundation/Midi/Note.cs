using System.Linq;

namespace Flux.Midi
{
  /// <summary>A MIDI note is an integer value in the range [1, 127]. It enables conversions to and from MIDI note numbers and other relative data points, e.g. pitch notations and frequencies.</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/MIDI_tuning_standard"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
  public struct Note
    : System.IComparable<Note>, System.IEquatable<Note>
  {
    public const byte ReferenceA4 = 69;

    private int m_number;

    public Note(int midiNoteNumber)
      => m_number = IsMidiNote(midiNoteNumber) ? midiNoteNumber : throw new System.ArgumentOutOfRangeException(nameof(midiNoteNumber));

    public int Number
      => m_number;

    /// <summary>Determines the name of the specified MIDI note.</summary>
    public string Name()
      => Music.Note.Names.ElementAt(m_number % 12);
    /// <summary>Determines the octave of the specified MIDI note.</summary>
    public int Octave()
      => (m_number / 12) - 1;

    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public Units.Frequency ToFrequency()
      => new Units.Frequency(ConvertNoteToFrequency(m_number));

    #region Static methods
    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public static double ConvertNoteToFrequency(int midiNote)
      => midiNote >= 0 && midiNote <= 127 ? Units.Frequency.Reference440 * System.Math.Pow(2, (midiNote - ReferenceA4) / 12.0) : throw new System.ArgumentOutOfRangeException(nameof(midiNote));
    /// <summary>Convert the specified MIDI note to the corresponding frequency.</summary>
    public static int ConvertFrequencyToNote(double frequency)
      => (int)(ReferenceA4 + (System.Math.Log(frequency / Units.Frequency.Reference440, 2.0) * 12.0)) is var midiNote && midiNote >= 0 && midiNote <= 127 ? midiNote : throw new System.ArgumentOutOfRangeException(nameof(frequency));
    /// <summary>Determines the MIDI note from the specified frequency. An exception is thrown if the frequency is out of range.</summary>
    public static Note FromFrequency(Units.Frequency frequency)
      => new Note(ConvertFrequencyToNote(frequency.Hertz));
    public static bool IsMidiNote(int number)
      => number >= 0 && number <= 127;
    /// <summary>Parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static Note Parse(string scientificPitchNotation)
    {
      var m = System.Text.RegularExpressions.Regex.Match(scientificPitchNotation, @"^([^0-9\-]+)([\-0-9]+)$");

      if (m.Success && m.Groups is var gc && gc.Count >= 3 && gc[1].Success && gc[2].Success)
      {
        var octave = int.Parse(gc[2].Value, System.Globalization.CultureInfo.CurrentCulture);
        var offset = System.Array.FindIndex(Music.Note.Names.ToArray(), 0, n => n.StartsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase) || n.EndsWith(gc[1].Value, System.StringComparison.OrdinalIgnoreCase));

        if (octave < -1 && octave > 9 && offset == -1)
          throw new System.ArgumentException($"Invalid note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));

        return new Note((byte)((octave + 1) * 12 + offset));
      }

      throw new System.ArgumentException($"Cannot parse note and octave '{scientificPitchNotation}' string.", nameof(scientificPitchNotation));
    }
    /// <summary>Determines the MIDI note from the specified frequency, using the try paradigm.</summary>
    public static bool TryFromFrequency(Units.Frequency frequency, out Note result)
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
    /// <summary>Attempts to parse the specified SPN string into a MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public static bool TryParse(string scientificPitchNotation, out Note result)
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
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(Note a, Note b)
     => a.CompareTo(b) < 0;
    public static bool operator <=(Note a, Note b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Note a, Note b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Note a, Note b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Note a, Note b)
      => a.Equals(b);
    public static bool operator !=(Note a, Note b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Note other)
      => m_number.CompareTo(other.m_number);

    // IEquatable
    public bool Equals(Note other)
      => m_number == other.m_number;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Note o && Equals(o);
    public override int GetHashCode()
      => m_number.GetHashCode();
    /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
    public override string ToString()
      => $"{GetType().Name}: {Name()}{Octave()} (#{m_number})";
    #endregion Object overrides
  }
}