namespace Flux
{
  public static partial class BitOps
  {
    public static void Example(System.Random? rng = null)
    {
      if (rng is null) rng = Random.NumberGenerators.Xoshiro256P;

      for (var i = 100; i >= 0; i--)
      //System.Linq.ParallelEnumerable.Range(-15, 32).ForAll(i =>
      {
        var number = (uint)rng.Next();
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"             Decimal: {number.ToBigInteger().ToGroupedString()}");
        sb.AppendLine($"                 Hex: {number.ToString(@"X8", null)}");
        sb.AppendLine($"              Binary: {System.Convert.ToString(number, 2).PadLeft(32, '0')}");
        sb.AppendLine($"           BitLength: {BitLength(number)}");
        sb.AppendLine($"              FoldHi: {System.Convert.ToString(FoldLeft(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($"              FoldLo: {System.Convert.ToString(FoldRight(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($"                Log2: {System.Numerics.BitOperations.Log2(number)} = {Log2(number)}");
        sb.AppendLine($"            PopCount: {System.Numerics.BitOperations.PopCount(number)} = {PopCount(number)}");
        sb.AppendLine($"LeastSignificant1Bit: {System.Convert.ToString(LeastSignificant1Bit(number), 2).PadLeft(32, '0')}");
        sb.AppendLine($" MostSignificant1Bit: {System.Convert.ToString(MostSignificant1Bit(number), 2).PadLeft(32, '0')}");
        System.Console.WriteLine($"{sb}");
        System.Console.ReadKey();
        System.Console.Clear();
      }
      //);
    }
  }
}
