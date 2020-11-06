//namespace Flux
//{
//  public static partial class Xtensions
//  {
//    /// <summary>Returns a string padded evenly on both sides to the specified width by the specified padding characters for left and right respectively. Padding is right biased, in case of uneveness.</summary>
//    public static string PadEven(this string source, int totalWidth, char paddingLeft, char paddingRight)
//      => totalWidth > (source ?? throw new System.ArgumentNullException(nameof(source))).Length && totalWidth - source.Length is int overflow && overflow / 2 is var overflowDiv2 ? new string(paddingLeft, overflowDiv2) + source + new string(paddingRight, overflow - overflowDiv2) : source;

//    /// <summary>Returns a string padded evenly on both sides to the specified width by the specified padding characters for left and right respectively. Padding is right biased, in case of uneveness.</summary>
//    public static string PadEven(this string source, int totalWidth, string paddingLeft, string paddingRight)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));
//      else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));
//      if (paddingLeft is null) throw new System.ArgumentNullException(nameof(paddingLeft));
//      else if (paddingLeft.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(paddingLeft));
//      if (paddingRight is null) throw new System.ArgumentNullException(nameof(paddingRight));
//      else if (paddingRight.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(paddingRight));

//      if (totalWidth < source.Length)
//        return source;

//      var sb = new System.Text.StringBuilder(source);

//      var totalPaddingLength = totalWidth - sb.Length;

//      if (totalPaddingLength / 2 is var paddingLengthLeft && paddingLengthLeft > 0)
//        sb.Insert(0, Replicate(paddingLeft, paddingLengthLeft / paddingLeft.Length).KeepRight(paddingLengthLeft));
//      if (totalPaddingLength - paddingLengthLeft is var paddingLengthRight && paddingLengthRight > 0)
//        sb.Append(Replicate(paddingRight, paddingLengthRight / paddingRight.Length).KeepLeft(paddingLengthRight));

//      return sb.ToString();
//    }

//    /// <summary>Returns a new string that right-aligns this string by padding them on the left with the specified padding string.</summary>
//    public static string PadLeft(this string source, int totalWidth, string padding)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));
//      else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));
//      if (padding is null) throw new System.ArgumentNullException(nameof(padding));
//      else if (padding.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(padding));

//      if (totalWidth < source.Length)
//        return source;

//      var sb = new System.Text.StringBuilder(source);
//      sb.Insert(0, Replicate(padding, (totalWidth - sb.Length) / padding.Length));
//      return sb.ToString(sb.Length - totalWidth, totalWidth);
//    }

//    /// <summary>Returns a new string that left-aligns this string by padding them on the right with the specified padding string.</summary>
//    public static string PadRight(this string source, int totalWidth, string padding)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));
//      else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));
//      if (padding is null) throw new System.ArgumentNullException(nameof(padding));
//      else if (padding.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(padding));

//      if (totalWidth < source.Length)
//        return source;

//      var sb = new System.Text.StringBuilder(source);
//      sb.Append(Replicate(padding, (totalWidth - sb.Length) / padding.Length));
//      return sb.ToString(0, totalWidth);
//    }
//  }
//}
