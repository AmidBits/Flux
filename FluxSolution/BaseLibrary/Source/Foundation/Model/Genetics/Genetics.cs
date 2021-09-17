using System.Diagnostics.CodeAnalysis;

namespace Flux.Genetics
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Phosphate"/>
  [System.Flags]
  public enum Phosphates
  {
    None = 0,
    Diphosphate,
    Monophosphate,
    Triphosphate
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Phosphate"/>
  [System.Flags]
  public enum FiveCarbonSugars
  {
    None = 0,
    Deoxyribose,
    Ribose,
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Phosphate"/>
  [System.Flags]
  public enum Nucleobases
  {
    None = 0,
    Adenine = 1, // DNA, RNA
    Cytosine = 2, // DNA, RNA
    Guanine = 4, // DNA, RNA
    Thymine = 8, // DNA
    Uracil = 16, // RNA
  }


  public enum NucleicAcid
  {
    DeoxyriboNucleicAcid = Nucleobases.Adenine | Nucleobases.Cytosine | Nucleobases.Guanine | Nucleobases.Thymine,
    RiboNucleicAcid = Nucleobases.Adenine | Nucleobases.Cytosine | Nucleobases.Guanine | Nucleobases.Uracil,
    NucleicAcidAnalogue = 0
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Nucleoside"/>
  public struct Nucleoside
    : System.IEquatable<Nucleoside>
  {
    public static readonly Nucleoside Empty;
    public bool IsEmpty => Equals(Empty);

    public Nucleobases Nucleobase { get; set; }
    public FiveCarbonSugars FiveCarbonSugar { get; set; }

    // Operators
    public static bool operator ==(Nucleoside a, Nucleoside b)
      => a.Equals(b);
    public static bool operator !=(Nucleoside a, Nucleoside b)
      => !a.Equals(b);
    // Equatable<Nucleoside>
    public bool Equals([AllowNull] Nucleoside other)
      => Nucleobase == other.Nucleobase && FiveCarbonSugar == other.FiveCarbonSugar;
    // Overrides
    public override bool Equals(object? obj)
      => obj is Nucleoside o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Nucleobase, FiveCarbonSugar);
    public override string ToString()
      => $"<{Nucleobase.ToString()}, {FiveCarbonSugar.ToString()}>";
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Nucleotide"/>
  public struct Nucleotide
    : System.IEquatable<Nucleotide>, System.IEquatable<Nucleoside>
  {
    public static readonly Nucleotide Empty;
    public bool IsEmpty => Nucleobase == default && FiveCarbonSugar == default && Phosphate == default;

    public Nucleobases Nucleobase { get; set; }
    public FiveCarbonSugars FiveCarbonSugar { get; set; }
    public Phosphates Phosphate { get; set; }

    public Nucleotide(Nucleobases nucleobase, FiveCarbonSugars fiveCarbonSugar, Phosphates phosphate)
    {
      Nucleobase = nucleobase;
      FiveCarbonSugar = fiveCarbonSugar;
      Phosphate = phosphate;
    }
    public Nucleotide(Nucleoside nucleoside, Phosphates phosphate)
    {
      Nucleobase = nucleoside.Nucleobase;
      FiveCarbonSugar = nucleoside.FiveCarbonSugar;
      Phosphate = phosphate;
    }

    // Operators
    public static bool operator ==(Nucleotide a, Nucleotide b)
      => a.Equals(b);
    public static bool operator !=(Nucleotide a, Nucleotide b)
      => !a.Equals(b);
    // Equatable<Nucleotide>
    public bool Equals([AllowNull] Nucleotide other)
      => Nucleobase == other.Nucleobase && FiveCarbonSugar == other.FiveCarbonSugar && Phosphate == other.Phosphate;
    // Equatable<Nucleoside>
    public bool Equals([AllowNull] Nucleoside other)
      => Nucleobase == other.Nucleobase && FiveCarbonSugar == other.FiveCarbonSugar;
    // Overrides
    public override bool Equals(object? obj)
      => obj is Nucleotide o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Nucleobase, FiveCarbonSugar, Phosphate);
    public override string ToString()
      => $"<{Nucleobase.ToString()}, {FiveCarbonSugar.ToString()}, {Phosphate.ToString()}>";
  }

  public static class Tools
  {
  }
}
