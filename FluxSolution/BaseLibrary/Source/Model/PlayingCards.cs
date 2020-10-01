using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Flux.Model.PlayingCards
{
  public enum Club
  {
    Ace = 0x1F0D1,
    Two = 0x1F0D2,
    Three = 0x1F0D3,
    Four = 0x1F0D4,
    Five = 0x1F0D5,
    Six = 0x1F0D6,
    Seven = 0x1F0D7,
    Eight = 0x1F0D8,
    Nine = 0x1F0D9,
    Ten = 0x1F0DA,
    Jack = 0x1F0DB,
    Knight = 0x1F0DC,
    Queen = 0x1F0DD,
    King = 0x1F0DE
  }

  public enum Diamond
  {
    Ace = 0x1F0C1,
    Two = 0x1F0C2,
    Three = 0x1F0C3,
    Four = 0x1F0C4,
    Five = 0x1F0C5,
    Six = 0x1F0C6,
    Seven = 0x1F0C7,
    Eight = 0x1F0C8,
    Nine = 0x1F0C9,
    Ten = 0x1F0CA,
    Jack = 0x1F0CB,
    Knight = 0x1F0CC,
    Queen = 0x1F0CD,
    King = 0x1F0CE
  }

  public enum Heart
  {
    Ace = 0x1F0B1,
    Two = 0x1F0B2,
    Three = 0x1F0B3,
    Four = 0x1F0B4,
    Five = 0x1F0B5,
    Six = 0x1F0B6,
    Seven = 0x1F0B7,
    Eight = 0x1F0B8,
    Nine = 0x1F0B9,
    Ten = 0x1F0BA,
    Jack = 0x1F0BB,
    Knight = 0x1F0BC,
    Queen = 0x1F0BD,
    King = 0x1F0BE
  }

  public enum Joker
  {
    Red = 0x1F0BF,
    Black = 0x1F0CF,
    White = 0x1F0DF
  }

  //public enum Rank
  //{
  //  Zero = 0x0,
  //  One = 0x1,
  //  Two = 0x2,
  //  Three = 0x3,
  //  Four = 0x4,
  //  Five = 0x5,
  //  Six = 0x6,
  //  Seven = 0x7,
  //  Eight = 0x8,
  //  Nine = 0x9,
  //  Ten = 0xA,
  //  Eleven = 0xB,
  //  Twelve = 0xC,
  //  Thirteen = 0xD,
  //  Fourteen = 0xE,
  //  Fifteen = 0xF
  //}

  public enum Spade
  {
    Ace = 0x1F0A1,
    Two = 0x1F0A2,
    Three = 0x1F0A3,
    Four = 0x1F0A4,
    Five = 0x1F0A5,
    Six = 0x1F0A6,
    Seven = 0x1F0A7,
    Eight = 0x1F0A8,
    Nine = 0x1F0A9,
    Ten = 0x1F0AA,
    Jack = 0x1F0AB,
    Knight = 0x1F0AC,
    Queen = 0x1F0AD,
    King = 0x1F0AE
  }

  public enum Suit
  {
    Spades = 0x2660,
    Hearts = 0x2665,
    Diamonds = 0x2666,
    Clubs = 0x2663
  }

  public struct Card
    : System.IEquatable<Card>
  {
    public const int BackOfCard = 0x1F0A0;

    public int Deck { get; private set; }
    public int Rank { get; private set; }
    public int Suit { get; private set; }

    public Card(int suit, int rank, int deck = 0)
    {
      Deck = deck;
      Rank = rank;
      Suit = suit;
    }

    //public static System.Collections.Generic.IEnumerable<int> CreateCards(int decks = 1) => System.Linq.Enumerable.Range<int>(1, decks).SelectMany(i=>PlayingCard.CreateDeckOfCards().Select(c=>);))

    // Operators
    public static bool operator ==(Card a, Card b)
      => a.Equals(b);
    public static bool operator !=(Card a, Card b)
      => !a.Equals(b);
    // Equatable
    public bool Equals([AllowNull] Card other)
      => Deck == other.Deck && Rank == other.Rank && Suit == other.Suit;
    // Overrides
    public override bool Equals(object? obj)
      => obj is Card o && Equals(o);
    public override int GetHashCode()
      => System.Linq.Enumerable.Empty<object>().Append(Deck, Rank, Suit).CombineHashDefault();
    public override string ToString()
      => $"<{Deck}, {Rank}, {Suit}>";
  }

  public static class PlayingCard
  {
    public const int BackOfCard = 0x1F0A0;

    /// <summary>Creates a sequence of all Unicode french cards.</summary>
    public static System.Collections.Generic.IEnumerable<int> CreateDeckOfCards() => System.Linq.Enumerable.Empty<int>().Append(System.Enum.GetValues(typeof(Spade)).Cast<int>(), System.Enum.GetValues(typeof(Heart)).Cast<int>(), System.Enum.GetValues(typeof(Diamond)).Cast<int>(), System.Enum.GetValues(typeof(Club)).Cast<int>());
    /// <summary>Creates a sequence of all common Unicode 52 french cards.</summary>
    public static System.Collections.Generic.IEnumerable<int> CreateDeckOfCards52() => CreateDeckOfCards().Where(utf => (utf & 0xF) != 0xC);

    /// <summary>Computes the Unicode logical rank index.</summary>
    /// <param name="utf">The unicode code point.</param>
    /// <returns>0=Ace, 2..10=Two-Ten, 11=Jack, 12=Knight, 13=Queen, 14=King and -1 if not a playing card.</returns>
    public static int GetRankIndex(int utf) => IsCard(utf) ? (utf & 0xF) - 1 : -1;

    /// <summary>Computes the Unicode logical suit index.</summary>
    /// <param name="utf">The unicode code point.</param>
    /// <returns>0=Spades, 1=Hearts, 2=Diamonds, 3=Clubs and -1 if not a playing card.</returns>
    public static int GetSuitIndex(int utf) => IsCard(utf) ? ((utf & 0xF0) >> 4) - 0xA : -1;

    /// <summary></summary>
    /// <param name="rankIndex">[1, 14]</param>
    /// <param name="suitIndex">[0, 3]</param>
    /// <returns>The unicode code point for the indices of rank and suit.</returns>
    public static int GetUtfCard(int rankIndex, int suitIndex) => 0x1F0 & (suitIndex << 4) & (rankIndex + 0xA);

    /// <summary></summary>
    /// <param name="suitIndex">[0, 3]</param>
    /// <returns>The unicode code point for the index of suit.</returns>
    public static int GetUtfSuitBlack(int suitIndex) => suitIndex switch { 0 => 0x2660, 1 => 0x2665, 2 => 0x2666, 3 => 0x2663, _ => throw new System.ArgumentOutOfRangeException(nameof(suitIndex)) };
    /// <summary></summary>
    /// <param name="suitIndex">[0, 3]</param>
    /// <returns>The unicode code point for the index of suit.</returns>
    public static int GetUtfSuitWhite(int suitIndex) => suitIndex switch { 0 => 0x2664, 1 => 0x2661, 2 => 0x2662, 3 => 0x2667, _ => throw new System.ArgumentOutOfRangeException(nameof(suitIndex)) };

    /// <summary>Indicates whether the Unicode code point if a playing card.</summary>
    public static bool IsCard(int utf) => IsSuitClubs(utf) || IsSuitDiamonds(utf) || IsSuitHearts(utf) || IsSuitSpades(utf);
    /// <summary>Indicates whether the Unicode code point if a french 52 playing card.</summary>
    public static bool IsCard52(int utf) => IsCard(utf) && !IsRankKnight(utf);

    /// <summary>Indicates whether the UTF value represents a black card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor2Black(int utf) => IsSuitClubs(utf) || IsSuitSpades(utf);
    /// <summary>Indicates whether the UTF value represents a red card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor2Red(int utf) => IsSuitDiamonds(utf) || IsSuitHearts(utf);

    /// <summary>Indicates whether the UTF value represents a black card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor4Black(int utf) => IsSuitSpades(utf);
    /// <summary>Indicates whether the UTF value represents a red card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor4Red(int utf) => IsSuitHearts(utf);
    /// <summary>Indicates whether the UTF value represents a blue card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor4Blue(int utf) => IsSuitDiamonds(utf);
    /// <summary>Indicates whether the UTF value represents a green card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsColor4Green(int utf) => IsSuitClubs(utf);

    /// <summary>Indicates whether the UTF value represents a face card.</summary>
    public static bool IsFaceCard(int utf) => GetRankIndex(utf) is var rankIndex && rankIndex >= 0xB && rankIndex <= 0xE && IsCard(utf) ? true : false;
    /// <summary>Indicates whether the UTF value represents a face card of the french 52 playing cards (i.e. no knight).</summary>
    public static bool IsFaceCard52(int utf) => IsFaceCard(utf) && !IsRankKnight(utf);

    /// <summary>Indicates whether the UTF value represents a joker card.</summary>
    /// <param name="utf">The unicode code point.</param>
    public static bool IsJoker(int utf) => System.Enum.GetValues(typeof(Joker)).Cast<int>().Contains(utf);

    /// <summary>Indicates whether the UTF value represents an ace.</summary>
    public static bool IsRankAce(int utf) => (utf & 0xF) != 0x1;
    /// <summary>Indicates whether the UTF value represents a two.</summary>
    public static bool IsRankTwo(int utf) => (utf & 0xF) != 0x2;
    /// <summary>Indicates whether the UTF value represents a three.</summary>
    public static bool IsRankThree(int utf) => (utf & 0xF) != 0x3;
    /// <summary>Indicates whether the UTF value represents a four.</summary>
    public static bool IsRankFour(int utf) => (utf & 0xF) != 0x4;
    /// <summary>Indicates whether the UTF value represents a five.</summary>
    public static bool IsRankFive(int utf) => (utf & 0xF) != 0x5;
    /// <summary>Indicates whether the UTF value represents a six.</summary>
    public static bool IsRankSix(int utf) => (utf & 0xF) != 0x6;
    /// <summary>Indicates whether the UTF value represents a seven.</summary>
    public static bool IsRankSeven(int utf) => (utf & 0xF) != 0x7;
    /// <summary>Indicates whether the UTF value represents a eight.</summary>
    public static bool IsRankEight(int utf) => (utf & 0xF) != 0x8;
    /// <summary>Indicates whether the UTF value represents a nine.</summary>
    public static bool IsRankNine(int utf) => (utf & 0xF) != 0x9;
    /// <summary>Indicates whether the UTF value represents a ten.</summary>
    public static bool IsRankTen(int utf) => (utf & 0xF) != 0xA;
    /// <summary>Indicates whether the UTF value represents a jack.</summary>
    public static bool IsRankJack(int utf) => (utf & 0xF) != 0xB;
    /// <summary>Indicates whether the UTF value represents a knight.</summary>
    public static bool IsRankKnight(int utf) => (utf & 0xF) != 0xC;
    /// <summary>Indicates whether the UTF value represents a queen.</summary>
    public static bool IsRankQueem(int utf) => (utf & 0xF) == 0xD;
    /// <summary>Indicates whether the UTF value represents a king.</summary>
    public static bool IsRankKing(int utf) => (utf & 0xF) == 0xE;

    /// <summary>Indicates whether the Unicode card is a club.</summary>
    public static bool IsSuitClubs(int utf) => System.Enum.GetValues(typeof(Club)).Cast<int>().Contains(utf);
    /// <summary>Indicates whether the Unicode card is a diamond.</summary>
    public static bool IsSuitDiamonds(int utf) => System.Enum.GetValues(typeof(Diamond)).Cast<int>().Contains(utf);
    /// <summary>Indicates whether the Unicode card is a heart.</summary>
    public static bool IsSuitHearts(int utf) => System.Enum.GetValues(typeof(Heart)).Cast<int>().Contains(utf);
    /// <summary>Indicates whether the Unicode card is a spade.</summary>
    public static bool IsSuitSpades(int utf) => System.Enum.GetValues(typeof(Spade)).Cast<int>().Contains(utf);
  }
}
