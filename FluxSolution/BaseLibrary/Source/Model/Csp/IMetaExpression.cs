namespace Flux.Csp
{
	public interface IMetaExpression<T>
	{
		System.Collections.Generic.IList<IVariable<T>> Support { get; }
	}
}
