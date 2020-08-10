namespace Flux
{
  public static partial class XtensionsArray
  {
    public static T[] Append<T>(this T[] source, params T[] append)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (append is null) throw new System.ArgumentNullException(nameof(append));

      var target = new T[source.Length + append.Length];
      System.Array.Copy(source, 0, target, 0, source.Length);
      System.Array.Copy(append, 0, target, source.Length, append.Length);
      return target;
    }

    public static T[] Prepend<T>(this T[] source, params T[] prepend)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (prepend is null) throw new System.ArgumentNullException(nameof(prepend));

      var target = new T[prepend.Length + source.Length];
      System.Array.Copy(prepend, 0, target, 0, prepend.Length);
      System.Array.Copy(source, 0, target, prepend.Length, source.Length);
      return target;
    }
  }
}
