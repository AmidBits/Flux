//using System.Linq;

//namespace Flux
//{
//  public static partial class Enumerable
//  {
//    /// <summary>Computes a <see cref="System.HashCode"/> from the object. This is the .NET <see cref="System.HashCode"/>.</summary>
//    public static System.HashCode CreateHashCode(this object source)
//    {
//      var hc = new System.HashCode();
//      hc.Add(source);
//      return hc;
//    }
//    /// <summary>Computes a <see cref="System.HashCode"/> from all elements in the sequence. This is the .NET <see cref="System.HashCode"/>.</summary>
//    public static System.HashCode CreateHashCode<T>(this System.Collections.Generic.IEnumerable<T> source)
//      => ThrowIfNull(source).Aggregate(new System.HashCode(), (hc, t) => { hc.Add(t); return hc; }, hc => hc);
//    /// <summary>Appends all elements in the sequence to the <see cref="System.HashCode"/>. This is the .NET <see cref="System.HashCode"/>.</summary>
//    public static System.HashCode Append<T>(this ref System.HashCode source, System.Collections.Generic.IEnumerable<T> append)
//      => ThrowIfNull(append).Aggregate(source, (hc, t) => { hc.Add(t); return hc; }, hc => hc);
//    /// <summary>Appends all elements in the array to the <see cref="System.HashCode"/>. This is the .NET <see cref="System.HashCode"/>.</summary>
//    public static System.HashCode Append<T>(this ref System.HashCode source, params T[] append)
//      => ThrowIfNull(append).Aggregate(source, (hc, t) => { hc.Add(t); return hc; }, hc => hc);


//    /// <summary>Combines the hash codes in the sequence. This is the .NET <see cref="System.HashCode"/>.</summary>
//    public static int CombineHashCodes(this System.Collections.Generic.IEnumerable<int> source)
//      => ThrowIfNull(source).Aggregate(new System.HashCode(), (hc, n) => { hc.Add(n); return hc; }, hc => hc.ToHashCode());

//    /// <summary>Combines the hash codes in the sequence. This is the 'boost' version from C++.</summary>
//    public static int CombineHashCodesBoost(this System.Collections.Generic.IEnumerable<int> source, int seed = 0)
//      => ThrowIfNull(source).Aggregate(seed, (hc, n) => unchecked(hc ^ (n + (int)0x9e3779b9 + (hc << 6) + (hc >> 2))), hc => hc);

//    /// <summary>Combines the hash codes in the sequence. This is using a custom simplicity by Special Sauce (see link).</summary>
//    /// <see cref="https://stackoverflow.com/a/34006336/3178666"/>
//    public static int CombineHashCodesCustom(this System.Collections.Generic.IEnumerable<int> source, int seed = 1009, int factor = 9176)
//      => ThrowIfNull(source).Aggregate(seed, (hc, n) => unchecked(hc * factor + n), (hc) => hc);

//    public static System.Collections.Generic.IEnumerable<int> GetHashCodes<T>(this System.Collections.Generic.IEnumerable<T> source)
//      => ThrowIfNull(source).Select(t => t?.GetHashCode() ?? default);
//  }
//}
