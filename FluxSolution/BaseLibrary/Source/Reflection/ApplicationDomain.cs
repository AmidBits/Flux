using System.Linq;

namespace Flux
{
	namespace Reflection
	{
		public static class ApplicationDomain
		{
			public static System.Collections.Generic.IList<TInterface> CreateFromTypes<TInterface>(this System.Collections.Generic.IEnumerable<System.Type> types)
				=> types.Select(t => System.Activator.CreateInstance(t)).Cast<TInterface>().ToList();

			public static System.Collections.Generic.IEnumerable<TInterface> CreateClassesImplementingInterface<TInterface>()
				=> GetClassesImplementingInterface<TInterface>().Select(t => System.Activator.CreateInstance(t)).Cast<TInterface>().ToArray();

			public static System.Collections.Generic.IEnumerable<System.Type> GetClassesImplementingInterface<TInterface>()
				=> typeof(TInterface).IsInterface
				? System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => typeof(TInterface).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
				: throw new System.ArgumentNullException(nameof(TInterface));
		}
	}
}
