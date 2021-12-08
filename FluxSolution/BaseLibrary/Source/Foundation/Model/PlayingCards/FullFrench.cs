using System.Linq;

namespace Flux.Model.PlayingCards.FullFrench
{
	//public enum CardEnum
	//{
	//	AceOfSpades = 0x1F0A1,
	//	TwoOfSpades = 0x1F0A2,
	//	ThreeOfSpades = 0x1F0A3,
	//	FourOfSpades = 0x1F0A4,
	//	FiveOfSpades = 0x1F0A5,
	//	SixOfSpades = 0x1F0A6,
	//	SevenOfSpades = 0x1F0A7,
	//	EightOfSpades = 0x1F0A8,
	//	NineOfSpades = 0x1F0A9,
	//	TenOfSpades = 0x1F0AA,
	//	JackOfSpades = 0x1F0AB,
	//	KnightOfSpades = 0x1F0AC,
	//	QueenOfSpades = 0x1F0AD,
	//	KingOfSpades = 0x1F0AE,

	//	AceOfHearts = 0x1F0B1,
	//	TwoOfHearts = 0x1F0B2,
	//	ThreeOfHearts = 0x1F0B3,
	//	FourOfHearts = 0x1F0B4,
	//	FiveOfHearts = 0x1F0B5,
	//	SixOfHearts = 0x1F0B6,
	//	SevenOfHearts = 0x1F0B7,
	//	EightOfHearts = 0x1F0B8,
	//	NineOfHearts = 0x1F0B9,
	//	TenOfHearts = 0x1F0BA,
	//	JackOfHearts = 0x1F0BB,
	//	KnightOfHearts = 0x1F0BC,
	//	QueenOfHearts = 0x1F0BD,
	//	KingOfHearts = 0x1F0BE,

	//	AceOfDiamonds = 0x1F0C1,
	//	TwoOfDiamonds = 0x1F0C2,
	//	ThreeOfDiamonds = 0x1F0C3,
	//	FourOfDiamonds = 0x1F0C4,
	//	FiveOfDiamonds = 0x1F0C5,
	//	SixOfDiamonds = 0x1F0C6,
	//	SevenOfDiamonds = 0x1F0C7,
	//	EightOfDiamonds = 0x1F0C8,
	//	NineOfDiamonds = 0x1F0C9,
	//	TenOfDiamonds = 0x1F0CA,
	//	JackOfDiamonds = 0x1F0CB,
	//	KnightOfDiamonds = 0x1F0CC,
	//	QueenOfDiamonds = 0x1F0CD,
	//	KingOfDiamonds = 0x1F0CE,

	//	AceOfClubs = 0x1F0D1,
	//	TwoOfClubs = 0x1F0D2,
	//	ThreeOfClubs = 0x1F0D3,
	//	FourOfClubs = 0x1F0D4,
	//	FiveOfClubs = 0x1F0D5,
	//	SixOfClubs = 0x1F0D6,
	//	SevenOfClubs = 0x1F0D7,
	//	EightOfClubs = 0x1F0D8,
	//	NineOfClubs = 0x1F0D9,
	//	TenOfClubs = 0x1F0DA,
	//	JackOfClubs = 0x1F0DB,
	//	KnightOfClubs = 0x1F0DC,
	//	QueenOfClubs = 0x1F0DD,
	//	KingOfClubs = 0x1F0DE,

	//	RedJoker = 0x1F0BF,
	//	BlackJoker = 0x1F0CF,
	//	WhiteJoker = 0x1F0DF
	//}

	public enum Rank
	{
		Unranked = -1,
		Ace = 1,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Knight,
		Queen,
		King
	}

	public enum Suit
	{
		Unsuited = -1,
		Spades = 0x2660,
		Clubs = 0x2663,
		Hearts = 0x2665,
		Diamonds = 0x2666,
	}

