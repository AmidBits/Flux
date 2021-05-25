using System.Linq;

namespace Flux.Reflexion
{
	public static partial class Types
	{
		/// <summary>Returns all the fields in a dictionary. To get the public fields of a static object, use the parameter typeof([StaticObjectName]).</summary>
		public static System.Collections.Generic.IDictionary<string, object?> GetFields(object source)
			=> source is null
			? throw new System.ArgumentNullException(nameof(source))
			: source is System.Type type && type.IsStaticClass() // If source a System.Type object and a static class, rather than being an instance object..
			? type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(null)) // ..then use a null as an object, which is how you enumerate members of a static object.
			: source.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(fi => fi.Name, fi => fi.GetValue(source)); // ..otherwise just use the instance.
	}
}
