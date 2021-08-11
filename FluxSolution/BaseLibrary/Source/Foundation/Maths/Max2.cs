namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Max routine for 2 values of T (where T : System.IComparable<T>).</summary>
		public static T Max<T>(T a, T b)
			where T : System.IComparable<T>
			=> a.CompareTo(b) >= 0 ? a : b;
	}
}