	public struct Card
		: System.IEquatable<Card>
	{
		public static readonly Card Empty;
		public bool IsEmpty => Equals(Empty);

		public const int BackOfCard = 0x1F0A0;

		public int Deck { get; private set; }
		public System.Text.Rune Rune { get; private set; }

		public Rank Rank
			=> (Rank)Rune.Value;
		public Suit Suit
			=> GetSuit(Rune);

		public Card(System.Text.Rune rune, int deck)
		{
			Deck = deck;
			Rune = IsCard(rune) ? rune : throw new System.ArgumentOutOfRangeException(nameof(rune));
		}

		#region Static methods
		/// <summary>Creates a new sequence of all Unicode French cards.</summary>
		public static System.Collections.Generic.IEnumerable<System.Text.Rune> CreateDeck(bool includeKnights, bool includeJokers)
		{
			yield return new System.Text.Rune(0x1F0A1);
			yield return new System.Text.Rune(0x1F0A2);
			yield return new System.Text.Rune(0x1F0A3);
			yield return new System.Text.Rune(0x1F0A4);
			yield return new System.Text.Rune(0x1F0A5);
			yield return new System.Text.Rune(0x1F0A6);
			yield return new System.Text.Rune(0x1F0A7);
			yield return new System.Text.Rune(0x1F0A8);
			yield return new System.Text.Rune(0x1F0A9);
			yield return new System.Text.Rune(0x1F0AA);
			yield return new System.Text.Rune(0x1F0AB);
			if (includeKnights) yield return new System.Text.Rune(0x1F0AC);
			yield return new System.Text.Rune(0x1F0AD);
			yield return new System.Text.Rune(0x1F0AE);

			yield return new System.Text.Rune(0x1F0B1);
			yield return new System.Text.Rune(0x1F0B2);
			yield return new System.Text.Rune(0x1F0B3);
			yield return new System.Text.Rune(0x1F0B4);
			yield return new System.Text.Rune(0x1F0B5);
			yield return new System.Text.Rune(0x1F0B6);
			yield return new System.Text.Rune(0x1F0B7);
			yield return new System.Text.Rune(0x1F0B8);
			yield return new System.Text.Rune(0x1F0B9);
			yield return new System.Text.Rune(0x1F0BA);
			yield return new System.Text.Rune(0x1F0BB);
			if (includeKnights) yield return new System.Text.Rune(0x1F0BC);
			yield return new System.Text.Rune(0x1F0BD);
			yield return new System.Text.Rune(0x1F0BE);

			yield return new System.Text.Rune(0x1F0C1);
			yield return new System.Text.Rune(0x1F0C2);
			yield return new System.Text.Rune(0x1F0C3);
			yield return new System.Text.Rune(0x1F0C4);
			yield return new System.Text.Rune(0x1F0C5);
			yield return new System.Text.Rune(0x1F0C6);
			yield return new System.Text.Rune(0x1F0C7);
			yield return new System.Text.Rune(0x1F0C8);
			yield return new System.Text.Rune(0x1F0C9);
			yield return new System.Text.Rune(0x1F0CA);
			yield return new System.Text.Rune(0x1F0CB);
			if (includeKnights) yield return new System.Text.Rune(0x1F0CC);
			yield return new System.Text.Rune(0x1F0CD);
			yield return new System.Text.Rune(0x1F0CE);

			yield return new System.Text.Rune(0x1F0D1);
			yield return new System.Text.Rune(0x1F0D2);
			yield return new System.Text.Rune(0x1F0D3);
			yield return new System.Text.Rune(0x1F0D4);
			yield return new System.Text.Rune(0x1F0D5);
			yield return new System.Text.Rune(0x1F0D6);
			yield return new System.Text.Rune(0x1F0D7);
			yield return new System.Text.Rune(0x1F0D8);
			yield return new System.Text.Rune(0x1F0D9);
			yield return new System.Text.Rune(0x1F0DA);
			yield return new System.Text.Rune(0x1F0DB);
			if (includeKnights) yield return new System.Text.Rune(0x1F0DC);
			yield return new System.Text.Rune(0x1F0DD);
			yield return new System.Text.Rune(0x1F0DE);

			if (includeJokers)
			{
				yield return new System.Text.Rune(0x1F0BF);
				yield return new System.Text.Rune(0x1F0CF);
				yield return new System.Text.Rune(0x1F0DF);
			}
		}

		/// <summary>Creates a new sequence of the standard 52 Unicode French cards.</summary>
		public static System.Collections.Generic.List<Card> CreateDeck52()
			=> CreateDeck(false, false).Select(rune => new Card(rune, 1)).ToList();

		/// <summary>Creates a new sequence with each element having a deck of Unicode French cards.</summary>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Card>> CreateDecks(bool includeKnights, bool includeJokers, int decks = 1)
		{
			if (decks < 1 || decks > ushort.MaxValue) throw new System.ArgumentOutOfRangeException(nameof(decks));

			for (var deck = 1; deck <= decks; deck++)
				yield return CreateDeck(includeKnights, includeJokers).Select(rune => new Card(rune, deck)).ToList();
		}

		///// <summary>Creates a sequence of all common Unicode 52 french cards. Knights are discarded.</summary>
		//public static System.Collections.Generic.IEnumerable<CardEnum> CreateDeckOfCards52(int decks = 1)
		//{
		//	foreach(var deck in )
		//	//=> CreateCards(decks).Where(utf => !IsRankKnight(utf) && !IsJoker(utf)).Cast<CardEnum>();
		//}

		/// <summary>Computes the Unicode logical rank index.</summary>
		/// <param name="utf">The unicode code point.</param>
		/// <returns>0=Ace, 2..10=Two-Ten, 11=Jack, 12=Knight, 13=Queen, 14=King and -1 if not a playing card.</returns>
		public static int GetRankCode(System.Text.Rune rune)
			=> IsCard(rune) ? (rune.Value & 0xF) : -1;
		public static Rank GetRank(System.Text.Rune rune)
			=> (Rank)GetRankCode(rune);

		/// <summary>Computes the Unicode logical suit index.</summary>
		/// <param name="rune">The unicode code point.</param>
		/// <returns>0=Spades, 1=Hearts, 2=Diamonds, 3=Clubs and -1 if not a playing card.</returns>
		public static int GetSuitCode(System.Text.Rune rune)
			=> IsCard(rune) ? (rune.Value & 0xF0) : -1;
		public static Suit GetSuit(System.Text.Rune rune)
			=> GetSuitCode(rune) switch
			{
				0xA0 => Suit.Spades,
				0xB0 => Suit.Hearts,
				0xC0 => Suit.Diamonds,
				0xD0 => Suit.Clubs,
				_ => Suit.Unsuited
			};

		/// <summary></summary>
		/// <param name="rankCode">[1, 14]</param>
		/// <param name="suitCode">[0xA, 0xD]</param>
		/// <returns>The unicode code point for the indices of rank and suit.</returns>
		public static int GetUtfCard(int rankCode, int suitCode)
			=> 0x1F0 | suitCode | rankCode;

		/// <summary>Indicates whether the Unicode code point if a playing card.</summary>
		public static bool IsCard(System.Text.Rune rune)
			=> IsSuitOfClubs(rune) || IsSuitOfDiamonds(rune) || IsSuitOfHearts(rune) || IsSuitOfSpades(rune);

		/// <summary>Indicates whether the UTF value represents a face card.</summary>
		public static bool IsFaceCard(System.Text.Rune rune)
			=> IsRankOfJack(rune) || IsRankOfKnight(rune) || IsRankOfQueen(rune) || IsRankOfKing(rune);

		/// <summary>Indicates whether the UTF value represents a joker card.</summary>
		public static bool IsJoker(System.Text.Rune rune)
			=> rune.Value == 0x1F0BF || rune.Value == 0x1F0CF || rune.Value == 0x1F0DF;

		/// <summary>Indicates whether the UTF value represents an ace.</summary>
		public static bool IsRankOfAce(System.Text.Rune rune)
			=> rune.Value == 0x1F0A1 || rune.Value == 0x1F0B1 || rune.Value == 0x1F0C1 || rune.Value == 0x1F0D1;
		/// <summary>Indicates whether the UTF value represents a two.</summary>
		public static bool IsRankOfTwo(System.Text.Rune rune)
			=> rune.Value == 0x1F0A2 || rune.Value == 0x1F0B2 || rune.Value == 0x1F0C2 || rune.Value == 0x1F0D2;
		/// <summary>Indicates whether the UTF value represents a three.</summary>
		public static bool IsRankOfThree(System.Text.Rune rune)
			=> rune.Value == 0x1F0A3 || rune.Value == 0x1F0B3 || rune.Value == 0x1F0C3 || rune.Value == 0x1F0D3;
		/// <summary>Indicates whether the UTF value represents a four.</summary>
		public static bool IsRankOfFour(System.Text.Rune rune)
			=> rune.Value == 0x1F0A4 || rune.Value == 0x1F0B4 || rune.Value == 0x1F0C4 || rune.Value == 0x1F0D4;
		/// <summary>Indicates whether the UTF value represents a five.</summary>
		public static bool IsRankOfFive(System.Text.Rune rune)
			=> rune.Value == 0x1F0A5 || rune.Value == 0x1F0B5 || rune.Value == 0x1F0C5 || rune.Value == 0x1F0D5;
		/// <summary>Indicates whether the UTF value represents a six.</summary>
		public static bool IsRankOfSix(System.Text.Rune rune)
			=> rune.Value == 0x1F0A6 || rune.Value == 0x1F0B6 || rune.Value == 0x1F0C6 || rune.Value == 0x1F0D6;
		/// <summary>Indicates whether the UTF value represents a seven.</summary>
		public static bool IsRankOfSeven(System.Text.Rune rune)
			=> rune.Value == 0x1F0A7 || rune.Value == 0x1F0B7 || rune.Value == 0x1F0C7 || rune.Value == 0x1F0D7;
		/// <summary>Indicates whether the UTF value represents a eight.</summary>
		public static bool IsRankOfEight(System.Text.Rune rune)
			=> rune.Value == 0x1F0A8 || rune.Value == 0x1F0B8 || rune.Value == 0x1F0C8 || rune.Value == 0x1F0D8;
		/// <summary>Indicates whether the UTF value represents a nine.</summary>
		public static bool IsRankOfNine(System.Text.Rune rune)
			=> rune.Value == 0x1F0A9 || rune.Value == 0x1F0B9 || rune.Value == 0x1F0C9 || rune.Value == 0x1F0D9;
		/// <summary>Indicates whether the UTF value represents a ten.</summary>
		public static bool IsRankOfTen(System.Text.Rune rune)
			=> rune.Value == 0x1F0AA || rune.Value == 0x1F0BA || rune.Value == 0x1F0CA || rune.Value == 0x1F0DA;
		/// <summary>Indicates whether the UTF value represents a jack.</summary>
		public static bool IsRankOfJack(System.Text.Rune rune)
			=> rune.Value == 0x1F0AB || rune.Value == 0x1F0BB || rune.Value == 0x1F0CB || rune.Value == 0x1F0DB;
		/// <summary>Indicates whether the UTF value represents a knight.</summary>
		public static bool IsRankOfKnight(System.Text.Rune rune)
			=> rune.Value == 0x1F0AC || rune.Value == 0x1F0BC || rune.Value == 0x1F0CC || rune.Value == 0x1F0DC;
		/// <summary>Indicates whether the UTF value represents a queen.</summary>
		public static bool IsRankOfQueen(System.Text.Rune rune)
			=> rune.Value == 0x1F0AD || rune.Value == 0x1F0BD || rune.Value == 0x1F0CD || rune.Value == 0x1F0DD;
		/// <summary>Indicates whether the UTF value represents a king.</summary>
		public static bool IsRankOfKing(System.Text.Rune rune)
			=> rune.Value == 0x1F0AE || rune.Value == 0x1F0BE || rune.Value == 0x1F0CE || rune.Value == 0x1F0DE;

		/// <summary>Indicates whether the Unicode card is a club.</summary>
		public static bool IsSuitOfClubs(System.Text.Rune rune)
			=> rune.Value >= 0x1F0D1 && rune.Value <= 0x1F0DE;
		/// <summary>Indicates whether the Unicode card is a diamond.</summary>
		public static bool IsSuitOfDiamonds(System.Text.Rune rune)
			=> rune.Value >= 0x1F0C1 && rune.Value <= 0x1F0CE;
		/// <summary>Indicates whether the Unicode card is a heart.</summary>
		public static bool IsSuitOfHearts(System.Text.Rune rune)
			=> rune.Value >= 0x1F0B1 && rune.Value <= 0x1F0BE;
		/// <summary>Indicates whether the Unicode card is a spade.</summary>
		public static bool IsSuitOfSpades(System.Text.Rune rune)
			=> rune.Value >= 0x1F0A1 && rune.Value <= 0x1F0AE;
		#endregion Static methods

		// Operators
		public static bool operator ==(Card a, Card b)
			=> a.Equals(b);
		public static bool operator !=(Card a, Card b)
			=> !a.Equals(b);
		// Equatable
		public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] Card other)
			=> Deck == other.Deck && Rune == other.Rune;
		// Overrides
		public override bool Equals(object? obj)
			=> obj is Card o && Equals(o);
		public override int GetHashCode()
			=> System.HashCode.Combine(Deck, Rune);
		public override string ToString()
			=> $"{GetType().Name} {{ {GetRank(Rune)} of {GetSuit(Rune)} '{Rune}' {Text.Unicode.NotationToString(Rune)}, Deck #{Deck} }}";
	}
}
