using System.Linq;

namespace Flux
{
	public static partial class Reflect
	{
		/// <summary>Returns all the properties in a dictionary. To get the public properties of a static object, use the parameter typeof([StaticObjectName]).</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetProperties(object source)
			=> source is null
			? throw new System.ArgumentNullException(nameof(source))
			: source is System.Type type
			? type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(null, null))
			: source.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(source, System.Array.Empty<object>()));
	}
}
