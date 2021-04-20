namespace Flux
{
  public static partial class BitOps
  {
    public static void Example(System.Random? rng = null)
    {
      if (rng is null) rng = Flux.Random.NumberGenerator.Crypto;

      for (var i = 100; i >= 0; i--)
      //System.Linq.ParallelEnumerable.Range(-15, 32).ForAll(i =>
      {
        var number = (uint)rng.NextInt32();
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"             Decimal: {number.ToBigInteger().ToGroupString()}");
        sb.AppendLine($"                 Hex: {number.ToString(@"X8", null)}");
        sb.AppendLine($"              Binary: {System.Convert.ToString(number, 2).PadLeft(32, '0')}");
        sb.AppendLine($"           BitLength: {Flux.BitOps.BitLength(number)}");
        sb.AppendLine($"              FoldHi: {System.Convert.ToString(Flux.BitOps.FoldLeft(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($"              FoldLo: {System.Convert.ToString(Flux.BitOps.FoldRight(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($"    LeadingZeroCount: {System.Numerics.BitOperations.LeadingZeroCount(number)} = {Flux.BitOps.LeadingZeroCount(number)}");
        sb.AppendLine($"                Log2: {System.Numerics.BitOperations.Log2(number)} = {Flux.BitOps.Log2(number)}");
        sb.AppendLine($"            PopCount: {System.Numerics.BitOperations.PopCount(number)} = {Flux.BitOps.PopCount(number)}");
        sb.AppendLine($"LeastSignificant1Bit: {System.Convert.ToString(Flux.BitOps.LeastSignificant1Bit(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($" MostSignificant1Bit: {System.Convert.ToString(Flux.BitOps.MostSignificant1Bit(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($"   TrailingZeroCount: {System.Numerics.BitOperations.TrailingZeroCount(number)} = {Flux.BitOps.TrailingZeroCount(number)}");
        System.Console.WriteLine($"{sb}");
        System.Console.ReadKey();
        System.Console.Clear();
      }
      //);
    }
  }
}
