using System.Linq;

namespace Flux
{
	public static partial class Xtensions
	{
		public static string ToStringXsd(this System.TimeSpan source)
		{
			var sb = new System.Text.StringBuilder(24);

			if (source.Ticks < 0)
				sb.Append('-');

			sb.Append('P');

			if (System.Math.Abs(source.Days) is var days && days > 0)
			{
				sb.Append(days);
				sb.Append('D');
			}

			if (System.Math.Abs(source.Hours) is var h && System.Math.Abs(source.Minutes) is var m && System.Math.Abs(source.Seconds) is var s && System.Math.Abs(source.Milliseconds) is var ms && (h > 0 || m > 0 || s > 0 || ms > 0))
			{
				sb.Append('T');

				if (h > 0)
				{
					sb.Append(h);
					sb.Append('H');
				}

				if (m > 0)
				{
					sb.Append(m);
					sb.Append('M');
				}

				if (s > 0 || ms > 0)
				{
					sb.Append(s);

					if (ms > 0)
					{
						sb.Append('.');
						sb.Append(ms);
					}

					sb.Append('S');
				}
			}

			return sb.ToString();
		}
		public static string ToStringXsdBasic(this System.TimeSpan source)
			=> $"{(source.Ticks < 0 ? @"-" : string.Empty)}P000000{System.Math.Abs(source.Days):D2}T{System.Math.Abs(source.Hours):D2}{System.Math.Abs(source.Minutes):D2}{System.Math.Abs(source.Seconds):D2}";
		public static string ToStringXsdBasicExtended(this System.TimeSpan source)
			=> $"{(source.Ticks < 0 ? @"-" : string.Empty)}P0000-00-{System.Math.Abs(source.Days):D2}T{System.Math.Abs(source.Hours):D2}:{System.Math.Abs(source.Minutes):D2}:{System.Math.Abs(source.Seconds):D2}";
	}
}
