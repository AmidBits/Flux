namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Find all repetitions. A repetition is two occurrences of a string in a row, i.e. two adjacent sequences (of one or more elements). In other words a repetition can be described by a pair of indices  i&lt;j  such that the substring s[i...j]  consists of two identical strings written after each other.</para>
    /// <see href="https://cp-algorithms.com/string/main_lorentz.html"/>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<Interval<int>> MainLorentzRepetitions(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var repetitions = new System.Collections.Generic.List<Interval<int>>();

      LocateRepetitions(source);

      return repetitions;

      static int GetZ(int[] z, int i)
      {
        if (0 <= i && i < z.Length)
          return z[i];
        else
          return 0;
      }

      void ConvertToRepetitions(int shift, bool left, int cntr, int l, int k1, int k2)
      {
        for (var l1 = int.Max(1, l - k2); l1 <= int.Min(l, k1); l1++)
        {
          if (left && l1 == l) break;
          //var l2 = l - l1;
          var pos = shift + (left ? cntr - l1 : cntr - l - l1 + 1);
          repetitions.Add(new(pos, pos + 2 * l - 1));
        }
      }

      void LocateRepetitions(System.ReadOnlySpan<char> ros, int shift = 0)
      {
        if (ros.Length is var n && n <= 1)
          return;

        var s = ros.ToString();

        var nu = n / 2;
        var nv = n - nu;
        var u = s[..nu];
        var v = s[nu..];
        var ru = string.Concat(u.Reverse());
        var rv = string.Concat(v.Reverse());

        LocateRepetitions(u, shift);
        LocateRepetitions(v, shift + nu);

        var z1 = ru.AsSpan().Zfunction();
        var z2 = (v + '#' + u).AsSpan().Zfunction();
        var z3 = (ru + '#' + rv).AsSpan().Zfunction();
        var z4 = v.AsSpan().Zfunction();

        //var z1 = Zfunction(ru);
        //var z2 = Zfunction(v + '#' + u);
        //var z3 = Zfunction(ru + '#' + rv);
        //var z4 = Zfunction(v);

        for (var cntr = 0; cntr < n; cntr++)
        {
          int l, k1, k2;

          if (cntr < nu)
          {
            l = nu - cntr;
            k1 = GetZ(z1, nu - cntr);
            k2 = GetZ(z2, nv + 1 + cntr);
          }
          else
          {
            l = cntr - nu + 1;
            k1 = GetZ(z3, nu + 1 + nv - 1 - (cntr - nu));
            k2 = GetZ(z4, (cntr - nu) + 1);
          }

          if (k1 + k2 >= l)
            ConvertToRepetitions(shift, cntr < nu, cntr, l, k1, k2);
        }
      }
    }
  }
}
