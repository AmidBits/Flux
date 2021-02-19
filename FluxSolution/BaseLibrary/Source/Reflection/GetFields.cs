using System.Linq;

namespace Flux
{
	public static partial class Reflect
	{
		/// <summary>Returns all the fields in a dictionary. To get the public fields of a static object, use the parameter typeof([StaticObjectName]).</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetFields(object source)
			=> source is null
			? throw new System.ArgumentNullException(nameof(source))
			: source is System.Type type
			? type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(null))
			: source.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(source));
	}
}
