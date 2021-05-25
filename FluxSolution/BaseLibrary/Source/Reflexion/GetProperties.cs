using System.Linq;

namespace Flux.Reflexion
{
	public static partial class Types
	{
		/// <summary>Returns all the properties in a dictionary. To get the public properties of a static object, use the parameter typeof([StaticObjectName]).</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetProperties(object source)
			=> source is null
			? throw new System.ArgumentNullException(nameof(source))
			: source is System.Type type && type.IsStaticClass() // If source a System.Type object and a static class, rather than being an instance object..
			? type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(null, null)) // ..then use a null as an object, which is how you enumerate members of a static object.
			: source.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(pi => pi.Name, pi => pi.GetValue(source, System.Array.Empty<object>())); // ..otherwise just use the instance.
	}
}
