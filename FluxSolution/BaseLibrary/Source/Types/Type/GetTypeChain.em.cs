namespace Flux
{
  public static partial class TypeEm
  {
    /// <summary>Creates a new sequence with the implementation and inheritance type chain of the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.IList<System.Type> GetTypeChain(this System.Type source)
    {
      var types = new System.Collections.Generic.List<System.Type>();

      foreach (var bt in GetBaseTypeChain(source))
      {
        if (!types.Contains(bt))
          types.Add(bt);

        var interfaces = bt.GetInterfaces();

        for (var i = interfaces.Length - 1; i >= 0; i--)
          if (interfaces[i] is var it && !types.Contains(it))
            types.Add(it);
      }

      return types;
    }
  }
}
