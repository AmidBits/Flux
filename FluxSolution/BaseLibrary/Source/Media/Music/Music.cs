//using System.Linq;

//namespace Flux.Media.Music
//{
//  public static class Note
//  {
//    public static readonly double ReferenceTuning = 440.0;

//    public static char SignFlat = 'b'; // True Unicode character '\u266D';
//    public static char SignSharp = '#'; // True Unicode character '\u266F';

//    public static System.Collections.Generic.IEnumerable<string> GetNames() => System.Linq.Enumerable.Empty<string>().Append(@"C", $"C{SignSharp}/D{SignFlat}", @"D", $"D{SignSharp}/E{SignFlat}", @"E", @"F", $"F{SignSharp}/G{SignFlat}", @"G", $"G{SignSharp}/A{SignFlat}", @"A", $"A{SignSharp}/B{SignFlat}", @"B");
//  }

//  public static class Interval
//  {
//    public static class TwelveToneEqualTemperament
//    {
//      public enum Name
//      {
//        Unison = 0,
//        MinorSecond = 1,
//        MajorSecond = 2,
//        MinorThird = 3,
//        MajorThird = 4,
//        PerfectFourth = 5,
//        Tritone = 6,
//        PerfectFifth = 7,
//        MinorSixth = 8,
//        MajorSixth = 9,
//        MinorSeventh = 10,
//        MajorSeventh = 11,
//        Octave = 12
//      }

//      public static System.Collections.Generic.IEnumerable<double> GetFrequencyRatios => System.Enum.GetValues(typeof(Name)).Cast<int>().Select(i => System.Math.Pow(2.0, i / 12.0));

//      public static System.Collections.Generic.IEnumerable<int> GetCents => System.Enum.GetValues(typeof(Name)).Cast<int>().Select(i => i * 100);
//    }

//    /// <summary></summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Cent_(music)"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
//    public static class Cent
//    {
//      public const double FrequencyRatio = 1.0005777895065548592967925757932;

//      /// <summary>Convert a specified interval ratio to cents.</summary>
//      public static int FromFrequencyRatio(double ratio) => (int)(System.Math.Log(ratio, 2.0) * 1200.0);

//      /// <summary>Convert a specified cents to an interval ratio.</summary>
//      public static double ToFrequencyRatio(int cents) => System.Math.Pow(2.0, cents / 1200.0);
//    }

//    /// <summary></summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Semitone"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Interval_(music)"/>
//    public static class Semitone
//    {
//      public const double FrequencyRatio = 1.0594630943592952645618252949463;

//      /// <summary>Convert a specified interval ratio to cents.</summary>
//      public static int FromFrequencyRatio(double ratio) => (int)(System.Math.Log(ratio, 2.0) * 12.0); //Cent.FromFrequencyRatio(ratio) / 100;

//      /// <summary>Convert a specified semitone to an interval ratio.</summary>
//      public static double ToFrequencyRatio(int semitones) => System.Math.Pow(2.0, semitones / 12.0); //Cent.ToFrequencyRatio(semitones * 100);
//    }
//  }

//  /// <summary></summary>
//  /// <seealso cref="http://www.angelfire.com/in2/yala/t4scales.htm"/>
//  public static class Scale
//  {
//    public static readonly int[] Chromatic = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
//    public static readonly int[] Major = new int[] { 0, 2, 4, 5, 7, 9, 11 };
//    public static readonly int[] MinorNatural = new int[] { 0, 2, 3, 5, 7, 8, 10 };
//    public static readonly int[] HarmonicMinor = new int[] { 0, 2, 3, 5, 7, 8, 11 };
//  }
//}
